using Horizon.XmlRpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpcApp.Domain.Structures
{
    public struct ValueStruct
    {
        public int VALUE;
    }

    public struct GetDeviceListAsyncResultParams
    {
        public XmlRpcStruct[] ComPortList;
    }

    /// <summary>
    /// Параметры ответа GetKeyCode
    /// </summary>
    public struct GetKeyCodeParams
    {
        public int AggregateAddress;
        public string Result;
        public string IPSERVER;
        public string MethodName;
        public int MessageType;
        public int ComPort;
        public int PKUAddress;
        public int DeviceAddress;
        public int ID;
    }

}
