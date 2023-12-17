using System.Net.Sockets;
using System.Text;
using System.Net;
using System.Linq.Expressions;

using TcpClient clientTcp = new TcpClient();
Console.WriteLine("Client is work");

await clientTcp.ConnectAsync(IPAddress.Loopback.ToString(), 5000);

//byte[] buffer = new byte[1024];
//NetworkStream streamNet = clientTcp.GetStream();

//int bytesCount = await streamNet.ReadAsync(buffer);

//string message = Encoding.UTF8.GetString(buffer, 0, bytesCount);
//Console.WriteLine($"Message from server: {message}");

NetworkStream streamNet = clientTcp.GetStream();
string message = "Client data: " + DateTime.Now.ToLongTimeString();
byte[] buffer = Encoding.UTF8.GetBytes(message);

await streamNet.WriteAsync(buffer);
Console.WriteLine($"Client send data to server: {message}");

//if(clientTcp.Connected)
//    Console.WriteLine($"Client conected with server: {clientTcp.Client.RemoteEndPoint}");
//else
//    Console.WriteLine("Disconnect");

/*
string server = "www.yandex.ru";
int port = 80;


try
{
    await clientTcp.ConnectAsync(server, port);
    Console.WriteLine($"Client connect");

    var streamNet = clientTcp.GetStream();

    string message = $"GET / HTTP/1.1\r\nHost: {server}\r\nConnection: Close\r\n\r\n";
    var requestBuffer = Encoding.UTF8.GetBytes(message);
    await streamNet.WriteAsync(requestBuffer);


    var responseBuffer = new byte[512];
    var textFull = new StringBuilder();
    int bytesCount;

    do
    {
        bytesCount = await streamNet.ReadAsync(responseBuffer);
        textFull.Append(Encoding.UTF8.GetString(responseBuffer));
    } while (streamNet.DataAvailable ); // clientTcp.Available > 0


Console.WriteLine(textFull);
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}
*/