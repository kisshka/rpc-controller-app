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

        client.CloseScribe(guid);
        client.SetConfigurationHwSrv(guid);
        /*  для ручного тестирования
                subscriber.DebugRawHttp();*/

    }
}