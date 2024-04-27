using System;
using System.Net;
using System.Threading;

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome");
        Console.WriteLine("Enter the directory where you want to save files (e.g., 'C:\\Users\\MO99ME99\\Downloads\\New folder'): ");
        string dir = Console.ReadLine();
        Console.WriteLine("How many files do you want to download?");
        int counter = int.Parse(Console.ReadLine());
        string[] urls = GetUrls(counter);
        foreach (string url in urls)
        {
            // Extracting file name from the URL
            string fileName = GetFileNameFromUrl(url);
            DownloadFile(url, dir + "\\" + fileName); // Concatenating directory and file name
        }
        Console.ReadLine();
    }

    public static string[] GetUrls(int counter)
    {
        string[] urls = new string[counter];
        for (int i = 0; i < counter; i++)
        {
            Console.WriteLine($"URL {i + 1}: ");
            urls[i] = Console.ReadLine();
            Console.Clear();
        }
        Console.Clear();
        return urls;
    }

    public static void DownloadFile(string url, string fileName)
    {
        Thread thread = new Thread(() => DownloadProcess(url, fileName));
        thread.Start();
    }

    public static void DownloadProcess(string url, string fileName)
    {
        try
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(new Uri(url), fileName);
                client.DownloadProgressChanged += (sender, e) =>
                {
                    Console.WriteLine(e.ProgressPercentage);
                };
            }

            // Prompt for when a download completes
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Download of '{fileName}' completed!");
            Console.ResetColor();
        }
        catch (Exception e)
        {
            // Handling errors
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"An error occurred for '{url}'! Error message: {e.Message}");
            Console.ResetColor();
        }
    }

    // Method to extract file name from URL
    public static string GetFileNameFromUrl(string url)
    {
        Uri uri = new Uri(url);
        string fileName = System.IO.Path.GetFileName(uri.LocalPath);
        return fileName;
    }
}
