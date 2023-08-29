using System.Net;
using Server.App.Router;
using Server.Routes;

namespace Server;

class Program
{
    static string _host = "http://localhost:8000";
    static void Main(string[] args)
    {
        using (HttpListener listener = new()){
            listener.Prefixes.Add(_host + "/");
            listener.Start();
            Console.WriteLine("Listening on " + _host);
            Web.RegisterRoutes();
            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;
                Console.WriteLine("Request: {0} | Method: {1} | Content type: {2} | Accept types: {3}", request.Url, request.HttpMethod, request.ContentType, request.AcceptTypes);
                Router.ResolveCall(request.Url.ToString(), request.HttpMethod);

                string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }
    }
}
