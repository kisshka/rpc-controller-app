using Horizon.XmlRpc.Core;
using RpcApp.Domain;
using System.Text;
using System.Xml;


class Program
{
    static void Main()
    {
        //Необходимо для того чтобы не было ошибки с кодировкой
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        var client = new RequestSender();
        string guid = client.SetSubscribe();
        client.SetConfigurationHwSrv(guid);
        //client.GetDevice(guid);

        //client.GetDeviceListAsync();

        //client.ReadKeyCodeFromReader(guid);

        /*        client.GetPasswordListWithStatus(guid);
                client.ReadDeviceKeyList(guid);*/

        //client.SynchronizeOneKey(guid, 369);




        Console.ReadKey(true);
        SendFullConfiguration();

         Console.ReadKey(true);
        AddPerson();

        Console.ReadKey(true);
        UpdatePerson();

        Console.ReadKey(true);
        Deleteperson();



        void SendFullConfiguration()
        {
            List<XmlRpcStruct> tables = new List<XmlRpcStruct>();

            // 1. TimeWindows
            int[] calendar = TablesManage.CreateAlwaysActiveCalendar();
            XmlRpcStruct timeWindow = new XmlRpcStruct
            {
                ["ID"] = 1,
                ["Name"] = "Allways",  // Обрати внимание: "Name", а не "NAME"
                ["Calendar"] = calendar
            };

            tables.Add(CreateTable("TimeWindows", 0, new XmlRpcStruct[] { timeWindow }));

            // 2. Relays
            XmlRpcStruct relay1 = new XmlRpcStruct
            {
                ["ID"] = 1,
                ["DeviceAddress"] = new XmlRpcStruct
                {
                    ["ComPort"] = 3,
                    ["DeviceAddress"] = 1,
                    ["AggregateAddress"] = 1,
                    ["PKUAddress"] = 0
                }
            };

            XmlRpcStruct relay2 = new XmlRpcStruct
            {
                ["ID"] = 2,
                ["DeviceAddress"] = new XmlRpcStruct
                {
                    ["ComPort"] = 3,
                    ["DeviceAddress"] = 1,
                    ["AggregateAddress"] = 2,
                    ["PKUAddress"] = 0
                }
            };

            tables.Add(CreateTable("Relays", 0, new XmlRpcStruct[] { relay1, relay2 }));

            // 3. Readers
            XmlRpcStruct reader1 = new XmlRpcStruct
            {
                ["ID"] = 1,
                ["DeviceAddress"] = new XmlRpcStruct
                {
                    ["ComPort"] = 3,
                    ["DeviceAddress"] = 1,
                    ["AggregateAddress"] = 1,
                    ["PKUAddress"] = 0
                }
            };

            XmlRpcStruct reader2 = new XmlRpcStruct
            {
                ["ID"] = 2,
                ["DeviceAddress"] = new XmlRpcStruct
                {
                    ["ComPort"] = 3,
                    ["DeviceAddress"] = 1,
                    ["AggregateAddress"] = 2,
                    ["PKUAddress"] = 0
                }
            };

            tables.Add(CreateTable("Readers", 0, new XmlRpcStruct[] { reader1, reader2 }));

            // 4. Groups (обрати внимание: 3 элемента, где 2 и 3 - null!)
            XmlRpcStruct group1 = new XmlRpcStruct
            {
                ["ID"] = 3,
                ["Name"] = "Name"
            };

            // Создаем массив с null элементами
            List<object> groupsList = new List<object>
    {
        group1
    };

            XmlRpcStruct groupsTable = new XmlRpcStruct
            {
                ["DataList"] = groupsList.ToArray(),
                ["TableName"] = "Groups",
                ["Action"] = 0
            };
            tables.Add(groupsTable);

            // 5. AccessPoints
            XmlRpcStruct accessPoint = new XmlRpcStruct
            {
                ["ID"] = 1,
                ["Name"] = "accessPoint1",
                ["InKeyID"] = 1,
                ["OutKeyID"] = 2,
                ["Mode"] = 3,
                ["InCommand"] = 0,
                ["OutCommand"] = 0,
                ["IndexZone1"] = 0,
                ["IndexZone2"] = 0,
                ["InDuration"] = 0,
                ["OutDuration"] = 0,
                ["PointType"] = 3
            };

            tables.Add(CreateTable("AccessPoints", 0, new XmlRpcStruct[] { accessPoint }));

            // 6. RdrAccessPoints
            XmlRpcStruct rdrAccess1 = new XmlRpcStruct
            {
                ["ID"] = 1,
                ["ReaderID"] = 1,
                ["AccessPointID"] = 1,
                ["AccessMode"] = 0,
                ["Mode"] = 12
            };

            XmlRpcStruct rdrAccess2 = new XmlRpcStruct
            {
                ["ID"] = 2,
                ["ReaderID"] = 2,
                ["AccessPointID"] = 1,
                ["AccessMode"] = 0,
                ["Mode"] = 12
            };

            tables.Add(CreateTable("RdrAccessPoints", 0, new XmlRpcStruct[] { rdrAccess1, rdrAccess2 }));

            // 7. GroupAccesses
            XmlRpcStruct groupAccess = new XmlRpcStruct
            {
                ["ID"] = 1,
                ["GroupID"] = 3,
                ["Mode"] = 12,
                ["PardonTime"] = "30.12.1899 00:00:00",
                ["AccessID"] = 1,
                ["Antipassback"] = 0,
                ["Config"] = 3,
                ["ConfirmID"] = 0,
                ["ConfirmID2"] = 0,
                ["Flags"] = 0,
                ["TimeZone"] = 1
            };

            tables.Add(CreateTable("GroupAccesses", 0, new XmlRpcStruct[] { groupAccess }));

            Console.WriteLine("Отправляю полную конфигурацию...");
            client.RefreshTablesData(guid, tables.ToArray());
        }


        void AddPerson()
        {
            List<XmlRpcStruct> tableArray = new ();
            // ДОБАВЛЕНИЕ ЧЕЛОВЕКА
            XmlRpcStruct person = TablesManage.AddPerson(1, "jnujmjimk", "", "");
            XmlRpcStruct[] personsArray = [ person ];

            XmlRpcStruct personTable = new()
            {
                ["DataList"] = personsArray,
                ["TableName"] = "Persons",
                ["Action"] = 0,
            };


            //F70000DCBFDAA401
            int[] codeArray = [1, 164, 218, 191, 220, 0, 0, 247];
            XmlRpcStruct password = TablesManage.AddPassword(1, 4, 128, 1, 3,
                "04.12.2025 13:02:02", "04.12.2026 23:59:00", codeArray);
            XmlRpcStruct[] passwordsArray = [ password ];

            XmlRpcStruct passwordsTable = new()
            {
                ["DataList"] = passwordsArray,
                ["TableName"] = "Passwords",
                ["Action"] = 0,
            };

            tableArray.Add(personTable);
            tableArray.Add(passwordsTable);
            client.RefreshTablesData(guid, tableArray.ToArray());

        }


        void UpdatePerson()
        {
            List<XmlRpcStruct> tableArray = new List<XmlRpcStruct>();
            // ДОБАВЛЕНИЕ ЧЕЛОВЕКА
            XmlRpcStruct person = TablesManage.AddPerson(1, "jnujmjimk", "", "");
            XmlRpcStruct[] personsArray = [person];

            XmlRpcStruct personTable = new()
            {
                ["DataList"] = personsArray,
                ["TableName"] = "Persons",
                ["Action"] = 1,
            };


            //F70000DCBFDAA401
            int[] codeArray = [1, 164, 218, 191, 220, 0, 0, 247];
            XmlRpcStruct password = TablesManage.AddPassword(1, 4, 32896, 1, 3,
                "04.12.2025 13:02:02", "04.12.2026 23:59:00", codeArray);
            XmlRpcStruct[] passwordsArray = [password];

            XmlRpcStruct passwordsTable = new()
            {
                ["DataList"] = passwordsArray,
                ["TableName"] = "Passwords",
                ["Action"] = 1,
            };

            tableArray.Add(personTable);
            tableArray.Add(passwordsTable);
            client.RefreshTablesData(guid, tableArray.ToArray());

        }


        void Deleteperson()
        {
            List<XmlRpcStruct> tableArray = new List<XmlRpcStruct>();
            int[] codeArray = [1, 164, 218, 191, 220, 0, 0, 247];
            XmlRpcStruct person = TablesManage.AddPerson(1, "jnujmjimk", "", "");
            XmlRpcStruct password = TablesManage.AddPassword(1, 4, 128, 1, 3,
    "04.12.2025 13:02:02", "04.12.2026 23:59:00", codeArray);

            // УДАЛЕНИЕ ЧЕЛОВЕКА
            XmlRpcStruct[] personsArrayDelete = [ person ];

            XmlRpcStruct personTableDelete = new()
            {
                ["DataList"] = personsArrayDelete,
                ["TableName"] = "Persons",
                ["Action"] = 0,
            };

            XmlRpcStruct[] passwordsArrayDelete = [ password ];

            XmlRpcStruct passwordsTableDelete = new XmlRpcStruct()
            {
                ["DataList"] = passwordsArrayDelete,
                ["TableName"] = "Passwords",
                ["Action"] = 2,
            };
            
            tableArray.Add(personTableDelete);
            tableArray.Add(passwordsTableDelete);
            client.RefreshTablesData(guid, tableArray.ToArray());
        }

        XmlRpcStruct CreateTable(string tableName, int action, XmlRpcStruct[] dataList)
        {
            return new XmlRpcStruct
            {
                ["DataList"] = dataList,
                ["TableName"] = tableName,
                ["Action"] = action
            };
        }

    }
}