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

        [XmlRpcMethod("GetDeviceListAsyncResult")]
        XmlRpcStruct GetDeviceListAsyncResult(RequestData p);
    }

    [XmlRpcService]
    public class AddService : XmlRpcListenerService, IDeviceService
    {
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
