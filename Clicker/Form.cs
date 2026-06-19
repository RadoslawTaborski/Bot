using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            listBox1.Items.Clear();
            listBox1.DataSource = settings.Moves;
            listBox1.HorizontalScrollbar = true;
            this.tabPage1.Text = "Sterowanie";
            this.tabPage2.Text = "Ustawienia";
            this.tabPage3.Text = "Sekwencja";
            this.tabPage4.Text = "Profile";
            this.Text = "Clicker";
            btnRecord.Enabled = true;
            btnStopRecord.Enabled = false;
            btnStart.Enabled = false;
            btnStop.Enabled = false;
            btnClear.Enabled = false;
            numPeriod1.Enabled = true;
            numPeriodA.Enabled = false;
            numPeriodB.Enabled = false;
            numOfRepeats.Enabled = false;
            cbRepeat.Enabled = true;
            cbRepeat.Checked = true;
            numPeriod1.Minimum = 100;
            numPeriod1.Maximum = 10000000;
            numPeriod1.Value = 2000;
            numPeriod1.Increment = 1000;
            numPeriodA.Minimum = 100;
            numPeriodA.Maximum = 10000000;
            numPeriodA.Increment = 1000;
            numPeriodB.Minimum = 100;
            numPeriodB.Maximum = 10000000;
            numPeriodB.Increment = 1000;
            numPeriodB.Value = 2000;
            numPeriodA.Value = 2000;
            numNewPeriod1.Minimum = 100;
            numNewPeriod1.Maximum = 10000000;
            numNewPeriod1.Value = 100;
            numNewPeriod1.Increment = 1000;
            numNewX.Minimum = -10000000;
            numNewX.Maximum = 10000000;
            numNewX.Value = 0;
            numNewY.Minimum = -10000000;
            numNewY.Maximum = 10000000;
            numNewY.Value = 0;
            textNew.Text = "";
            textNew.Visible = false;
            numOfRepeats.Minimum = 2;
            numOfRepeats.Maximum = 100000;
            numOfRepeats.Value = 1000;
            btnEdit.Enabled = false;
            label13.Text = "";

            string path = Directory.GetCurrentDirectory();
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (var fi in di.GetFiles("*.json"))
            {
                files.Add(fi.Name);
            }
            listBox2.DataSource = files;
            listBox2.HorizontalScrollbar = true;

            if (listBox2.Items.Count!=0)
            {
                btnLoad.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                btnLoad.Enabled = false;
                btnDelete.Enabled = false;
            }

            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.Items.Add(MouseActions.Left);
            comboBox2.Items.Add(MouseActions.Right);
            comboBox2.Items.Add(MouseActions.Middle);
            comboBox2.Items.Add(MouseActions.Left_Down);
            comboBox2.Items.Add(MouseActions.Left_Up);
            comboBox2.SelectedIndex = 0;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Items.Add(Actions.Mouse);
            comboBox1.Items.Add(Actions.Keyboard);
            comboBox1.SelectedIndex = 0;
        }

        public void DoAction(object sender, ElapsedEventArgs e)
        {
            label13.Invoke((MethodInvoker)(() =>
            {
                label13.Text = $"Iteracja: {repeatCounter + 1} z {(cbRepeat.Checked ? numOfRepeats.Value : 1)}";
            }));
            var numberOfActions = actionExecutor.Execute(settings.Moves[iteration], settings.Moves.Cast<Action>().ElementAtOrDefault(iteration + 1));
            iteration += numberOfActions;
            timer.Interval = settings.Moves[iteration].Period;
            
            if (iteration == settings.Moves.Count-2)
            {
                repeatCounter++;
                if (cbRepeat.Checked == true && repeatCounter < numOfRepeats.Value)
                {
                    var time = random.Next((int)numPeriodA.Value, (int)numPeriodB.Value);
                    timer.Interval = time;
                    iteration = 1;
                }
                else
                {
                    Invoke(new System.Action(delegate () 
                    {
                        btnStop_Click(null, null);
                    }));
                }
            }
        }

        private void RunTimer(int a)
        {
            timer.Elapsed += new ElapsedEventHandler(DoAction);
            timer.Interval = a;
            timer.Enabled = true;
            timer.Start();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (numPeriodA.Value > numPeriodB.Value)
            {
                MessageBox.Show("Złe wartości dla odstępu między sekwencjami");
            }
            else
            {
                RunTimer(2000);
                btnRecord.Enabled = false;
                btnStopRecord.Enabled = false;
                btnStart.Enabled = false;
                btnStop.Enabled = true;
                btnClear.Enabled = false;
                numPeriod1.Enabled = false;
                numPeriodA.Enabled = false;
                numPeriodB.Enabled = false;
                numOfRepeats.Enabled = false;
                cbRepeat.Enabled = false;
                btnLoad.Enabled = false;
                btnDelete.Enabled = false;
                btnSave.Enabled = false;
                tbName.Enabled = false;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer.Stop();
            timer.Elapsed -= new ElapsedEventHandler(DoAction);
            iteration = 1;
            repeatCounter = 0;
            label13.Text = "";
            btnRecord.Enabled = false;
            btnStopRecord.Enabled = false;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            btnClear.Enabled = true;
            numPeriod1.Enabled = true;
            btnLoad.Enabled = true;
            btnDelete.Enabled = true;
            btnSave.Enabled = true;
            tbName.Enabled = true;
            cbRepeat.Enabled = true;
            if (cbRepeat.Checked == true)
            {
                numPeriodA.Enabled = true;
                if (cbRandomInterval.Checked)
                {
                    numPeriodB.Enabled = true;
                }
                numOfRepeats.Enabled = true;
            }
            else
            {
                numPeriodA.Enabled = false;
                numPeriodB.Enabled = false;
                numOfRepeats.Enabled = false;
            }
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            Activation();
            btnRecord.Enabled = false;
            btnStopRecord.Enabled = true;
            btnStart.Enabled = false;
            btnStop.Enabled = false;
            btnClear.Enabled = false;
            numPeriod1.Enabled = false;
            cbRepeat.Enabled = false;
            numPeriodA.Enabled = false;
            numPeriodB.Enabled = false;
            numOfRepeats.Enabled = false;
            btnLoad.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
            tbName.Enabled = false;
            label13.Text = "";
        }

        private void btnStopRecord_Click(object sender, EventArgs e)
        {
            Deactivation();

            btnRecord.Enabled = false;
            btnStopRecord.Enabled = false;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            btnClear.Enabled = true;
            numPeriod1.Enabled = true;
            cbRepeat.Enabled = true;
            btnLoad.Enabled = true;
            btnDelete.Enabled = true;
            btnSave.Enabled = true;
            tbName.Enabled = true;
            listBox1.DataSource = settings.Moves;
            if (cbRepeat.Checked == true)
            {
                numPeriodA.Enabled = true;
                if (cbRandomInterval.Checked)
                {
                    numPeriodB.Enabled = true;
                }
                numOfRepeats.Enabled = true;
            }
            else
            {
                numPeriodA.Enabled = false;
                numPeriodB.Enabled = false;
                numOfRepeats.Enabled = false;
            }
            comboBox1.SelectedIndex = 0;
            numNewPeriod1.Value = 100;
            numNewX.Value = 0;
            numNewY.Value = 0;
            textNew.Text = "";
            comboBox2.SelectedIndex = 0;
            numNewX.Visible = true;
            numNewY.Visible = true;
            textNew.Visible = false;
            comboBox2.Visible = true;
            btnEdit.Enabled = settings.Moves.Count != 0;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            settings.Moves.Clear();
            btnRecord.Enabled = true;
            btnStopRecord.Enabled = false;
            btnStart.Enabled = false;
            btnStop.Enabled = false;
            btnClear.Enabled = false;
            numPeriod1.Enabled = true;
            cbRepeat.Enabled = true;
            btnLoad.Enabled = true;
            btnDelete.Enabled = true;
            btnSave.Enabled = true;
            tbName.Enabled = true;
            btnEdit.Enabled = false;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            numNewPeriod1.Value = 100;
            numNewX.Value = 0;
            numNewY.Value = 0;
            textNew.Text = "";
            label13.Text = "";
            numNewX.Visible = true;
            numNewY.Visible = true;
            textNew.Visible = false;
            comboBox2.Visible = true;
            iteration = 1;
            repeatCounter = 0;
            if (cbRepeat.Checked == true)
            {
                numPeriodA.Enabled = true;
                if (cbRandomInterval.Checked)
                {
                    numPeriodB.Enabled = true;
                }
                numOfRepeats.Enabled = true;
            }
            else
            {
                numPeriodA.Enabled = false;
                numPeriodB.Enabled = false;
                numOfRepeats.Enabled = false;
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
            settings.Moves.Add(new MouseAction {
                Id=settings.Moves.Count+1,
                Point = new Point(Cursor.Position.X, Cursor.Position.Y),
                Period = (int)numPeriod1.Value, 
                Button=e.Button == MouseButtons.Middle 
                ? MouseActions.Middle 
                : e.Button == MouseButtons.Right 
                    ? MouseActions.Right 
                    : MouseActions.Left});
        }

        private void cbRepeat_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRepeat.Checked)
            {
                numPeriodA.Enabled = true;
                if (cbRandomInterval.Checked)
                {
                    numPeriodB.Enabled = true;
                }
                numOfRepeats.Enabled = true;
            }
            else
            {
                numPeriodA.Enabled = false;
                numPeriodB.Enabled = false;
                numOfRepeats.Enabled = false;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

            var str = listBox2.SelectedItem.ToString();
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

            numOfRepeats.Value = settings.NumberOfRepeats;
            cbRepeat.Checked = settings.Repeat;
            cbRepeat.CheckedChanged += new EventHandler(cbRepeat_CheckedChanged);
            cbRandomInterval.Checked = settings.RandomTimeInterval;
            numPeriod1.Value = settings.Period1;
            numPeriodA.Value = settings.PeriodA;
            if (settings.RandomTimeInterval)
            {
                numPeriodB.Value = settings.PeriodB;
            } 
            else
            {
                numPeriodB.Value = settings.PeriodA;
            }             

            listBox1.DataSource = settings.Moves;

            btnRecord.Enabled = false;
            btnStopRecord.Enabled = false;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            btnClear.Enabled = true;
            numPeriod1.Enabled = true;
            cbRepeat.Enabled = true;
            btnLoad.Enabled = true;
            btnDelete.Enabled = true;
            btnSave.Enabled = true;
            tbName.Enabled = true;
            tbName.Text = Path.GetFileNameWithoutExtension(str);
            if (cbRepeat.Checked == true)
            {
                numPeriodA.Enabled = true;
                if (cbRandomInterval.Checked)
                {
                    numPeriodB.Enabled = true;
                }
                numOfRepeats.Enabled = true;
            }
            else
            {
                numPeriodA.Enabled = false;
                numPeriodB.Enabled = false;
                numOfRepeats.Enabled = false;
            }
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            numNewPeriod1.Value = 100;
            numNewX.Value = 0;
            numNewY.Value = 0;
            textNew.Text = "";
            numNewX.Visible = true;
            numNewY.Visible = true;
            textNew.Visible = false;
            comboBox2.Visible = true;
            btnEdit.Enabled = settings.Moves.Count != 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            settings.Period1 = (int)numPeriod1.Value;
            settings.PeriodA = (int)numPeriodA.Value;
            settings.PeriodB = (int)numPeriodB.Value;
            settings.NumberOfRepeats = (int)numOfRepeats.Value;
            settings.Repeat = cbRepeat.Checked;
            settings.RandomTimeInterval = cbRandomInterval.Checked;

            try
            {
                var json = JsonConvert.SerializeObject(settings, Formatting.Indented, jsonSettings);
                File.WriteAllText(tbName.Text + ".json", json);
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
            listBox2.DataSource = null;
            listBox2.DataSource = files;
            if (listBox2.Items.Count != 0)
            {
                btnLoad.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                btnLoad.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (settings.Moves[listBox1.SelectedIndex].Type != (Actions)comboBox1.SelectedItem)
            {
                settings.Moves[listBox1.SelectedIndex] = MigrateMove(settings.Moves[listBox1.SelectedIndex], (Actions)comboBox1.SelectedItem);
            }
            else
            {
                switch (settings.Moves[listBox1.SelectedIndex])
                {
                    case MouseAction mouse:
                        mouse.Point = new Point((int)numNewX.Value, (int)numNewY.Value);
                        mouse.Button = (MouseActions)comboBox2.SelectedItem;
                        break;
                    case KeyboardAction keyboard:
                        keyboard.Text = textNew.Text;
                        break;
                }
                settings.Moves[listBox1.SelectedIndex].Period = (int)numNewPeriod1.Value;
            }

            this.listBox1.SelectedIndexChanged -= new EventHandler(this.listBox1_SelectedIndexChanged);
            listBox1.DataSource = null;
            listBox1.DataSource = settings.Moves;
            this.listBox1.SelectedIndexChanged += new EventHandler(this.listBox1_SelectedIndexChanged);
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
                        Period = (int)numNewPeriod1.Value,
                        Point = new Point((int)numNewX.Value, (int)numNewY.Value),
                        Button = (MouseActions)comboBox2.SelectedItem
                    };
                case Actions.Keyboard:
                    return new KeyboardAction
                    {
                        Id = action.Id,
                        Type = Actions.Keyboard,
                        Period = (int)numNewPeriod1.Value,
                        Text = textNew.Text
                    };
            }
            throw new NotImplementedException();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {        
            if (listBox1 != null && listBox1.SelectedIndex > -1)
            {
                numNewPeriod1.Value = settings.Moves[listBox1.SelectedIndex].Period;
                comboBox1.SelectedItem = settings.Moves[listBox1.SelectedIndex].Type;
                switch (settings.Moves[listBox1.SelectedIndex])
                {
                    case MouseAction mouse:
                        textNew.Visible = false;
                        numNewX.Visible = true;
                        numNewY.Visible = true;
                        comboBox2.Visible = true;
                        textNew.Text = "";
                        numNewX.Value = mouse.Point.X;
                        numNewY.Value = mouse.Point.Y;
                        break;
                    case KeyboardAction keyboard:
                        textNew.Visible = true;
                        numNewX.Visible = false;
                        numNewY.Visible = false;
                        comboBox2.Visible = false;
                        textNew.Text = keyboard.Text;
                        numNewX.Value = 0;
                        numNewY.Value = 0;
                        break;
                }
            }
            else
            {
                numNewPeriod1.Value = 100;
                numNewX.Value = 0;
                numNewY.Value = 0;
                textNew.Text = "";
                comboBox2.SelectedIndex = 0;
                numNewX.Visible = true;
                numNewY.Visible = true;
                textNew.Visible = false;
                comboBox2.Visible = true;
            }
            btnEdit.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var str = listBox2.SelectedItem.ToString();
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
                listBox2.DataSource = null;
                listBox2.DataSource = files;
                if (listBox2.Items.Count != 0)
                {
                    btnLoad.Enabled = true;
                    btnDelete.Enabled = true;
                }
                else
                {
                    btnLoad.Enabled = false;
                    btnDelete.Enabled = false;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var move = listBox1.SelectedIndex > -1 && settings.Moves.Count > 0 ? settings.Moves[listBox1.SelectedIndex] : null;

            switch ((Actions)comboBox1.SelectedItem)
            {
                case Actions.Mouse:
                    if (move != null && move is MouseAction mouseMove)
                    {
                        numNewX.Value = mouseMove.Point.X;
                        numNewY.Value = mouseMove.Point.Y;
                        comboBox2.SelectedIndex = (int)mouseMove.Button;
                    }
                    else
                    {
                        numNewX.Value = 0;
                        numNewY.Value = 0;
                        comboBox2.SelectedIndex = 0;
                    }
                    textNew.Visible = false;
                    numNewX.Visible = true;
                    numNewY.Visible = true;
                    comboBox2.Visible = true;
                    textNew.Text = "";
                    break;
                case Actions.Keyboard:
                    if (move != null && move is KeyboardAction keyboardMove)
                    {
                        textNew.Text = keyboardMove.Text;
                    }
                    else
                    {
                        textNew.Text = "";
                    }
                    textNew.Visible = true;
                    numNewX.Visible = false;
                    numNewY.Visible = false;
                    comboBox2.Visible = false;
                    numNewX.Value = 0;
                    numNewY.Value = 0;
                    comboBox2.SelectedIndex = 0;
                    break;
            }
        }

        private void cbRandomInterval_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRandomInterval.Checked)
            {
                numPeriodB.Enabled = true;
            }
            else
            {
                numPeriodB.Enabled = false;
            }
        }

        private void numPeriodA_ValueChanged(object sender, EventArgs e)
        {
            if (!cbRandomInterval.Checked && numPeriodA.Value >= numPeriodA.Minimum)
            {
                numPeriodB.Value = numPeriodA.Value;
            }
        }
    }
}