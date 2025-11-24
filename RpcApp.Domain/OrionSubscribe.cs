using CookComputing.XmlRpc;
using System;
using System.Net;
using System.Text;

namespace RpcApp.Domain
{
    [XmlRpcUrl("http://localhost:8080/")]

    public interface IRpcProxy : IXmlRpcProxy
    {
        [XmlRpcMethod("SETSUBSCRIBE")]
        XmlRpcStruct SetSubscribe(SetSubscribeParams p);
    }

    /// <summary>
    /// Структура параметров подписки
    /// </summary>
    public struct SetSubscribeParams
    {
        public string Login;
        public string password;
        public int scribe;
        public string ipserver;
        public int portserver;
        public string scribeports;
    }

    /// <summary>
    /// Класс для осуществления подписки клиента к модулю управления с запросом жетона безопасности
    /// </summary>
    public class OrionSubscribe
    {
        private readonly IRpcProxy _rpcClient;

        public OrionSubscribe()
        {
            _rpcClient = XmlRpcProxyGen.Create<IRpcProxy>();

           // _rpcClient.XmlEncoding = Encoding.GetEncoding("windows-1251");
           //_rpcClient.UseEmptyParamsTag = true;
           //_rpcClient.UseIndentation = false;
           // _rpcClient.NonStandard = XmlRpcNonStandard.All;
        }

        /// <summary>
        /// Отправка запроса на подписку
        /// </summary>
        /// <exception cref="Exception"></exception>
        /// <exception cref="XmlRpcFaultException">Ошибка xml-rpc</exception>
        public void Subscribe()
        {
            try
            {
                XmlRpcStruct requestParams = new XmlRpcStruct();

                SetSubscribeParams subscribeParams = new SetSubscribeParams
                {
                    Login = "ADMINISTRATOR",
                    password = "ORION",
                    scribe = 65535,
                    scribeports = "SCRIBEALLPORTS",
                    ipserver = "",
                    portserver = 8095
                };

                //XmlRpcStruct deviceStruct = new XmlRpcStruct();
                //deviceStruct["ADDRDEVICE"] = 70;
                //deviceStruct["ADDRPULT"] = 4;
                //deviceStruct["ADDRPORT"] = 3;

                //XmlRpcStruct[] devicesArray = new XmlRpcStruct[] { deviceStruct };
                //requestParams["DEVICES"] = devicesArray;


                Console.WriteLine("Отправляем запрос SetSubscribe...");
                var response = _rpcClient.SetSubscribe(subscribeParams);

                // Ответ
                if (response == null)
                    throw new Exception("Пустой ответ от сервера");

                string result = (string)response["RESULT"];
                Console.WriteLine($"Результат: {result}");


                if (result == "METHOD IS EXECUTE")
                {
                    if (response.ContainsKey("RESULTDATA"))
                    {
                        XmlRpcStruct resultData = (XmlRpcStruct)response["RESULTDATA"];
                        string guid = (string)resultData["GUID"];
                        Console.WriteLine($"Подписка успешно оформлена. GUID: {guid}");
                    }
                    else
                    {
                        Console.WriteLine("Подписка успешна, но GUID не получен");
                    }
                }
                else
                {
                    throw new Exception($"Ошибка подписки: {result}");
                }
            }
            catch (XmlRpcFaultException ex)
            {
                throw new Exception($"XML-RPC ошибка: {ex.FaultCode} - {ex.FaultString}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка: {ex.Message}");
            }
        }

// /// <summary>
// /// Метод для тестирования ручной отправки к серверу запроса на подписку
// /// </summary>
//        public void DebugRawHttp()
//        {
//            try
//            {
//                string xmlRequest = @"<?xml version=""1.0""?>
//<methodCall>
//    <methodName>SETSUBSCRIBE</methodName>
//    <params>
//        <param>
//            <value>
//                <struct>
//                    <member>
//                        <name>LOGIN</name>
//                        <value><string>ADMINISTRATOR</string></value>
//                    </member>
//                    <member>
//                        <name>PASSWORD</name>
//                        <value><string>ORION</string></value>
//                    </member>
//                    <member>
//                        <name>SCRIBE</name>
//                        <value><int>65535</int></value>
//                    </member>
//                    <member>
//                        <name>SCRIBEPORTS</name>
//                        <value><string>SCRIBEALLPORTS</string></value>
//                    </member>
//                </struct>
//            </value>
//        </param>
//    </params>
//</methodCall>";

//                using (var client = new WebClient())
//                {
//                    client.Headers[HttpRequestHeader.ContentType] = "text/xml; charset=utf-8";
//                    client.Headers[HttpRequestHeader.UserAgent] = "XmlRpcCS";

//                    Console.WriteLine("🔍 Отправляем RAW HTTP запрос...");

//                    string response = client.UploadString("http://localhost:8080/", xmlRequest);

//                    Console.WriteLine("📨 RAW RESPONSE:");
//                    Console.WriteLine("=== START ===");
//                    Console.WriteLine(response);
//                    Console.WriteLine("=== END ===");
//                }
//            }
//            catch (WebException webEx)
//            {
//                Console.WriteLine($"🌐 WEB EXCEPTION: {webEx.Status}");

//                if (webEx.Response is HttpWebResponse httpResponse)
//                {
//                    Console.WriteLine($"HTTP {httpResponse.StatusCode}: {httpResponse.StatusDescription}");
//                    Console.WriteLine($"Content-Type: {httpResponse.ContentType}");

//                    using (var stream = httpResponse.GetResponseStream())
//                    using (var reader = new StreamReader(stream))
//                    {
//                        string errorContent = reader.ReadToEnd();
//                        Console.WriteLine("ERROR CONTENT:");
//                        Console.WriteLine(errorContent);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"❌ EXCEPTION: {ex.Message}");
//            }
//        }
    }
}