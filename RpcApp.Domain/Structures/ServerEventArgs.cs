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
        public string Guid { get; set; }
        public string NameType { get; set; }
        public int Event { get; set; }
        public string NameEvent { get; set; }
    }

    /// <summary>
    /// Поля которые передаются подписанным на событие KeyCodeReceived клиентам
    /// </summary>
    public class KeyCodeEventArgs : EventArgs
    {
        public string KeyCode { get; set; }
    }
}
