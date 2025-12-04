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

    public struct LongState
    {
        public ValueStruct[] data;
    }

    public struct ShleifItem
    {
        public int ID;
        public int Address;
        public int State;
        public ValueStruct[] LONGSTATE;
    }

    public struct ReaderItem
    {
        public int ID;
        public int Address;
        public int State;
        public ValueStruct[] LONGSTATE;
    }

    public struct RelayItem
    {
        public int ID;
        public int Address;
        public int State;
        public ValueStruct[] LONGSTATE;
    }

    public struct DeviceItem
    {
        public int DeviceAddress;
        public int DeviceType;
        public int DeviceVersion;
        public int OnConnect;
        public ValueStruct[] LONGSTATE;
        public ShleifItem[] ShleifList;
        public ReaderItem[] ReaderList;
        public RelayItem[] RelayList;
    }

    public struct ComPortItem
    {
        public int ComPort;
        public DeviceItem[] DeviceList;
    }

    /// <summary>
    /// Параметры ответа GetDeviceListAsyncResult
    /// </summary>
    public struct GetDeviceListAsyncResultParams
    {
        public ComPortItem[] ComPortList;
        public string IPSERVER;
        public string MethodName;
        public bool Result;
        public int MessageType;
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
