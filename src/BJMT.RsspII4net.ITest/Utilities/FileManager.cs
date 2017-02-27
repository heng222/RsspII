using System;
using System.Collections.Generic;
using System.Linq;
using BJMT.RsspII4net.Events;
using BJMT.RsspII4net.ITest.Utilities;
using BJMT.RsspII4net.Utilities;

namespace BJMT.RsspII4net.ITest
{
    class FileManager : IDisposable
    {
        private bool _disposed = false;

        /// <summary>
        /// 已接收的文件
        /// key = Remote node ID.
        /// </summary>
        private Dictionary<uint, CustomFile> _businessFiles = new Dictionary<uint, CustomFile>(10);

        private object _syncLock = new object();

        public FileManager()
        {
        }

        ~FileManager()
        {
            this.Dispose(false);
        }

        #region "private methods"
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                try
                {
                    this.Clear();
                }
                catch (System.Exception /*ex*/)
                {
                }
            }
        }

        private void AddFile(uint key, CustomFile value)
        {
            lock (_syncLock)
            {
                _businessFiles.Add(key, value);
            }
        }

        private CustomFile GetFile(uint key)
        {
            CustomFile file;

            lock (_syncLock)
            {
                _businessFiles.TryGetValue(key, out file);
            }

            return file;
        }

        private void RemoveFile(uint key)
        {
            CustomFile file;

            lock (_syncLock)
            {
                _businessFiles.TryGetValue(key, out file);
                _businessFiles.Remove(key);
            }

            if (file != null)
            {
                file.Dispose();
            }
        }
        #endregion



        /// <summary>
        /// 保存收到的用户数据
        /// </summary>
        public void SaveUserData(uint remoteID, byte[] userData)
        {
            try
            {
                var dataLength = userData.Length;

                if (CustomFile.ContainsFileStartFlag(userData, dataLength))
                {
                    // 先关闭旧的文件
                    this.RemoveFile(remoteID);

                    // 创建新的业务文件
                    var ext = RsspEncoding.ToHostString(userData, CustomFile.StartFlagLen, dataLength - CustomFile.StartFlagLen);
                    var bdfile = new CustomFile(remoteID.ToString(), ext);

                    this.AddFile(remoteID, bdfile);
                }
                else if (CustomFile.ContainsFileEndFlag(userData, dataLength))
                {
                    // 关闭业务文件
                    this.RemoveFile(remoteID);
                }
                else
                {
                    // 保存收到的数据
                    var theFile = this.GetFile(remoteID);
                    if (theFile != null)
                    {
                        theFile.Write(userData);
                    }
                }
            }
            catch (System.Exception ex)
            {
                LogUtility.Error(ex);
            }
        }

        public void Clear()
        {
            List<CustomFile> files = null;
            lock(_syncLock)
            {
                files = _businessFiles.Select(p=>p.Value).ToList();
                _businessFiles.Clear();
            }

            files.ForEach(p => p.Dispose());
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
