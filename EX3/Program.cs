using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json;

class Program
{
    const string PATH = @"G:\contacts.json";

    static void Main(string[] args)
    {
        bool flag = true;

        while (flag == true)
        {
            Console.WriteLine("welcome \n1. load from file \n2. create new contacts \n3. quit");
            string command = Console.ReadLine();
            switch (command)
            {
                case "1":
                    var data = loadJsonFile();
                    Console.WriteLine(ShowContacts(data));
                    break;
                case "2":
                    Dictionary<string, string> dictionary = new Dictionary<string, string>();
                    dictionary = initDict(dictionary);
                    Console.WriteLine(ShowContacts(dictionary));
                    writeJsonFile(dictionary);
                    break;
                case "3":
                    flag = false;
                    break;
                default:
                    Console.WriteLine("try valid command : 1 or 2 or 3");
                    break;

            }



        }

        static Dictionary<string, string> initDict(Dictionary<string, string> dictionary)
        {
            Console.WriteLine("enter the number of contacts: ");
            int count = Convert.ToInt32(Console.ReadLine());

            for (int indexer = 0; indexer < count; indexer++)
            {
                Console.Write($"\n{indexer + 1}- enter name : ");
                string name = Console.ReadLine();
                Console.Write($"\n{indexer + 1}- enter phone number : ");
                string phoneNumber = Console.ReadLine();
                dictionary.Add(name, phoneNumber);

            }
            return dictionary;
        }
        static string ShowContacts(Dictionary<string, string> dictionary)
        {
            string result = "";
            foreach (KeyValuePair<string, string> contact in dictionary)
            {
                result += $"name: {contact.Key}, phoneNumber: {contact.Value}\n";
            }
            return result;
        }
        static void writeJsonFile(Dictionary<string, string> data)
        {
            string json = JsonSerializer.Serialize(data);
            File.WriteAllText(PATH, json);
        }


        static Dictionary<string, string> loadJsonFile()
        {
            string text = File.ReadAllText(PATH);
            var values = JsonSerializer.Deserialize<Dictionary<string, string>>(text);
            return values;
        }
    }
}