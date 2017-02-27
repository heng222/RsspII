

using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using BJMT.RsspII4net.Config;

namespace BJMT.RsspII4net.ITest.Infrastructure
{
    interface ICommConfigProvider
    {
        Control View { get; }

        uint LocalID { get; }

        byte ApplicationType { get; }

        EquipmentType DeviceType { get; }

        string Title { get; }

        bool UserDataStorageEnabled { get; }

        ushort EcInterval { get; }

        byte SeqNoThreshold { get; }

        byte[] GetAuthenticationKeys();
        
        void UpdateControl(bool actived);

        event EventHandler<EventArgs> LocalNodeIdChanged;
    }


    interface IClientConfigProvider : ICommConfigProvider
    {
        Dictionary<uint, List<RsspTcpLinkConfig>> GetLinkConfig();

        MessageDelayDefenseTech DefenseTech { get; }
    }


    interface IServerConfigProvider : ICommConfigProvider
    {
        List<IPEndPoint> GetListeningEndPoints();

        IEnumerable<KeyValuePair<uint, List<IPEndPoint>>> GetAcceptableClients();
    }
}
