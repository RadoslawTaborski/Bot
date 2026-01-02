using Clicker.Properties;
using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace Clicker
{
    public partial class Form : System.Windows.Forms.Form
    {
        private const int LEFT_DOWN = 0x0002;
        private const int LEFT_UP = 0x0004;
        private const int RIGHT_DOWN = 0x0008;
        private const int RIGHT_UP = 0x0010;
        private const int MIDDLE_DOWN = 0x0020;
        private const int MIDDLE_UP = 0x0040;
        private const int MOVE = 0x0001;
        private const int ABSOLUTE = 0x8000;
        private const int KEYUP = 0x0002;
        private const int VK_SHIFT = 0x10;

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void keybd_event(uint bVk, uint bScan, uint dwFlags, uint dwExtraInfo);

        [DllImport("user32.dll")]
        static extern short VkKeyScan(char ch);

        private readonly BindingList<string> files = new BindingList<string>();
        private readonly System.Timers.Timer timer = new System.Timers.Timer();
        private readonly Random random = new Random(DateTime.Now.Millisecond);
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
            numPeriodA.Minimum = 100;
            numPeriodA.Maximum = 10000000;
            numPeriodA.Value = 2000;
            numPeriodB.Minimum = 100;
            numPeriodB.Maximum = 10000000;
            numPeriodB.Value = 3000;
            numNewPeriod1.Minimum = 100;
            numNewPeriod1.Maximum = 10000000;
            numNewPeriod1.Value = 100;
            numNewX.Minimum = 0;
            numNewX.Maximum = Screen.PrimaryScreen.Bounds.Width;
            numNewX.Value = 0;
            numNewY.Minimum = 0;
            numNewY.Maximum = Screen.PrimaryScreen.Bounds.Height;
            numNewY.Value = 0;
            textNew.Text = "";
            textNew.Visible = false;
            numOfRepeats.Minimum = 2;
            numOfRepeats.Maximum = 100000;
            numOfRepeats.Value = 1000;
            btnEdit.Enabled = false;

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

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Items.Add(Actions.MouseLeft);
            comboBox1.Items.Add(Actions.MouseRight);
            comboBox1.Items.Add(Actions.MouseMiddle);
            comboBox1.Items.Add(Actions.MouseLeftDown);
            comboBox1.Items.Add(Actions.MouseLeftUp);
            comboBox1.Items.Add(Actions.Keyboard);
            comboBox1.SelectedIndex = 0;
        }

        public void DoAction(object sender, ElapsedEventArgs e)
        {
            if (settings.Moves[iteration].Action.Equals(Actions.Keyboard))
            {
                foreach (char c in settings.Moves[iteration].Text)
                {
                    var vk = VkKeyScan(c);
                    bool shift = (vk & 0x0100) != 0;

                    if (shift)
                        keybd_event(VK_SHIFT, 0, 0, 0);

                    keybd_event((byte)(vk & 0xFF), 0, 0, 0);
                    keybd_event((byte)(vk & 0xFF), 0, KEYUP, 0);

                    if (shift)
                        keybd_event(VK_SHIFT, 0, KEYUP, 0);
                }
            }
            else
            {
                SetCursorPos(settings.Moves[iteration].Point.X, settings.Moves[iteration].Point.Y);

                if (settings.Moves[iteration].Action.Equals(Actions.MouseLeft))
                    mouse_event(LEFT_DOWN | LEFT_UP, 0, 0, 0, 0);
                if (settings.Moves[iteration].Action.Equals(Actions.MouseRight))
                    mouse_event(RIGHT_DOWN | RIGHT_UP, 0, 0, 0, 0);
                if (settings.Moves[iteration].Action.Equals(Actions.MouseMiddle))
                    mouse_event(MIDDLE_DOWN | MIDDLE_UP, 0, 0, 0, 0);
                if (settings.Moves[iteration].Action.Equals(Actions.MouseLeftDown))
                {
                    mouse_event(LEFT_DOWN, 0, 0, 0, 0);
                    if (settings.Moves[iteration + 1].Action.Equals(Actions.MouseLeftUp))
                    {
                        int absX = settings.Moves[iteration + 1].Point.X * 65535 / Screen.PrimaryScreen.Bounds.Width;
                        int absY = settings.Moves[iteration + 1].Point.Y * 65535 / Screen.PrimaryScreen.Bounds.Height;
                        Thread.Sleep(settings.Moves[iteration].Period);
                        mouse_event(MOVE | ABSOLUTE, (uint)absX, (uint)absY, 0, 0);
                        Thread.Sleep(settings.Moves[iteration].Period);
                        mouse_event(LEFT_UP, 0, 0, 0, 0);
                    }
                    iteration++;
                }
            }
            timer.Interval = settings.Moves[iteration].Period;
            iteration++;
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
                numPeriodB.Enabled = true;
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
                numPeriodB.Enabled = true;
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
            numNewX.Visible = true;
            numNewY.Visible = true;
            textNew.Visible = false;
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
            numNewPeriod1.Value = 100;
            numNewX.Value = 0;
            numNewY.Value = 0;
            textNew.Text = "";
            numNewX.Visible = true;
            numNewY.Visible = true;
            textNew.Visible = false;
            iteration = 1;
            repeatCounter = 0;
            if (cbRepeat.Checked == true)
            {
                numPeriodA.Enabled = true;
                numPeriodB.Enabled = true;
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
            settings.Moves.Add(new Parameters {Id=settings.Moves.Count+1,
                Point = new Point(Cursor.Position.X, Cursor.Position.Y),
                Period = (int)numPeriod1.Value, Action=e.Button == MouseButtons.Middle 
                ? Actions.MouseMiddle 
                : e.Button == MouseButtons.Right 
                    ? Actions.MouseRight 
                    : Actions.MouseLeft});
        }

        private void cbCookies_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRepeat.Checked == true)
            {
                numPeriodA.Enabled = true;
                numPeriodB.Enabled = true;
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

            var str=listBox2.SelectedItem.ToString();
            try
            {
                var json = File.ReadAllText(str);
                settings = JsonConvert.DeserializeObject<Settings>(json);
            }
            catch (SerializationException ex)
            {
                MessageBox.Show(this, "Nastąpił następujący błąd: \n" + ex.ToString(), "BLAD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }

            numPeriod1.Value = settings.Period1;
            numPeriodA.Value = settings.PeriodA;
            numPeriodB.Value = settings.PeriodB;
            numOfRepeats.Value = settings.NumberOfRepeats;
            cbRepeat.Checked = settings.Repeat;
            cbRepeat.CheckedChanged += new EventHandler(cbCookies_CheckedChanged);

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
                numPeriodB.Enabled = true;
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
            numNewX.Visible = true;
            numNewY.Visible = true;
            textNew.Visible = false;
            btnEdit.Enabled = settings.Moves.Count != 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            settings.Period1 = (int)numPeriod1.Value;
            settings.PeriodA = (int)numPeriodA.Value;
            settings.PeriodB = (int)numPeriodB.Value;
            settings.NumberOfRepeats = (int)numOfRepeats.Value;
            settings.Repeat = cbRepeat.Checked;

            try
            {
                var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
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
            settings.Moves[listBox1.SelectedIndex].Period = (int)numNewPeriod1.Value;
            settings.Moves[listBox1.SelectedIndex].Point = new Point((int)numNewX.Value, (int)numNewY.Value);
            settings.Moves[listBox1.SelectedIndex].Text = textNew.Text;
            settings.Moves[listBox1.SelectedIndex].Action = (Actions)comboBox1.SelectedItem;
            this.listBox1.SelectedIndexChanged -= new EventHandler(this.listBox1_SelectedIndexChanged);
            listBox1.DataSource = null;
            listBox1.DataSource = settings.Moves;
            this.listBox1.SelectedIndexChanged += new EventHandler(this.listBox1_SelectedIndexChanged);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {        
            if (listBox1 != null && listBox1.SelectedIndex > -1)
            {
                numNewX.Value = settings.Moves[listBox1.SelectedIndex].Point.X;
                numNewY.Value = settings.Moves[listBox1.SelectedIndex].Point.Y;
                numNewPeriod1.Value = settings.Moves[listBox1.SelectedIndex].Period;
                comboBox1.SelectedItem = settings.Moves[listBox1.SelectedIndex].Action;
                if (settings.Moves[listBox1.SelectedIndex].Action.Equals(Actions.Keyboard))
                {
                    textNew.Visible = true;
                    numNewX.Visible = false;
                    numNewY.Visible = false;
                    textNew.Text = settings.Moves[listBox1.SelectedIndex].Text;
                    numNewX.Value = 0;
                    numNewY.Value = 0;
                } 
                else
                {
                    textNew.Visible = false;
                    numNewX.Visible = true;
                    numNewY.Visible = true;
                    textNew.Text = "";
                    numNewX.Value = settings.Moves[listBox1.SelectedIndex].Point.X;
                    numNewY.Value = settings.Moves[listBox1.SelectedIndex].Point.Y;
                }
            }
            else
            {
                numNewPeriod1.Value = 100;
                numNewX.Value = 0;
                numNewY.Value = 0;
                textNew.Text = "";
                numNewX.Visible = true;
                numNewY.Visible = true;
                textNew.Visible = false;
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
            if (comboBox1.SelectedItem.Equals(Actions.Keyboard))
            {
                textNew.Visible = true;
                numNewX.Visible = false;
                numNewY.Visible = false;
                textNew.Text = move?.Text ?? "";
                numNewX.Value = 0;
                numNewY.Value = 0;
            }
            else
            {
                textNew.Visible = false;
                numNewX.Visible = true;
                numNewY.Visible = true;
                textNew.Text = "";
                numNewX.Value = move?.Point.X ?? 0;
                numNewY.Value = move?.Point.Y ?? 0;
            }
        }
    }
}