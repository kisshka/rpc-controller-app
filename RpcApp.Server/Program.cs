using System.Net;
using System.Xml;

namespace RpcApp.Server
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8095/");
            listener.Start();

            Console.WriteLine("Сервер запущен на http://localhost:8095/");

            while (true)
            {
                var context = await listener.GetContextAsync();
                _ = Task.Run(() => HandleRequest(context));
            }
        }


        static void HandleRequest(HttpListenerContext context)
        {
                if (context.Request.HttpMethod != "POST")
                {
                    context.Response.StatusCode = 405;
                    context.Response.Close();
                    return;
                }

                // Читаем запрос
                using var reader = new StreamReader(context.Request.InputStream);
                var xml = reader.ReadToEnd();

                Console.WriteLine(xml);

                // Парсим метод и параметры
                var doc = new XmlDocument();
                doc.LoadXml(xml);
                Console.WriteLine(doc);
        }
    }
}
    

