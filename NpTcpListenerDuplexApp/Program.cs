using System.Net;
using System.Net.Sockets;
using System.Text;




var listener = new TcpListener(IPAddress.Loopback, 5000);

try
{
    listener.Start();
    Console.WriteLine("Server start!");

    while(true)
    {
        var client = await listener.AcceptTcpClientAsync();
        Task.Run(async () => await TranslateAsync(client));
    }    
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}
finally
{
    listener.Stop();
}

async Task TranslateAsync(TcpClient client)
{
    var dict = new Dictionary<string, string>()
    {
        { "table", "стол" },
        { "house", "дом" },
        { "apple", "яблоко" },
        { "computer", "компьютер" },
        { "dog", "собака" }
    };

    var stream = client.GetStream();
    var request = new List<byte>();
    int bytesCount = 1;

    while (true)
    {
        while ((bytesCount = stream.ReadByte()) != '#')
            request.Add((byte)bytesCount);
        var word = Encoding.UTF8.GetString(request.ToArray());
        if (word == "END") break;

        Console.WriteLine($"Word from client {client.Client.RemoteEndPoint}: {word}");
        if (!dict.TryGetValue(word, out var translate))
            translate = "not found translate";
        translate += '#';

        await stream.WriteAsync(Encoding.UTF8.GetBytes(translate));
        request.Clear();
    }
    client.Close();
}