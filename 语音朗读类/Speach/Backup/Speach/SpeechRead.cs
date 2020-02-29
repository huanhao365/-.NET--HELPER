using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Speach
{
    public partial class SpeechRead : Form
    {
        public SpeechRead()
        {
            InitializeComponent();
        }

        #region 播放
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                Program.sp.Volume = trackBar1.Value;
                Program.sp.Rate = 1;
                Program.sp.AnalyseSpeak(textBox1.Text);
            }
        }
        #endregion

        #region 播放
        private void button2_Click(object sender, EventArgs e)
        {
            Program.sp.Stop();
        }
        #endregion

        #region 暂停
        private void button3_Click(object sender, EventArgs e)
        {
            Program.sp.Pause();
        }
        #endregion

        #region 继续
        private void button4_Click(object sender, EventArgs e)
        {
            Program.sp.Continue();
        }
        #endregion
    }
}
