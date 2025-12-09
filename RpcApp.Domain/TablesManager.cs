using Horizon.XmlRpc.Core;
using RpcApp.Domain.Structures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RpcApp.Domain
{
    public class TablesManager
    {
        RequestSender client = new ();

        /// <summary>Метод, который генерирует и отправляет запрос на добавление базовой конфигурации.
        /// Конфигурацию следует устанавливать до добавления в устройство сотрудников и паролей.
        /// Включает таблицы TimeWindows, Relays, Readers, Groups, AccessPoints, RdrAccessPoints и GroupAccesses.
        /// </summary>
        public void SendBaseConfiguration( string guid)
        {
            List<XmlRpcStruct> tables = [];

            // Временные окна
            int[] calendar = TablesManager.CreateAlwaysActiveCalendar();
            XmlRpcStruct timeWindow = TablesManager.AddTimeWindows(1, calendar, "always");
            tables.Add(CreateTable("TimeWindows", 0, [timeWindow]));

            // Реле
            XmlRpcStruct relay1 = TablesManager.AddRelay(1, 3, 1, 1);
            XmlRpcStruct relay2 = TablesManager.AddRelay(2, 3, 1, 2);
            tables.Add(CreateTable("Relays", 0, [relay1, relay2]));

            // Считыватели
            XmlRpcStruct reader1 = TablesManager.AddReader(1, 3, 1, 1);
            XmlRpcStruct reader2 = TablesManager.AddReader(2, 3, 1, 2);
            tables.Add(CreateTable("Readers", 0, [reader1, reader2]));

            // Группы доступа
            XmlRpcStruct group1 = TablesManager.AddGroup(3, "Pupil");
            tables.Add(CreateTable("Groups", 0, [group1]));

            // Точки доступа
            XmlRpcStruct accessPoint = TablesManager.AddAccessPoint(1, "accessPoint1", 1, 2, 3);
            tables.Add(CreateTable("AccessPoints", 0, [accessPoint]));

            // Связи считывателей и точек доступа
            XmlRpcStruct rdrAccess1 = TablesManager.AddRdrAccessPoint(1, 1, 1);
            XmlRpcStruct rdrAccess2 = TablesManager.AddRdrAccessPoint(2, 2, 1);
            tables.Add(CreateTable("RdrAccessPoints", 0, [rdrAccess1, rdrAccess2]));

            // Уровни доступа
            XmlRpcStruct groupAccess = TablesManager.AddGroupAccess(1, 3, 1);
            tables.Add(CreateTable("GroupAccesses", 0, [groupAccess]));

            Console.WriteLine("Отправка полной конфигурации");
            client.RefreshTablesData(guid, tables.ToArray());
        }


        /// <summary> Метод для добавления сотрудника и его пароля в устройство</summary>
        /// <example>
        /// Пример валидных данных для отправки запроса
        /// <code>
        /// string guid = client.SetSubscribe();
        /// TablesManager tablesManager = new();
        /// tablesManager.AddPersonWithPassword(guid, 1, "Alexei", " ", " ", 1 , 4, 128, 1, 3, "04.12.2025 13:02:02", "04.12.2026 23:59:00", [1, 164, 218, 191, 220, 0, 0, 247]);
        /// </code>
        /// </example>

        public void AddPersonWithPassword(string guid, int personId, string personName, string personSurname, string personMidName,
                                    int passwordId, int codeType, int config, int passwordIdOwner, int passwordGroupId, string start, string finish, int[] code)
        {
            List<XmlRpcStruct> tables = [];

            XmlRpcStruct person = TablesManager.AddPerson(personId, personName, personSurname, personMidName);
            tables.Add(CreateTable("Persons", 0, [person]));

            XmlRpcStruct password = TablesManager.AddPassword(passwordId, codeType, config, passwordIdOwner, passwordGroupId, start, finish, code);
            tables.Add(CreateTable("Passwords", 0, [password]));

            client.RefreshTablesData(guid, tables.ToArray());
        }


        public void UpdatePerson()
        {

        }

        /// <summary>Метод для удаления сотруника и его пароля из устройства</summary>

         public void DeletePersonWithPassword(string guid, int personId, string personName, string personSurname, string personMidName,
                                    int passwordId, int codeType, int config, int passwordIdOwner, int passwordGroupId, string start, string finish, int[] code)
        {
            List<XmlRpcStruct> tables = [];

            XmlRpcStruct person = TablesManager.AddPerson(personId, personName, personSurname, personMidName);
            tables.Add(CreateTable("Persons", 0, [person]));

            XmlRpcStruct password = TablesManager.AddPassword(passwordId, codeType, config, passwordIdOwner, passwordGroupId, start, finish, code);
            tables.Add(CreateTable("Passwords", 2, [password]));

            client.RefreshTablesData(guid, tables.ToArray());
        }


        public XmlRpcStruct CreateTable(string tableName, int action, XmlRpcStruct[] dataList)
        {
            return new XmlRpcStruct
            {
                ["DataList"] = dataList,
                ["TableName"] = tableName,
                ["Action"] = action
            };
        }






        public static XmlRpcStruct AddPerson(int id, string Name, string FirstName, string MidName)
        {
            XmlRpcStruct person = new()
            {
                ["ID"] = id,
                ["Name"] = Name,
                ["FirstName"] = FirstName,
                ["MidName"] = MidName
            };
            return person;
        }

        public static XmlRpcStruct AddPassword (int id, int codeType, int config, int owner, int groupId, string start, string finish, int[] code)
        {
            XmlRpcStruct password = new XmlRpcStruct
            {
                ["ID"] = id,
                ["CodeType"] = codeType,
                ["Config"] = config,
                ["Owner"] = owner,
                ["GroupID"] = groupId,
                ["Start"] = start,
                ["Finish"] = finish,
                ["Code"] = code
            };
            return password;
        }


        public static XmlRpcStruct AddTimeWindows (int id, int[] calendar, string name)
        {
            XmlRpcStruct timeWindow = new XmlRpcStruct
            {
                ["ID"] = id,
                ["Name"] = name,
                ["Calendar"] = calendar
            };
            return timeWindow;
        }

        public static XmlRpcStruct AddGroup(int id, string name)
        {
            XmlRpcStruct group = new XmlRpcStruct
            {
                ["ID"] = id,
                ["Name"] = name
            };
            return group;
        }

        public static XmlRpcStruct AddRelay(int id, int comPort, int deviceAddress, int aggregateAddress, int pkuAddress = 0)
        {
            XmlRpcStruct relay = new XmlRpcStruct
            {
                ["ID"] = id,
                ["DeviceAddress"] = new XmlRpcStruct
                {
                    ["ComPort"] = comPort,
                    ["DeviceAddress"] = deviceAddress,
                    ["AggregateAddress"] = aggregateAddress,
                    ["PKUAddress"] = pkuAddress
                }
            };
            return relay;
        }


        public static XmlRpcStruct AddReader(int id, int comPort, int deviceAddress, int aggregateAddress, int pkuAddress = 0)
        {
            XmlRpcStruct reader = new()
            {
                ["ID"] = id,
                ["DeviceAddress"] = new XmlRpcStruct
                {
                    ["ComPort"] = comPort,
                    ["DeviceAddress"] = deviceAddress,
                    ["AggregateAddress"] = aggregateAddress,
                    ["PKUAddress"] = pkuAddress
                }
            };
            return reader;
        }

        public static XmlRpcStruct AddAccessPoint(  int id, string name, int inKeyId, int outKeyId, int mode, int inCommand = 0,
                                                    int outCommand = 0, int indexZone1 = 0, int indexZone2 = 0, int inDuration = 0,
                                                    int outDuration = 0, int pointType = 3 )
        {
            XmlRpcStruct accessPoint = new()
            {
                ["ID"] = id,
                ["Name"] = name,
                ["InKeyID"] = inKeyId,
                ["OutKeyID"] = outKeyId,
                ["Mode"] = mode,
                ["InCommand"] = inCommand,
                ["OutCommand"] = outCommand,
                ["IndexZone1"] = indexZone1,
                ["IndexZone2"] = indexZone2,
                ["InDuration"] = inDuration,
                ["OutDuration"] = outDuration,
                ["PointType"] = pointType
            };
            return accessPoint;
        }


        public static XmlRpcStruct AddRdrAccessPoint( int id, int readerId, int accessPointId, int accessMode = 0, int mode = 12)
        {
            XmlRpcStruct rdrAccess = new XmlRpcStruct
            {
                ["ID"] = id,
                ["ReaderID"] = readerId,
                ["AccessPointID"] = accessPointId,
                ["AccessMode"] = accessMode,
                ["Mode"] = mode
            };
            return rdrAccess;
        }

        public static XmlRpcStruct AddGroupAccess( int id, int groupId, int accessId, int mode = 12,string pardonTime = "30.12.1899 00:00:00",
                                                   int antipassback = 0, int config = 3, int confirmId = 0, int confirmId2 = 0, int flags = 0, int timeZone = 1)
        {
            XmlRpcStruct groupAccess = new()
            {
                ["ID"] = id,
                ["GroupID"] = groupId,
                ["Mode"] = mode,
                ["PardonTime"] = pardonTime,
                ["AccessID"] = accessId,
                ["Antipassback"] = antipassback,
                ["Config"] = config,
                ["ConfirmID"] = confirmId,
                ["ConfirmID2"] = confirmId2,
                ["Flags"] = flags,
                ["TimeZone"] = timeZone
            };
            return groupAccess;
        }



        /// <summary>
        /// Устанавливает расписание работы турникетов в запросе TimeWindows.
        /// Данный метод устанавливает параметры, при которых возможность прохода всегда активна 
        /// </summary>
        /// <returns>Возвращает массив байт для параметра Calendar</returns>
        public static int[] CreateAlwaysActiveCalendar()
        {
            int[] calendar = new int[192];

            for (int i = 0; i < 192; i++)
            {
                calendar[i] = 255; 
            }

            return calendar;
        }
    }




}
