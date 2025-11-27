using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpcApp.Domain
{
    /// <summary>
    /// Структура параметров метода SetSubscribe
    /// </summary>
    public struct SetSubscribeParams
    {
        public string login;
        public string password;
        public int scribe;
        public string ipServer;
        public int portServer;
        public string scribePorts;
    }

    /// <summary>
    /// Структура параметров метода CloseScribe
    /// </summary>
    public struct CloseScribeParams
    {
        public string guid;
        public string login;
        public string password;
        public string ipServer;
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
    /// Структура параметров метода метода GetDevice
    /// </summary>
    public struct GetDeviceParams
    {
        public string guid;
        public string ipServer;
        public int portServer;
        public string resultReturnMethod;
        public Adress[] devices;
        public ReturnDataFromClient returnDataFromClient;
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
    /// Структура параметра ReturnDataFromClient для метода GetDevice
    /// </summary>
    public struct ReturnDataFromClient
    {
        public string clientMessage;
    }

    /// <summary>
    /// Структура параметров метода метода GetDevice
    /// </summary>
    public struct GetDeviceListAsyncParams
    {
        public string ipServer;
        public string methodNameForAnswer;
        public int portserver;
    }

    /// <summary>
    /// Структура параметров метода метода ControlObjectsParams
    /// </summary>
    public struct ControlObjectsParams
    {
        public string guid;
        public Objects[] objects;
    }

    /// <summary>
    /// Структура параметра Object метода метода ControlObjectsParams
    /// </summary>
    public struct Objects
    {
        public string typeObject;
        public string controlWord;
        public Adress2 address;
    }

    /// <summary>
    /// Временная структура. Если не понадобится, удалить
    /// </summary>
    public struct Adress2
    {
        public int addrDevice;
        public int addPort;
        public int addRelement;
    }

    public struct SynchronizeOneKeyParams
    {
        public string guid;
        public string ipServer;
        public int portServer;
        public string methodNameForAnswer;

        public int id;
        public int autoWriting;
        public int rewriteDeleted;
        public int rewriteBlocked;
    } 

}
