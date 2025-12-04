using Horizon.XmlRpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpcApp.Domain.Structures
{
    /// <summary>
    /// Структура параметров метода SetSubscribe
    /// </summary>
    public struct SetSubscribeParams
    {
        [XmlRpcMember("LOGIN")]
        public string login;

        [XmlRpcMember("PASSWORD")]
        public string password;

        [XmlRpcMember("SCRIBE")]
        public int scribe;

        [XmlRpcMember("IPSERVER")]
        public string ipServer;

        [XmlRpcMember("PORTSERVER")]
        public int portServer;

        [XmlRpcMember("SCRIBEPORTS")]
        public string scribePorts;

        [XmlRpcMember("PORTS")]
        public SubscribePort[] ports;
    }

    /// <summary>
    /// Структура параметра port для метода SetSubscribe
    /// </summary>
    public struct SubscribePort
    {
        [XmlRpcMember("ADDRPORT")]
        public int addPort;

        [XmlRpcMember("SCRIBEDEVICES")]
        public string scribeDevices;
    }

    /// <summary>
    /// Структура параметров метода CloseScribe
    /// </summary>
    public struct CloseScribeParams
    {
        [XmlRpcMember("GUID")]
        public string guid;

        [XmlRpcMember("LOGIN")]
        public string login;

        [XmlRpcMember("PASSWORD")]
        public string password;

        [XmlRpcMember("IPSERVER")]
        public string ipServer;

        [XmlRpcMember("PORTSERVER")]
        public int portServer;
    }

    /// <summary>
    /// Структура параметров метода SetConfigurationHwSrw
    /// </summary>
    public struct SetConfigurationHwSrvParams
    {
        [XmlRpcMember("GUID")]
        public string guid;

        [XmlRpcMember("PORTS")]
        public Ports[] ports;

        [XmlRpcMember("DEVICES")]
        public Device[] devices;
    }

    /// <summary>
    /// Структура параметра Device метода SetConfigurationHwSrw
    /// </summary>
    public struct Device
    {
        [XmlRpcMember("ADDRESS")]
        public Adress address;

        [XmlRpcMember("RPCSTATUSDEVICE")]
        public string rpcStatusDevice;

        [XmlRpcMember("TYPE")]
        public int type;

        [XmlRpcMember("PRIORITYDEVICE")]
        public int priorityDevice;

        [XmlRpcMember("VERSION")]
        public int version;
    }

    /// <summary>
    /// Структура параметра Ports для метода SetConfigurationHwSrw
    /// </summary>
    public struct Ports
    {
        [XmlRpcMember("TYPEPORT")]
        public string typePort;

        [XmlRpcMember("NUMBERPORT")]
        public int numberPort;

        [XmlRpcMember("TYPEPROTOCOL")]
        public int typeProtocol;

        [XmlRpcMember("TYPECONVERTER")]
        public int typeConverter;

        [XmlRpcMember("PORTPRIORITY")]
        public int portPriority;

        [XmlRpcMember("PORTBAUND")]
        public int portBaund;

        [XmlRpcMember("RPCSTATEPORT")]
        public string rpcStatePort;

        [XmlRpcMember("LOOPSTATE")]
        public string loopState;
    }

    /// <summary>
    /// Структура параметра Adress для метода GetDevice
    /// </summary>
    public struct Adress
    {
        [XmlRpcMember("ADDRDEVICE")]
        public int addrdevice;

        [XmlRpcMember("ADDRPULT")]
        public int addrPult;

        [XmlRpcMember("ADDRPORT")]
        public int addrPort;
    }

    /// <summary>
    /// Структура параметров метода GetDevice
    /// </summary>
    public struct GetDeviceParams
    {
        [XmlRpcMember("GUID")]
        public string guid;

        [XmlRpcMember("IPSERVER")]
        public string ipServer;

        [XmlRpcMember("PORTSERVER")]
        public int portServer;

        [XmlRpcMember("RESULTRETURNMETHOD")]
        public string resultReturnMethod;

        [XmlRpcMember("DEVICES")]
        public Adress[] devices;

        [XmlRpcMember("RETURNDATAFROMCLIENT")]
        public ReturnDataFromClient returnDataFromClient;
    }

    /// <summary>
    /// Структура параметра ReturnDataFromClient для метода GetDevice
    /// </summary>
    public struct ReturnDataFromClient
    {
        [XmlRpcMember("CLIENTMESSAGE")]
        public string clientMessage;
    }

    /// <summary>
    /// Структура параметров метода GetDeviceListAsync
    /// </summary>
    public struct GetDeviceListAsyncParams
    {
        [XmlRpcMember("IPSERVER")]
        public string ipServer;

        [XmlRpcMember("METHODNAMEFORANSWER")]
        public string methodNameForAnswer;

        [XmlRpcMember("PORTSERVER")]
        public int portserver;
    }

    /// <summary>
    /// Структура параметров метода ControlObjectsParams
    /// </summary>
    public struct ControlObjectsParams
    {
        [XmlRpcMember("GUID")]
        public string guid;

        [XmlRpcMember("OBJECTS")]
        public Objects[] objects;
    }

    /// <summary>
    /// Структура параметра Object метода ControlObjectsParams
    /// </summary>
    public struct Objects
    {
        [XmlRpcMember("TYPEOBJECT")]
        public string typeObject;

        [XmlRpcMember("CONTROLWORD")]
        public string controlWord;

        [XmlRpcMember("ADDRESS")]
        public Adress2 address;
    }

    /// <summary>
    /// Структура адреса для Objects
    /// </summary>
    public struct Adress2
    {
        [XmlRpcMember("ADRDEVICE")]
        public int addrDevice;

        [XmlRpcMember("ADDPORT")]
        public int addPort;

        [XmlRpcMember("ADDRELEMENT")]
        public int addRelement;
    }

    /// <summary>
    /// Структура параметров метода SynchronizeOneKeyParams
    /// </summary>
    public struct SynchronizeOneKeyParams
    {
        [XmlRpcMember("GUID")]
        public string guid;

        [XmlRpcMember("IPSERVER")]
        public string ipServer;

        [XmlRpcMember("PORTSERVER")]
        public int portServer;

        [XmlRpcMember("METHODNAMEFORANSWER")]
        public string methodNameForAnswer;

        [XmlRpcMember("ID")]
        public int id;

        [XmlRpcMember("AUTOWRITING")]
        public int autoWriting;

        [XmlRpcMember("REWRITEDELETED")]
        public int rewriteDeleted;

        [XmlRpcMember("REWRITEBLOCKED")]
        public int rewriteBlocked;
    }

    /// <summary>
    /// Структура параметров метода ReadKeyCodeFromReader - Считывание ключа со считывателя
    /// </summary>
    public struct ReadKeyCodeFromReaderParams {
        [XmlRpcMember("GUID")]
        public string guid;

        [XmlRpcMember("IPSERVER")]
        public string ipServer;

        [XmlRpcMember("PORTSERVER")]
        public int portServer;

        [XmlRpcMember("METHODNAMEFORANSWER")]
        public string methodNameForAnswer;

        [XmlRpcMember("COMPORT")]
        public int ComPort;

        [XmlRpcMember("PKUAddress")]
        public int PKUAddress;

        [XmlRpcMember("DeviceAddress")]
        public int DeviceAddress;

        [XmlRpcMember("DeviceType")]
        public int DeviceType;

        [XmlRpcMember("AggregateAddress")]
        public int AggregateAddress;
    }

    public struct GetPasswordListWithStatusParams {

        [XmlRpcMember("GUID")]
        public string guid;

        [XmlRpcMember("IPSERVER")]
        public string ipServer;

        [XmlRpcMember("PORTSERVER")]
        public int portServer;

        [XmlRpcMember("METHODNAMEFORANSWER")]
        public string methodNameForAnswer;
    }


    public struct ReadDeviceKeyListParams()
    {
        [XmlRpcMember("GUID")]
        public string guid;

        [XmlRpcMember("IPSERVER")]
        public string ipServer;

        [XmlRpcMember("PORTSERVER")]
        public int portServer;

        [XmlRpcMember("METHODNAMEFORANSWER")]
        public string methodNameForAnswer;

        [XmlRpcMember("PKUAddress")]
        public int PKUAddress;

        [XmlRpcMember("DeviceAddress")]
        public int DeviceAddress;

        [XmlRpcMember("ReaderAddress")]
        public int ReaderAddress;
    }


    public struct ReadConfigurationParams()
    {
        [XmlRpcMember("GUID")]
        public string guid;

        [XmlRpcMember("IPSERVER")]
        public string ipServer;

        [XmlRpcMember("PORTSERVER")]
        public int portServer;

        [XmlRpcMember("METHODNAMEFORANSWER")]
        public string methodNameForAnswer;

        [XmlRpcMember("COMPORT")]
        public int ComPort;

        [XmlRpcMember("PKUAddress")]
        public int PKUAddress;

        [XmlRpcMember("DeviceAddress")]
        public int DeviceAddress;

        [XmlRpcMember("ReaderAddress")]
        public int ReaderAddress;
    }

    public struct GetListMethodsParams() {

        public string guid;
    }

    public struct RefreshTablesDataParams() {
        [XmlRpcMember("GUID")]
        public string guid;

        [XmlRpcMember("IPSERVER")]
        public string ipServer;

        [XmlRpcMember("PORTSERVER")]
        public int portServer;

        [XmlRpcMember("METHODNAMEFORANSWER")]
        public string methodNameForAnswer;

        [XmlRpcMember("TableList")]
        public XmlRpcStruct[] TableList;
    }

}