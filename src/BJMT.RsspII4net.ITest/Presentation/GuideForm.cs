using System;
using System.Reflection;
using System.Windows.Forms;
using BJMT.RsspII4net.ITest.Presentation;


namespace BJMT.RsspII4net.ITest
{
    partial class GuideForm : Form
    {
        Form _form = null;

        public GuideForm()
        {
            InitializeComponent();

            // ÉèÖÃË«»÷ÊÂ¼þ
            try
            {
                var m = typeof(RadioButton).GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic);
                if (m != null)
                {
                    m.Invoke(this.rbtnClient, new object[] { ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, true });
                    m.Invoke(this.rbtnServer, new object[] { ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, true });
                }
                rbtnClient.MouseDoubleClick += new MouseEventHandler(rbtnClient_MouseDoubleClick);
                rbtnServer.MouseDoubleClick += new MouseEventHandler(rbtnClient_MouseDoubleClick);
            }
            catch (System.Exception /*ex*/)
            {            	
            }
        }

        void rbtnClient_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Lanuch();      
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            Lanuch();
        }

        private void Lanuch()
        {
            try
            {
                this.Hide();

                _form = new MainForm(rbtnClient.Checked);

                _form.ShowDialog();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}