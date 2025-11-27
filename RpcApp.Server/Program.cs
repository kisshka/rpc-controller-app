using System.Net;
using static RpcApp.Server.Program;
using Horizon.XmlRpc.Server;
using RpcApp.Domain;
using Horizon.XmlRpc.Core;
using System;
using System.Text;


namespace RpcApp.Server
{
    public interface IDeviceService
    {
        [XmlRpcMethod("SETSUBSCRIBE")]
        XmlRpcStruct SetSubscribe(Params p);

        [XmlRpcMethod("GetDeviceListAsyncResult")]
        XmlRpcStruct GetDeviceListAsyncResult(RequestData p);
    }
    public class Params
    {
        public string login { get; set; }
        public string password { get; set; }
        public int scribe { get; set; }
        public string scribePorts { get; set; }
        public string ipServer { get; set; }
        public int portServer { get; set; }
    }

    [XmlRpcService]
    public class AddService : XmlRpcListenerService, IDeviceService
    {
        public XmlRpcStruct SetSubscribe(Params subscribeParams)
        {
            Console.WriteLine("=== SetSubscribe Request Received ===");
            Console.WriteLine($"Login: {subscribeParams.login}");
            Console.WriteLine($"Password: {subscribeParams.password}");
            Console.WriteLine($"Scribe: {subscribeParams.scribe}");
            Console.WriteLine($"ScribePorts: {subscribeParams.scribePorts}");
            Console.WriteLine($"IP Server: {subscribeParams.ipServer}");
            Console.WriteLine($"Port Server: {subscribeParams.portServer}");

            string guid = Guid.NewGuid().ToString();
            Console.WriteLine($"Generated GUID: {guid}");

            var resultData = new XmlRpcStruct();
            resultData["GUID"] = guid;

            var response = new XmlRpcStruct();
            response["RESULT"] = "METHOD IS EXECUTE";
            response["RESULTDATA"] = resultData;

            Console.WriteLine("=== Response sent ===");
            return response;
        }
        public XmlRpcStruct GetDeviceListAsyncResult(RequestData requestData)
        {
            Console.WriteLine("=== XML-RPC Request Received ===");
            Console.WriteLine($"Method: GetDeviceListAsyncResult");
            Console.WriteLine($"IP Server: {requestData.IPSERVER}");
            Console.WriteLine($"Original Method: {requestData.MethodName}");
            Console.WriteLine($"Result: {requestData.Result}");
            Console.WriteLine($"Message Type: {requestData.MessageType}");

            var resultData = new XmlRpcStruct();

            var response = new XmlRpcStruct();
            response["RESULT"] = "METHOD IS EXECUTE";
            response["RESULTDATA"] = resultData;

            Console.WriteLine("=== Response sent ===");
            return response;
        }

    }
    internal class Program
    {
        static void Main() {
            //Необходимо для того чтобы не было ошибки с кодировкой
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var service = new AddService();
            var listener = new HttpListener();
            listener.Prefixes.Add("http://127.0.0.1:8095/");
            listener.Start();
            Console.WriteLine("Сервер запущен на http://127.0.0.1:8095/");
            while (true)
            {
                var context = listener.GetContext();
                service.ProcessRequest(context);
            }

        }

    }

}
