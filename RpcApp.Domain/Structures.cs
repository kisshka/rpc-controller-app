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
        public string ipserver;
        public int portserver;
        public string scribeports;
    }

    /// <summary>
    /// Структура параметров метода CloseScribe
    /// </summary>
    public struct CloseScribeParams
    {
        public string guid;
        public string login;
        public string password;
        public string ipserver;
        public int portserver;
    }

    /// <summary>
    /// Структура параметров метода SetConfigurationHwSrw
    /// </summary>
    public struct SetConfigurationHwSrvParams
    {
        public string guid;
        public Ports ports;
    }

    /// <summary>
    /// Структура параметра Ports для метода SetConfigurationHwSrw
    /// </summary>
    public struct Ports
    {
        public int numberport;
        public string typeport;
        public int typeprotocol;
        public int typeconverter;
        public int portpriority;
        public int portbaund;
        public string portstate;
        public string loopstate;
    }


}
