using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

using var client = new TcpClient();
await client.ConnectAsync(IPAddress.Loopback, 5000);

string[] words = new[] { "table", "apple", "computer", "dog", "house", "spoon" };

var stream = client.GetStream();

var response = new List<byte>();
int bytesCount = 1;

foreach(string word in words)
{
    byte[] buffer = Encoding.UTF8.GetBytes(word + '#');
    await stream.WriteAsync(buffer);

    while ((bytesCount = stream.ReadByte()) != '#')
        response.Add((byte)bytesCount);

    var translation = Encoding.UTF8.GetString(response.ToArray());
    Console.WriteLine($"Translate word: {word} from server: {translation}");

    response.Clear();

    await Task.Delay(2000);
}

await stream.WriteAsync(Encoding.UTF8.GetBytes("END#"));
Console.WriteLine("Dialog is close");