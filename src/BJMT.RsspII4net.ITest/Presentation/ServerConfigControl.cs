using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using BJMT.RsspII4net.ITest.Infrastructure;
using BJMT.RsspII4net.ITest.Utilities;
using BJMT.Utility;

namespace BJMT.RsspII4net.ITest.Presentation
{
    /// <summary>
    /// 服务器参数配置控件
    /// </summary>
    partial class ServerConfigControl : UserControl, IServerConfigProvider
    {
        #region "field"   

        #endregion

        #region "properties"
        /// <summary>
        /// 是否保存业务数据
        /// </summary>
        public bool UserDataStorageEnabled { get; set; }

        public event EventHandler<EventArgs> LocalNodeIdChanged;
        #endregion

        public ServerConfigControl()
        {
            // 
            InitializeComponent();

            InitialUI();

            // 
            AddLocalIpToCombox();
        }

        #region "public methods"
        #endregion

        #region "private methods"
        private void InitialUI()
        {
            // 初始化设备类型。
            var des = EnumUtility.GetDescriptions<EquipmentType>();
            cbxEquipmentType.Items.AddRange(des.Where(p => p != EquipmentType.None.ToString()).ToArray());
            cbxEquipmentType.SelectedIndex = 0;
        }
        private void AddLocalIpToCombox()
        {
            try
            {
                cbx1ListenIP.Items.Clear();
                cbx2ListenIP.Items.Clear();

                foreach (var ip in HelperTools.LocalIpAddress)
                {
                    cbx1ListenIP.Items.Add(ip);
                    cbx2ListenIP.Items.Add(ip);
                }
                if (cbx1ListenIP.Items.Count > 0)
                {
                    cbx1ListenIP.SelectedIndex = 0;
                    cbx2ListenIP.SelectedIndex = 0;
                }
                if (cbx2ListenIP.Items.Count > 1)
                {
                    cbx2ListenIP.SelectedIndex = 1;
                }
            }
            catch (System.Exception /*ex*/)
            {
            }
        }
        #endregion

        #region "Control events"
        private void cbx1ListenIP_DropDown(object sender, EventArgs e)
        {
            try
            {
                ComboBox cbx = sender as ComboBox;
                if (cbx != null)
                {
                    int oldIndex = cbx.SelectedIndex;
                    cbx.Items.Clear();
                    foreach (var ip in HelperTools.LocalIpAddress)
                    {
                        cbx.Items.Add(ip.ToString());
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
            }
        }


        private void nudLocalID_ValueChanged(object sender, EventArgs e)
        {
            if (this.LocalNodeIdChanged != null)
            {
                this.LocalNodeIdChanged(this, e);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                cbx1ListenIP.Enabled = true;
                txt1ListenPort.Enabled = true;
            }
            else
            {
                cbx1ListenIP.Enabled = false;
                txt1ListenPort.Enabled = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                cbx2ListenIP.Enabled = true;
                txt2ListenPort.Enabled = true;
            }
            else
            {
                cbx2ListenIP.Enabled = false;
                txt2ListenPort.Enabled = false;
            }
        }

        private void chkSaveBussinessData_CheckedChanged(object sender, EventArgs e)
        {
            this.UserDataStorageEnabled = chkSaveFile.Checked;
        }


        private void OnLabelServerIdClicked(object sender, EventArgs e)
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
        #endregion


        #region ISettingPage 成员

        public System.Windows.Forms.Control View { get { return this; } }

        public uint LocalID { get { return (uint)(this.nudLocalID.Value); } }

        public string Title { get { return string.Format("服务器 {0}", this.LocalID); } }
        
        public byte ApplicationType
        {
            get { return Convert.ToByte(nudAppType.Value); }
            set { this.nudAppType.Value = value; }
        }

        public EquipmentType DeviceType
        {
            get { return EnumUtility.GetValue<EquipmentType>(cbxEquipmentType.Text); }
        }

        public ushort EcInterval { get { return (ushort)nudEcInterval.Value; } }

        public byte SeqNoThreshold { get { return Convert.ToByte(nudSeqNoThreshold.Value); } }

        public void UpdateControl(bool actived)
        {
            if (actived)
            {
                this.nudLocalID.Enabled = false;
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;

                cbx1ListenIP.Enabled = false;
                txt1ListenPort.Enabled = false;
                cbx2ListenIP.Enabled = false;
                txt2ListenPort.Enabled = false;
            }
            else
            {
                nudLocalID.Enabled = true;
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                cbx1ListenIP.Enabled = checkBox1.Checked;
                txt1ListenPort.Enabled = checkBox1.Checked;
                cbx2ListenIP.Enabled = checkBox2.Checked;
                txt2ListenPort.Enabled = checkBox2.Checked;
            }

            cbxEquipmentType.Enabled = !actived;
            nudAppType.Enabled = !actived;
            txtAuthKeys.Enabled = !actived;
            nudEcInterval.Enabled = !actived;
            txtAcceptableClients.Enabled = !actived;
            chkAcceptableClients.Enabled = !actived;
            nudSeqNoThreshold.Enabled = !actived;
        }
        
        public byte[] GetAuthenticationKeys()
        {
            return HelperTools.SplitHexText(txtAuthKeys.Text);
        }

        public List<IPEndPoint> GetListeningEndPoints()
        {
            List<IPEndPoint> listeners = new List<IPEndPoint>();

            if (checkBox1.Checked)
            {
                IPEndPoint point1 = new IPEndPoint(IPAddress.Parse(cbx1ListenIP.Text),
                    Convert.ToInt32(txt1ListenPort.Text));
                listeners.Add(point1);
            }

            if (checkBox2.Checked)
            {
                IPEndPoint point2 = new IPEndPoint(IPAddress.Parse(cbx2ListenIP.Text),
                    Convert.ToInt32(txt2ListenPort.Text));
                listeners.Add(point2);
            }
            return listeners;
        }

        public IEnumerable<KeyValuePair<uint, List<IPEndPoint>>> GetAcceptableClients()
        {
            try
            {
                if (this.chkAcceptableClients.Checked)
                {
                    var result = new List<KeyValuePair<uint, List<IPEndPoint>>>();

                    var splitedText = this.txtAcceptableClients.Text.Trim().
                        Split(new string[] { ";", "；", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var item in splitedText)
                    {
                        var value = HelperTools.ParseIdAndEndPoints(item);
                        result.Add(value);
                    }

                    return result;
                }
                else
                {
                    return null; // 空引用表示不指定客户端，即接受所有客户端。
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("无法解析指定的客户端，" + ex.Message + "\r\n多个客户端使用半角逗号分隔。", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        #endregion
    }
}
