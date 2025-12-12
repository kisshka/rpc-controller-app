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
        var server = new Server();
        server.RsEventReceived += (sender, RsEventArgs) =>
        {
            Console.WriteLine($"Клиент: получил сообщение - {RsEventArgs.NameEvent}");
        };


        var serverThread = new Thread(() => server.StartListener());
        serverThread.Start();

        server.KeyCodeReceived += (sender, RsEventArgs) =>
        {
            Console.WriteLine($"Клиент: получил сообщение - {RsEventArgs.KeyCode}");
        };

        var client = new RequestSender();
        string guid = client.SetSubscribe();
        client.SetConfigurationHwSrv(guid);

        Console.ReadKey(true);
        //client.GetDevice(guid);

        client.GetDeviceListAsync();

        Console.ReadKey(true);

        client.ReadKeyCodeFromReader(guid);

        /*client.GetPasswordListWithStatus(guid);
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