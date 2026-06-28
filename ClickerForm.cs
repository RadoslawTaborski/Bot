using Clicker.Enums;
using Clicker.Extensions;
using Clicker.Json;
using Clicker.Models;
using Clicker.Models.Actions;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Timers;
using Gma.System.MouseKeyHook;
using Action = Clicker.Models.Actions.Action;

namespace Clicker;

public partial class ClickerForm : Form
{
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        Converters = { new ActionConverter(), new Json.PointConverter() }
    };

    private readonly BindingList<string> _files;
    private readonly System.Timers.Timer _timer = new();
    private readonly Random _random = new(DateTime.Now.Millisecond);

    private IKeyboardMouseEvents? _globalHook;

    private readonly ClickerState _state;
    private readonly string _sequencePath;

    public ClickerForm()
    {
        InitializeComponent();

        PrepareComboBoxes();

        _files = [];
        _state = new ClickerState();

        var path = Directory.GetCurrentDirectory();
        _sequencePath = Path.Combine(path, "Sequences");
        Directory.CreateDirectory(_sequencePath);

        LoadProfiles();
        SetValuesFromSettings(_state.Settings);
    }

    private void PrepareComboBoxes()
    {
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
        newActionsComboBox.Items.Add(Actions.Pause);
        newActionsComboBox.SelectedIndex = 0;
    }

    private void GlobalHook_MouseDownExt(object? sender, MouseEventExtArgs e)
    {
        _state.Settings.Moves.Add(
            new MouseAction
            {
                Id = (_state.Settings.Moves.Count + 1).ToString(),
                Type = Actions.Mouse,
                Point = new Point(Cursor.Position.X, Cursor.Position.Y),
                Period = (int)afterActionPeriodNum.Value,
                Button = e.Button == MouseButtons.Middle
                    ? MouseActions.Middle
                    : e.Button == MouseButtons.Right
                        ? MouseActions.Right
                        : MouseActions.Left,
                Active = true
            });
    }

    private void RecordButton_Click(object? sender, EventArgs e)
    {
        Activation();
        ResetInfoLabels();
        SetMainTabControls(stopRecord: true);
        SetSettingsTabControls(enabled: false);
        SetSequenceTabControls(enabled: false);
        SetProfilesTabControls(enabledSave: false, enableLoadAndDelete: false);
    }

    private void StopRecordButton_Click(object? sender, EventArgs e)
    {
        Deactivation();

        if (_state.Settings.Moves.Count > 2)
        {
            _state.Settings.Moves[0] = new PauseAction
            {
                Id = "0",
                Type = Actions.Pause,
                Description = "Dummy",
                Period = 0,
                Active = true
            };
            _state.Settings.Moves[^1] = new PauseAction
            {
                Id = "0",
                Type = Actions.Pause,
                Description = "Dummy",
                Period = 0,
                Active = true
            };
            _state.Settings.Moves[^2] = new PauseAction
            {
                Id = "0",
                Type = Actions.Pause,
                Description = "Dummy",
                Period = 0,
                Active = true
            };
        } else
        {
            _state.Settings.Moves.Clear();
            SetMainTabControls(record: true);
            SetSettingsTabControls(enabled: true);
            SetProfilesTabControls(enabledSave: false, enableLoadAndDelete: true);
            return;
        }

        SetMainTabControls(start: true);
        SetSettingsTabControls(enabled: true);
        SetProfilesTabControls(enabledSave: true, enableLoadAndDelete: true);
        sequenceListBox.DataSource = _state.Settings.Moves;
        SetSequenceTabControls(enabled: true);
        ResetEditActionControls();
    }

    private void StartButton_Click(object? sender, EventArgs e)
    {
        if (afterSequencePeriodStartNum.Value > afterSequencePeriodStopNum.Value)
        {
            MessageBox.Show("Złe wartości dla odstępu między sekwencjami");
        }
        else
        {
            _state.CurrentSequence = ProvideSequence();
            _state.OverrideIterationsQueues = GetIterationsDictionary();
            RunTimer(2000);

            SetMainTabControls(stop: true);
            SetSettingsTabControls(enabled: false);
            SetSequenceTabControls(enabled: false);
            SetProfilesTabControls(enabledSave: false, enableLoadAndDelete: false);
        }
    }

    private void StopButton_Click(object? sender, EventArgs e)
    {
        _state.NestedSequencesNotes.Clear();
        _timer.Stop();
        _timer.Elapsed -= new ElapsedEventHandler(DoAction);
        _state.CurrentIndex = 1;
        _state.IterationsCounter = 0;

        ResetInfoLabels();
        SetMainTabControls(start: true);
        SetSettingsTabControls(enabled: true);
        SetSequenceTabControls(enabled: true);
        SetProfilesTabControls(enabledSave: true, enableLoadAndDelete: true);
    }

    private void ClearButton_Click(object sender, EventArgs e)
    {
        _state.Reset();

        ResetInfoLabels();
        SetMainTabControls(record: true);

        iterationsHelperText.Text = "";
        SetSettingsTabControls(true);

        editButton.Enabled = false;
        ResetEditActionControls();

        SetSequenceTabControls(enabled: false);

        tagsListBox.Items.Clear();
        SetProfilesTabControls(enabledSave: false, enableLoadAndDelete: true);
    }

    private void RepeatSequenceCheckbox_CheckedChanged(object? sender, EventArgs e)
    {
        SetRepeatSettingsControls(true);
    }

    private void EditButton_Click(object? sender, EventArgs e)
    {
        if (_state.Settings.Moves[sequenceListBox.SelectedIndex].Type != (Actions)(newActionsComboBox.SelectedItem ?? 0))
        {
            _state.Settings.Moves[sequenceListBox.SelectedIndex] = MigrateMove(_state.Settings.Moves[sequenceListBox.SelectedIndex], (Actions)(newActionsComboBox.SelectedItem ?? 0));
        }
        else
        {
            switch (_state.Settings.Moves[sequenceListBox.SelectedIndex])
            {
                case MouseAction mouse:
                    mouse.Point = new Point((int)newPointXNum.Value, (int)newPointYNum.Value);
                    mouse.Button = (MouseActions)(newMouseButtonsComboBox.SelectedItem ?? 0);
                    break;
                case KeyboardAction keyboard:
                    keyboard.Text = newKeyboardText.Text;
                    break;
                case SubSequenceAction subSequence:
                    subSequence.FileName = newSubsequenceFilenameText.Text;
                    subSequence.Iterations = (int)newSubsequenceIterationsNum.Value;
                    break;
                case PauseAction:
                    break;
            }
            _state.Settings.Moves[sequenceListBox.SelectedIndex].Period = (int)newAfterActionPeriodNum.Value;
            _state.Settings.Moves[sequenceListBox.SelectedIndex].Tag = newTagText.Text;
            _state.Settings.Moves[sequenceListBox.SelectedIndex].Description = newDescriptionText.Text;
        }

        UpdateTags(_state.Settings);
        sequenceListBox.DataSource = _state.Settings.Moves;
    }

    private void LoadButton_Click(object? sender, EventArgs e)
    {
        if (profilesListBox.SelectedItem == null)
        {
            MessageBox.Show("Nie wybrano profilu do wczytania", "BLAD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        var fileName = profilesListBox.SelectedItem.ToString();
        _state.Settings = LoadSettings(fileName!);
        SetValuesFromSettings(_state.Settings);
        fileNameText.Enabled = true;
        fileNameText.Text = Path.GetFileNameWithoutExtension(fileName);
    }

    private void SaveButton_Click(object? sender, EventArgs e)
    {
        _state.Settings.Period1 = (int)afterActionPeriodNum.Value;
        _state.Settings.PeriodA = (int)afterSequencePeriodStartNum.Value;
        _state.Settings.PeriodB = (int)afterSequencePeriodStopNum.Value;
        _state.Settings.NumberOfRepeats = (int)numberOfRepeatNum.Value;
        _state.Settings.Repeat = repeatSequenceCheckbox.Checked;
        _state.Settings.RandomTimeInterval = randomIntervalCheckbox.Checked;
        _state.Settings.Iterations = GetIterationsDictionary();
        UpdateActionsIds(_state.Settings);
        SaveTags();

        try
        {
            var json = JsonSerializer.Serialize(_state.Settings, _jsonOptions);
            File.WriteAllText(@$"Sequences\{fileNameText.Text}.json", json);
        }
        catch (SerializationException ex)
        {
            MessageBox.Show(this, "Nastąpił następujący błąd: \n" + ex.ToString(), "BLAD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        LoadProfiles();
        SetProfilesTabControls(enabledSave: true, enableLoadAndDelete: true);
    }

    private void DeleteButton_Click(object? sender, EventArgs e)
    {
        var selectedProfileName = profilesListBox.SelectedItem?.ToString();
        if (string.IsNullOrEmpty(selectedProfileName))
        {
            return;
        }

        var filePath = Path.Combine(_sequencePath, selectedProfileName);
        if (File.Exists(filePath))
        {
            try
            {
                File.Delete(filePath);
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }

            LoadProfiles();
            SetProfilesTabControls(enabledSave: true, enableLoadAndDelete: true);
        }
    }

    private void ActionsComboBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (newActionsComboBox.SelectedItem == null)
        {
            return;
        }

        var action = sequenceListBox.SelectedIndex > -1 && _state.Settings.Moves.Count > 0 ? _state.Settings.Moves[sequenceListBox.SelectedIndex] : null;

        switch ((Actions)newActionsComboBox.SelectedItem)
        {
            case Actions.Mouse:
                if (action != null && action is MouseAction mouseMove)
                {
                    SetEditMouseActionControls(selectedIndex: (int)mouseMove.Button, pointX: mouseMove.Point.X, pointY: mouseMove.Point.Y, visible: true);
                }
                else
                {
                    SetEditMouseActionControls(visible: true);
                }
                SetEditKeyboardActionControls();
                SetEditSubSequenceActionControls();
                break;
            case Actions.Keyboard:
                if (action != null && action is KeyboardAction keyboardMove)
                {
                    SetEditKeyboardActionControls(keyboardText: keyboardMove.Text, visible: true);
                }
                else
                {
                    SetEditKeyboardActionControls(visible: true);
                }
                SetEditMouseActionControls();
                SetEditSubSequenceActionControls();
                break;
            case Actions.SubSequence:
                if (action != null && action is SubSequenceAction subsequenceMove)
                {
                    SetEditSubSequenceActionControls(numOfIterations: subsequenceMove.Iterations, filename: subsequenceMove.FileName, visible: true);
                }
                else
                {
                    SetEditSubSequenceActionControls(visible: true);
                }
                SetEditMouseActionControls();
                SetEditKeyboardActionControls();
                break;
            case Actions.Pause:
                SetEditMouseActionControls();
                SetEditKeyboardActionControls();
                SetEditSubSequenceActionControls();
                break;
            default:
                break;
        }
    }

    private void SequenceListBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (sequenceListBox != null && sequenceListBox.SelectedIndex > -1)
        {
            newAfterActionPeriodNum.Value = _state.Settings.Moves[sequenceListBox.SelectedIndex].Period;
            newTagText.Text = _state.Settings.Moves[sequenceListBox.SelectedIndex].Tag;
            newDescriptionText.Text = _state.Settings.Moves[sequenceListBox.SelectedIndex].Description;
            newActionsComboBox.SelectedItem = _state.Settings.Moves[sequenceListBox.SelectedIndex].Type;

            switch (_state.Settings.Moves[sequenceListBox.SelectedIndex])
            {
                case MouseAction mouse:
                    SetEditMouseActionControls(pointX: mouse.Point.X, pointY: mouse.Point.Y, visible: true);
                    SetEditKeyboardActionControls();
                    SetEditSubSequenceActionControls();
                    break;
                case KeyboardAction keyboard:
                    SetEditMouseActionControls();
                    SetEditKeyboardActionControls(keyboardText: keyboard.Text, visible: true);
                    SetEditSubSequenceActionControls();
                    break;
                case SubSequenceAction subsequence:
                    SetEditMouseActionControls();
                    SetEditKeyboardActionControls();
                    SetEditSubSequenceActionControls(numOfIterations: subsequence.Iterations, filename: subsequence.FileName, visible: true);
                    break;
                case PauseAction:
                    SetEditMouseActionControls();
                    SetEditKeyboardActionControls();
                    SetEditSubSequenceActionControls();
                    break;
            }
        }
        else
        {
            ResetEditActionControls();
        }
        editButton.Enabled = true;
    }

    private void RandomIntervalCheckbox_CheckedChanged(object? sender, EventArgs e)
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

    private void AfterSequencePeriodStartNum_ValueChanged(object? sender, EventArgs e)
    {
        if (!randomIntervalCheckbox.Checked && afterSequencePeriodStartNum.Value >= afterSequencePeriodStartNum.Minimum)
        {
            afterSequencePeriodStopNum.Value = afterSequencePeriodStartNum.Value;
        }
    }

    public void Activation()
    {
        _globalHook = Hook.GlobalEvents();
        _globalHook.MouseDownExt += GlobalHook_MouseDownExt;
    }

    public void Deactivation()
    {
        _globalHook?.Dispose();
    }

    private void RunTimer(int newInterval)
    {
        _timer.Elapsed += new ElapsedEventHandler(DoAction);
        _timer.Interval = newInterval;
        _timer.Enabled = true;
        _timer.Start();
    }

    public void DoAction(object? sender, ElapsedEventArgs e)
    {
        var action = _state.CurrentSequence[_state.CurrentIndex];

        UpdateInfoLabels(action);

        if (action is SubSequenceAction subSequenceAction)
        {
            int overrideIteration = ProvideOverrideNumberOfIterations(subSequenceAction);

            _state.CurrentSequence.ReplaceWithRange(_state.CurrentIndex,
                LoadNestedSequence(subSequenceAction.FileName, subSequenceAction.Id, overrideIteration, subSequenceAction.Period));

            DoAction(sender, e);
            return;
        }

        var numberOfActions = ActionExecutor.Execute(_state.CurrentSequence[_state.CurrentIndex], _state.CurrentSequence.Cast<Action>().ElementAtOrDefault(_state.CurrentIndex + 1));
        _timer.Interval = _state.CurrentSequence[_state.CurrentIndex].Period;
        _state.CurrentIndex += numberOfActions;

        if (_state.CurrentIndex >= _state.CurrentSequence.Count - 2)
        {
            _state.IterationsCounter++;
            if (repeatSequenceCheckbox.Checked == true && _state.IterationsCounter < numberOfRepeatNum.Value)
            {
                var time = _random.Next((int)afterSequencePeriodStartNum.Value, (int)afterSequencePeriodStopNum.Value);

                action.Period = time;
                UpdateInfoLabels(action);

                _timer.Interval = time;
                _state.CurrentIndex = 1;
            }
            else
            {
                Invoke(new System.Action(delegate ()
                {
                    StopButton_Click(sender, e);
                }));
            }
        }
    }

    private Settings LoadSettings(string fileName)
    {
        try
        {
            var json = File.ReadAllText(@$"Sequences\{fileName}");
            var settings = JsonSerializer.Deserialize<Settings>(json, _jsonOptions);
            return settings is null ? throw new SerializationException("Deserialized Settings is null.") : settings;
        }
        catch (SerializationException ex)
        {
            MessageBox.Show(this, "Nastąpił następujący błąd: \n" + ex.ToString(), "BLAD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            throw;
        }
    }

    private List<Action> LoadNestedSequence(string fileName, string oldId, int numberOfIterations, int period)
    {
        if (numberOfIterations == 0)
        {
            return [];
        }
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
        newSequenceList[^1].Period = tmpSettings.PeriodB;
        var id = 0;
        for (int i = 0; i < newSequenceList.Count; i++)
        {
            newSequenceList[i].Id = $"{oldId}_{++id}";
        }

        var result = Enumerable.Range(0, numberOfIterations)
            .SelectMany(_ => newSequenceList.Select(x => x.Clone()))
            .ToList();

        result.ElementAt(result.Count - 1).Period = period;

        if (numberOfIterations > 1)
        {
            for (int i = 0; i < result.Count; i++)
            {
                _state.NestedSequencesNotes.Add(result[i].Guid, $"{(i / newSequenceList.Count) + 1} z {numberOfIterations}");
            }
        }

        return result;
    }

    private Dictionary<string, Queue<int>> GetIterationsDictionary()
    {
        var result = new Dictionary<string, Queue<int>>();
        var text = iterationsHelperText.Text;

        foreach (string line in text.Split(["\r\n", "\n"], StringSplitOptions.RemoveEmptyEntries))
        {
            var parts = line.Split([':'], 2);

            if (parts.Length == 2)
            {
                var iterationsPart = parts[1].Split([',']);

                foreach (var iteration in iterationsPart)
                {
                    if (int.TryParse(iteration.Trim(), out int value))
                    {
                        if (!result.ContainsKey(parts[0].Trim()))
                        {
                            result[parts[0].Trim()] = new Queue<int>();
                        }
                        result[parts[0].Trim()].Enqueue(value);
                    }
                }
            }
        }
        return result;
    }

    private void SetValuesFromSettings(Settings settings)
    {
        if (settings.Moves.Any())
        {
            SetMainTabControls(start: true);
            SetProfilesTabControls(enabledSave: true, enableLoadAndDelete: true);
            SetSequenceTabControls(enabled: true);
            UpdateActionsIds(settings);
        } else
        {
            SetProfilesTabControls(enabledSave: false, enableLoadAndDelete: true);
            SetSequenceTabControls(enabled: false);
        }
        SetTags(settings);
        SetIntervalsHelper(settings);
        SetSettingsTabControls(enabled: true);

        afterActionPeriodNum.Value = settings.Period1;
        afterSequencePeriodStartNum.Value = settings.PeriodA;
        numberOfRepeatNum.Value = settings.NumberOfRepeats;
        randomIntervalCheckbox.Checked = settings.RandomTimeInterval;
        repeatSequenceCheckbox.Checked = settings.Repeat;

        if (settings.RandomTimeInterval)
        {
            afterSequencePeriodStopNum.Value = settings.PeriodB;
        }
        else
        {
            afterSequencePeriodStopNum.Value = settings.PeriodA;
        }

        sequenceListBox.DataSource = settings.Moves;

        ResetEditActionControls();
    }

    private void LoadProfiles()
    {
        _files.Clear();
        DirectoryInfo dictionaryInfo = new(_sequencePath);
        foreach (var fileInfo in dictionaryInfo.GetFiles("*.json"))
        {
            _files.Add(fileInfo.Name);
        }
        profilesListBox.DataSource = _files;
    }

    private List<Action> ProvideSequence()
    {
        var selectedTags = tagsListBox.CheckedItems.Cast<string>().ToList();
        return [.. _state.Settings.Moves.Where(x => x.Active && (string.IsNullOrWhiteSpace(x.Tag) || selectedTags.Contains(x.Tag)))];
    }

    private int ProvideOverrideNumberOfIterations(SubSequenceAction subSequenceAction)
    {
        var overrideIteration = subSequenceAction.Iterations;
        if (_state.OverrideIterationsQueues.TryGetValue(subSequenceAction.FileName, out Queue<int>? queue))
        {
            if (queue.Count > 1)
            {
                overrideIteration = queue.Dequeue();
            }
            else if (queue.Count == 1)
            {
                overrideIteration = queue.Peek();
            }
        }

        return overrideIteration;
    }

    private static void UpdateActionsIds(Settings settings)
    {
        var id = 0;
        for (int i = 0; i < settings.Moves.Count - 2; i++)
        {
            var move = settings.Moves[i];
            move.Id = id++.ToString();
        }
        settings.Moves[^1].Id = "0";
        settings.Moves[^2].Id = "0";
    }

    private void SaveTags()
    {
        var allTags = _state.Settings.Moves.Where(x => !string.IsNullOrEmpty(x.Tag)).Select(x => x.Tag).Distinct().ToList();
        var selectedTags = tagsListBox.CheckedItems.Cast<string>().ToList();
        _state.Settings.Tags.Clear();
        allTags = [.. allTags.OrderBy(x => x)];
        foreach (var tag in allTags)
        {
            _state.Settings.Tags.Add(new TagSetting { Name = tag!, Active = selectedTags.Contains(tag!) });
        }
    }

    private void UpdateTags(Settings settings)
    {
        var actionsTags = settings.Moves.Where(x => !string.IsNullOrEmpty(x.Tag)).Select(x => x.Tag).Distinct().ToList();
        var listedTags = tagsListBox.Items
            .Cast<string>()
            .ToDictionary(
                item => item,
                item => tagsListBox.GetItemChecked(
                    tagsListBox.Items.IndexOf(item)
                )
            );
        tagsListBox.Items.Clear();
        actionsTags = [.. actionsTags.OrderBy(x => x)];
        foreach (var tag in actionsTags)
        {
            tagsListBox.Items.Add(tag!, !listedTags.ContainsKey(tag!) || listedTags[tag!]);
        }
    }

    private void SetTags(Settings settings)
    {
        var actionsTags = settings.Moves.Where(x => !string.IsNullOrEmpty(x.Tag)).Select(x => x.Tag).Distinct().ToList();
        var finalTags = new List<TagSetting>();
        foreach (var tag in actionsTags)
        {
            var existingTag = settings.Tags.FirstOrDefault(t => t.Name == tag);
            if (existingTag != null)
            {
                finalTags.Add(existingTag);
            }
            else
            {
                finalTags.Add(new TagSetting { Name = tag!, Active = true });
            }
        }

        tagsListBox.Items.Clear();
        finalTags = [.. finalTags.OrderBy(x => x.Name)];
        foreach (var tag in finalTags)
        {
            tagsListBox.Items.Add(tag.Name, tag.Active);
        }
    }

    private Action MigrateMove(Action action, Actions newType)
    {
        return newType switch
        {
            Actions.Mouse => new MouseAction
            {
                Id = action.Id,
                Type = Actions.Mouse,
                Description = newDescriptionText.Text,
                Tag = newTagText.Text,
                Period = (int)newAfterActionPeriodNum.Value,
                Point = new Point((int)newPointXNum.Value, (int)newPointYNum.Value),
                Button = (MouseActions)(newMouseButtonsComboBox.SelectedItem ?? 0),
                Active = true
            },
            Actions.Keyboard => new KeyboardAction
            {
                Id = action.Id,
                Type = Actions.Keyboard,
                Description = newDescriptionText.Text,
                Tag = newTagText.Text,
                Period = (int)newAfterActionPeriodNum.Value,
                Text = newKeyboardText.Text,
                Active = true
            },
            Actions.SubSequence => new SubSequenceAction
            {
                Id = action.Id,
                Type = Actions.SubSequence,
                Description = newDescriptionText.Text,
                Tag = newTagText.Text,
                Period = (int)newAfterActionPeriodNum.Value,
                FileName = newSubsequenceFilenameText.Text,
                Iterations = (int)newSubsequenceIterationsNum.Value,
                Active = true
            },
            Actions.Pause => new PauseAction
            {
                Id = action.Id,
                Type = Actions.Pause,
                Description = newDescriptionText.Text,
                Tag = newTagText.Text,
                Period = (int)newAfterActionPeriodNum.Value,
                Active = true
            },
            _ => throw new NotImplementedException()
        };
    }

    private void ResetInfoLabels()
    {
        sequenceCounterLabel.Text = "";
        subsequenceCounterLabel.Text = "";
        actionLabel.Text = "";
    }

    private void UpdateInfoLabels(Action action)
    {
        sequenceCounterLabel.Invoke((MethodInvoker)(() =>
        {
            sequenceCounterLabel.Text = $"Iteracja: {_state.IterationsCounter + 1} z {(repeatSequenceCheckbox.Checked ? numberOfRepeatNum.Value : 1)}";
        }));

        actionLabel.Invoke((MethodInvoker)(() =>
        {
            actionLabel.Text = $"Akcja: {action}";
        }));

        subsequenceCounterLabel.Invoke((MethodInvoker)(() =>
        {
            if (_state.NestedSequencesNotes.TryGetValue(action.Guid, out string? value))
            {
                subsequenceCounterLabel.Text = $"Sub-iteracja: {value}";
            }
            else
            {
                subsequenceCounterLabel.Text = "";
            }
        }));
    }

    private void SetMainTabControls(bool record = false, bool stopRecord = false, bool start = false, bool stop = false)
    {
        recordButton.Enabled = record;
        stopRecordButton.Enabled = stopRecord;
        startButton.Enabled = start;
        stopButton.Enabled = stop;
        clearButton.Enabled = start;
    }

    private void SetSettingsTabControls(bool enabled = false)
    {
        afterActionPeriodNum.Enabled = enabled;
        repeatSequenceCheckbox.Enabled = enabled;
        randomIntervalCheckbox.Enabled = enabled;
        iterationsHelperText.Enabled = enabled;
        SetRepeatSettingsControls(enabled);
    }

    private void SetRepeatSettingsControls(bool enabled = false)
    {
        if (enabled && repeatSequenceCheckbox.Checked)
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

    private void SetIntervalsHelper(Settings settings)
    {
        var lines = settings.Iterations.Select(x => $"{x.Key}: {string.Join(", ", x.Value)}").ToList();
        iterationsHelperText.Text = string.Join(Environment.NewLine, lines);
    }

    private void ResetEditActionControls()
    {
        SetEditActionMainControls();
        SetEditMouseActionControls(visible: true);
        SetEditKeyboardActionControls();
        SetEditSubSequenceActionControls();
    }

    private void SetEditActionMainControls(int action = 0, int period = 100, string description = "", string tag = "")
    {
        newActionsComboBox.SelectedIndex = action;
        newAfterActionPeriodNum.Value = period;
        newDescriptionText.Text = description;
        newTagText.Text = tag;
    }

    private void SetEditMouseActionControls(int selectedIndex = 0, int pointX = 0, int pointY = 0, bool visible = false)
    {
        newMouseButtonsComboBox.Visible = visible;
        newMouseButtonsComboBox.SelectedIndex = selectedIndex;
        newPointXLabel.Visible = visible;
        newPointXNum.Visible = visible;
        newPointXNum.Value = pointX;
        newPointYLabel.Visible = visible;
        newPointYNum.Visible = visible;
        newPointYNum.Value = pointY;
    }

    private void SetEditKeyboardActionControls(string keyboardText = "", bool visible = false)
    {
        newKeyboardText.Visible = visible;
        newKeyboardText.Text = keyboardText;
    }

    private void SetEditSubSequenceActionControls(int numOfIterations = 1, string filename = "", bool visible = false)
    {
        newSubsequenceIterationsNum.Visible = visible;
        newSubsequenceIterationsNum.Value = numOfIterations;
        newSubsequenceFilenameText.Visible = visible;
        newSubsequenceFilenameText.Text = filename;
        newSubsequenceIterationsLabel.Visible = visible;
    }

    private void SetSequenceTabControls(bool enabled = false)
    {
        newActionsComboBox.Enabled = enabled;
        newAfterActionPeriodNum.Enabled = enabled;
        newDescriptionText.Enabled = enabled;
        newTagText.Enabled = enabled;
        newMouseButtonsComboBox.Enabled = enabled;
        newPointXNum.Enabled = enabled;
        newPointYNum.Enabled = enabled;
        newKeyboardText.Enabled = enabled;
        newSubsequenceIterationsNum.Visible = enabled;
        newSubsequenceFilenameText.Visible = enabled;
        editButton.Enabled = enabled && _state.Settings.Moves.Count != 0;
    }

    private void SetProfilesTabControls(bool enabledSave = false, bool enableLoadAndDelete = false)
    {
        if (enableLoadAndDelete && profilesListBox.Items.Count > 0)
        {
            loadButton.Enabled = true;
            deleteButton.Enabled = true;
        }
        else
        {
            loadButton.Enabled = false;
            deleteButton.Enabled = false;
        }

        saveButton.Enabled = enabledSave;
        fileNameText.Enabled = enabledSave;
    }
}