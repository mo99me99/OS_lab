using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        TcpListener tcpServer = null;
        int port = 5000;

        try
        {
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            tcpServer = new TcpListener(localAddr, port);
            tcpServer.Start();

            Console.WriteLine("Server started. Listening for incoming connections...");

            while (true)
            {
                using (TcpClient client = tcpServer.AcceptTcpClient())
                using (NetworkStream ns = client.GetStream())
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead;

                    while ((bytesRead = ns.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        memoryStream.Write(buffer, 0, bytesRead);
                    }

                    byte[] fileData = memoryStream.ToArray();
                    string fileName = GenerateFileName("received_file", fileData);
                    string savePath = Path.Combine(Environment.CurrentDirectory, fileName);

                    File.WriteAllBytes(savePath, fileData);

                    Console.WriteLine($"File saved to: {savePath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            tcpServer?.Stop();
        }
    }

    static string GenerateFileName(string baseName, byte[] fileData)
    {
        string extension = GetFileExtension(fileData);
        return $"{baseName}{extension}";
    }

    static string GetFileExtension(byte[] fileData)
    {
        // This method will detect the file extension based on the first few bytes of the file.
        // You can extend this method to cover more file types if needed.

        Dictionary<string, string> fileSignatures = new Dictionary<string, string>()
        {
            { "FFD8FF", ".jpg" }, // JPEG
            { "89504E47", ".png" }, // PNG
            { "47494638", ".gif" }, // GIF
            { "25504446", ".pdf" } // PDF
            // Add more file signatures here...
        };

        byte[] signatureBytes = new byte[4];
        Array.Copy(fileData, signatureBytes, 4); // Extract first 4 bytes of the file

        string signature = BitConverter.ToString(signatureBytes).Replace("-", "");

        if (fileSignatures.ContainsKey(signature))
        {
            return fileSignatures[signature];
        }
        else
        {
            return ".dat"; // Default extension if signature not found
        }
    }
}
