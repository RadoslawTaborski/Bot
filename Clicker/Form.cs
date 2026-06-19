using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Timers;
using System.Windows.Forms;

namespace Clicker
{
    public partial class Form : System.Windows.Forms.Form
    {
        private readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        };

        private readonly BindingList<string> files = new BindingList<string>();
        private readonly System.Timers.Timer timer = new System.Timers.Timer();
        private readonly Random random = new Random(DateTime.Now.Millisecond);
        private readonly ActionExecutor actionExecutor = new ActionExecutor();

        private MouseHookListener m_mouseListener;
        private Settings settings = new Settings();
        private int iteration = 1;
        private int repeatCounter = 0;

        public Form()
        {
            InitializeComponent();
            this.Icon = new Icon("icon.ico");
            sequenceListBox.Items.Clear();
            sequenceListBox.DataSource = settings.Moves;
            sequenceListBox.HorizontalScrollbar = true;
            this.mainPage.Text = "Sterowanie";
            this.settingsPage.Text = "Ustawienia";
            this.sequencePage.Text = "Sekwencja";
            this.profilesPage.Text = "Profile";
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
            newAfterActionPeriodNum.Minimum = 100;
            newAfterActionPeriodNum.Maximum = 10000000;
            newAfterActionPeriodNum.Value = 100;
            newAfterActionPeriodNum.Increment = 1000;
            newPointXNum.Minimum = -10000000;
            newPointXNum.Maximum = 10000000;
            newPointXNum.Value = 0;
            newPointYNum.Minimum = -10000000;
            newPointYNum.Maximum = 10000000;
            newPointYNum.Value = 0;
            newKeyboardText.Text = "";
            newKeyboardText.Visible = false;
            numberOfRepeatNum.Minimum = 2;
            numberOfRepeatNum.Maximum = 100000;
            numberOfRepeatNum.Value = 1000;
            editButton.Enabled = false;
            sequenceCounterLabel.Text = "";

            string path = Directory.GetCurrentDirectory();
            DirectoryInfo dictionaryInfo = new DirectoryInfo(path);
            foreach (var fileInfo in dictionaryInfo.GetFiles("*.json"))
            {
                files.Add(fileInfo.Name);
            }
            profilesListBox.DataSource = files;
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
            newActionsComboBox.SelectedIndex = 0;
        }

        public void DoAction(object sender, ElapsedEventArgs e)
        {
            sequenceCounterLabel.Invoke((MethodInvoker)(() =>
            {
                sequenceCounterLabel.Text = $"Iteracja: {repeatCounter + 1} z {(repeatSequenceCheckbox.Checked ? numberOfRepeatNum.Value : 1)}";
            }));
            var numberOfActions = actionExecutor.Execute(settings.Moves[iteration], settings.Moves.Cast<Action>().ElementAtOrDefault(iteration + 1));
            iteration += numberOfActions;
            timer.Interval = settings.Moves[iteration].Period;

            if (iteration == settings.Moves.Count - 2)
            {
                repeatCounter++;
                if (repeatSequenceCheckbox.Checked == true && repeatCounter < numberOfRepeatNum.Value)
                {
                    var time = random.Next((int)afterSequencePeriodStartNum.Value, (int)afterSequencePeriodStopNum.Value);
                    timer.Interval = time;
                    iteration = 1;
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
            timer.Elapsed += new ElapsedEventHandler(DoAction);
            timer.Interval = newInterval;
            timer.Enabled = true;
            timer.Start();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (afterSequencePeriodStartNum.Value > afterSequencePeriodStopNum.Value)
            {
                MessageBox.Show("Złe wartości dla odstępu między sekwencjami");
            }
            else
            {
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

        private void StopButton_Click(object sender, EventArgs e)
        {
            timer.Stop();
            timer.Elapsed -= new ElapsedEventHandler(DoAction);
            iteration = 1;
            repeatCounter = 0;
            sequenceCounterLabel.Text = "";
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
            sequenceListBox.DataSource = settings.Moves;
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
            newPointXNum.Value = 0;
            newPointYNum.Value = 0;
            newKeyboardText.Text = "";
            newMouseButtonsComboBox.SelectedIndex = 0;
            newPointXNum.Visible = true;
            newPointYNum.Visible = true;
            newKeyboardText.Visible = false;
            newMouseButtonsComboBox.Visible = true;
            editButton.Enabled = settings.Moves.Count != 0;
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            settings.Moves.Clear();
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
            newPointXNum.Value = 0;
            newPointYNum.Value = 0;
            newKeyboardText.Text = "";
            sequenceCounterLabel.Text = "";
            newPointXNum.Visible = true;
            newPointYNum.Visible = true;
            newKeyboardText.Visible = false;
            newMouseButtonsComboBox.Visible = true;
            iteration = 1;
            repeatCounter = 0;
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
            m_mouseListener = new MouseHookListener(new GlobalHooker())
            {
                Enabled = true
            };
            m_mouseListener.MouseDownExt += MouseListener_MouseDownExt;
        }

        public void Deactivation()
        {
            m_mouseListener.Dispose();
        }

        private void MouseListener_MouseDownExt(object sender, MouseEventExtArgs e)
        {
            settings.Moves.Add(
                new MouseAction
                {
                    Id = settings.Moves.Count + 1,
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

            var str = profilesListBox.SelectedItem.ToString();
            try
            {
                var json = File.ReadAllText(str);
                settings = JsonConvert.DeserializeObject<Settings>(json, jsonSettings);
            }
            catch (SerializationException ex)
            {
                MessageBox.Show(this, "Nastąpił następujący błąd: \n" + ex.ToString(), "BLAD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }

            numberOfRepeatNum.Value = settings.NumberOfRepeats;
            repeatSequenceCheckbox.Checked = settings.Repeat;
            repeatSequenceCheckbox.CheckedChanged += new EventHandler(RepeatSequenceCheckbox_CheckedChanged);
            randomIntervalCheckbox.Checked = settings.RandomTimeInterval;
            afterActionPeriodNum.Value = settings.Period1;
            afterSequencePeriodStartNum.Value = settings.PeriodA;
            if (settings.RandomTimeInterval)
            {
                afterSequencePeriodStopNum.Value = settings.PeriodB;
            }
            else
            {
                afterSequencePeriodStopNum.Value = settings.PeriodA;
            }

            sequenceListBox.DataSource = settings.Moves;

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
            fileNameText.Text = Path.GetFileNameWithoutExtension(str);
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
            newPointXNum.Value = 0;
            newPointYNum.Value = 0;
            newKeyboardText.Text = "";
            newPointXNum.Visible = true;
            newPointYNum.Visible = true;
            newKeyboardText.Visible = false;
            newMouseButtonsComboBox.Visible = true;
            editButton.Enabled = settings.Moves.Count != 0;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            settings.Period1 = (int)afterActionPeriodNum.Value;
            settings.PeriodA = (int)afterSequencePeriodStartNum.Value;
            settings.PeriodB = (int)afterSequencePeriodStopNum.Value;
            settings.NumberOfRepeats = (int)numberOfRepeatNum.Value;
            settings.Repeat = repeatSequenceCheckbox.Checked;
            settings.RandomTimeInterval = randomIntervalCheckbox.Checked;

            try
            {
                var json = JsonConvert.SerializeObject(settings, Formatting.Indented, jsonSettings);
                File.WriteAllText(fileNameText.Text + ".json", json);
            }
            catch (SerializationException ex)
            {
                MessageBox.Show(this, "Nastąpił następujący błąd: \n" + ex.ToString(), "BLAD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            files.Clear();
            string path = Directory.GetCurrentDirectory();
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (var fi in di.GetFiles("*.json"))
            {
                files.Add(fi.Name);
            }
            profilesListBox.DataSource = null;
            profilesListBox.DataSource = files;
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

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (settings.Moves[sequenceListBox.SelectedIndex].Type != (Actions)newActionsComboBox.SelectedItem)
            {
                settings.Moves[sequenceListBox.SelectedIndex] = MigrateMove(settings.Moves[sequenceListBox.SelectedIndex], (Actions)newActionsComboBox.SelectedItem);
            }
            else
            {
                switch (settings.Moves[sequenceListBox.SelectedIndex])
                {
                    case MouseAction mouse:
                        mouse.Point = new Point((int)newPointXNum.Value, (int)newPointYNum.Value);
                        mouse.Button = (MouseActions)newMouseButtonsComboBox.SelectedItem;
                        break;
                    case KeyboardAction keyboard:
                        keyboard.Text = newKeyboardText.Text;
                        break;
                }
                settings.Moves[sequenceListBox.SelectedIndex].Period = (int)newAfterActionPeriodNum.Value;
            }

            this.sequenceListBox.SelectedIndexChanged -= new EventHandler(this.SequenceListBox_SelectedIndexChanged);
            sequenceListBox.DataSource = null;
            sequenceListBox.DataSource = settings.Moves;
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
                        Period = (int)newAfterActionPeriodNum.Value,
                        Point = new Point((int)newPointXNum.Value, (int)newPointYNum.Value),
                        Button = (MouseActions)newMouseButtonsComboBox.SelectedItem
                    };
                case Actions.Keyboard:
                    return new KeyboardAction
                    {
                        Id = action.Id,
                        Type = Actions.Keyboard,
                        Period = (int)newAfterActionPeriodNum.Value,
                        Text = newKeyboardText.Text
                    };
            }
            throw new NotImplementedException();
        }

        private void SequenceListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sequenceListBox != null && sequenceListBox.SelectedIndex > -1)
            {
                newAfterActionPeriodNum.Value = settings.Moves[sequenceListBox.SelectedIndex].Period;
                newActionsComboBox.SelectedItem = settings.Moves[sequenceListBox.SelectedIndex].Type;
                switch (settings.Moves[sequenceListBox.SelectedIndex])
                {
                    case MouseAction mouse:
                        newKeyboardText.Visible = false;
                        newPointXNum.Visible = true;
                        newPointYNum.Visible = true;
                        newMouseButtonsComboBox.Visible = true;
                        newKeyboardText.Text = "";
                        newPointXNum.Value = mouse.Point.X;
                        newPointYNum.Value = mouse.Point.Y;
                        break;
                    case KeyboardAction keyboard:
                        newKeyboardText.Visible = true;
                        newPointXNum.Visible = false;
                        newPointYNum.Visible = false;
                        newMouseButtonsComboBox.Visible = false;
                        newKeyboardText.Text = keyboard.Text;
                        newPointXNum.Value = 0;
                        newPointYNum.Value = 0;
                        break;
                }
            }
            else
            {
                newAfterActionPeriodNum.Value = 100;
                newPointXNum.Value = 0;
                newPointYNum.Value = 0;
                newKeyboardText.Text = "";
                newMouseButtonsComboBox.SelectedIndex = 0;
                newPointXNum.Visible = true;
                newPointYNum.Visible = true;
                newKeyboardText.Visible = false;
                newMouseButtonsComboBox.Visible = true;
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
                files.Clear();
                string path = Directory.GetCurrentDirectory();
                DirectoryInfo di = new DirectoryInfo(path);
                foreach (var fi in di.GetFiles("*.json"))
                {
                    files.Add(fi.Name);
                }
                profilesListBox.DataSource = null;
                profilesListBox.DataSource = files;
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
            var move = sequenceListBox.SelectedIndex > -1 && settings.Moves.Count > 0 ? settings.Moves[sequenceListBox.SelectedIndex] : null;

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
    }
}