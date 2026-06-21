using Clicker.Extensions;
using Clicker.Json;
using Clicker.Models;
using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Timers;
using System.Windows.Forms;

namespace Clicker
{
    public partial class Form : System.Windows.Forms.Form
    {
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new ActionConverter(), new Json.PointConverter() }
        };

        private readonly BindingList<string> _files = new BindingList<string>();
        private readonly System.Timers.Timer _timer = new System.Timers.Timer();
        private readonly Random _random = new Random(DateTime.Now.Millisecond);
        private readonly ActionExecutor _actionExecutor = new ActionExecutor();

        private MouseHookListener _mouseListener;
        private Settings _settings = new Settings();

        private List<Action> _sequence;
        private int _iteration = 1;
        private int _repeatCounter = 0;
        private Dictionary<Guid, string> _nestedIterationNotes = new Dictionary<Guid, string>();

        public Form()
        {
            InitializeComponent();
            this.Icon = new Icon("icon.ico");
            sequenceListBox.Items.Clear();
            sequenceListBox.DataSource = _settings.Moves;
            sequenceListBox.HorizontalScrollbar = true;
            this.Text = "Clicker";
            recordButton.Enabled = true;
            stopRecordButton.Enabled = false;
            startButton.Enabled = false;
            stopButton.Enabled = false;
            clearButton.Enabled = false;
            afterActionPeriodNum.Enabled = true;
            afterSequencePeriodStartNum.Enabled = false;
            afterSequencePeriodStopNum.Enabled = false;
            numberOfRepeatNum.Enabled = false;
            repeatSequenceCheckbox.Enabled = true;
            repeatSequenceCheckbox.Checked = true;
            afterActionPeriodNum.Minimum = 100;
            afterActionPeriodNum.Maximum = 10000000;
            afterActionPeriodNum.Value = 2000;
            afterActionPeriodNum.Increment = 1000;
            afterSequencePeriodStartNum.Minimum = 100;
            afterSequencePeriodStartNum.Maximum = 10000000;
            afterSequencePeriodStartNum.Increment = 1000;
            afterSequencePeriodStopNum.Minimum = 100;
            afterSequencePeriodStopNum.Maximum = 10000000;
            afterSequencePeriodStopNum.Increment = 1000;
            afterSequencePeriodStopNum.Value = 2000;
            afterSequencePeriodStartNum.Value = 2000;
            numberOfRepeatNum.Minimum = 2;
            numberOfRepeatNum.Maximum = 100000;
            numberOfRepeatNum.Value = 1000;
            editButton.Enabled = false;
            sequenceCounterLabel.Text = "";
            subsequenceCounterLabel.Text = "";
            actrionLabel.Text = "";
            newAfterActionPeriodNum.Minimum = 100;
            newAfterActionPeriodNum.Maximum = 10000000;
            newAfterActionPeriodNum.Value = 100;
            newAfterActionPeriodNum.Increment = 1000;
            newTagText.Text = "";
            newDescription.Text = "";
            newPointXNum.Minimum = -10000000;
            newPointXNum.Maximum = 10000000;
            newPointXNum.Value = 0;
            newPointYNum.Minimum = -10000000;
            newPointYNum.Maximum = 10000000;
            newPointYNum.Value = 0;
            newKeyboardText.Text = "";
            newKeyboardText.Visible = false;
            newSubsequenceIterationsNum.Minimum = 1;
            newSubsequenceIterationsNum.Maximum = 100000;
            newSubsequenceIterationsNum.Value = 1;
            newSubsequenceIterationsNum.Visible = false;
            newSubsequenceFilenameText.Text = "";
            newSubsequenceFilenameText.Visible = false;

            string path = Directory.GetCurrentDirectory();
            DirectoryInfo dictionaryInfo = new DirectoryInfo(path);
            foreach (var fileInfo in dictionaryInfo.GetFiles("*.json"))
            {
                _files.Add(fileInfo.Name);
            }
            profilesListBox.DataSource = _files;
            profilesListBox.HorizontalScrollbar = true;

            if (profilesListBox.Items.Count != 0)
            {
                loadButton.Enabled = true;
                deleteButton.Enabled = true;
            }
            else
            {
                loadButton.Enabled = false;
                deleteButton.Enabled = false;
            }

            newMouseButtonsComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            newMouseButtonsComboBox.Items.Add(MouseActions.Left);
            newMouseButtonsComboBox.Items.Add(MouseActions.Right);
            newMouseButtonsComboBox.Items.Add(MouseActions.Middle);
            newMouseButtonsComboBox.Items.Add(MouseActions.Left_Down);
            newMouseButtonsComboBox.Items.Add(MouseActions.Left_Up);
            newMouseButtonsComboBox.SelectedIndex = 0;
            newActionsComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            newActionsComboBox.Items.Add(Actions.Mouse);
            newActionsComboBox.Items.Add(Actions.Keyboard);
            newActionsComboBox.Items.Add(Actions.SubSequence);
            newActionsComboBox.SelectedIndex = 0;
        }

        public void DoAction(object sender, ElapsedEventArgs e)
        {
            sequenceCounterLabel.Invoke((MethodInvoker)(() =>
            {
                sequenceCounterLabel.Text = $"Iteracja: {_repeatCounter + 1} z {(repeatSequenceCheckbox.Checked ? numberOfRepeatNum.Value : 1)}";
            }));

            if (_sequence[_iteration].Type == Actions.SubSequence)
            {
                var subSequenceAction = _sequence[_iteration] as SubSequenceAction;
                _sequence.ReplaceWithRange(_iteration,
                    LoadNestedSequence(subSequenceAction.FileName, subSequenceAction.Id, subSequenceAction.Iterations, subSequenceAction.Period));
            }

            actrionLabel.Invoke((MethodInvoker)(() =>
            {
                actrionLabel.Text = $"Akcja: {_sequence[_iteration]}";
            }));

            subsequenceCounterLabel.Invoke((MethodInvoker)(() =>
            {
                if (_nestedIterationNotes.ContainsKey(_sequence[_iteration].Guid))
                {
                    subsequenceCounterLabel.Text = $"Sub-iteracja: {_nestedIterationNotes[_sequence[_iteration].Guid]}";
                }
                else
                {
                    subsequenceCounterLabel.Text = "";
                }                
            }));

            var numberOfActions = _actionExecutor.Execute(_sequence[_iteration], _sequence.Cast<Action>().ElementAtOrDefault(_iteration + 1));
            _timer.Interval = _sequence[_iteration].Period;
            _iteration += numberOfActions;

            if (_iteration == _sequence.Count - 2)
            {
                _repeatCounter++;
                if (repeatSequenceCheckbox.Checked == true && _repeatCounter < numberOfRepeatNum.Value)
                {
                    var time = _random.Next((int)afterSequencePeriodStartNum.Value, (int)afterSequencePeriodStopNum.Value);
                    _timer.Interval = time;
                    _iteration = 1;
                }
                else
                {
                    Invoke(new System.Action(delegate ()
                    {
                        StopButton_Click(null, null);
                    }));
                }
            }
        }

        private void RunTimer(int newInterval)
        {
            _timer.Elapsed += new ElapsedEventHandler(DoAction);
            _timer.Interval = newInterval;
            _timer.Enabled = true;
            _timer.Start();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (afterSequencePeriodStartNum.Value > afterSequencePeriodStopNum.Value)
            {
                MessageBox.Show("Złe wartości dla odstępu między sekwencjami");
            }
            else
            {
                _sequence = ProvideSequence();
                RunTimer(2000);
                recordButton.Enabled = false;
                stopRecordButton.Enabled = false;
                startButton.Enabled = false;
                stopButton.Enabled = true;
                clearButton.Enabled = false;
                afterActionPeriodNum.Enabled = false;
                afterSequencePeriodStartNum.Enabled = false;
                afterSequencePeriodStopNum.Enabled = false;
                numberOfRepeatNum.Enabled = false;
                repeatSequenceCheckbox.Enabled = false;
                loadButton.Enabled = false;
                deleteButton.Enabled = false;
                saveButton.Enabled = false;
                fileNameText.Enabled = false;
            }
        }

        private List<Action> ProvideSequence()
        {
            var selectedTags = tagsListBox.CheckedItems.Cast<string>().ToList();
            return _settings.Moves.Where(x => x.Active && (selectedTags.Contains(x.Tag) || string.IsNullOrWhiteSpace(x.Tag))).ToList();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            _nestedIterationNotes.Clear();
            _timer.Stop();
            _timer.Elapsed -= new ElapsedEventHandler(DoAction);
            _iteration = 1;
            _repeatCounter = 0;
            sequenceCounterLabel.Text = "";
            subsequenceCounterLabel.Text = "";
            actrionLabel.Text = "";
            recordButton.Enabled = false;
            stopRecordButton.Enabled = false;
            startButton.Enabled = true;
            stopButton.Enabled = false;
            clearButton.Enabled = true;
            afterActionPeriodNum.Enabled = true;
            loadButton.Enabled = true;
            deleteButton.Enabled = true;
            saveButton.Enabled = true;
            fileNameText.Enabled = true;
            repeatSequenceCheckbox.Enabled = true;
            if (repeatSequenceCheckbox.Checked == true)
            {
                afterSequencePeriodStartNum.Enabled = true;
                if (randomIntervalCheckbox.Checked)
                {
                    afterSequencePeriodStopNum.Enabled = true;
                }
                numberOfRepeatNum.Enabled = true;
            }
            else
            {
                afterSequencePeriodStartNum.Enabled = false;
                afterSequencePeriodStopNum.Enabled = false;
                numberOfRepeatNum.Enabled = false;
            }
        }

        private void RecordButton_Click(object sender, EventArgs e)
        {
            Activation();
            recordButton.Enabled = false;
            stopRecordButton.Enabled = true;
            startButton.Enabled = false;
            stopButton.Enabled = false;
            clearButton.Enabled = false;
            afterActionPeriodNum.Enabled = false;
            repeatSequenceCheckbox.Enabled = false;
            afterSequencePeriodStartNum.Enabled = false;
            afterSequencePeriodStopNum.Enabled = false;
            numberOfRepeatNum.Enabled = false;
            loadButton.Enabled = false;
            deleteButton.Enabled = false;
            saveButton.Enabled = false;
            fileNameText.Enabled = false;
            sequenceCounterLabel.Text = "";
            subsequenceCounterLabel.Text = "";
            actrionLabel.Text = "";
        }

        private void StopRecordButton_Click(object sender, EventArgs e)
        {
            Deactivation();

            recordButton.Enabled = false;
            stopRecordButton.Enabled = false;
            startButton.Enabled = true;
            stopButton.Enabled = false;
            clearButton.Enabled = true;
            afterActionPeriodNum.Enabled = true;
            repeatSequenceCheckbox.Enabled = true;
            loadButton.Enabled = true;
            deleteButton.Enabled = true;
            saveButton.Enabled = true;
            fileNameText.Enabled = true;
            sequenceListBox.DataSource = _settings.Moves;
            if (repeatSequenceCheckbox.Checked == true)
            {
                afterSequencePeriodStartNum.Enabled = true;
                if (randomIntervalCheckbox.Checked)
                {
                    afterSequencePeriodStopNum.Enabled = true;
                }
                numberOfRepeatNum.Enabled = true;
            }
            else
            {
                afterSequencePeriodStartNum.Enabled = false;
                afterSequencePeriodStopNum.Enabled = false;
                numberOfRepeatNum.Enabled = false;
            }
            newActionsComboBox.SelectedIndex = 0;
            newAfterActionPeriodNum.Value = 100;
            newTagText.Text = "";
            newDescription.Text = "";
            newPointXNum.Value = 0;
            newPointYNum.Value = 0;
            newKeyboardText.Text = "";
            newMouseButtonsComboBox.SelectedIndex = 0;
            newPointXNum.Visible = true;
            newPointYNum.Visible = true;
            newKeyboardText.Visible = false;
            newMouseButtonsComboBox.Visible = true;
            newSubsequenceIterationsNum.Value = 1;
            newSubsequenceIterationsNum.Visible = false;
            newSubsequenceFilenameText.Text = "";
            newSubsequenceFilenameText.Visible = false;
            editButton.Enabled = _settings.Moves.Count != 0;
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            _settings.Moves.Clear();
            recordButton.Enabled = true;
            stopRecordButton.Enabled = false;
            startButton.Enabled = false;
            stopButton.Enabled = false;
            clearButton.Enabled = false;
            afterActionPeriodNum.Enabled = true;
            repeatSequenceCheckbox.Enabled = true;
            loadButton.Enabled = true;
            deleteButton.Enabled = true;
            saveButton.Enabled = true;
            fileNameText.Enabled = true;
            editButton.Enabled = false;
            newActionsComboBox.SelectedIndex = 0;
            newMouseButtonsComboBox.SelectedIndex = 0;
            newAfterActionPeriodNum.Value = 100;
            newTagText.Text = "";
            newDescription.Text = "";
            newPointXNum.Value = 0;
            newPointYNum.Value = 0;
            newKeyboardText.Text = "";
            newSubsequenceFilenameText.Text = "";
            newSubsequenceFilenameText.Visible = false;
            newSubsequenceIterationsNum.Value = 1;
            newSubsequenceIterationsNum.Visible = false;
            sequenceCounterLabel.Text = "";
            subsequenceCounterLabel.Text = "";
            actrionLabel.Text = "";
            newPointXNum.Visible = true;
            newPointYNum.Visible = true;
            newKeyboardText.Visible = false;
            newMouseButtonsComboBox.Visible = true;
            _iteration = 1;
            _repeatCounter = 0;
            if (repeatSequenceCheckbox.Checked == true)
            {
                afterSequencePeriodStartNum.Enabled = true;
                if (randomIntervalCheckbox.Checked)
                {
                    afterSequencePeriodStopNum.Enabled = true;
                }
                numberOfRepeatNum.Enabled = true;
            }
            else
            {
                afterSequencePeriodStartNum.Enabled = false;
                afterSequencePeriodStopNum.Enabled = false;
                numberOfRepeatNum.Enabled = false;
            }
        }

        public void Activation()
        {
            _mouseListener = new MouseHookListener(new GlobalHooker())
            {
                Enabled = true
            };
            _mouseListener.MouseDownExt += MouseListener_MouseDownExt;
        }

        public void Deactivation()
        {
            _mouseListener.Dispose();
        }

        private void MouseListener_MouseDownExt(object sender, MouseEventExtArgs e)
        {
            _settings.Moves.Add(
                new MouseAction
                {
                    Id = (_settings.Moves.Count + 1).ToString(),
                    Point = new Point(Cursor.Position.X, Cursor.Position.Y),
                    Period = (int)afterActionPeriodNum.Value,
                    Button = e.Button == MouseButtons.Middle
                ? MouseActions.Middle
                : e.Button == MouseButtons.Right
                    ? MouseActions.Right
                    : MouseActions.Left
                });
        }

        private void RepeatSequenceCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (repeatSequenceCheckbox.Checked)
            {
                afterSequencePeriodStartNum.Enabled = true;
                if (randomIntervalCheckbox.Checked)
                {
                    afterSequencePeriodStopNum.Enabled = true;
                }
                numberOfRepeatNum.Enabled = true;
            }
            else
            {
                afterSequencePeriodStartNum.Enabled = false;
                afterSequencePeriodStopNum.Enabled = false;
                numberOfRepeatNum.Enabled = false;
            }
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            var fileName = profilesListBox.SelectedItem.ToString();
            _settings = LoadSettings(fileName);
            UpdateMovesIds();

            numberOfRepeatNum.Value = _settings.NumberOfRepeats;
            repeatSequenceCheckbox.Checked = _settings.Repeat;
            repeatSequenceCheckbox.CheckedChanged += new EventHandler(RepeatSequenceCheckbox_CheckedChanged);
            randomIntervalCheckbox.Checked = _settings.RandomTimeInterval;
            afterActionPeriodNum.Value = _settings.Period1;
            afterSequencePeriodStartNum.Value = _settings.PeriodA;
            SetTags();
            if (_settings.RandomTimeInterval)
            {
                afterSequencePeriodStopNum.Value = _settings.PeriodB;
            }
            else
            {
                afterSequencePeriodStopNum.Value = _settings.PeriodA;
            }

            sequenceListBox.DataSource = _settings.Moves;

            recordButton.Enabled = false;
            stopRecordButton.Enabled = false;
            startButton.Enabled = true;
            stopButton.Enabled = false;
            clearButton.Enabled = true;
            afterActionPeriodNum.Enabled = true;
            repeatSequenceCheckbox.Enabled = true;
            loadButton.Enabled = true;
            deleteButton.Enabled = true;
            saveButton.Enabled = true;
            fileNameText.Enabled = true;
            fileNameText.Text = Path.GetFileNameWithoutExtension(fileName);
            if (repeatSequenceCheckbox.Checked == true)
            {
                afterSequencePeriodStartNum.Enabled = true;
                if (randomIntervalCheckbox.Checked)
                {
                    afterSequencePeriodStopNum.Enabled = true;
                }
                numberOfRepeatNum.Enabled = true;
            }
            else
            {
                afterSequencePeriodStartNum.Enabled = false;
                afterSequencePeriodStopNum.Enabled = false;
                numberOfRepeatNum.Enabled = false;
            }
            newActionsComboBox.SelectedIndex = 0;
            newMouseButtonsComboBox.SelectedIndex = 0;
            newAfterActionPeriodNum.Value = 100;
            newTagText.Text = "";
            newDescription.Text = "";
            newPointXNum.Value = 0;
            newPointYNum.Value = 0;
            newKeyboardText.Text = "";
            newPointXNum.Visible = true;
            newPointYNum.Visible = true;
            newKeyboardText.Visible = false;
            newSubsequenceFilenameText.Text = "";
            newSubsequenceFilenameText.Visible = false;
            newSubsequenceIterationsNum.Value = 1;
            newSubsequenceIterationsNum.Visible = false;
            newMouseButtonsComboBox.Visible = true;
            editButton.Enabled = _settings.Moves.Count != 0;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            _settings.Period1 = (int)afterActionPeriodNum.Value;
            _settings.PeriodA = (int)afterSequencePeriodStartNum.Value;
            _settings.PeriodB = (int)afterSequencePeriodStopNum.Value;
            _settings.NumberOfRepeats = (int)numberOfRepeatNum.Value;
            _settings.Repeat = repeatSequenceCheckbox.Checked;
            _settings.RandomTimeInterval = randomIntervalCheckbox.Checked;

            UpdateMovesIds();
            SaveTags();

            try
            {
                var json = JsonSerializer.Serialize(_settings, _jsonOptions);
                File.WriteAllText(fileNameText.Text + ".json", json);
            }
            catch (SerializationException ex)
            {
                MessageBox.Show(this, "Nastąpił następujący błąd: \n" + ex.ToString(), "BLAD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _files.Clear();
            string path = Directory.GetCurrentDirectory();
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (var fi in di.GetFiles("*.json"))
            {
                _files.Add(fi.Name);
            }
            profilesListBox.DataSource = null;
            profilesListBox.DataSource = _files;
            if (profilesListBox.Items.Count != 0)
            {
                loadButton.Enabled = true;
                deleteButton.Enabled = true;
            }
            else
            {
                loadButton.Enabled = false;
                deleteButton.Enabled = false;
            }
        }

        private void UpdateMovesIds()
        {
            var id = 0;
            for (int i = 0; i < _settings.Moves.Count - 2; i++)
            {
                var move = _settings.Moves[i];
                move.Id = (id++).ToString();
            }
            _settings.Moves[_settings.Moves.Count - 1].Id = "0";
            _settings.Moves[_settings.Moves.Count - 2].Id = "0";
        }

        private void SaveTags()
        {
            var allTags = _settings.Moves.Where(x => !string.IsNullOrEmpty(x.Tag)).Select(x => x.Tag).Distinct().ToList();
            var selectedTags = tagsListBox.CheckedItems.Cast<string>().ToList();
            _settings.Tags.Clear();
            foreach (var tag in allTags)
            {
                _settings.Tags.Add(new TagSetting { Name = tag, Active = selectedTags.Contains(tag) });
            }
        }

        private void UpdateTags()
        {
            var actionsTags = _settings.Moves.Where(x => !string.IsNullOrEmpty(x.Tag)).Select(x => x.Tag).Distinct().ToList();
            var listedTags = tagsListBox.Items
                .Cast<string>()
                .ToDictionary(
                    item => item,
                    item => tagsListBox.GetItemChecked(
                        tagsListBox.Items.IndexOf(item)
                    )
                );
            tagsListBox.Items.Clear();
            var finalTags = new List<TagSetting>();
            foreach (var tag in actionsTags)
            {
                tagsListBox.Items.Add(tag, !listedTags.ContainsKey(tag) || listedTags[tag]);
            }
        }

        private void SetTags()
        {
            var actionsTags = _settings.Moves.Where(x => !string.IsNullOrEmpty(x.Tag)).Select(x => x.Tag).Distinct().ToList();
            var finalTags = new List<TagSetting>();
            foreach (var tag in actionsTags)
            {
                var existingTag = _settings.Tags.FirstOrDefault(t => t.Name == tag);
                if (existingTag != null)
                {
                    finalTags.Add(existingTag);
                }
                else
                {
                    finalTags.Add(new TagSetting { Name = tag, Active = true });
                }
            }

            tagsListBox.Items.Clear();
            foreach (var tag in finalTags)
            {
                tagsListBox.Items.Add(tag.Name, tag.Active);
            }
        }


        private void EditButton_Click(object sender, EventArgs e)
        {
            if (_settings.Moves[sequenceListBox.SelectedIndex].Type != (Actions)newActionsComboBox.SelectedItem)
            {
                _settings.Moves[sequenceListBox.SelectedIndex] = MigrateMove(_settings.Moves[sequenceListBox.SelectedIndex], (Actions)newActionsComboBox.SelectedItem);
            }
            else
            {
                switch (_settings.Moves[sequenceListBox.SelectedIndex])
                {
                    case MouseAction mouse:
                        mouse.Point = new Point((int)newPointXNum.Value, (int)newPointYNum.Value);
                        mouse.Button = (MouseActions)newMouseButtonsComboBox.SelectedItem;
                        break;
                    case KeyboardAction keyboard:
                        keyboard.Text = newKeyboardText.Text;
                        break;
                    case SubSequenceAction subSequence:
                        subSequence.FileName = newSubsequenceFilenameText.Text;
                        subSequence.Iterations = (int)newSubsequenceIterationsNum.Value;
                        break;
                }
                _settings.Moves[sequenceListBox.SelectedIndex].Period = (int)newAfterActionPeriodNum.Value;
                _settings.Moves[sequenceListBox.SelectedIndex].Tag = newTagText.Text;
                _settings.Moves[sequenceListBox.SelectedIndex].Description = newDescription.Text;
            }

            UpdateTags();
            this.sequenceListBox.SelectedIndexChanged -= new EventHandler(this.SequenceListBox_SelectedIndexChanged);
            sequenceListBox.DataSource = null;
            sequenceListBox.DataSource = _settings.Moves;
            this.sequenceListBox.SelectedIndexChanged += new EventHandler(this.SequenceListBox_SelectedIndexChanged);
        }

        private Action MigrateMove(Action action, Actions newType)
        {
            switch (newType)
            {
                case Actions.Mouse:
                    return new MouseAction
                    {
                        Id = action.Id,
                        Type = Actions.Mouse,
                        Description = newDescription.Text,
                        Tag = newTagText.Text,
                        Period = (int)newAfterActionPeriodNum.Value,
                        Point = new Point((int)newPointXNum.Value, (int)newPointYNum.Value),
                        Button = (MouseActions)newMouseButtonsComboBox.SelectedItem
                    };
                case Actions.Keyboard:
                    return new KeyboardAction
                    {
                        Id = action.Id,
                        Type = Actions.Keyboard,
                        Description = newDescription.Text,
                        Tag = newTagText.Text,
                        Period = (int)newAfterActionPeriodNum.Value,
                        Text = newKeyboardText.Text
                    };
                case Actions.SubSequence:
                    return new SubSequenceAction
                    {
                        Id = action.Id,
                        Type = Actions.SubSequence,
                        Description = newDescription.Text,
                        Tag = newTagText.Text,
                        Period = (int)newAfterActionPeriodNum.Value,
                        FileName = newSubsequenceFilenameText.Text,
                        Iterations = (int)newSubsequenceIterationsNum.Value
                    };
            }
            throw new NotImplementedException();
        }

        private void SequenceListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sequenceListBox != null && sequenceListBox.SelectedIndex > -1)
            {
                newAfterActionPeriodNum.Value = _settings.Moves[sequenceListBox.SelectedIndex].Period;
                newTagText.Text = _settings.Moves[sequenceListBox.SelectedIndex].Tag;
                newDescription.Text = _settings.Moves[sequenceListBox.SelectedIndex].Description;
                newActionsComboBox.SelectedItem = _settings.Moves[sequenceListBox.SelectedIndex].Type;
                switch (_settings.Moves[sequenceListBox.SelectedIndex])
                {
                    case MouseAction mouse:
                        newKeyboardText.Visible = false;
                        newPointXNum.Visible = true;
                        newPointYNum.Visible = true;
                        newMouseButtonsComboBox.Visible = true;
                        newKeyboardText.Text = "";
                        newPointXNum.Value = mouse.Point.X;
                        newPointYNum.Value = mouse.Point.Y;
                        newSubsequenceFilenameText.Text = "";
                        newSubsequenceFilenameText.Visible = false;
                        newSubsequenceIterationsNum.Value = 1;
                        newSubsequenceIterationsNum.Visible = false;
                        break;
                    case KeyboardAction keyboard:
                        newKeyboardText.Visible = true;
                        newPointXNum.Visible = false;
                        newPointYNum.Visible = false;
                        newMouseButtonsComboBox.Visible = false;
                        newKeyboardText.Text = keyboard.Text;
                        newPointXNum.Value = 0;
                        newPointYNum.Value = 0;
                        newSubsequenceFilenameText.Text = "";
                        newSubsequenceFilenameText.Visible = false;
                        newSubsequenceIterationsNum.Value = 1;
                        newSubsequenceIterationsNum.Visible = false;
                        break;
                    case SubSequenceAction subsequence:
                        newKeyboardText.Visible = false;
                        newPointXNum.Visible = false;
                        newPointYNum.Visible = false;
                        newMouseButtonsComboBox.Visible = false;
                        newKeyboardText.Text = "";
                        newPointXNum.Value = 0;
                        newPointYNum.Value = 0;
                        newSubsequenceFilenameText.Text = subsequence.FileName;
                        newSubsequenceFilenameText.Visible = true;
                        newSubsequenceIterationsNum.Value = subsequence.Iterations;
                        newSubsequenceIterationsNum.Visible = true;
                        break;
                }
            }
            else
            {
                newAfterActionPeriodNum.Value = 100;
                newTagText.Text = "";
                newDescription.Text = "";
                newPointXNum.Value = 0;
                newPointYNum.Value = 0;
                newKeyboardText.Text = "";
                newMouseButtonsComboBox.SelectedIndex = 0;
                newPointXNum.Visible = true;
                newPointYNum.Visible = true;
                newKeyboardText.Visible = false;
                newMouseButtonsComboBox.Visible = true;
                newSubsequenceFilenameText.Text = "";
                newSubsequenceFilenameText.Visible = false;
                newSubsequenceIterationsNum.Value = 1;
                newSubsequenceIterationsNum.Visible = false;
            }
            editButton.Enabled = true;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var str = profilesListBox.SelectedItem.ToString();
            if (File.Exists(str))
            {
                try
                {
                    File.Delete(str);
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                _files.Clear();
                string path = Directory.GetCurrentDirectory();
                DirectoryInfo di = new DirectoryInfo(path);
                foreach (var fi in di.GetFiles("*.json"))
                {
                    _files.Add(fi.Name);
                }
                profilesListBox.DataSource = null;
                profilesListBox.DataSource = _files;
                if (profilesListBox.Items.Count != 0)
                {
                    loadButton.Enabled = true;
                    deleteButton.Enabled = true;
                }
                else
                {
                    loadButton.Enabled = false;
                    deleteButton.Enabled = false;
                }
            }
        }

        private void ActionsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var move = sequenceListBox.SelectedIndex > -1 && _settings.Moves.Count > 0 ? _settings.Moves[sequenceListBox.SelectedIndex] : null;

            switch ((Actions)newActionsComboBox.SelectedItem)
            {
                case Actions.Mouse:
                    if (move != null && move is MouseAction mouseMove)
                    {
                        newPointXNum.Value = mouseMove.Point.X;
                        newPointYNum.Value = mouseMove.Point.Y;
                        newMouseButtonsComboBox.SelectedIndex = (int)mouseMove.Button;
                    }
                    else
                    {
                        newPointXNum.Value = 0;
                        newPointYNum.Value = 0;
                        newMouseButtonsComboBox.SelectedIndex = 0;
                    }
                    newKeyboardText.Visible = false;
                    newPointXNum.Visible = true;
                    newPointYNum.Visible = true;
                    newMouseButtonsComboBox.Visible = true;
                    newKeyboardText.Text = "";
                    newSubsequenceFilenameText.Text = "";
                    newSubsequenceFilenameText.Visible = false;
                    newSubsequenceIterationsNum.Value = 1;
                    newSubsequenceIterationsNum.Visible = false;
                    break;
                case Actions.Keyboard:
                    if (move != null && move is KeyboardAction keyboardMove)
                    {
                        newKeyboardText.Text = keyboardMove.Text;
                    }
                    else
                    {
                        newKeyboardText.Text = "";
                    }
                    newKeyboardText.Visible = true;
                    newPointXNum.Visible = false;
                    newPointYNum.Visible = false;
                    newMouseButtonsComboBox.Visible = false;
                    newPointXNum.Value = 0;
                    newPointYNum.Value = 0;
                    newMouseButtonsComboBox.SelectedIndex = 0;
                    newSubsequenceFilenameText.Text = "";
                    newSubsequenceFilenameText.Visible = false;
                    newSubsequenceIterationsNum.Value = 1;
                    newSubsequenceIterationsNum.Visible = false;
                    break;
                case Actions.SubSequence:
                    if (move != null && move is SubSequenceAction subsequenceMove)
                    {
                        newSubsequenceFilenameText.Text = subsequenceMove.FileName;
                        newSubsequenceIterationsNum.Value = subsequenceMove.Iterations;
                    }
                    else
                    {
                        newSubsequenceFilenameText.Text = "";
                        newSubsequenceIterationsNum.Value = 1;
                    }
                    newKeyboardText.Text = "";
                    newKeyboardText.Visible = false;
                    newPointXNum.Visible = false;
                    newPointYNum.Visible = false;
                    newMouseButtonsComboBox.Visible = false;
                    newPointXNum.Value = 0;
                    newPointYNum.Value = 0;
                    newMouseButtonsComboBox.SelectedIndex = 0;
                    newSubsequenceFilenameText.Visible = true;
                    newSubsequenceIterationsNum.Visible = true;
                    break;
            }
        }

        private void RandomIntervalCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (randomIntervalCheckbox.Checked)
            {
                afterSequencePeriodStopNum.Enabled = true;
            }
            else
            {
                afterSequencePeriodStopNum.Enabled = false;
            }
        }

        private void AfterSequencePeriodStartNum_ValueChanged(object sender, EventArgs e)
        {
            if (!randomIntervalCheckbox.Checked && afterSequencePeriodStartNum.Value >= afterSequencePeriodStartNum.Minimum)
            {
                afterSequencePeriodStopNum.Value = afterSequencePeriodStartNum.Value;
            }
        }

        private Settings LoadSettings(string fileName)
        {
            try
            {
                var json = File.ReadAllText(fileName);
                return JsonSerializer.Deserialize<Settings>(json, _jsonOptions);
            }
            catch (SerializationException ex)
            {
                MessageBox.Show(this, "Nastąpił następujący błąd: \n" + ex.ToString(), "BLAD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private List<Action> LoadNestedSequence(string fileName, string oldId, int numberOfIterations, int period)
        {
            var tmpSettings = LoadSettings(fileName);
            var newSequence = tmpSettings.Moves;
            newSequence.RemoveAt(newSequence.Count - 1);
            newSequence.RemoveAt(newSequence.Count - 1);
            newSequence.RemoveAt(0);
            var listedTags = tagsListBox.Items
                .Cast<string>()
                .ToDictionary(
                    item => item,
                    item => tagsListBox.GetItemChecked(
                        tagsListBox.Items.IndexOf(item)
                    )
                );

            var newSequenceList = newSequence.Where(x => x.Active && (string.IsNullOrWhiteSpace(x.Tag) || !listedTags.ContainsKey(x.Tag) || listedTags[x.Tag])).ToList();
            var id = 0;
            for (int i = 0; i < newSequenceList.Count; i++)
            {
                newSequenceList[i].Id = $"{oldId}_{++id}";
            }

            var result = Enumerable.Range(0, numberOfIterations)
                .SelectMany(_ => newSequenceList.Select(x => x.Clone()))
                .ToList();

            result.ElementAt(newSequenceList.Count - 1).Period = period;

            for (int i = 0; i < result.Count; i++)
            {
                _nestedIterationNotes.Add(result[i].Guid, $"{(i / newSequenceList.Count) + 1} z {numberOfIterations}");
            }

            return result;
        }
    }
}