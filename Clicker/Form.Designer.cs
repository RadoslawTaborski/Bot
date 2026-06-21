namespace Clicker
{
    partial class Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.startButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.recordButton = new System.Windows.Forms.Button();
            this.stopRecordButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.afterActionPeriodNum = new System.Windows.Forms.NumericUpDown();
            this.afterSequencePeriodStartNum = new System.Windows.Forms.NumericUpDown();
            this.afterSequencePeriodStopNum = new System.Windows.Forms.NumericUpDown();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.mainPage = new System.Windows.Forms.TabPage();
            this.actrionLabel = new System.Windows.Forms.Label();
            this.subsequenceCounterLabel = new System.Windows.Forms.Label();
            this.sequenceCounterLabel = new System.Windows.Forms.Label();
            this.settingsPage = new System.Windows.Forms.TabPage();
            this.randomIntervalCheckbox = new System.Windows.Forms.CheckBox();
            this.numberOfRepeatNum = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.repeatSequenceCheckbox = new System.Windows.Forms.CheckBox();
            this.sequencePage = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.newDescription = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.newTagText = new System.Windows.Forms.TextBox();
            this.newSubsequenceIterationsNum = new System.Windows.Forms.NumericUpDown();
            this.newSubsequenceFilenameText = new System.Windows.Forms.TextBox();
            this.newKeyboardText = new System.Windows.Forms.TextBox();
            this.newMouseButtonsComboBox = new System.Windows.Forms.ComboBox();
            this.newPointYNum = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.newPointXNum = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.newActionsComboBox = new System.Windows.Forms.ComboBox();
            this.newAfterActionPeriodNum = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.editButton = new System.Windows.Forms.Button();
            this.sequenceListBox = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tags = new System.Windows.Forms.TabPage();
            this.tagsListBox = new System.Windows.Forms.CheckedListBox();
            this.profilesPage = new System.Windows.Forms.TabPage();
            this.deleteButton = new System.Windows.Forms.Button();
            this.profilesListBox = new System.Windows.Forms.ListBox();
            this.loadButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.fileNameText = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.afterActionPeriodNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.afterSequencePeriodStartNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.afterSequencePeriodStopNum)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.mainPage.SuspendLayout();
            this.settingsPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfRepeatNum)).BeginInit();
            this.sequencePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newSubsequenceIterationsNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.newPointYNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.newPointXNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.newAfterActionPeriodNum)).BeginInit();
            this.tags.SuspendLayout();
            this.profilesPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(2, 83);
            this.startButton.Margin = new System.Windows.Forms.Padding(4);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(172, 79);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(182, 83);
            this.stopButton.Margin = new System.Windows.Forms.Padding(4);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(172, 79);
            this.stopButton.TabIndex = 1;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(151, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 16);
            this.label1.TabIndex = 2;
            // 
            // recordButton
            // 
            this.recordButton.Location = new System.Drawing.Point(2, 43);
            this.recordButton.Margin = new System.Windows.Forms.Padding(4);
            this.recordButton.Name = "recordButton";
            this.recordButton.Size = new System.Drawing.Size(172, 36);
            this.recordButton.TabIndex = 3;
            this.recordButton.Text = "Nagraj";
            this.recordButton.UseVisualStyleBackColor = true;
            this.recordButton.Click += new System.EventHandler(this.RecordButton_Click);
            // 
            // stopRecordButton
            // 
            this.stopRecordButton.Location = new System.Drawing.Point(181, 43);
            this.stopRecordButton.Margin = new System.Windows.Forms.Padding(4);
            this.stopRecordButton.Name = "stopRecordButton";
            this.stopRecordButton.Size = new System.Drawing.Size(172, 36);
            this.stopRecordButton.TabIndex = 5;
            this.stopRecordButton.Text = "Zatrzymaj nagrywanie";
            this.stopRecordButton.UseVisualStyleBackColor = true;
            this.stopRecordButton.Click += new System.EventHandler(this.StopRecordButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(3, 165);
            this.clearButton.Margin = new System.Windows.Forms.Padding(4);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(351, 29);
            this.clearButton.TabIndex = 6;
            this.clearButton.Text = "Wyczyść pamięć";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 14);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(212, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "odstęp czasu między kliknięciami: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(221, 126);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "-";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(329, 18);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "ms";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(196, 128);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 16);
            this.label6.TabIndex = 14;
            this.label6.Text = "ms";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(329, 131);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 16);
            this.label7.TabIndex = 15;
            this.label7.Text = "ms";
            // 
            // afterActionPeriodNum
            // 
            this.afterActionPeriodNum.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.afterActionPeriodNum.Location = new System.Drawing.Point(237, 10);
            this.afterActionPeriodNum.Margin = new System.Windows.Forms.Padding(4);
            this.afterActionPeriodNum.Name = "afterActionPeriodNum";
            this.afterActionPeriodNum.Size = new System.Drawing.Size(92, 22);
            this.afterActionPeriodNum.TabIndex = 16;
            // 
            // afterSequencePeriodStartNum
            // 
            this.afterSequencePeriodStartNum.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.afterSequencePeriodStartNum.Location = new System.Drawing.Point(101, 122);
            this.afterSequencePeriodStartNum.Margin = new System.Windows.Forms.Padding(4);
            this.afterSequencePeriodStartNum.Name = "afterSequencePeriodStartNum";
            this.afterSequencePeriodStartNum.Size = new System.Drawing.Size(92, 22);
            this.afterSequencePeriodStartNum.TabIndex = 17;
            this.afterSequencePeriodStartNum.ValueChanged += new System.EventHandler(this.AfterSequencePeriodStartNum_ValueChanged);
            // 
            // afterSequencePeriodStopNum
            // 
            this.afterSequencePeriodStopNum.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.afterSequencePeriodStopNum.Location = new System.Drawing.Point(237, 122);
            this.afterSequencePeriodStopNum.Margin = new System.Windows.Forms.Padding(4);
            this.afterSequencePeriodStopNum.Name = "afterSequencePeriodStopNum";
            this.afterSequencePeriodStopNum.Size = new System.Drawing.Size(92, 22);
            this.afterSequencePeriodStopNum.TabIndex = 18;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.mainPage);
            this.tabControl1.Controls.Add(this.settingsPage);
            this.tabControl1.Controls.Add(this.sequencePage);
            this.tabControl1.Controls.Add(this.tags);
            this.tabControl1.Controls.Add(this.profilesPage);
            this.tabControl1.Location = new System.Drawing.Point(11, 9);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(371, 225);
            this.tabControl1.TabIndex = 19;
            // 
            // mainPage
            // 
            this.mainPage.Controls.Add(this.actrionLabel);
            this.mainPage.Controls.Add(this.subsequenceCounterLabel);
            this.mainPage.Controls.Add(this.sequenceCounterLabel);
            this.mainPage.Controls.Add(this.stopButton);
            this.mainPage.Controls.Add(this.startButton);
            this.mainPage.Controls.Add(this.label1);
            this.mainPage.Controls.Add(this.recordButton);
            this.mainPage.Controls.Add(this.stopRecordButton);
            this.mainPage.Controls.Add(this.clearButton);
            this.mainPage.Controls.Add(this.groupBox1);
            this.mainPage.Location = new System.Drawing.Point(4, 25);
            this.mainPage.Margin = new System.Windows.Forms.Padding(4);
            this.mainPage.Name = "mainPage";
            this.mainPage.Padding = new System.Windows.Forms.Padding(4);
            this.mainPage.Size = new System.Drawing.Size(363, 196);
            this.mainPage.TabIndex = 0;
            this.mainPage.Text = "Główne";
            this.mainPage.UseVisualStyleBackColor = true;
            // 
            // actrionLabel
            // 
            this.actrionLabel.AutoSize = true;
            this.actrionLabel.Location = new System.Drawing.Point(5, 23);
            this.actrionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.actrionLabel.Name = "actrionLabel";
            this.actrionLabel.Size = new System.Drawing.Size(66, 16);
            this.actrionLabel.TabIndex = 12;
            this.actrionLabel.Text = "Akcja: xyz";
            // 
            // subsequenceCounterLabel
            // 
            this.subsequenceCounterLabel.AutoSize = true;
            this.subsequenceCounterLabel.Location = new System.Drawing.Point(183, 3);
            this.subsequenceCounterLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.subsequenceCounterLabel.Name = "subsequenceCounterLabel";
            this.subsequenceCounterLabel.Size = new System.Drawing.Size(114, 16);
            this.subsequenceCounterLabel.TabIndex = 11;
            this.subsequenceCounterLabel.Text = "Wewnętrzna: a z b";
            // 
            // sequenceCounterLabel
            // 
            this.sequenceCounterLabel.AutoSize = true;
            this.sequenceCounterLabel.Location = new System.Drawing.Point(4, 4);
            this.sequenceCounterLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.sequenceCounterLabel.Name = "sequenceCounterLabel";
            this.sequenceCounterLabel.Size = new System.Drawing.Size(85, 16);
            this.sequenceCounterLabel.TabIndex = 10;
            this.sequenceCounterLabel.Text = "Iteracja: a z b";
            // 
            // settingsPage
            // 
            this.settingsPage.Controls.Add(this.randomIntervalCheckbox);
            this.settingsPage.Controls.Add(this.numberOfRepeatNum);
            this.settingsPage.Controls.Add(this.label11);
            this.settingsPage.Controls.Add(this.repeatSequenceCheckbox);
            this.settingsPage.Controls.Add(this.afterSequencePeriodStopNum);
            this.settingsPage.Controls.Add(this.label2);
            this.settingsPage.Controls.Add(this.afterSequencePeriodStartNum);
            this.settingsPage.Controls.Add(this.afterActionPeriodNum);
            this.settingsPage.Controls.Add(this.label4);
            this.settingsPage.Controls.Add(this.label7);
            this.settingsPage.Controls.Add(this.label5);
            this.settingsPage.Controls.Add(this.label6);
            this.settingsPage.Location = new System.Drawing.Point(4, 25);
            this.settingsPage.Margin = new System.Windows.Forms.Padding(4);
            this.settingsPage.Name = "settingsPage";
            this.settingsPage.Padding = new System.Windows.Forms.Padding(4);
            this.settingsPage.Size = new System.Drawing.Size(363, 196);
            this.settingsPage.TabIndex = 1;
            this.settingsPage.Text = "Ustawienia";
            this.settingsPage.UseVisualStyleBackColor = true;
            // 
            // randomIntervalCheckbox
            // 
            this.randomIntervalCheckbox.AutoSize = true;
            this.randomIntervalCheckbox.Location = new System.Drawing.Point(12, 92);
            this.randomIntervalCheckbox.Margin = new System.Windows.Forms.Padding(4);
            this.randomIntervalCheckbox.Name = "randomIntervalCheckbox";
            this.randomIntervalCheckbox.Size = new System.Drawing.Size(296, 20);
            this.randomIntervalCheckbox.TabIndex = 26;
            this.randomIntervalCheckbox.Text = "Ruchomy odstęp czasu między sekwencjami";
            this.randomIntervalCheckbox.UseVisualStyleBackColor = true;
            this.randomIntervalCheckbox.CheckedChanged += new System.EventHandler(this.RandomIntervalCheckbox_CheckedChanged);
            // 
            // numberOfRepeatNum
            // 
            this.numberOfRepeatNum.Location = new System.Drawing.Point(13, 122);
            this.numberOfRepeatNum.Margin = new System.Windows.Forms.Padding(4);
            this.numberOfRepeatNum.Name = "numberOfRepeatNum";
            this.numberOfRepeatNum.Size = new System.Drawing.Size(67, 22);
            this.numberOfRepeatNum.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(85, 123);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(13, 16);
            this.label11.TabIndex = 24;
            this.label11.Text = "x";
            // 
            // repeatSequenceCheckbox
            // 
            this.repeatSequenceCheckbox.AutoSize = true;
            this.repeatSequenceCheckbox.Location = new System.Drawing.Point(12, 61);
            this.repeatSequenceCheckbox.Margin = new System.Windows.Forms.Padding(4);
            this.repeatSequenceCheckbox.Name = "repeatSequenceCheckbox";
            this.repeatSequenceCheckbox.Size = new System.Drawing.Size(154, 20);
            this.repeatSequenceCheckbox.TabIndex = 19;
            this.repeatSequenceCheckbox.Text = "Powtarzaj sekwencję";
            this.repeatSequenceCheckbox.UseVisualStyleBackColor = true;
            this.repeatSequenceCheckbox.CheckedChanged += new System.EventHandler(this.RepeatSequenceCheckbox_CheckedChanged);
            // 
            // sequencePage
            // 
            this.sequencePage.Controls.Add(this.label14);
            this.sequencePage.Controls.Add(this.newDescription);
            this.sequencePage.Controls.Add(this.label13);
            this.sequencePage.Controls.Add(this.newTagText);
            this.sequencePage.Controls.Add(this.newSubsequenceIterationsNum);
            this.sequencePage.Controls.Add(this.newSubsequenceFilenameText);
            this.sequencePage.Controls.Add(this.newKeyboardText);
            this.sequencePage.Controls.Add(this.newMouseButtonsComboBox);
            this.sequencePage.Controls.Add(this.newPointYNum);
            this.sequencePage.Controls.Add(this.label12);
            this.sequencePage.Controls.Add(this.newPointXNum);
            this.sequencePage.Controls.Add(this.label10);
            this.sequencePage.Controls.Add(this.newActionsComboBox);
            this.sequencePage.Controls.Add(this.newAfterActionPeriodNum);
            this.sequencePage.Controls.Add(this.label8);
            this.sequencePage.Controls.Add(this.editButton);
            this.sequencePage.Controls.Add(this.sequenceListBox);
            this.sequencePage.Controls.Add(this.label3);
            this.sequencePage.Location = new System.Drawing.Point(4, 25);
            this.sequencePage.Margin = new System.Windows.Forms.Padding(4);
            this.sequencePage.Name = "sequencePage";
            this.sequencePage.Padding = new System.Windows.Forms.Padding(4);
            this.sequencePage.Size = new System.Drawing.Size(363, 196);
            this.sequencePage.TabIndex = 2;
            this.sequencePage.Text = "Sekwencja";
            this.sequencePage.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 139);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(20, 16);
            this.label14.TabIndex = 31;
            this.label14.Text = "D:";
            // 
            // newDescription
            // 
            this.newDescription.Location = new System.Drawing.Point(30, 136);
            this.newDescription.Margin = new System.Windows.Forms.Padding(4);
            this.newDescription.Name = "newDescription";
            this.newDescription.Size = new System.Drawing.Size(152, 22);
            this.newDescription.TabIndex = 30;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(190, 139);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(19, 16);
            this.label13.TabIndex = 29;
            this.label13.Text = "T:";
            // 
            // newTagText
            // 
            this.newTagText.Location = new System.Drawing.Point(212, 136);
            this.newTagText.Margin = new System.Windows.Forms.Padding(4);
            this.newTagText.Name = "newTagText";
            this.newTagText.Size = new System.Drawing.Size(141, 22);
            this.newTagText.TabIndex = 28;
            // 
            // newSubsequenceIterationsNum
            // 
            this.newSubsequenceIterationsNum.Location = new System.Drawing.Point(4, 163);
            this.newSubsequenceIterationsNum.Margin = new System.Windows.Forms.Padding(4);
            this.newSubsequenceIterationsNum.Name = "newSubsequenceIterationsNum";
            this.newSubsequenceIterationsNum.Size = new System.Drawing.Size(67, 22);
            this.newSubsequenceIterationsNum.TabIndex = 27;
            // 
            // newSubsequenceFilenameText
            // 
            this.newSubsequenceFilenameText.Location = new System.Drawing.Point(97, 163);
            this.newSubsequenceFilenameText.Margin = new System.Windows.Forms.Padding(4);
            this.newSubsequenceFilenameText.Name = "newSubsequenceFilenameText";
            this.newSubsequenceFilenameText.Size = new System.Drawing.Size(257, 22);
            this.newSubsequenceFilenameText.TabIndex = 25;
            // 
            // newKeyboardText
            // 
            this.newKeyboardText.Location = new System.Drawing.Point(3, 163);
            this.newKeyboardText.Margin = new System.Windows.Forms.Padding(4);
            this.newKeyboardText.Name = "newKeyboardText";
            this.newKeyboardText.Size = new System.Drawing.Size(351, 22);
            this.newKeyboardText.TabIndex = 23;
            // 
            // newMouseButtonsComboBox
            // 
            this.newMouseButtonsComboBox.FormattingEnabled = true;
            this.newMouseButtonsComboBox.Location = new System.Drawing.Point(3, 162);
            this.newMouseButtonsComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.newMouseButtonsComboBox.Name = "newMouseButtonsComboBox";
            this.newMouseButtonsComboBox.Size = new System.Drawing.Size(133, 24);
            this.newMouseButtonsComboBox.TabIndex = 24;
            // 
            // newPointYNum
            // 
            this.newPointYNum.Location = new System.Drawing.Point(268, 163);
            this.newPointYNum.Margin = new System.Windows.Forms.Padding(4);
            this.newPointYNum.Name = "newPointYNum";
            this.newPointYNum.Size = new System.Drawing.Size(84, 22);
            this.newPointYNum.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(248, 166);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(19, 16);
            this.label12.TabIndex = 8;
            this.label12.Text = "Y:";
            // 
            // newPointXNum
            // 
            this.newPointXNum.Location = new System.Drawing.Point(159, 163);
            this.newPointXNum.Margin = new System.Windows.Forms.Padding(4);
            this.newPointXNum.Name = "newPointXNum";
            this.newPointXNum.Size = new System.Drawing.Size(84, 22);
            this.newPointXNum.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(140, 166);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(18, 16);
            this.label10.TabIndex = 6;
            this.label10.Text = "X:";
            // 
            // newActionsComboBox
            // 
            this.newActionsComboBox.FormattingEnabled = true;
            this.newActionsComboBox.Location = new System.Drawing.Point(3, 108);
            this.newActionsComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.newActionsComboBox.Name = "newActionsComboBox";
            this.newActionsComboBox.Size = new System.Drawing.Size(133, 24);
            this.newActionsComboBox.TabIndex = 5;
            this.newActionsComboBox.SelectedIndexChanged += new System.EventHandler(this.ActionsComboBox_SelectedIndexChanged);
            // 
            // newAfterActionPeriodNum
            // 
            this.newAfterActionPeriodNum.Location = new System.Drawing.Point(159, 109);
            this.newAfterActionPeriodNum.Margin = new System.Windows.Forms.Padding(4);
            this.newAfterActionPeriodNum.Name = "newAfterActionPeriodNum";
            this.newAfterActionPeriodNum.Size = new System.Drawing.Size(84, 22);
            this.newAfterActionPeriodNum.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(140, 112);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 16);
            this.label8.TabIndex = 2;
            this.label8.Text = "P:";
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(247, 106);
            this.editButton.Margin = new System.Windows.Forms.Padding(4);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(108, 28);
            this.editButton.TabIndex = 1;
            this.editButton.Text = "Edytuj";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // sequenceListBox
            // 
            this.sequenceListBox.FormattingEnabled = true;
            this.sequenceListBox.ItemHeight = 16;
            this.sequenceListBox.Location = new System.Drawing.Point(3, 4);
            this.sequenceListBox.Margin = new System.Windows.Forms.Padding(4);
            this.sequenceListBox.Name = "sequenceListBox";
            this.sequenceListBox.Size = new System.Drawing.Size(351, 100);
            this.sequenceListBox.TabIndex = 0;
            this.sequenceListBox.SelectedIndexChanged += new System.EventHandler(this.SequenceListBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(76, 166);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 16);
            this.label3.TabIndex = 26;
            this.label3.Text = "x";
            // 
            // tags
            // 
            this.tags.Controls.Add(this.tagsListBox);
            this.tags.Location = new System.Drawing.Point(4, 25);
            this.tags.Name = "tags";
            this.tags.Padding = new System.Windows.Forms.Padding(3);
            this.tags.Size = new System.Drawing.Size(363, 196);
            this.tags.TabIndex = 4;
            this.tags.Text = "Tagi";
            this.tags.UseVisualStyleBackColor = true;
            // 
            // tagsListBox
            // 
            this.tagsListBox.FormattingEnabled = true;
            this.tagsListBox.Location = new System.Drawing.Point(3, 3);
            this.tagsListBox.Name = "tagsListBox";
            this.tagsListBox.Size = new System.Drawing.Size(354, 191);
            this.tagsListBox.TabIndex = 0;
            // 
            // profilesPage
            // 
            this.profilesPage.Controls.Add(this.deleteButton);
            this.profilesPage.Controls.Add(this.label9);
            this.profilesPage.Controls.Add(this.saveButton);
            this.profilesPage.Controls.Add(this.fileNameText);
            this.profilesPage.Controls.Add(this.profilesListBox);
            this.profilesPage.Controls.Add(this.loadButton);
            this.profilesPage.Location = new System.Drawing.Point(4, 25);
            this.profilesPage.Margin = new System.Windows.Forms.Padding(4);
            this.profilesPage.Name = "profilesPage";
            this.profilesPage.Padding = new System.Windows.Forms.Padding(4);
            this.profilesPage.Size = new System.Drawing.Size(363, 196);
            this.profilesPage.TabIndex = 3;
            this.profilesPage.Text = "Profile";
            this.profilesPage.UseVisualStyleBackColor = true;
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(256, 136);
            this.deleteButton.Margin = new System.Windows.Forms.Padding(4);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(100, 28);
            this.deleteButton.TabIndex = 22;
            this.deleteButton.Text = "Usuń";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // profilesListBox
            // 
            this.profilesListBox.FormattingEnabled = true;
            this.profilesListBox.ItemHeight = 16;
            this.profilesListBox.Location = new System.Drawing.Point(3, 2);
            this.profilesListBox.Margin = new System.Windows.Forms.Padding(4);
            this.profilesListBox.Name = "profilesListBox";
            this.profilesListBox.Size = new System.Drawing.Size(353, 132);
            this.profilesListBox.TabIndex = 0;
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(3, 136);
            this.loadButton.Margin = new System.Windows.Forms.Padding(4);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(100, 28);
            this.loadButton.TabIndex = 21;
            this.loadButton.Text = "Wczytaj";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(2, -6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(351, 48);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(3, 166);
            this.saveButton.Margin = new System.Windows.Forms.Padding(4);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(100, 28);
            this.saveButton.TabIndex = 20;
            this.saveButton.Text = "Zapisz";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // fileNameText
            // 
            this.fileNameText.Location = new System.Drawing.Point(111, 168);
            this.fileNameText.Margin = new System.Windows.Forms.Padding(4);
            this.fileNameText.Name = "fileNameText";
            this.fileNameText.Size = new System.Drawing.Size(209, 22);
            this.fileNameText.TabIndex = 22;
            this.fileNameText.Text = "nazwa pliku";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(320, 174);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 16);
            this.label9.TabIndex = 23;
            this.label9.Text = ".json";
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 238);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.afterActionPeriodNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.afterSequencePeriodStartNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.afterSequencePeriodStopNum)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.mainPage.ResumeLayout(false);
            this.mainPage.PerformLayout();
            this.settingsPage.ResumeLayout(false);
            this.settingsPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfRepeatNum)).EndInit();
            this.sequencePage.ResumeLayout(false);
            this.sequencePage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.newSubsequenceIterationsNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.newPointYNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.newPointXNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.newAfterActionPeriodNum)).EndInit();
            this.tags.ResumeLayout(false);
            this.profilesPage.ResumeLayout(false);
            this.profilesPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button recordButton;
        private System.Windows.Forms.Button stopRecordButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown afterActionPeriodNum;
        private System.Windows.Forms.NumericUpDown afterSequencePeriodStartNum;
        private System.Windows.Forms.NumericUpDown afterSequencePeriodStopNum;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage mainPage;
        private System.Windows.Forms.TabPage settingsPage;
        private System.Windows.Forms.CheckBox repeatSequenceCheckbox;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.TabPage sequencePage;
        private System.Windows.Forms.ListBox sequenceListBox;
        private System.Windows.Forms.NumericUpDown newAfterActionPeriodNum;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.TabPage profilesPage;
        private System.Windows.Forms.ListBox profilesListBox;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.ComboBox newActionsComboBox;
        private System.Windows.Forms.NumericUpDown numberOfRepeatNum;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown newPointYNum;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown newPointXNum;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox newKeyboardText;
        private System.Windows.Forms.Label sequenceCounterLabel;
        private System.Windows.Forms.CheckBox randomIntervalCheckbox;
        private System.Windows.Forms.ComboBox newMouseButtonsComboBox;
        private System.Windows.Forms.TextBox newSubsequenceFilenameText;
        private System.Windows.Forms.NumericUpDown newSubsequenceIterationsNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tags;
        private System.Windows.Forms.TextBox newTagText;
        private System.Windows.Forms.CheckedListBox tagsListBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox newDescription;
        private System.Windows.Forms.Label subsequenceCounterLabel;
        private System.Windows.Forms.Label actrionLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox fileNameText;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button saveButton;
    }
}

