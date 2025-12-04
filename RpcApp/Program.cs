using RpcApp.Domain;
using System.Text;


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

        client.GetDeviceListAsync();
        client.GetListMethods(guid);
        //client.ReadKeyCodeFromReader(guid);

        /*        client.GetPasswordListWithStatus(guid);
                client.ReadDeviceKeyList(guid);*/

        //client.SynchronizeOneKey(guid, 369);



        Console.ReadKey(true);
        
    }
}