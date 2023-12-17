using System.Net;
using System.Net.Sockets;
using System.Text;

//string? server = "www.yandex.ru";
//string? server = IPAddress.Loopback.ToString();
//int port = 80;

using Socket socketClient = new Socket(AddressFamily.InterNetwork,
                                        SocketType.Stream,
                                        ProtocolType.Tcp);

try
{
    await socketClient.ConnectAsync(IPAddress.Loopback, 5000);
    Console.WriteLine("Client socket connecting to server");

    //string[] messages = new[] { "Hello server!\n", "Hi people!\n", "END\n" };
    //foreach(var m in messages)
    //{
    //    byte[] dataBuffer = Encoding.UTF8.GetBytes(m);
    //    await socketClient.SendAsync(dataBuffer);
    //}

    while (true)
    {
        Console.Write("Message: ");
        var message = Console.ReadLine() + "#";

        byte[] dataBuffer = Encoding.UTF8.GetBytes(message);
        await socketClient.SendAsync(dataBuffer);

        if (message == "END#")
            break;
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

/*
try
{
    await socketClient.ConnectAsync(IPAddress.Loopback, 5000);
    Console.WriteLine("Client socket connecting to server");

    string dataString = "Client data..." + DateTime.Now.ToLongTimeString();
    byte[] dataBuffer = Encoding.UTF8.GetBytes(dataString);
    byte[] sizeBuffer = BitConverter.GetBytes(dataBuffer.Length);
    
    int bytesCount;
    bytesCount = await socketClient.SendAsync(sizeBuffer);
    bytesCount = await socketClient.SendAsync(dataBuffer);

    Console.WriteLine("Client send data to server");
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
*/

/*
try
{
    await socketClient.ConnectAsync(IPAddress.Loopback, 5000);
    byte[] dataBuffer = new byte[1024];

    int bytesCount = await socketClient.ReceiveAsync(dataBuffer);

    string text = Encoding.UTF8.GetString(dataBuffer);

    Console.WriteLine(text);
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}
*/

//await TcpClientSendReceive(socketClient);


//try
//{
//    await socketClient.ConnectAsync(server, port);
//    Console.WriteLine($"Client {socketClient.RemoteEndPoint} connect");
//}
//catch(Exception e)
//{
//    Console.WriteLine($"{ e.Message }");
//}

/*
async Task<Socket> TcpClientSendReceive(Socket socketClient)
{
    await socketClient.ConnectAsync(server, port);

    using var streamNet = new NetworkStream(socketClient);

    Console.WriteLine($"Local client: {streamNet.Socket.LocalEndPoint}");
    Console.WriteLine($"Remote server: {streamNet.Socket.RemoteEndPoint}");

    string message = $"GET / HTTP/1.1\r\nHost: {server}\r\nConnection: Close\r\n\r\n";

    var buffer = Encoding.UTF8.GetBytes(message);
    await streamNet.WriteAsync(buffer, 0, buffer.Length);


    buffer = new byte[512];
    var textFull = new StringBuilder();
    int bytesCount;
    do
    {
        bytesCount = await streamNet.ReadAsync(buffer);

        textFull.Append(Encoding.UTF8.GetString(buffer));
    } while (streamNet.Socket.Available > 0);
    
    Console.WriteLine(textFull);

    
    try
    {

        await socketClient.ConnectAsync(server, port);
        Console.WriteLine($"Connect with {server}");
        Console.WriteLine($"Address remote: {socketClient.RemoteEndPoint}");
        Console.WriteLine($"Address loacal: {socketClient.LocalEndPoint}");

        string message = $"GET / HTTP/1.1\r\nHost: {server}\r\n\r\n";
        byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
        Console.WriteLine($"Total size of buffer: {messageBuffer.Length}");

        int byteSend = await socketClient.SendAsync(messageBuffer);
        Console.WriteLine($"To {server} send {byteSend} bytes");

        socketClient.Shutdown(SocketShutdown.Send);


        // response of data
        var responseBuffer = new byte[512];
        string textFull = "";
        int bytesCount;

        do
        {
            bytesCount = await socketClient.ReceiveAsync(responseBuffer);
            textFull += Encoding.UTF8.GetString(responseBuffer, 0, bytesCount);
        } while (socketClient.Available > 0);


        Console.WriteLine(textFull);

    }
    catch (SocketException e)
    {
        Console.WriteLine(e.Message);
    }
    finally
    {
        await socketClient.DisconnectAsync(true);
    }
    
    return null;
}
*/