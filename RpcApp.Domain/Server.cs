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

    [XmlRpcService]
    public class Server : XmlRpcListenerService, IDeviceService
    {
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
            Console.WriteLine($"GUID: {(string)p["GUID"]}");
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


            return GetBaseResponse();
        }

        public XmlRpcStruct GetKeyCode(GetKeyCodeParams p)
        {
            Console.WriteLine("===КЛЮЧ СЧИТАН===");
            Console.WriteLine(p.Result);

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
