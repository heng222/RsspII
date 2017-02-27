using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using BJMT.RsspII4net.Config;
using BJMT.RsspII4net.ITest.Infrastructure;
using BJMT.RsspII4net.ITest.Utilities;
using BJMT.Utility;
using System.Drawing;


namespace BJMT.RsspII4net.ITest.Presentation
{
    /// <summary>
    /// 客户端连接参数设置控件
    /// </summary>
    public partial class ClientConfigControl : UserControl, IClientConfigProvider
    {
        #region "Field"

        /// <summary>
        /// 是否保存业务数据
        /// </summary>
        private bool _saveBusinessData = false;
        #endregion

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ClientConfigControl()            
        {
            InitializeComponent();

            InitialUI();
            
            // 
            EnumIPToComboxControl();
        }

        #region "Properties"

        public event EventHandler<EventArgs> LocalNodeIdChanged;
        #endregion

        #region "private methods"

        private void InitialUI()
        {
            nudServer1ID.Enabled = this.checkBox1.Checked;
            nudServer2ID.Enabled = this.checkBox3.Checked;
           
            // 
            #region "选择默认的ServerIP"
            txt1ServerIP.SelectedIndex = 0;
            txt2ServerIP.SelectedIndex = 1;
            txt3ServerIP.SelectedIndex = 0;
            txt4ServerIP.SelectedIndex = 1;
            #endregion

            // 初始化设备类型。
            var des = EnumUtility.GetDescriptions<EquipmentType>();
            cbxEquipmentType.Items.AddRange(des.Where(p => p != EquipmentType.None.ToString()).ToArray());
            cbxEquipmentType.SelectedIndex = 0;

            // 初始化防御技术。
            des = EnumUtility.GetDescriptions<MessageDelayDefenseTech>();
            cbxDefenseTech.Items.AddRange(des.Where(p => p != MessageDelayDefenseTech.None.ToString()).ToArray());
            cbxDefenseTech.SelectedIndex = 0;
        }

        private void EnumIPToComboxControl()
        {
            this.Dock = DockStyle.Fill;

            cbx1LocalIP.Items.Clear();
            cbx2LocalIP.Items.Clear();
            cbx3LocalIP.Items.Clear();
            cbx4LocalIP.Items.Clear();

            foreach (var ip in HelperTools.LocalIpAddress)
            {
                cbx1LocalIP.Items.Add(ip);
                cbx2LocalIP.Items.Add(ip);
                cbx3LocalIP.Items.Add(ip);
                cbx4LocalIP.Items.Add(ip);
            }
            if (cbx1LocalIP.Items.Count > 0)
            {
                cbx1LocalIP.SelectedIndex = 0;
                cbx2LocalIP.SelectedIndex = 0;
                cbx3LocalIP.SelectedIndex = 0;
                cbx4LocalIP.SelectedIndex = 0;
            }
            if (cbx2LocalIP.Items.Count > 1)
            {
                cbx2LocalIP.SelectedIndex = 1;
                cbx4LocalIP.SelectedIndex = 1;
            }
        }
        #endregion

        #region "public methods"
        #endregion


        #region "Control events"

        private void chkSaveBussinessData_CheckedChanged(object sender, EventArgs e)
        {
            _saveBusinessData = chkSaveFile.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                cbx1LocalIP.Enabled = true;
                txt1ServerIP.Enabled = true;
                txt1ServerPort.Enabled = true;
                nudServer1ID.Enabled = true;

                checkBox2.Enabled = true;
            }
            else
            {
                cbx1LocalIP.Enabled = false;
                txt1ServerIP.Enabled = false;
                txt1ServerPort.Enabled = false;
                nudServer1ID.Enabled = false;

                checkBox2.Checked = false;
                checkBox2.Enabled = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                cbx2LocalIP.Enabled = true;
                txt2ServerIP.Enabled = true;
                txt2ServerPort.Enabled = true;
            }
            else
            {
                cbx2LocalIP.Enabled = false;
                txt2ServerIP.Enabled = false;
                txt2ServerPort.Enabled = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                cbx3LocalIP.Enabled = true;
                txt3ServerIP.Enabled = true;
                txt3ServerPort.Enabled = true;
                nudServer2ID.Enabled = true;

                checkBox4.Enabled = true;
            }
            else
            {
                cbx3LocalIP.Enabled = false;
                txt3ServerIP.Enabled = false;
                txt3ServerPort.Enabled = false;
                nudServer2ID.Enabled = false;

                checkBox4.Checked = false;
                checkBox4.Enabled = false;
            }

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                cbx4LocalIP.Enabled = true;
                txt4ServerIP.Enabled = true;
                txt4ServerPort.Enabled = true;
            }
            else
            {
                cbx4LocalIP.Enabled = false;
                txt4ServerIP.Enabled = false;
                txt4ServerPort.Enabled = false;
            }
        }    

        private void cbxLocalIP_DropDown(object sender, EventArgs e)
        {
            try
            {
                var cbx = sender as ComboBox;
                if (cbx != null)
                {
                    int oldIndex = cbx.SelectedIndex;
                    cbx.Items.Clear();
                    foreach (var ip in HelperTools.LocalIpAddress)
                    {
                        cbx.Items.Add(ip);
                    }

                    if (oldIndex < cbx.Items.Count)
                    {
                        cbx.SelectedIndex = oldIndex;
                    }
                    else if (cbx.Items.Count > 0)
                    {
                        cbx.SelectedIndex = 0;
                    }
                }
            }
            catch (System.Exception /*ex*/)
            {
                // MessageBox.Show(ex.Message, "Error", 
                // MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void nudLocalID_ValueChanged(object sender, EventArgs e)
        {
            if (this.LocalNodeIdChanged != null)
            {
                this.LocalNodeIdChanged(this, e);
            }
        }

        private void cbxDefenseTech_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                nudEcInterval.Enabled = this.DefenseTech == MessageDelayDefenseTech.EC;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnLocalIDLabelClick(object sender, EventArgs e)
        {
            try
            {
                this.nudLocalID.Hexadecimal = !this.nudLocalID.Hexadecimal;
                if (nudLocalID.Hexadecimal)
                {
                    this.nudLocalID.ForeColor = Color.Blue;
                }
                else
                {
                    this.nudLocalID.ForeColor = Color.Black;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnServer1IdLabelClick(object sender, EventArgs e)
        {
            try
            {
                this.nudServer1ID.Hexadecimal = !this.nudServer1ID.Hexadecimal;
                if (nudServer1ID.Hexadecimal)
                {
                    this.nudServer1ID.ForeColor = Color.Blue;
                }
                else
                {
                    this.nudServer1ID.ForeColor = Color.Black;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnServer2IdLabelClick(object sender, EventArgs e)
        {
            try
            {
                this.nudServer2ID.Hexadecimal = !this.nudServer2ID.Hexadecimal;
                if (nudServer2ID.Hexadecimal)
                {
                    this.nudServer2ID.ForeColor = Color.Blue;
                }
                else
                {
                    this.nudServer2ID.ForeColor = Color.Black;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion


        #region ISettingPage 成员

        public System.Windows.Forms.Control View
        {
            get { return this; }
        }

        public byte ApplicationType
        {
            get { return Convert.ToByte(nudAppType.Value); }
            set { this.nudAppType.Value = value; }
        }
        public uint LocalID
        {
            get { return (uint)nudLocalID.Value; }
        }

        public string Title { get { return string.Format("客户端 {0}", this.LocalID); } }

        public bool UserDataStorageEnabled
        {
            get { return _saveBusinessData; }
        }

        public EquipmentType DeviceType 
        { 
            get { return EnumUtility.GetValue<EquipmentType>(cbxEquipmentType.Text); } 
        }

        public MessageDelayDefenseTech DefenseTech 
        { 
            get { return EnumUtility.GetValue<MessageDelayDefenseTech>(cbxDefenseTech.Text); } 
        }
        
        public ushort EcInterval
        {
            get { return (ushort)nudEcInterval.Value; }
        }

        public byte SeqNoThreshold { get { return Convert.ToByte(nudSeqNoThreshold.Value); } }

        public Dictionary<uint, List<RsspTcpLinkConfig>> GetLinkConfig()
        {
            var result = new Dictionary<uint, List<RsspTcpLinkConfig>>();

            var linkCfg1 = new List<RsspTcpLinkConfig>();
            var linkCfg2 = new List<RsspTcpLinkConfig>();
            IPAddress localIP = null;
            IPEndPoint remote = null;

            if (checkBox1.Checked)
            {
                localIP = IPAddress.Parse(cbx1LocalIP.Text);
                remote = new IPEndPoint(IPAddress.Parse(txt1ServerIP.Text), Convert.ToInt16(txt1ServerPort.Text));
                linkCfg1.Add(new RsspTcpLinkConfig(localIP, remote));

                result.Add(Convert.ToUInt32(nudServer1ID.Value), linkCfg1);
            }
            if (checkBox2.Checked)
            {
                localIP = IPAddress.Parse(cbx2LocalIP.Text);
                remote = new IPEndPoint(IPAddress.Parse(txt2ServerIP.Text), Convert.ToInt16(txt2ServerPort.Text));
                linkCfg1.Add(new RsspTcpLinkConfig(localIP, remote));
            }


            if (checkBox3.Checked)
            {
                localIP = IPAddress.Parse(cbx3LocalIP.Text);
                remote = new IPEndPoint(IPAddress.Parse(txt3ServerIP.Text), Convert.ToInt16(txt3ServerPort.Text));
                linkCfg2.Add(new RsspTcpLinkConfig(localIP, remote));

                result.Add(Convert.ToUInt32(nudServer2ID.Value), linkCfg2);
            }
            if (checkBox4.Checked)
            {
                localIP = IPAddress.Parse(cbx4LocalIP.Text);
                remote = new IPEndPoint(IPAddress.Parse(txt4ServerIP.Text), Convert.ToInt16(txt4ServerPort.Text));
                linkCfg2.Add(new RsspTcpLinkConfig(localIP, remote));
            }

            return result;
        }

        public byte[] GetAuthenticationKeys()
        {
            return HelperTools.SplitHexText(txtAuthKeys.Text);
        }

        public void UpdateControl(bool actived)
        {
            if (actived)
            {
                cbx1LocalIP.Enabled = false;
                txt1ServerIP.Enabled = false;
                txt1ServerPort.Enabled = false;

                cbx2LocalIP.Enabled = false;
                txt2ServerIP.Enabled = false;
                txt2ServerPort.Enabled = false;

                cbx3LocalIP.Enabled = false;
                txt3ServerIP.Enabled = false;
                txt3ServerPort.Enabled = false;

                cbx4LocalIP.Enabled = false;
                txt4ServerIP.Enabled = false;
                txt4ServerPort.Enabled = false;
            }
            else
            {
                cbx1LocalIP.Enabled = checkBox1.Checked;
                txt1ServerIP.Enabled = checkBox1.Checked;
                txt1ServerPort.Enabled = checkBox1.Checked;

                cbx2LocalIP.Enabled = checkBox2.Checked;
                txt2ServerIP.Enabled = checkBox2.Checked;
                txt2ServerPort.Enabled = checkBox2.Checked;

                cbx3LocalIP.Enabled = checkBox3.Checked;
                txt3ServerIP.Enabled = checkBox3.Checked;
                txt3ServerPort.Enabled = checkBox3.Checked;

                cbx4LocalIP.Enabled = checkBox4.Checked;
                txt4ServerIP.Enabled = checkBox4.Checked;
                txt4ServerPort.Enabled = checkBox4.Checked;
            }

            nudServer1ID.Enabled = !actived;
            nudServer2ID.Enabled = !actived;

            checkBox1.Enabled = !actived;
            checkBox2.Enabled = !actived;
            checkBox3.Enabled = !actived;
            checkBox4.Enabled = !actived;

            nudLocalID.Enabled = !actived;
            nudAppType.Enabled = !actived;
            nudSeqNoThreshold.Enabled = !actived;

            cbxEquipmentType.Enabled = !actived;
            cbxDefenseTech.Enabled = !actived;

            txtAuthKeys.Enabled = !actived;

            if (!actived)
            {
                nudEcInterval.Enabled = this.DefenseTech == MessageDelayDefenseTech.EC;
            }
            else
            {
                nudEcInterval.Enabled = false;
            }
        }
        #endregion
    }
}
