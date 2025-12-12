using Horizon.XmlRpc.Core;
using Horizon.XmlRpc.Server;
using RpcApp.Domain.Structures;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RpcApp.Domain
{
    public interface IDeviceService
    {

        [XmlRpcMethod("GetDeviceListAsyncResult")]
        XmlRpcStruct GetDeviceListAsyncResult(XmlRpcStruct p);

        [XmlRpcMethod("ONRSEVENT")]
        XmlRpcStruct OnRsEvent(XmlRpcStruct p);


        [XmlRpcMethod("GetKeyCode")]
        XmlRpcStruct GetKeyCode(GetKeyCodeParams p);

        [XmlRpcMethod("GetDeviceKeyList")]
        XmlRpcStruct GetDeviceKeyList(XmlRpcStruct p);

        [XmlRpcMethod("ONINITIATIVE")]
        XmlRpcStruct OnInitiative(XmlRpcStruct p);
    }


    /// <summary>
    ///  Сервер для прослушивания событий, приходящих от ядра Орион
    ///  Запускается по адресу http://127.0.0.1:8095/
    ///  По этому же адресу доступна документация всех запросов, которые обрабатывает сервер.
    ///  </summary>
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

    [XmlRpcService]
    public class Server : XmlRpcListenerService, IDeviceService
    {
        public event EventHandler<RsEventArgs> RsEventReceived;
        public event EventHandler<KeyCodeEventArgs> KeyCodeReceived;
        public event EventHandler<DeviceDataEventArgs> DeviceDataReceived;

        public XmlRpcStruct GetDeviceListAsyncResult(XmlRpcStruct p)
        {
            Console.WriteLine("=== Получен список устройств ===");

                var deviceData = new DeviceDataEventArgs();
                var comPortList = (object[])p["ComPortList"];

                foreach (var portObj in comPortList)
                {
                    var port = (XmlRpcStruct)portObj;

                    var comPortInfo = new ComPortInfo
                    {
                        PortNumber = (int)port["ComPort"]
                    };

                    var deviceList = (object[])port["DeviceList"];

                    foreach (var deviceObj in deviceList)
                    {
                        var device = (XmlRpcStruct)deviceObj;

                        var controller = new ControllerInfo
                        {
                            Address = (int)device["DeviceAddress"],
                            DeviceType = (int)device["DeviceType"],
                            OnConnect = (int)device["OnConnect"]
                        };

                        // Реле
                        if (device.ContainsKey("RelayList"))
                        {
                            var relayList = (object[])device["RelayList"];

                            foreach (var relayObj in relayList)
                            {
                                var relay = (XmlRpcStruct)relayObj;

                                var relayInfo = new RelayInfo
                                {
                                    Id = (int)relay["ID"],
                                    Address = (int)relay["Address"],
                                    State = (int)relay["State"]
                                };

                                controller.Relays.Add(relayInfo);
                            }
                        }

                        comPortInfo.Controllers.Add(controller);
                    }

                    deviceData.ComPorts.Add(comPortInfo);
                }

                DeviceDataReceived?.Invoke(this, deviceData);

            return GetBaseResponse();
        }

        public XmlRpcStruct OnRsEvent(XmlRpcStruct p)
        {
            Console.WriteLine("===НОВОЕ СОБЫТИЕ===");
            XmlRpcStruct dataEvents = (XmlRpcStruct)p["DATAEVENTS"];
            Console.WriteLine((string)dataEvents["NAMETYPE"]);

            Console.WriteLine((int)dataEvents["EVENT"]);
            Console.WriteLine((string)dataEvents["NAMEEVENT"]);

            int idPerson;
            if (dataEvents.ContainsKey("IDPERSON"))
            {
                idPerson = (int)dataEvents["IDPERSON"];
            }
            else idPerson = -1;

            if ((XmlRpcStruct)dataEvents["EXTENDEDDATA"] != null)
            {
                XmlRpcStruct extendedData = (XmlRpcStruct)dataEvents["EXTENDEDDATA"];

                Console.WriteLine("Код:" + (string)extendedData["STRINGPASSWORD"]);
                Console.WriteLine("Код:" + (string)extendedData["RPCTYPEPASSWORD"]);
            }
            Console.WriteLine("=== End of ONRSEVENT ===");

            //Получение устройства
            var address = (XmlRpcStruct)dataEvents["ADDRESS"];
            int rele = -1;
            if (address.Contains("ADDRELEMENT") )
                rele = (int)address["ADDRELEMENT"]; 

            RsEventReceived?.Invoke(this, new RsEventArgs
            {
                IdPerson = idPerson,
                NameType = (string)dataEvents["NAMETYPE"],
                Event = (int)dataEvents["EVENT"],
                NameEvent = (string)dataEvents["NAMEEVENT"],
                Type = (int)dataEvents["TYPE"],
                Device = (int)address["ADDRDEVICE"],
                Rele = rele
        });

            return GetBaseResponse();
        }

        public XmlRpcStruct GetKeyCode(GetKeyCodeParams p)
        {
            Console.WriteLine("===КЛЮЧ СЧИТАН===");
            Console.WriteLine(p.Result);

            KeyCodeReceived?.Invoke(this, new KeyCodeEventArgs
            {
                KeyCode = p.Result
            });

            return GetBaseResponse();
        }

        public XmlRpcStruct GetDeviceKeyList(XmlRpcStruct p)
        {
            return GetBaseResponse();
        }

        public XmlRpcStruct OnInitiative (XmlRpcStruct p)
        {
            Console.WriteLine("===ПРИШЛО СОБЫТИЕ OnInitiative ===");

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
        /// Запуск сервера и его прослушивания.
        /// Рекомендуется использовать для листенера отдельный поток или асинхронный запуск
        /// </summary>
        
        public void StartListener()
        {
            //Необходимо для того чтобы не было ошибки с кодировкой
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var listener = new HttpListener();
            listener.Prefixes.Add("http://127.0.0.1:8095/");
            listener.Start();

            Console.WriteLine("Сервер запущен на http://127.0.0.1:8095/");
            Console.WriteLine();
            while (true)
            {
                var context = listener.GetContext();
                this.ProcessRequest(context);
            }

        }


    }
}
