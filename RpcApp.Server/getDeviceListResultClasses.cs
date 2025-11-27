using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpcApp.Server
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

    // Основной класс запроса
    public struct RequestData
    {
        public ComPortItem[] ComPortList;
        public string IPSERVER;
        public string MethodName;
        public bool Result;
        public int MessageType;
    }
}
