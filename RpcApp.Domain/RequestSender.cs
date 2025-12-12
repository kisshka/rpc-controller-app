using Horizon.XmlRpc.Core;
using Horizon.XmlRpc.Client;
using RpcApp.Domain.Structures;
using System.Text;

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

        [XmlRpcMethod("GETDEVICE")]
        XmlRpcStruct GetDevice(GetDeviceParams p);

        [XmlRpcMethod("GetDeviceListAsync")]
        XmlRpcStruct GetDeviceListAsync(GetDeviceListAsyncParams p);

        [XmlRpcMethod("CONTROLOBJECTS")]
        XmlRpcStruct ControlObjects (ControlObjectsParams p);

        [XmlRpcMethod("ReadKeyCodeFromReader")]
        XmlRpcStruct ReadKeyCodeFromReader(ReadKeyCodeFromReaderParams p);

        [XmlRpcMethod("GetPasswordListWithStatus")]
        XmlRpcStruct GetPasswordListWithStatus(GetPasswordListWithStatusParams p);

        [XmlRpcMethod("ReadDeviceKeyList")]
        void ReadDeviceKeyList(ReadDeviceKeyListParams p);

        [XmlRpcMethod("ReadConfiguration")]
        void ReadConfiguration(ReadConfigurationParams p);

        [XmlRpcMethod("REFRESHTABLESDATA")]
        public void RefreshTablesData(XmlRpcStruct p);
    }

    /// <summary>
    /// Класс, реализующий методы для отправки XML-запросов к веб серверу Orion
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
        /// <exception cref="Exception">Неопределенная ошибка</exception>
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
                    ipServer = "127.0.0.1",
                    portServer = 8095,
                    scribePorts = "SCRIBEALLPORTS",
                    ports = [
                        new SubscribePort() {
                            addPort = 3,
                            scribeDevices = "SCRIBEALLDEVICES"
                        }
                    ]
                };

                Console.WriteLine("Отправляем запрос SetSubscribe...");
                var response = _rpcClient.SetSubscribe(subscribeParams) ?? throw new Exception("Пустой ответ от сервера");

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
        /// <param name="guid">жетон безопасности</param>
        /// <exception cref="Exception">Неопределенная ошибка</exception>
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
                    ipServer = "127.0.0.1",
                    portServer = 8095
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
        /// <param name="guid">жетон безопасности</param>
        /// <exception cref="Exception">Неопределенная ошибка</exception>
        /// <exception cref="XmlRpcFaultException">Ошибка xml-rpc</exception>
        public void SetConfigurationHwSrv(string guid)
        {
            try
            {
                Adress address = new()
                {
                    addrdevice = 1,
                    addrPult = 0,
                    addrPort = 3,
                };

                SetConfigurationHwSrvParams configurationParams = new()
                {
                    guid = guid,
                    ports = [
                    new Ports()
                    {

                        typePort = "COM",
                        numberPort = 3,
                        typeProtocol = 1,
                        typeConverter = 2,
                        portPriority = 0,
                        portBaud = 9600,
                        rpcStatePort = "OPEN",
                        loopState = "ACTIVE"
                    }
                    ],
                    devices = [
                      new Device ()
                        {
                         address = address,
                         rpcStatusDevice = "ON",
                         type = 16,
                        priorityDevice = 0,
                        version = 220,
                        }
                    ]
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

        /// <summary>
        /// Отправка запроса GetDevice -  Узнать состояние и информацию о устройстве
        /// </summary>
        /// <param name="guid">жетон безопасности</param>
        /// <exception cref="Exception">Неопределенная ошибка</exception>
        /// <exception cref="XmlRpcFaultException">Ошибка xml-rpc</exception>
        public void GetDevice(string guid)
        {
            try
            {
                GetDeviceParams getDeviceParams = new()
                {
                    guid = guid,
                    ipServer = "127.0.0.1",
                    portServer = 8095,
                    resultReturnMethod = "ASYNCRESULT",
                    devices = [
                        new Adress()
                        {
                            addrdevice = 1,
                            addrPort = 3
                        }
                    ],
                    returnDataFromClient = new()
                    {
                        clientMessage = "my message"
                    }
                };

                Console.WriteLine("Отправляем запрос GetDevice...");
                var response = _rpcClient.GetDevice(getDeviceParams) ?? throw new Exception("Пустой ответ от сервера");

                string result = (string)response["RESULT"];
                Console.WriteLine(response);
                Console.WriteLine($"Результат: {result}");


                if (result == "METHOD IS EXECUTE")
                {
                    Console.WriteLine($"Информация об устройстве получена");
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
        /// Отправка запроса GetDeviceListAsync -  Узнать состояние и информацию о устройстве
        /// </summary>
        /// <param name="guid">жетон безопасности</param>
        /// <exception cref="Exception">Неопределенная ошибка</exception>
        /// <exception cref="XmlRpcFaultException">Ошибка xml-rpc</exception>
        public void GetDeviceListAsync()
        {
            GetDeviceListAsyncParams getDeviceListAsyncParams = new()
            {
                ipServer = "127.0.0.1",
                portserver = 8095,
                methodNameForAnswer = "GetDeviceListAsyncResult"
            };

            Console.WriteLine("Отправляем запрос GetDeviceListAsync...");
            _ = _rpcClient.GetDeviceListAsync(getDeviceListAsyncParams);
        }

        /// <summary>
        /// Отправка запроса ControlObjects -  упраление взятием снятием различных объектов охранной системы
        /// </summary>
        /// <exception cref="Exception">Неопределенная ошибка</exception>
        /// <exception cref="XmlRpcFaultException">Ошибка xml-rpc</exception>
        public void ControlObjects(string guid)
        {
            try
            {
                Adress2 address = new()
                {
                    addrDevice = 127,
                    addPort = 3,
                    addRelement = 1
                };

                ControlObjectsParams controlObjectsParams = new()
                {
                    guid = guid,
                    objects = [
                        new Objects()
                        {
                            typeObject = "ZONE",
                            controlWord = "ARM",
                            address = address,
                        }
                    ]
                };

                Console.WriteLine("Отправляем запрос ControlObjects...");
                var response = _rpcClient.ControlObjects(controlObjectsParams) ?? throw new Exception("Пустой ответ от сервера");

                string result = (string)response["RESULT"];
                Console.WriteLine(response);
                Console.WriteLine($"Результат: {result}");


                if (result == "METHOD IS EXECUTE")
                {
                    Console.WriteLine($"Информация об устройстве получена");
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
        /// Отправка запроса ReadKeyCodeFromReader -  Считать и передать ключ карты
        /// </summary>
        /// <param name="guid">жетон безопасности</param>
        /// <exception cref="Exception">Неопределенная ошибка</exception>
        /// <exception cref="XmlRpcFaultException">Ошибка xml-rpc</exception>
        public void ReadKeyCodeFromReader(string guid)
        {
            ReadKeyCodeFromReaderParams keyCodeFromReaderParams = new()
            {
                guid = guid,
                ipServer = "127.0.0.1",
                portServer = 8095,
                methodNameForAnswer = "GetKeyCode",
                ComPort = 3,
                PKUAddress = 0,
                DeviceAddress = 1,
                DeviceType = 16,
                AggregateAddress = 1,
            };

            Console.WriteLine("Отправляем запрос ReadKeyCodeFromReader...");
            _ = _rpcClient.ReadKeyCodeFromReader(keyCodeFromReaderParams);
        }

        public void GetPasswordListWithStatus(string guid)
        {
            GetPasswordListWithStatusParams passwordListParams = new()
            {
                guid = guid,
                ipServer = "127.0.0.1",
                portServer = 8095,
                methodNameForAnswer = "ReturnPasswordList",
            };

            Console.WriteLine("Отправляем запрос ReadKeyCodeFromReader...");
            _ = _rpcClient.GetPasswordListWithStatus(passwordListParams);
        }

        public void ReadDeviceKeyList(string guid)
        {
            ReadDeviceKeyListParams deviceKeyListParams = new()
            {
                guid = guid,
                ipServer = "127.0.0.1",
                portServer = 8095,
                methodNameForAnswer = "GetDeviceKeyList",
                PKUAddress = 0,
                DeviceAddress = 1,
                ReaderAddress = 1,
            };

            Console.WriteLine("Отправляем запрос ReadKeyCodeFromReader...");
            _rpcClient.ReadDeviceKeyList(deviceKeyListParams);
        }

        /// <summary>
        /// Отправка запроса ReadConfiguration -  Считать конфигурацию из устройства
        /// </summary>
        /// <param name="guid">жетон безопасности</param>
        /// <exception cref="Exception">Неопределенная ошибка</exception>
        /// <exception cref="XmlRpcFaultException">Ошибка xml-rpc</exception>
        public void ReadConfiguration(string guid)
        {
            ReadConfigurationParams configurationParams = new()
            {
                guid = guid,
                ipServer = "127.0.0.1",
                portServer = 8095,
                methodNameForAnswer = "GetConfiguration",
                ComPort = 3,
                PKUAddress = 0,
                DeviceAddress = 1,
                ReaderAddress = 1,
            };

            Console.WriteLine("Отправляем запрос ReadConfiguration...");
            _rpcClient.ReadConfiguration(configurationParams);
        }

        /// <summary>
        /// Отправка запроса GetDevice -  Узнать состояние и информацию о устройстве
        /// </summary>
        /// <param name="guid">жетон безопасности</param>
        /// <param name="tableArray">Редактируемые таблицы</param>
        /// <exception cref="Exception">Неопределенная ошибка</exception>
        /// <exception cref="XmlRpcFaultException">Ошибка xml-rpc</exception>
        public void RefreshTablesData(string guid, XmlRpcStruct[] tableArray)
        {
            XmlRpcStruct parameters = new()
            {
                ["GUID"] = guid,
                ["MethodNameForAnswer"] = "RefreshTablesResult",
                ["TableList"] = tableArray,
                ["IPSERVER"] = "127.0.0.1",
                ["PORTSERVER"] = 8095
            };

            Console.WriteLine("Отправляем запрос RefreshTablesData...");
            Console.WriteLine($"Таблиц: {tableArray.Length}");

            _rpcClient.RefreshTablesData(parameters);
        }

    }
}
