using System.Net.Sockets;
using System.Text;

string? server = "yandex.ru";
int port = 80;

using Socket socketClient = new Socket(AddressFamily.InterNetwork,
                                        SocketType.Stream,
                                        ProtocolType.Tcp);

try
{
    await socketClient.ConnectAsync(server, port);
    Console.WriteLine($"Connect with {server}");
    Console.WriteLine($"Address remote: {socketClient.RemoteEndPoint}");
    Console.WriteLine($"Address loacal: {socketClient.LocalEndPoint}");

    string message = $"GET / HTTP/1.1\r\nHost: {server}\r\nConnection: close\r\n\r\n";
    byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
    Console.WriteLine($"Total size of buffer: {messageBuffer.Length}");

    int byteSend = await socketClient.SendAsync(messageBuffer);
    Console.WriteLine($"To {server} send {byteSend} bytes");

}
catch(SocketException e)
{
    Console.WriteLine(e.Message);
}
finally
{
    await socketClient.DisconnectAsync(true);
}