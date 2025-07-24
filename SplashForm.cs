using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace MasterMech
{
    public partial class SplashForm : Form
    {
        public SplashForm()
        {
            InitializeComponent();
        }

        private void Closetimer_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SplashForm_Load(object sender, EventArgs e)
        {
            this.Closetimer.Enabled = true;
            LblVersion.Text = "V:" + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
