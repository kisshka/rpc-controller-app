using Horizon.XmlRpc.Core;
using Horizon.XmlRpc.Server;
using RpcApp.Domain.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RpcApp.Domain
{
    //КЛАССЫ СОБЫТИЙ ДЛЯ ПЕРЕДАЧИ ДАННЫХ
    public class RsEventArgs : EventArgs
    {
        public string Guid { get; set; }
        public string NameType { get; set; }
        public int Event { get; set; }
        public string NameEvent { get; set; }
    }


    public class KeyCodeEventArgs : EventArgs
    {
        public string KeyCode { get; set; }
    }


    public interface IDeviceService
    {

        [XmlRpcMethod("GetDeviceListAsyncResult")]
        XmlRpcStruct GetDeviceListAsyncResult(GetDeviceListAsyncResultParams p);

        [XmlRpcMethod("ONRSEVENT")]
        XmlRpcStruct OnRsEvent(XmlRpcStruct p);


        [XmlRpcMethod("GetKeyCode")]
        XmlRpcStruct GetKeyCode(GetKeyCodeParams p);

        [XmlRpcMethod("GetDeviceKeyList")]
        XmlRpcStruct GetDeviceKeyList(XmlRpcStruct p);
    }


    /// <summary>
    ///  Сервер для прослушивания событий, приходящих от ядра Орион
    /// <example>
    /// Запуск листенера и подписка на получение событий
    /// <code>
    /// 
    ///    var server = new Server();

    ///     server.RsEventReceived += (args) =>
    ///        {
    ///    Console.WriteLine($"\nПОЛУЧЕНО НОВОЕ СОБЫТИЕ");
    ///     Console.WriteLine($"  Событие: {args.NameEvent}");
    ///    Console.WriteLine($"  Тип: {args.NameType}");
    ///   Console.WriteLine($"  Код события: {args.Event}");
    ///   Console.WriteLine($"  Время: {DateTime.Now:HH:mm:ss}");
    ///    Console.WriteLine();
    /// };
    /// 
    ///     server.KeyCodeReceived += (args) =>
    /// {
    ///    Console.WriteLine($"\n[ПОЛУЧЕНО НОВОЕ СОБЫТИЕ");
    ///    Console.WriteLine($"  Код ключа: {args.KeyCode}");
    ///   Console.WriteLine($"  Время: {DateTime.Now:HH:mm:ss}");
    ///    Console.WriteLine();
    /// };

    ///     var serverThread = new Thread(() => Server.StartListener());
    /// serverThread.Start();
    /// </code>
    /// </example>
    /// </summary>
    [XmlRpcService]
    public class Server : XmlRpcListenerService, IDeviceService
    {
        public event Action<RsEventArgs> RsEventReceived;
        public event Action<KeyCodeEventArgs> KeyCodeReceived;

        public XmlRpcStruct GetDeviceListAsyncResult(GetDeviceListAsyncResultParams p)
        {
            Console.WriteLine("=== XML-RPC Request Received ===");
            Console.WriteLine($"Method: GetDeviceListAsyncResult");
            Console.WriteLine($"IP Server: {p.IPSERVER}");
            Console.WriteLine($"Original Method: {p.MethodName}");
            Console.WriteLine($"Result: {p.Result}");
            Console.WriteLine($"Message Type: {p.MessageType}");

            return GetBaseResponse();
        }

        public XmlRpcStruct OnRsEvent(XmlRpcStruct p)
        {
            Console.WriteLine("===НОВОЕ СОБЫТИЕ===");
            var guid = (string)p["GUID"];
            Console.WriteLine($"GUID: {guid}");
            XmlRpcStruct dataEvents = (XmlRpcStruct)p["DATAEVENTS"];
            Console.WriteLine((string)dataEvents["NAMETYPE"]);

            Console.WriteLine((int)dataEvents["EVENT"]);
            Console.WriteLine((string)dataEvents["NAMEEVENT"]);

            if ((XmlRpcStruct)dataEvents["EXTENDEDDATA"] != null)
            {
                XmlRpcStruct extendedData = (XmlRpcStruct)dataEvents["EXTENDEDDATA"];

                Console.WriteLine("Код:" + (string)extendedData["STRINGPASSWORD"]);
                Console.WriteLine("Код:" + (string)extendedData["RPCTYPEPASSWORD"]);
            }
            Console.WriteLine("=== End of ONRSEVENT ===");

            RsEventReceived?.Invoke(new RsEventArgs
            {
                Guid = guid,
                NameType = (string)dataEvents["NAMETYPE"],
                Event = (int)dataEvents["EVENT"],
                NameEvent = (string)dataEvents["NAMEEVENT"]
            });


            return GetBaseResponse();
        }

        public XmlRpcStruct GetKeyCode(GetKeyCodeParams p)
        {
            Console.WriteLine("===КЛЮЧ СЧИТАН===");
            Console.WriteLine(p.Result);

            KeyCodeReceived?.Invoke(new KeyCodeEventArgs
            {
                KeyCode = p.Result
            });

            return GetBaseResponse();
        }

        public XmlRpcStruct GetDeviceKeyList(XmlRpcStruct p)
        {
            return GetBaseResponse();
        }

        private static XmlRpcStruct GetBaseResponse()
        {
            Console.WriteLine();
            var resultData = new XmlRpcStruct();
            var response = new XmlRpcStruct
            {
                ["RESULT"] = "METHOD IS EXECUTE",
                ["RESULTDATA"] = resultData
            };

            return response;
        }

        /// <summary>
        /// Запуск сервера и его прослушивания
        /// </summary>
        
        public static void StartListener()
        {
            //Необходимо для того чтобы не было ошибки с кодировкой
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var service = new Server();
            var listener = new HttpListener();
            listener.Prefixes.Add("http://127.0.0.1:8095/");
            listener.Start();
            Console.WriteLine("Сервер запущен на http://127.0.0.1:8095/");
            Console.WriteLine();
            while (true)
            {
                var context = listener.GetContext();
                service.ProcessRequest(context);
            }

        }


    }
}
