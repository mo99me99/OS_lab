using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

public class TcpClientSample
{
    public static void Main()
    {
        int port;
        string filePath;

        Console.WriteLine("Please Enter the port number of Server:");
        port = Int32.Parse(Console.ReadLine());

        Console.WriteLine("Enter the path of the file to send:");
        filePath = Console.ReadLine();

        try
        {
            using (TcpClient server = new TcpClient("127.0.0.1", port))
            using (NetworkStream ns = server.GetStream())
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                fileStream.CopyTo(ns);
                Console.WriteLine("File sent successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        Console.ReadKey();
    }
}
