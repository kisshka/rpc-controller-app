using RpcApp.Domain;
using System.Net;
using System.Text;
using System.Xml;

class Program
{
    static void Main()
    {
        //Необходимо для того чтобы не было ошибки с кодировкой
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        var subscriber = new OrionSubscribe();
        subscriber.Subscribe();
        /*  для ручного тестирования
                subscriber.DebugRawHttp();*/
    }
}