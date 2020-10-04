using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Timers;
using System.Collections.Generic;
using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public BindingList<string> files = new BindingList<string>();
        Settings settings = new Settings();
        public System.Timers.Timer timer = new System.Timers.Timer();
        public System.Random x = new Random(System.DateTime.Now.Millisecond);
        private MouseHookListener m_mouseListener;

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private int iteration = 1;

        public Form1()
        {
            InitializeComponent();
            listBox1.Items.Clear();
            listBox1.DataSource = settings.moves;
            listBox1.HorizontalScrollbar = true;
            this.tabPage1.Text = "Bot";
            this.tabPage2.Text = "Ustawienia";
            this.tabPage3.Text = "Sekwencja";
            this.tabPage4.Text = "Profile";
            this.Text = "BOT";
            btnRecord.Enabled = true;
            btnStopRecord.Enabled = false;
            btnStart.Enabled = false;
            btnStop.Enabled = false;
            btnClear.Enabled = false;
            numPeriod1.Enabled = true;
            numPeriodA.Enabled = true;
            numPeriodB.Enabled = true;
            cbRepeat.Enabled = true;
            cbRepeat.Checked = true;
            numPeriod1.Minimum = 100;
            numPeriod1.Maximum = 10000000;
            numPeriod1.Value = 2000;
            numPeriodA.Minimum = 100;
            numPeriodA.Maximum = 10000000;
            numPeriodA.Value = 40000;
            numPeriodB.Minimum = 100;
            numPeriodB.Maximum = 10000000;
            numPeriodB.Value = 90000;
            numNewPeriod1.Minimum = 100;
            numNewPeriod1.Maximum = 10000000;
            numNewPeriod1.Value = 100;
            btnEdit.Enabled = false;

            string path = System.IO.Directory.GetCurrentDirectory();
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (var fi in di.GetFiles("*.dat"))
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
            comboBox1.Items.Add(MouseButtons.Left);
            comboBox1.Items.Add(MouseButtons.Right);
            comboBox1.SelectedIndex = 0;
        }

        public void DoMouseClick(object sender, ElapsedEventArgs e)
        {
            Cursor.Position = settings.moves[iteration].Point;
            if(settings.moves[iteration].Button.Equals(MouseButtons.Left))
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, 0);
            if (settings.moves[iteration].Button.Equals(MouseButtons.Right))
                mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, (uint)Cursor.Position.X, (uint)Cursor.Position.Y, 0, 0);
            timer.Interval = settings.moves[iteration].Period;
            iteration++;
            if (iteration == settings.moves.Count-2)
            {
                if (cbRepeat.Checked == true)
                {
                    var time = x.Next((int)numPeriodA.Value, (int)numPeriodB.Value);
                    timer.Interval = time;
                    iteration = 1;
                }
                else
                {
                    Invoke(new Action(delegate () 
                    {
                        btnStop_Click(null, null);
                    }));
                }
            }
        }

        private void runTimer(int a)
        {
            timer.Elapsed += new ElapsedEventHandler(DoMouseClick);
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
                runTimer(2000);
                btnRecord.Enabled = false;
                btnStopRecord.Enabled = false;
                btnStart.Enabled = false;
                btnStop.Enabled = true;
                btnClear.Enabled = false;
                numPeriod1.Enabled = false;
                numPeriodA.Enabled = false;
                numPeriodB.Enabled = false;
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
            timer.Elapsed -= new ElapsedEventHandler(DoMouseClick);
            iteration = 1;
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
            }
            else
            {
                numPeriodA.Enabled = false;
                numPeriodB.Enabled = false;
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
            listBox1.DataSource = settings.moves;
            if (cbRepeat.Checked == true)
            {
                numPeriodA.Enabled = true;
                numPeriodB.Enabled = true;
            }
            else
            {
                numPeriodA.Enabled = false;
                numPeriodB.Enabled = false;
            }
            if (settings.moves.Count == 0)
            {
                numNewPeriod1.Value = 0;
                btnEdit.Enabled = false;
            }
            else
            {
             //   listBox1.SelectedIndex = 1;
               // numNewPeriod1.Value = settings.moves[listBox1.SelectedIndex].Period;
                btnEdit.Enabled = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            settings.moves.Clear();
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
            numNewPeriod1.Value = 100;
            if (cbRepeat.Checked == true)
            {
                numPeriodA.Enabled = true;
                numPeriodB.Enabled = true;
            }
            else
            {
                numPeriodA.Enabled = false;
                numPeriodB.Enabled = false;
            }
        }

        public void Activation()
        {
            m_mouseListener = new MouseHookListener(new GlobalHooker());
            m_mouseListener.Enabled = true;
            m_mouseListener.MouseDownExt += MouseListener_MouseDownExt;
        }

        public void Deactivation()
        {
            m_mouseListener.Dispose();
        }

        private void MouseListener_MouseDownExt(object sender, MouseEventExtArgs e)
        {
            settings.moves.Add(new ClickParameters {ID=settings.moves.Count+1,
                Point = new System.Drawing.Point(Cursor.Position.X, Cursor.Position.Y),
                Period =(int)numPeriod1.Value, Button=e.Button});
        }

        private void cbCookies_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRepeat.Checked == true)
            {
                numPeriodA.Enabled = true;
                numPeriodB.Enabled = true;
            }
            else
            {
                numPeriodA.Enabled = false;
                numPeriodB.Enabled = false;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            Hashtable dane = null;

            var str=listBox2.SelectedItem.ToString();
            FileStream fs = null;
            try
            {
                fs = new FileStream(str, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                dane = (Hashtable)formatter.Deserialize(fs);
            }
            catch (SerializationException ex)
            {
                MessageBox.Show(this, "Nastąpił następujący błąd: \n" + ex.ToString(), "BLAD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            finally
            {
                fs.Close();
            }

            foreach (DictionaryEntry de in dane)
            {
                settings = ((Settings)de.Key);
            }

            numPeriod1.Value = settings.Period1;
            numPeriodA.Value = settings.PeriodA;
            numPeriodB.Value = settings.PeriodB;
            cbRepeat.Checked = settings.Repeat;
            cbRepeat.CheckedChanged += new System.EventHandler(this.cbCookies_CheckedChanged);

            listBox1.DataSource = settings.moves;

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
            }
            else
            {
                numPeriodA.Enabled = false;
                numPeriodB.Enabled = false;
            }
            if (settings.moves.Count == 0)
            {
                numNewPeriod1.Value = 100;
                btnEdit.Enabled = false;
            }
            else
            {
                numNewPeriod1.Value = 100;
                btnEdit.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Hashtable dane = new Hashtable();
            settings.Period1 = (int)numPeriod1.Value;
            settings.PeriodA = (int)numPeriodA.Value;
            settings.PeriodB = (int)numPeriodB.Value;
            settings.Repeat = cbRepeat.Checked;
            dane.Add(settings, null);

            FileStream fs = null;

            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                fs=new FileStream(tbName.Text + ".dat", FileMode.Create);
                formatter.Serialize(fs, dane);
            }
            catch (SerializationException ex)
            {
                MessageBox.Show(this, "Nastąpił następujący błąd: \n" + ex.ToString(), "BLAD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                fs.Close();
            }
            files.Clear();
            string path = System.IO.Directory.GetCurrentDirectory();
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (var fi in di.GetFiles("*.dat"))
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
            settings.moves[listBox1.SelectedIndex].Period = (int)numNewPeriod1.Value;
            settings.moves[listBox1.SelectedIndex].Button = comboBox1.SelectedIndex==0?MouseButtons.Left:MouseButtons.Right;
            this.listBox1.SelectedIndexChanged -= new System.EventHandler(this.listBox1_SelectedIndexChanged);
            listBox1.DataSource = null;
            listBox1.DataSource = settings.moves;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {        
            if (listBox1!=null)
            {
                lblNr.Text = "Nr:" + (listBox1.SelectedIndex + 1);
                numNewPeriod1.Value = settings.moves[listBox1.SelectedIndex].Period;
                comboBox1.SelectedIndex = settings.moves[listBox1.SelectedIndex].Button == MouseButtons.Left ? 0 : 1;
            }
            else
            {
                lblNr.Text = "Nr:";
                numNewPeriod1.Value = 100;
            }
            btnEdit.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var str = listBox2.SelectedItem.ToString();
            if (System.IO.File.Exists(str))
            {
                try
                {
                    System.IO.File.Delete(str);
                }
                catch (System.IO.IOException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                files.Clear();
                string path = System.IO.Directory.GetCurrentDirectory();
                DirectoryInfo di = new DirectoryInfo(path);
                foreach (var fi in di.GetFiles("*.dat"))
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
    }
}