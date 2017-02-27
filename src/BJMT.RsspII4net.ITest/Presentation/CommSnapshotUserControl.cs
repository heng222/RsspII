using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BJMT.RsspII4net.ITest.Presentation
{
    public partial class CommSnapshotUserControl : UserControl
    {
        public IRsspNode CommNode { get; set; }

        public CommSnapshotUserControl()
        {
            InitializeComponent();

            this.textBox1.Dock = DockStyle.Fill;
        }

        private void getSnapshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.CommNode != null)
                {
                    textBox1.Text = this.CommNode.ToString();
                }
            }
            catch (System.Exception ex)
            {
                textBox1.Text = ex.ToString();
            }
        }
    }
}
