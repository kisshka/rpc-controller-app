using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpcApp.Domain.Structures
{
    /// <summary>
    /// Поля которые передаются подписанным на событие RsEventReceived клиентам
    /// </summary>
    public class RsEventArgs : EventArgs
    {
       public int  IdPerson { get; set; }
       public string NameType { get; set; }
       public int Event { get; set; }
       public string NameEvent { get; set; }
        public int Type { get; set; }
        public int Device { get; set; }
        public int Rele { get; set; }
    }

    /// <summary>
    /// Поля которые передаются подписанным на событие KeyCodeReceived клиентам
    /// </summary>
    public class KeyCodeEventArgs : EventArgs
    {
        public string KeyCode { get; set; }
    }

    /// <summary>
    /// Поля которые передаются подписанным на событие DeviceDataReceived клиентам
    /// </summary>
    public class DeviceDataEventArgs : EventArgs
    {
        public List<ComPortInfo> ComPorts { get; set; } = new();
    }

    public class ComPortInfo
    {
        public int PortNumber { get; set; }
        public List<ControllerInfo> Controllers { get; set; } = new();
    }

    public class ControllerInfo
    {
        public int Address { get; set; }
        public int DeviceType { get; set; }
        public int OnConnect { get; set; }

        public List<RelayInfo> Relays { get; set; } = new();
    }

    public class RelayInfo
    {
        public int Id { get; set; }
        public int Address { get; set; }
        public int State { get; set; }
    }


}
