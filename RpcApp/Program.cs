using Horizon.XmlRpc.Core;
using RpcApp.Domain;
using System.Reflection.Emit;
using System.Text;
using System.Xml;


class Program
{
    static void Main()
    {
        // Необходимо для того чтобы не было ошибки с кодировкой
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
        TablesManager tablesManager = new();

        tablesManager.SendBaseConfiguration(guid);

         Console.ReadKey(true);
        tablesManager.AddPersonWithPassword( guid,1, "Alexei", " ", " ", 1, 4, 128, 1, 3, "04.12.2025 13:02:02", "04.12.2026 23:59:00", [1, 164, 218, 191, 220, 0, 0, 247]);

        Console.ReadKey(true);
        tablesManager.DeletePersonWithPassword(guid, 1, "Alexei", " ", " ", 1, 4, 128, 1, 3, "04.12.2025 13:02:02", "04.12.2026 23:59:00", [1, 164, 218, 191, 220, 0, 0, 247]);


    }
}