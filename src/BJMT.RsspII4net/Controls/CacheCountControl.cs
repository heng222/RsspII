using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Windows.Forms;

using MyTuple = System.Tuple<System.Windows.Forms.Label, System.Windows.Forms.ProgressBar, System.Windows.Forms.Label>;

namespace BJMT.RsspII4net.Controls
{
    /// <summary>
    /// 一个控件，用于实时显示缓存池个数。
    /// </summary>
    partial class CacheCountControl : UserControl
    {
        /// <summary>
        /// 可用的ProgressBar。
        /// </summary>
        private readonly MyTuple[] _availableProgressBars;

        /// <summary>
        /// 正在使用的ProgressBar。
        /// Key = 缓存名称。
        /// </summary>
        private ConcurrentDictionary<string, MyTuple> _usedProgressBar =
            new ConcurrentDictionary<string, MyTuple>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public CacheCountControl()
        {
            InitializeComponent();

            this.CreateGraphics();

            this._availableProgressBars = new MyTuple[] 
            { 
                new MyTuple(label1, progressBar1, label5), 
                new MyTuple(label2, progressBar2, label6), 
                new MyTuple(label3, progressBar3, label7), 
                new MyTuple(label4, progressBar4, label8), 
            };

            this.Reset();
        }

        #region "public methods"
        public void ShowCount(string name, int count)
        {
            if (_usedProgressBar.Count >= _availableProgressBars.Length)
            {
                throw new InvalidOperationException(string.Format("最多只能显示{0}个缓存的个数。", _availableProgressBars.Length));
            }

            MyTuple value;
            bool flag = _usedProgressBar.TryGetValue(name, out value);

            if (!flag)
            {
                value = _usedProgressBar.GetOrAdd(name, _availableProgressBars[_usedProgressBar.Count]);
            }

            this.BeginInvoke(new Action(() =>
            {
                try
                {
                    if (value.Item2.Maximum < count)
                    {
                        value.Item2.Maximum = count;
                    }

                    value.Item1.Visible = true;
                    value.Item2.Visible = true;
                    value.Item3.Visible = true;

                    value.Item1.Text = string.Format("{0}：", name);
                    value.Item2.Value = count;
                    value.Item3.Text = string.Format(@"{0} / {1}", count, value.Item2.Maximum);
                }
                catch (Exception)
                {                	
                }
            }));
        }

        public void Reset()
        {
            this.BeginInvoke(new Action(() =>
            {
                try
                {
                    this._availableProgressBars.ToList().ForEach(p =>
                    {
                        p.Item1.Visible = false;
                        p.Item2.Visible = false;
                        p.Item3.Visible = false;
                        p.Item2.Maximum = 0;
                        p.Item2.Value = 0;
                    });
                }
                catch (Exception)
                {
                }
            }));
        }
        #endregion

        #region "控件事件"
        #endregion
    }
}
