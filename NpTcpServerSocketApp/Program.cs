using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml;

IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Loopback, 5000);
using Socket socketServer = new(AddressFamily.InterNetwork, 
                                SocketType.Stream, 
                                ProtocolType.Tcp);


try
{
    socketServer.Bind(iPEndPoint);
    socketServer.Listen();
    Console.WriteLine("Server socket is work. Waiting connections...");

    while(true)
    {
        using Socket socketClient = await socketServer.AcceptAsync();
        Console.WriteLine($"Cilent end point: {socketClient.RemoteEndPoint}");

        var dataBuffer = new List<byte>();
        var byteBuffer = new byte[1];

        while(true)
        {
            while(true)
            {
                var bytesCount = await socketClient.ReceiveAsync(byteBuffer);
                if (bytesCount == 0 || byteBuffer[0] == '#')
                    break;
                dataBuffer.Add(byteBuffer[0]);
            }
            var message = Encoding.UTF8.GetString(dataBuffer.ToArray());
            if (message == "END")
                break;
            Console.WriteLine($"Message: {message}");
            dataBuffer.Clear();
        }
        Console.WriteLine("Dialog ending");
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

/*
try
{
    socketServer.Bind(iPEndPoint);
    socketServer.Listen();
    Console.WriteLine("Server socket is work. Waiting connections...");

    while (true)
    {
        using Socket socketClient = await socketServer.AcceptAsync();
        Console.WriteLine($"Cilent end point: {socketClient.RemoteEndPoint}");

        //var dataBuffer = new List<byte>();
        //var byteBuffer = new byte[1];
        byte[] sizeBuffer = new byte[4];
        int bytesCount;
        bytesCount = await socketClient.ReceiveAsync(sizeBuffer);

        int sizeMessage = BitConverter.ToInt32(sizeBuffer, 0);
        byte[] dataBuffer = new byte[sizeMessage];

        bytesCount = await socketClient.ReceiveAsync(dataBuffer);

        //while(true)
        //{
        //    bytesCount = await socketClient.ReceiveAsync(byteBuffer);
        //    if(bytesCount == 0 || byteBuffer[0] == '#')
        //        break;
        //    dataBuffer.Add(byteBuffer[0]);
        //};

        var text = Encoding.UTF8.GetString(dataBuffer.ToArray());
        Console.WriteLine($"Text from client: {text}");
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
*/

/*
try
{
    socketServer.Bind(iPEndPoint);
    socketServer.Listen();
    Console.WriteLine("Server is work. Waiting connections...");

    while(true)
    {
        using Socket socketClient = await socketServer.AcceptAsync();
        Console.WriteLine($"Cilent end point: {socketClient.RemoteEndPoint}");

        string dataString = "Server data..." + DateTime.Now.ToLongTimeString();
        byte[] dataBuffer = Encoding.UTF8.GetBytes(dataString);

        await socketClient.SendAsync(dataBuffer);

        Console.WriteLine($"Server sending data to client {socketClient.RemoteEndPoint}");
    }
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}
*/



