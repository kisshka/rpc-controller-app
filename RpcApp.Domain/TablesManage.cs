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
    public class TablesManage
    {

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
        public static XmlRpcStruct AddTimeWindows (int[] calendar, string name)
        {
            XmlRpcStruct timeWindow = new XmlRpcStruct
            {
                ["ID"] = 1,
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
            XmlRpcStruct reader = new XmlRpcStruct
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

        /// <summary>
        /// Метод для установки расписания в TimeWindows
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
