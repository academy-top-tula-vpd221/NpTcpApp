using System.Net.Sockets;
using System.Net;
using System.Text;

using TcpListener listenerTcp = new(IPAddress.Loopback, 5000);

try
{
    listenerTcp.Start();
    Console.WriteLine($"Server is work. Waitnig connections...");

    while(true)
    {
        using var clientTcp = await listenerTcp.AcceptTcpClientAsync();
        NetworkStream streamNet = clientTcp.GetStream();

        //string message = "Server data: " + DateTime.Now.ToLongTimeString();
        //byte[] buffer = Encoding.UTF8.GetBytes(message);

        //await streamNet.WriteAsync(buffer);
        //Console.WriteLine($"Server send data to client");

        byte[] buffer = new byte[1024];
        int bytesCount = 0;

        string message = "";
        do
        {
            bytesCount = await streamNet.ReadAsync(buffer);
            message += Encoding.UTF8.GetString(buffer);

        } while (streamNet.DataAvailable);

        Console.WriteLine($"Message from client: {message}");
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
finally
{
    listenerTcp.Stop();
}

/*
try
{
    listenerTcp.Start();
    Console.WriteLine($"Server is work. Waitnig connections...");

    while(true)
    {
        using var clientTcp = await listenerTcp.AcceptTcpClientAsync();
        Console.WriteLine($"Client connect: {clientTcp.Client.RemoteEndPoint}");

        //var key = Console.ReadKey();
        //if (key.Key == ConsoleKey.Escape)
        //    break;
    }
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}
finally
{
    listenerTcp.Stop();
}
*/