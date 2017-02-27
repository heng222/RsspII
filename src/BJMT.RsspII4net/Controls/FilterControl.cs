using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace BJMT.RsspII4net.Controls
{
    internal partial class FilterControl : UserControl
    {
        private IFilterControlProvider _customFilter;

        public FilterControl()
        {
            InitializeComponent();

            this.cbxConfidtion2.SelectedIndex = 0;
            this.cbxConfidtion3.SelectedIndex = 0;
            this.cmbCondition1Operator.SelectedIndex = 0;
            this.cmbCondition2Operator.SelectedIndex = 0;
            this.cmbCondition3Operator.SelectedIndex = 0;
            this.tabControl1.Dock = DockStyle.Fill;

            RefresshFilterPanel();
        }
        
        public System.Windows.Forms.Control View { get { return this; } }

        public bool Filter(IEnumerable<byte> userData)
        {
            try
            {
                if (!this.FilterByBytes(userData))
                {
                    return false;
                }

                if (_customFilter != null)
                {
                    return _customFilter.Filter(userData);
                }
                else
                {
                    return true;
                }
            }
            catch (System.Exception)
            {
                return true;
            }
        }

        public void AddCustomFilter(IFilterControlProvider customFilter)
        {
            _customFilter = customFilter;

            var page = new TabPage(customFilter.Name);
            page.Controls.Add(customFilter.View);

            this.tabControl1.TabPages.Add(page);
        }

        private bool FilterByBytes(IEnumerable<byte> userData)
        {
            try
            {
                if (!chkConditionEnabled.Checked)
                {
                    return true;
                }

                int index = 1; // 表示第几个字节。
                byte value = 0; // 表示过滤的值。
                bool result = true;

                // 条件1
                index = Convert.ToInt32(nudCondition1Index.Value);
                value = Convert.ToByte(nudCondition1Value.Value);

                if (cmbCondition1Operator.SelectedIndex == 0)
                {
                    result = (value == userData.ElementAt(index - 1));
                }
                else
                {
                    result = (value != userData.ElementAt(index - 1));
                }

                // 条件2
                int condition2 = cbxConfidtion2.SelectedIndex; // “且”/“或”
                if (condition2 != 0)
                {
                    index = Convert.ToInt32(nudCondition2Index.Value);
                    value = Convert.ToByte(nudCondition2Value.Value);

                    bool filtered2 = true;
                    if (cmbCondition2Operator.SelectedIndex == 0)
                    {
                        filtered2 = (value == userData.ElementAt(index - 1));
                    }
                    else
                    {
                        filtered2 = (value != userData.ElementAt(index - 1));
                    }

                    if (condition2 == 1)
                    {
                        result = result && filtered2;
                    }
                    else
                    {
                        result = result || filtered2;
                    }
                }

                // 条件3
                int condition3 = cbxConfidtion3.SelectedIndex; // “且”/“或”
                if (condition3 != 0)
                {
                    index = Convert.ToInt32(nudCondition3Index.Value);
                    value = Convert.ToByte(nudCondition3Value.Value);

                    bool filtered3 = true;
                    if (cmbCondition3Operator.SelectedIndex == 0)
                    {
                        filtered3 = (value == userData.ElementAt(index - 1));
                    }
                    else
                    {
                        filtered3 = (value != userData.ElementAt(index - 1));
                    }

                    if (condition3 == 1)
                    {
                        result = result && filtered3;
                    }
                    else
                    {
                        result = result || filtered3;
                    }
                }

                return result;
            }
            catch (System.Exception)
            {
                return true;
            }
        }
        /// <summary>
        /// 刷新过滤界面
        /// </summary>
        private void RefresshFilterPanel()
        {
            this.nudCondition1Index.Enabled = this.chkConditionEnabled.Checked;
            this.cmbCondition1Operator.Enabled = this.chkConditionEnabled.Checked;
            this.nudCondition1Value.Enabled = this.chkConditionEnabled.Checked;
            this.cbxConfidtion2.Enabled = this.chkConditionEnabled.Checked;
            this.cbxConfidtion3.Enabled = this.chkConditionEnabled.Checked;

            this.nudCondition2Index.Enabled = this.chkConditionEnabled.Checked && (this.cbxConfidtion2.SelectedIndex != 0);
            this.cmbCondition2Operator.Enabled = this.chkConditionEnabled.Checked && (this.cbxConfidtion2.SelectedIndex != 0);
            this.nudCondition2Value.Enabled = this.chkConditionEnabled.Checked && (this.cbxConfidtion2.SelectedIndex != 0);

            this.nudCondition3Index.Enabled = this.chkConditionEnabled.Checked && (this.cbxConfidtion3.SelectedIndex != 0);
            this.cmbCondition3Operator.Enabled = this.chkConditionEnabled.Checked && (this.cbxConfidtion3.SelectedIndex != 0);
            this.nudCondition3Value.Enabled = this.chkConditionEnabled.Checked && (this.cbxConfidtion3.SelectedIndex != 0);
        }

        #region "控件事件"
        private void chkConditionEnabled_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.RefresshFilterPanel();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxConfidtion2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.RefresshFilterPanel();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxConfidtion3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.RefresshFilterPanel();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
