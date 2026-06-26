namespace Clicker
{
    partial class ClickerForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClickerForm));
            tabControl = new TabControl();
            mainPage = new TabPage();
            infoBox = new GroupBox();
            subsequenceCounterLabel = new Label();
            actionLabel = new Label();
            sequenceCounterLabel = new Label();
            stopRecordButton = new Button();
            recordButton = new Button();
            stopButton = new Button();
            clearButton = new Button();
            startButton = new Button();
            settingsPage = new TabPage();
            numberOfRepeatLabel = new Label();
            msLabel3 = new Label();
            afterSequencePeriodStopNum = new NumericUpDown();
            msLabel2 = new Label();
            afterSequencePeriodStartNum = new NumericUpDown();
            msLabel1 = new Label();
            afterActionPeriodNum = new NumericUpDown();
            iterationsHelperText = new TextBox();
            numberOfRepeatNum = new NumericUpDown();
            randomIntervalCheckbox = new CheckBox();
            repeatSequenceCheckbox = new CheckBox();
            afterActionPeriodLabel = new Label();
            sequencePage = new TabPage();
            newSubsequenceIterationsLabel = new Label();
            newSubsequenceIterationsNum = new NumericUpDown();
            newPointYLabel = new Label();
            newPointYNum = new NumericUpDown();
            newPointXLabel = new Label();
            newPointXNum = new NumericUpDown();
            newMouseButtonsComboBox = new ComboBox();
            editButton = new Button();
            newAfterActionPeriodLabel = new Label();
            newAfterActionPeriodNum = new NumericUpDown();
            newTagLabel = new Label();
            newTagText = new TextBox();
            newDescriptionLabel = new Label();
            newDescriptionText = new TextBox();
            newActionsComboBox = new ComboBox();
            sequenceListBox = new ListBox();
            newKeyboardText = new TextBox();
            newSubsequenceFilenameText = new TextBox();
            tagsPage = new TabPage();
            tagsListBox = new CheckedListBox();
            profilesPage = new TabPage();
            profilesListBox = new ListBox();
            extensionLabel = new Label();
            fileNameText = new TextBox();
            deleteButton = new Button();
            loadButton = new Button();
            saveButton = new Button();
            tabControl.SuspendLayout();
            mainPage.SuspendLayout();
            infoBox.SuspendLayout();
            settingsPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)afterSequencePeriodStopNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)afterSequencePeriodStartNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)afterActionPeriodNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numberOfRepeatNum).BeginInit();
            sequencePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)newSubsequenceIterationsNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)newPointYNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)newPointXNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)newAfterActionPeriodNum).BeginInit();
            tagsPage.SuspendLayout();
            profilesPage.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(mainPage);
            tabControl.Controls.Add(settingsPage);
            tabControl.Controls.Add(sequencePage);
            tabControl.Controls.Add(tagsPage);
            tabControl.Controls.Add(profilesPage);
            tabControl.Font = new Font("Microsoft Sans Serif", 7.8F);
            tabControl.Location = new Point(1, 0);
            tabControl.Margin = new Padding(3, 4, 3, 4);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(331, 263);
            tabControl.TabIndex = 0;
            // 
            // mainPage
            // 
            mainPage.Controls.Add(infoBox);
            mainPage.Controls.Add(stopRecordButton);
            mainPage.Controls.Add(recordButton);
            mainPage.Controls.Add(stopButton);
            mainPage.Controls.Add(clearButton);
            mainPage.Controls.Add(startButton);
            mainPage.Font = new Font("Microsoft Sans Serif", 7.8F);
            mainPage.Location = new Point(4, 25);
            mainPage.Margin = new Padding(3, 4, 3, 4);
            mainPage.Name = "mainPage";
            mainPage.Padding = new Padding(3, 4, 3, 4);
            mainPage.Size = new Size(323, 234);
            mainPage.TabIndex = 0;
            mainPage.Text = "Główne";
            mainPage.UseVisualStyleBackColor = true;
            // 
            // infoBox
            // 
            infoBox.Controls.Add(subsequenceCounterLabel);
            infoBox.Controls.Add(actionLabel);
            infoBox.Controls.Add(sequenceCounterLabel);
            infoBox.Font = new Font("Microsoft Sans Serif", 7.8F);
            infoBox.Location = new Point(3, -1);
            infoBox.Margin = new Padding(3, 4, 3, 4);
            infoBox.Name = "infoBox";
            infoBox.Padding = new Padding(3, 4, 3, 4);
            infoBox.Size = new Size(314, 66);
            infoBox.TabIndex = 5;
            infoBox.TabStop = false;
            // 
            // subsequenceCounterLabel
            // 
            subsequenceCounterLabel.AutoSize = true;
            subsequenceCounterLabel.Font = new Font("Microsoft Sans Serif", 7.8F);
            subsequenceCounterLabel.Location = new Point(166, 13);
            subsequenceCounterLabel.Name = "subsequenceCounterLabel";
            subsequenceCounterLabel.Size = new Size(0, 16);
            subsequenceCounterLabel.TabIndex = 2;
            // 
            // actionLabel
            // 
            actionLabel.AutoSize = true;
            actionLabel.Font = new Font("Microsoft Sans Serif", 7.8F);
            actionLabel.Location = new Point(2, 38);
            actionLabel.Name = "actionLabel";
            actionLabel.Size = new Size(0, 16);
            actionLabel.TabIndex = 1;
            // 
            // sequenceCounterLabel
            // 
            sequenceCounterLabel.AutoSize = true;
            sequenceCounterLabel.Font = new Font("Microsoft Sans Serif", 7.8F);
            sequenceCounterLabel.Location = new Point(2, 13);
            sequenceCounterLabel.Name = "sequenceCounterLabel";
            sequenceCounterLabel.Size = new Size(0, 16);
            sequenceCounterLabel.TabIndex = 0;
            // 
            // stopRecordButton
            // 
            stopRecordButton.Enabled = false;
            stopRecordButton.Font = new Font("Microsoft Sans Serif", 7.8F);
            stopRecordButton.Location = new Point(166, 66);
            stopRecordButton.Margin = new Padding(3, 4, 3, 4);
            stopRecordButton.Name = "stopRecordButton";
            stopRecordButton.Size = new Size(151, 53);
            stopRecordButton.TabIndex = 4;
            stopRecordButton.Text = "Zatrzymaj nagrywanie";
            stopRecordButton.UseVisualStyleBackColor = true;
            stopRecordButton.Click += StopRecordButton_Click;
            // 
            // recordButton
            // 
            recordButton.Font = new Font("Microsoft Sans Serif", 7.8F);
            recordButton.Location = new Point(3, 66);
            recordButton.Margin = new Padding(3, 4, 3, 4);
            recordButton.Name = "recordButton";
            recordButton.Size = new Size(160, 53);
            recordButton.TabIndex = 3;
            recordButton.Text = "Nagraj";
            recordButton.UseVisualStyleBackColor = true;
            recordButton.Click += RecordButton_Click;
            // 
            // stopButton
            // 
            stopButton.Enabled = false;
            stopButton.Font = new Font("Microsoft Sans Serif", 7.8F);
            stopButton.Location = new Point(166, 120);
            stopButton.Margin = new Padding(3, 4, 3, 4);
            stopButton.Name = "stopButton";
            stopButton.Size = new Size(151, 83);
            stopButton.TabIndex = 2;
            stopButton.Text = "Stop";
            stopButton.UseVisualStyleBackColor = true;
            stopButton.Click += StopButton_Click;
            // 
            // clearButton
            // 
            clearButton.Enabled = false;
            clearButton.Font = new Font("Microsoft Sans Serif", 7.8F);
            clearButton.Location = new Point(3, 203);
            clearButton.Margin = new Padding(3, 4, 3, 4);
            clearButton.Name = "clearButton";
            clearButton.Size = new Size(314, 29);
            clearButton.TabIndex = 1;
            clearButton.Text = "Wyczyść pamięć";
            clearButton.UseVisualStyleBackColor = true;
            clearButton.Click += ClearButton_Click;
            // 
            // startButton
            // 
            startButton.Enabled = false;
            startButton.Font = new Font("Microsoft Sans Serif", 7.8F);
            startButton.Location = new Point(3, 120);
            startButton.Margin = new Padding(3, 4, 3, 4);
            startButton.Name = "startButton";
            startButton.Size = new Size(160, 83);
            startButton.TabIndex = 0;
            startButton.Text = "Start";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += StartButton_Click;
            // 
            // settingsPage
            // 
            settingsPage.Controls.Add(numberOfRepeatLabel);
            settingsPage.Controls.Add(msLabel3);
            settingsPage.Controls.Add(afterSequencePeriodStopNum);
            settingsPage.Controls.Add(msLabel2);
            settingsPage.Controls.Add(afterSequencePeriodStartNum);
            settingsPage.Controls.Add(msLabel1);
            settingsPage.Controls.Add(afterActionPeriodNum);
            settingsPage.Controls.Add(iterationsHelperText);
            settingsPage.Controls.Add(numberOfRepeatNum);
            settingsPage.Controls.Add(randomIntervalCheckbox);
            settingsPage.Controls.Add(repeatSequenceCheckbox);
            settingsPage.Controls.Add(afterActionPeriodLabel);
            settingsPage.Font = new Font("Microsoft Sans Serif", 7.8F);
            settingsPage.Location = new Point(4, 25);
            settingsPage.Margin = new Padding(3, 4, 3, 4);
            settingsPage.Name = "settingsPage";
            settingsPage.Padding = new Padding(3, 4, 3, 4);
            settingsPage.Size = new Size(323, 234);
            settingsPage.TabIndex = 1;
            settingsPage.Text = "Ustawienia";
            settingsPage.UseVisualStyleBackColor = true;
            // 
            // numberOfRepeatLabel
            // 
            numberOfRepeatLabel.AutoSize = true;
            numberOfRepeatLabel.Location = new Point(75, 72);
            numberOfRepeatLabel.Name = "numberOfRepeatLabel";
            numberOfRepeatLabel.Size = new Size(13, 16);
            numberOfRepeatLabel.TabIndex = 11;
            numberOfRepeatLabel.Text = "x";
            // 
            // msLabel3
            // 
            msLabel3.AutoSize = true;
            msLabel3.Location = new Point(293, 72);
            msLabel3.Name = "msLabel3";
            msLabel3.Size = new Size(25, 16);
            msLabel3.TabIndex = 10;
            msLabel3.Text = "ms";
            // 
            // afterSequencePeriodStopNum
            // 
            afterSequencePeriodStopNum.Font = new Font("Microsoft Sans Serif", 7.8F);
            afterSequencePeriodStopNum.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            afterSequencePeriodStopNum.Location = new Point(213, 70);
            afterSequencePeriodStopNum.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            afterSequencePeriodStopNum.Minimum = new decimal(new int[] { 100, 0, 0, 0 });
            afterSequencePeriodStopNum.Name = "afterSequencePeriodStopNum";
            afterSequencePeriodStopNum.Size = new Size(80, 22);
            afterSequencePeriodStopNum.TabIndex = 9;
            afterSequencePeriodStopNum.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // msLabel2
            // 
            msLabel2.AutoSize = true;
            msLabel2.Location = new Point(172, 72);
            msLabel2.Name = "msLabel2";
            msLabel2.Size = new Size(41, 16);
            msLabel2.TabIndex = 8;
            msLabel2.Text = "ms   - ";
            // 
            // afterSequencePeriodStartNum
            // 
            afterSequencePeriodStartNum.Font = new Font("Microsoft Sans Serif", 7.8F);
            afterSequencePeriodStartNum.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            afterSequencePeriodStartNum.Location = new Point(92, 70);
            afterSequencePeriodStartNum.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            afterSequencePeriodStartNum.Minimum = new decimal(new int[] { 100, 0, 0, 0 });
            afterSequencePeriodStartNum.Name = "afterSequencePeriodStartNum";
            afterSequencePeriodStartNum.Size = new Size(80, 22);
            afterSequencePeriodStartNum.TabIndex = 7;
            afterSequencePeriodStartNum.Value = new decimal(new int[] { 100, 0, 0, 0 });
            afterSequencePeriodStartNum.ValueChanged += AfterSequencePeriodStartNum_ValueChanged;
            // 
            // msLabel1
            // 
            msLabel1.AutoSize = true;
            msLabel1.Location = new Point(293, 4);
            msLabel1.Name = "msLabel1";
            msLabel1.Size = new Size(25, 16);
            msLabel1.TabIndex = 6;
            msLabel1.Text = "ms";
            // 
            // afterActionPeriodNum
            // 
            afterActionPeriodNum.Font = new Font("Microsoft Sans Serif", 7.8F);
            afterActionPeriodNum.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            afterActionPeriodNum.Location = new Point(213, 2);
            afterActionPeriodNum.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            afterActionPeriodNum.Minimum = new decimal(new int[] { 100, 0, 0, 0 });
            afterActionPeriodNum.Name = "afterActionPeriodNum";
            afterActionPeriodNum.Size = new Size(80, 22);
            afterActionPeriodNum.TabIndex = 5;
            afterActionPeriodNum.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // iterationsHelperText
            // 
            iterationsHelperText.Font = new Font("Microsoft Sans Serif", 7.8F);
            iterationsHelperText.Location = new Point(0, 97);
            iterationsHelperText.Margin = new Padding(3, 4, 3, 4);
            iterationsHelperText.Multiline = true;
            iterationsHelperText.Name = "iterationsHelperText";
            iterationsHelperText.Size = new Size(320, 133);
            iterationsHelperText.TabIndex = 4;
            // 
            // numberOfRepeatNum
            // 
            numberOfRepeatNum.Font = new Font("Microsoft Sans Serif", 7.8F);
            numberOfRepeatNum.Location = new Point(4, 70);
            numberOfRepeatNum.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numberOfRepeatNum.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            numberOfRepeatNum.Name = "numberOfRepeatNum";
            numberOfRepeatNum.Size = new Size(65, 22);
            numberOfRepeatNum.TabIndex = 3;
            numberOfRepeatNum.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // randomIntervalCheckbox
            // 
            randomIntervalCheckbox.AutoSize = true;
            randomIntervalCheckbox.Font = new Font("Microsoft Sans Serif", 7.8F);
            randomIntervalCheckbox.Location = new Point(5, 46);
            randomIntervalCheckbox.Margin = new Padding(3, 4, 3, 4);
            randomIntervalCheckbox.Name = "randomIntervalCheckbox";
            randomIntervalCheckbox.Size = new Size(296, 20);
            randomIntervalCheckbox.TabIndex = 2;
            randomIntervalCheckbox.Text = "Ruchomy odstęp czasu między sekwencjami";
            randomIntervalCheckbox.UseVisualStyleBackColor = true;
            randomIntervalCheckbox.CheckedChanged += RandomIntervalCheckbox_CheckedChanged;
            // 
            // repeatSequenceCheckbox
            // 
            repeatSequenceCheckbox.AutoSize = true;
            repeatSequenceCheckbox.Font = new Font("Microsoft Sans Serif", 7.8F);
            repeatSequenceCheckbox.Location = new Point(5, 24);
            repeatSequenceCheckbox.Margin = new Padding(3, 4, 3, 4);
            repeatSequenceCheckbox.Name = "repeatSequenceCheckbox";
            repeatSequenceCheckbox.Size = new Size(154, 20);
            repeatSequenceCheckbox.TabIndex = 1;
            repeatSequenceCheckbox.Text = "Powtarzaj sekwencję";
            repeatSequenceCheckbox.UseVisualStyleBackColor = true;
            repeatSequenceCheckbox.CheckedChanged += RepeatSequenceCheckbox_CheckedChanged;
            // 
            // afterActionPeriodLabel
            // 
            afterActionPeriodLabel.AutoSize = true;
            afterActionPeriodLabel.Font = new Font("Microsoft Sans Serif", 7.8F);
            afterActionPeriodLabel.Location = new Point(2, 4);
            afterActionPeriodLabel.Name = "afterActionPeriodLabel";
            afterActionPeriodLabel.Size = new Size(212, 16);
            afterActionPeriodLabel.TabIndex = 0;
            afterActionPeriodLabel.Text = "odstęp czasu między kliknięciami: ";
            // 
            // sequencePage
            // 
            sequencePage.Controls.Add(newSubsequenceIterationsLabel);
            sequencePage.Controls.Add(newSubsequenceIterationsNum);
            sequencePage.Controls.Add(newMouseButtonsComboBox);
            sequencePage.Controls.Add(editButton);
            sequencePage.Controls.Add(newAfterActionPeriodLabel);
            sequencePage.Controls.Add(newAfterActionPeriodNum);
            sequencePage.Controls.Add(newTagLabel);
            sequencePage.Controls.Add(newTagText);
            sequencePage.Controls.Add(newDescriptionLabel);
            sequencePage.Controls.Add(newDescriptionText);
            sequencePage.Controls.Add(newActionsComboBox);
            sequencePage.Controls.Add(sequenceListBox);
            sequencePage.Controls.Add(newSubsequenceFilenameText);
            sequencePage.Controls.Add(newPointXNum);
            sequencePage.Controls.Add(newPointYNum);
            sequencePage.Controls.Add(newPointXLabel);
            sequencePage.Controls.Add(newPointYLabel);
            sequencePage.Controls.Add(newKeyboardText);
            sequencePage.Font = new Font("Microsoft Sans Serif", 7.8F);
            sequencePage.Location = new Point(4, 25);
            sequencePage.Margin = new Padding(3, 4, 3, 4);
            sequencePage.Name = "sequencePage";
            sequencePage.Size = new Size(323, 234);
            sequencePage.TabIndex = 2;
            sequencePage.Text = "Sekwencja";
            sequencePage.UseVisualStyleBackColor = true;
            // 
            // newSubsequenceIterationsLabel
            // 
            newSubsequenceIterationsLabel.AutoSize = true;
            newSubsequenceIterationsLabel.Location = new Point(70, 210);
            newSubsequenceIterationsLabel.Name = "newSubsequenceIterationsLabel";
            newSubsequenceIterationsLabel.Size = new Size(13, 16);
            newSubsequenceIterationsLabel.TabIndex = 17;
            newSubsequenceIterationsLabel.Text = "x";
            newSubsequenceIterationsLabel.Visible = false;
            // 
            // newSubsequenceIterationsNum
            // 
            newSubsequenceIterationsNum.Font = new Font("Microsoft Sans Serif", 7.8F);
            newSubsequenceIterationsNum.Location = new Point(2, 208);
            newSubsequenceIterationsNum.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            newSubsequenceIterationsNum.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            newSubsequenceIterationsNum.Name = "newSubsequenceIterationsNum";
            newSubsequenceIterationsNum.Size = new Size(62, 22);
            newSubsequenceIterationsNum.TabIndex = 16;
            newSubsequenceIterationsNum.Value = new decimal(new int[] { 1, 0, 0, 0 });
            newSubsequenceIterationsNum.Visible = false;
            // 
            // newPointYLabel
            // 
            newPointYLabel.AutoSize = true;
            newPointYLabel.Location = new Point(223, 210);
            newPointYLabel.Name = "newPointYLabel";
            newPointYLabel.Size = new Size(19, 16);
            newPointYLabel.TabIndex = 14;
            newPointYLabel.Text = "Y:";
            // 
            // newPointYNum
            // 
            newPointYNum.Location = new Point(242, 208);
            newPointYNum.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            newPointYNum.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            newPointYNum.Name = "newPointYNum";
            newPointYNum.Size = new Size(77, 22);
            newPointYNum.TabIndex = 13;
            // 
            // newPointXLabel
            // 
            newPointXLabel.AutoSize = true;
            newPointXLabel.Location = new Point(122, 210);
            newPointXLabel.Name = "newPointXLabel";
            newPointXLabel.Size = new Size(18, 16);
            newPointXLabel.TabIndex = 12;
            newPointXLabel.Text = "X:";
            // 
            // newPointXNum
            // 
            newPointXNum.Location = new Point(141, 208);
            newPointXNum.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            newPointXNum.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            newPointXNum.Name = "newPointXNum";
            newPointXNum.Size = new Size(77, 22);
            newPointXNum.TabIndex = 11;
            // 
            // newMouseButtonsComboBox
            // 
            newMouseButtonsComboBox.FormattingEnabled = true;
            newMouseButtonsComboBox.Location = new Point(2, 207);
            newMouseButtonsComboBox.Name = "newMouseButtonsComboBox";
            newMouseButtonsComboBox.Size = new Size(118, 24);
            newMouseButtonsComboBox.TabIndex = 10;
            // 
            // editButton
            // 
            editButton.Enabled = false;
            editButton.Location = new Point(225, 151);
            editButton.Name = "editButton";
            editButton.Size = new Size(94, 29);
            editButton.TabIndex = 9;
            editButton.Text = "Edytuj";
            editButton.UseVisualStyleBackColor = true;
            editButton.Click += EditButton_Click;
            // 
            // newAfterActionPeriodLabel
            // 
            newAfterActionPeriodLabel.AutoSize = true;
            newAfterActionPeriodLabel.Location = new Point(122, 157);
            newAfterActionPeriodLabel.Name = "newAfterActionPeriodLabel";
            newAfterActionPeriodLabel.Size = new Size(19, 16);
            newAfterActionPeriodLabel.TabIndex = 8;
            newAfterActionPeriodLabel.Text = "P:";
            // 
            // newAfterActionPeriodNum
            // 
            newAfterActionPeriodNum.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            newAfterActionPeriodNum.Location = new Point(141, 155);
            newAfterActionPeriodNum.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            newAfterActionPeriodNum.Minimum = new decimal(new int[] { 100, 0, 0, 0 });
            newAfterActionPeriodNum.Name = "newAfterActionPeriodNum";
            newAfterActionPeriodNum.Size = new Size(77, 22);
            newAfterActionPeriodNum.TabIndex = 7;
            newAfterActionPeriodNum.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // newTagLabel
            // 
            newTagLabel.AutoSize = true;
            newTagLabel.Location = new Point(176, 185);
            newTagLabel.Name = "newTagLabel";
            newTagLabel.Size = new Size(19, 16);
            newTagLabel.TabIndex = 6;
            newTagLabel.Text = "T:";
            // 
            // newTagText
            // 
            newTagText.Location = new Point(194, 182);
            newTagText.Name = "newTagText";
            newTagText.Size = new Size(125, 22);
            newTagText.TabIndex = 5;
            // 
            // newDescriptionLabel
            // 
            newDescriptionLabel.AutoSize = true;
            newDescriptionLabel.Location = new Point(2, 185);
            newDescriptionLabel.Name = "newDescriptionLabel";
            newDescriptionLabel.Size = new Size(20, 16);
            newDescriptionLabel.TabIndex = 4;
            newDescriptionLabel.Text = "D:";
            // 
            // newDescriptionText
            // 
            newDescriptionText.Location = new Point(21, 182);
            newDescriptionText.Name = "newDescriptionText";
            newDescriptionText.Size = new Size(154, 22);
            newDescriptionText.TabIndex = 2;
            // 
            // newActionsComboBox
            // 
            newActionsComboBox.FormattingEnabled = true;
            newActionsComboBox.Location = new Point(2, 154);
            newActionsComboBox.Name = "newActionsComboBox";
            newActionsComboBox.Size = new Size(118, 24);
            newActionsComboBox.TabIndex = 1;
            newActionsComboBox.SelectedIndexChanged += ActionsComboBox_SelectedIndexChanged;
            // 
            // sequenceListBox
            // 
            sequenceListBox.FormattingEnabled = true;
            sequenceListBox.HorizontalScrollbar = true;
            sequenceListBox.Location = new Point(2, 2);
            sequenceListBox.Name = "sequenceListBox";
            sequenceListBox.Size = new Size(317, 148);
            sequenceListBox.TabIndex = 0;
            sequenceListBox.SelectedIndexChanged += SequenceListBox_SelectedIndexChanged;
            // 
            // newKeyboardText
            // 
            newKeyboardText.Location = new Point(2, 210);
            newKeyboardText.Name = "newKeyboardText";
            newKeyboardText.Size = new Size(317, 22);
            newKeyboardText.TabIndex = 15;
            newKeyboardText.Visible = false;
            // 
            // newSubsequenceFilenameText
            // 
            newSubsequenceFilenameText.Location = new Point(88, 208);
            newSubsequenceFilenameText.Name = "newSubsequenceFilenameText";
            newSubsequenceFilenameText.Size = new Size(231, 22);
            newSubsequenceFilenameText.TabIndex = 18;
            newSubsequenceFilenameText.Visible = false;
            // 
            // tagsPage
            // 
            tagsPage.Controls.Add(tagsListBox);
            tagsPage.Font = new Font("Microsoft Sans Serif", 7.8F);
            tagsPage.Location = new Point(4, 25);
            tagsPage.Margin = new Padding(3, 4, 3, 4);
            tagsPage.Name = "tagsPage";
            tagsPage.Size = new Size(323, 234);
            tagsPage.TabIndex = 3;
            tagsPage.Text = "Tagi";
            tagsPage.UseVisualStyleBackColor = true;
            // 
            // tagsListBox
            // 
            tagsListBox.Font = new Font("Microsoft Sans Serif", 7.8F);
            tagsListBox.FormattingEnabled = true;
            tagsListBox.Location = new Point(2, 2);
            tagsListBox.Margin = new Padding(3, 4, 3, 4);
            tagsListBox.Name = "tagsListBox";
            tagsListBox.Size = new Size(317, 225);
            tagsListBox.TabIndex = 0;
            // 
            // profilesPage
            // 
            profilesPage.Controls.Add(profilesListBox);
            profilesPage.Controls.Add(extensionLabel);
            profilesPage.Controls.Add(fileNameText);
            profilesPage.Controls.Add(deleteButton);
            profilesPage.Controls.Add(loadButton);
            profilesPage.Controls.Add(saveButton);
            profilesPage.Font = new Font("Microsoft Sans Serif", 7.8F);
            profilesPage.Location = new Point(4, 25);
            profilesPage.Margin = new Padding(3, 4, 3, 4);
            profilesPage.Name = "profilesPage";
            profilesPage.Size = new Size(323, 234);
            profilesPage.TabIndex = 4;
            profilesPage.Text = "Profile";
            profilesPage.UseVisualStyleBackColor = true;
            // 
            // profilesListBox
            // 
            profilesListBox.Font = new Font("Microsoft Sans Serif", 7.8F);
            profilesListBox.FormattingEnabled = true;
            profilesListBox.HorizontalScrollbar = true;
            profilesListBox.Location = new Point(2, 2);
            profilesListBox.Margin = new Padding(3, 4, 3, 4);
            profilesListBox.Name = "profilesListBox";
            profilesListBox.Size = new Size(317, 164);
            profilesListBox.TabIndex = 5;
            // 
            // extensionLabel
            // 
            extensionLabel.AutoSize = true;
            extensionLabel.Font = new Font("Microsoft Sans Serif", 7.8F);
            extensionLabel.Location = new Point(282, 211);
            extensionLabel.Name = "extensionLabel";
            extensionLabel.Size = new Size(35, 16);
            extensionLabel.TabIndex = 4;
            extensionLabel.Text = ".json";
            // 
            // fileNameText
            // 
            fileNameText.Font = new Font("Microsoft Sans Serif", 7.8F);
            fileNameText.Location = new Point(98, 206);
            fileNameText.Margin = new Padding(3, 4, 3, 4);
            fileNameText.Name = "fileNameText";
            fileNameText.Size = new Size(187, 22);
            fileNameText.TabIndex = 3;
            // 
            // deleteButton
            // 
            deleteButton.Font = new Font("Microsoft Sans Serif", 7.8F);
            deleteButton.Location = new Point(223, 170);
            deleteButton.Margin = new Padding(3, 4, 3, 4);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(94, 29);
            deleteButton.TabIndex = 2;
            deleteButton.Text = "Usuń";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += DeleteButton_Click;
            // 
            // loadButton
            // 
            loadButton.Font = new Font("Microsoft Sans Serif", 7.8F);
            loadButton.Location = new Point(2, 170);
            loadButton.Margin = new Padding(3, 4, 3, 4);
            loadButton.Name = "loadButton";
            loadButton.Size = new Size(94, 29);
            loadButton.TabIndex = 1;
            loadButton.Text = "Wczytaj";
            loadButton.UseVisualStyleBackColor = true;
            loadButton.Click += LoadButton_Click;
            // 
            // saveButton
            // 
            saveButton.Font = new Font("Microsoft Sans Serif", 7.8F);
            saveButton.Location = new Point(2, 203);
            saveButton.Margin = new Padding(3, 4, 3, 4);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(94, 29);
            saveButton.TabIndex = 0;
            saveButton.Text = "Zapisz";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += SaveButton_Click;
            // 
            // ClickerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(332, 265);
            Controls.Add(tabControl);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MaximumSize = new Size(350, 312);
            MinimumSize = new Size(350, 312);
            Name = "ClickerForm";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "Clicker";
            tabControl.ResumeLayout(false);
            mainPage.ResumeLayout(false);
            infoBox.ResumeLayout(false);
            infoBox.PerformLayout();
            settingsPage.ResumeLayout(false);
            settingsPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)afterSequencePeriodStopNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)afterSequencePeriodStartNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)afterActionPeriodNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)numberOfRepeatNum).EndInit();
            sequencePage.ResumeLayout(false);
            sequencePage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)newSubsequenceIterationsNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)newPointYNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)newPointXNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)newAfterActionPeriodNum).EndInit();
            tagsPage.ResumeLayout(false);
            profilesPage.ResumeLayout(false);
            profilesPage.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl;
        private TabPage mainPage;
        private TabPage settingsPage;
        private TabPage sequencePage;
        private TabPage tagsPage;
        private TabPage profilesPage;
        private Button startButton;
        private Button clearButton;
        private Button stopButton;
        private Button recordButton;
        private Button stopRecordButton;
        private GroupBox infoBox;
        private Label actionLabel;
        private Label sequenceCounterLabel;
        private Label subsequenceCounterLabel;
        private Button deleteButton;
        private Button loadButton;
        private Button saveButton;
        private TextBox fileNameText;
        private Label extensionLabel;
        private ListBox profilesListBox;
        private CheckedListBox tagsListBox;
        private NumericUpDown numberOfRepeatNum;
        private CheckBox randomIntervalCheckbox;
        private CheckBox repeatSequenceCheckbox;
        private Label afterActionPeriodLabel;
        private TextBox iterationsHelperText;
        private NumericUpDown afterActionPeriodNum;
        private Label msLabel1;
        private Label numberOfRepeatLabel;
        private Label msLabel3;
        private NumericUpDown afterSequencePeriodStopNum;
        private Label msLabel2;
        private NumericUpDown afterSequencePeriodStartNum;
        private ListBox sequenceListBox;
        private TextBox newDescriptionText;
        private ComboBox newActionsComboBox;
        private Label newDescriptionLabel;
        private Label newTagLabel;
        private TextBox newTagText;
        private Label newAfterActionPeriodLabel;
        private NumericUpDown newAfterActionPeriodNum;
        private Button editButton;
        private Label newPointXLabel;
        private NumericUpDown newPointXNum;
        private ComboBox newMouseButtonsComboBox;
        private Label newPointYLabel;
        private NumericUpDown newPointYNum;
        private TextBox newKeyboardText;
        private Label newSubsequenceIterationsLabel;
        private NumericUpDown newSubsequenceIterationsNum;
        private TextBox newSubsequenceFilenameText;
    }
}
