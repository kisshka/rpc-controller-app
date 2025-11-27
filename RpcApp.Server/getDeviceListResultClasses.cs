using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpcApp.Server
{
    public class ValueStruct
    {
        public int VALUE;
    }

    public class LongState
    {
        public ValueStruct[] data;
    }

    public class ShleifItem
    {
        public int ID;
        public int Address;
        public int State;
        public ValueStruct[] LONGSTATE;
    }

    public class ReaderItem
    {
        public int ID;
        public int Address;
        public int State;
        public ValueStruct[] LONGSTATE;
    }

    public class RelayItem
    {
        public int ID;
        public int Address;
        public int State;
        public ValueStruct[] LONGSTATE;
    }

    public class DeviceItem
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

    public class ComPortItem
    {
        public int ComPort;
        public DeviceItem[] DeviceList;
    }

    // Основной класс запроса
    public class RequestData
    {
        public ComPortItem[] ComPortList;
        public string IPSERVER;
        public string MethodName;
        public bool Result;
        public int MessageType;
    }
}
