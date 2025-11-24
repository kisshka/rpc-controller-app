using CookComputing.XmlRpc;

namespace RpcApp.Domain
{
    [XmlRpcUrl("http://localhost:8080/")]

    public interface IRpcProxy : IXmlRpcProxy
    {
        [XmlRpcMethod("SETSUBSCRIBE")]
        XmlRpcStruct SetSubscribe(SetSubscribeParams p);

        [XmlRpcMethod("CLOSESCRIBE")]
        XmlRpcStruct CloseScribe(CloseScribeParams p);

        [XmlRpcMethod("SETCONFIGURATIONHWSRV")]
        XmlRpcStruct SetConfigurationHwSrw(SetConfigurationHwSrvParams p);
    }

    /// <summary>
    /// Класс, содержащий в себе методы для отправки XML-запросов к веб серверу Orion
    /// </summary>
    public class RequestSender
    {
        private readonly IRpcProxy _rpcClient;

        /// <summary>
        /// Создает клиент XML-RPC сервера при объявлении экземпляра класса  
        /// </summary>
        public RequestSender()
        {
            _rpcClient = XmlRpcProxyGen.Create<IRpcProxy>();
        }

        /// <summary>
        /// Отправка запроса SetSubscribe - Подпись клиента к модулю управления
        /// </summary>
        /// <exception cref="Exception"></exception>
        /// <exception cref="XmlRpcFaultException">Ошибка xml-rpc</exception>
        /// <returns name="guid">При успешной отправке XML-запроса возвращает жетон безопасности</returns> 

        public string SetSubscribe()
        {
            try
            {
                SetSubscribeParams subscribeParams = new()
                {
                    login = "ADMINISTRATOR",
                    password = "ORION",
                    scribe = 65535,
                    scribeports = "SCRIBEALLPORTS",
                    ipserver = "127.0.0.1",
                    portserver = 8095
                };

                Console.WriteLine("Отправляем запрос SetSubscribe...");
                var response = _rpcClient.SetSubscribe(subscribeParams) ??  throw new Exception("Пустой ответ от сервера");

                string result = (string)response["RESULT"];
                Console.WriteLine($"Результат: {result}");


                if (result == "METHOD IS EXECUTE")
                {
                    if (response.ContainsKey("RESULTDATA"))
                    {
                        XmlRpcStruct resultData = (XmlRpcStruct)response["RESULTDATA"];
                        string guid = (string)resultData["GUID"];
                        Console.WriteLine($"Подписка успешно оформлена. GUID: {guid}");
                        return guid;
                    }
                    else
                    {
                        Console.WriteLine("Подписка успешна, но GUID не получен");
                        throw new Exception("Подписка успешна, но GUID не получен");
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

        /// <summary>
        /// Отправка запроса CloseScribe - Отписаться от клиента 
        /// </summary>
        /// <exception cref="Exception"></exception>
        /// <exception cref="XmlRpcFaultException">Ошибка xml-rpc</exception>
        public void CloseScribe(string guid)
        {
            try
            {
                CloseScribeParams closeSubscribeParams = new()
                {
                    guid = guid,
                    login = "ADMINISTRATOR",
                    password = "ORION",
                    ipserver = "127.0.0.1",
                    portserver = 8095
                };

                Console.WriteLine("Отправляем запрос CloseScribe...");
                var response = _rpcClient.CloseScribe(closeSubscribeParams) ?? throw new Exception("Пустой ответ от сервера");

                string result = (string)response["RESULT"];
                Console.WriteLine($"Результат: {result}");


                if (result == "METHOD IS EXECUTE")
                {
                    Console.WriteLine("Метод выполнен. Действие подписки прекращено");
                }
                else
                {
                    throw new Exception($"Ошибка: {result}");
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

        /// <summary>
        /// Отправка запроса SetConfigurationHwSrv - Передать конфигурацию в модуль управления
        /// </summary>
        /// <exception cref="Exception"></exception>
        /// <exception cref="XmlRpcFaultException">Ошибка xml-rpc</exception>
        public void SetConfigurationHwSrv(string guid)
        {
            try
            {
                SetConfigurationHwSrvParams configurationParams = new()
                {
                    guid = guid,
                    ports = new Ports()
                    {
                        numberport = 3,
                        typeport = "COM",
                        typeprotocol = 1,
                        typeconverter = 2,
                        portpriority = 3,
                        portbaund = 9600,
                        portstate = "OPEN",
                        loopstate = "ACTIVE"
                    }
                };


                Console.WriteLine("Отправляем запрос SetConfigurationHwSrw...");
                var response = _rpcClient.SetConfigurationHwSrw(configurationParams) ?? throw new Exception("Пустой ответ от сервера");
                string result = (string)response["RESULT"];
                Console.WriteLine($"Результат: {result}");

                if (result == "METHOD IS EXECUTE")
                {
                    Console.WriteLine("Метод выполнен. Конфигурация успешно установлена");
                }
                else
                {
                    throw new Exception($"Ошибка: {result}");
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

    }
}