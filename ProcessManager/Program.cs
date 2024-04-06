using System;
using System.Diagnostics;
using System.Xml.Serialization;


class Program
{
    static void Main(string[] args)
    {
        show_menu();
        string command = Convert.ToString(Console.ReadLine());
        while (command != "5") {
            {
                switch (command)
                {
                    case "1":
                        start_process();
                        break;
                    case "2":
                        get_processes();
                        break;
                    case "3":
                        kill_process();
                        break;
                    case "4":
                        show_process_parent();
                        break;
                    case "5":
                        break;
                    default:
                        Console.WriteLine("\nchoose one of the operations(1-5) ! ");
                        break;
                }
                show_menu();
                command = Convert.ToString(Console.ReadLine());
            }



        }

    }
    static void show_menu()
    {
        Console.WriteLine("\n\n----------------------------");
        Console.WriteLine("MENU : ");
        Console.WriteLine("1. Start a process by name.");
        Console.WriteLine("2. Show processes list.");
        Console.WriteLine("3. Kill a process by id.");
        Console.WriteLine("4. Show process parent.");
        Console.WriteLine("5. Exit.");
        Console.WriteLine("\n" + "enter your command : .. ");
    }


    static void start_process()
    {
        Console.Write("enter the process name: .. ");
        string processName = Console.ReadLine();

        try
        {
            Process.Start(processName);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n\nfailed to run process. error message :\n\n {ex.ToString()}");
        }
    }

    static void get_processes()
    {
        Process[] processes = Process.GetProcesses();

        //sort processes by Name
        processes = processes.OrderBy(p => p.ProcessName).ToArray();

        //find the maximum length of processes name to have a better show 
        int max_len = 0;
        foreach (Process p in processes)
        {
            if (p.ProcessName.Length > max_len)
            {
                max_len= p.ProcessName.Length;
            }
        }
        Console.WriteLine("List of processes:");
        foreach (Process process in processes)
        {
            int num_of_spaces = (max_len - process.ProcessName.Length);
            char space_char = ' '; 
            string spaces = new string(space_char, num_of_spaces);
            Console.WriteLine($"Name: {process.ProcessName}{spaces} , PID: {process.Id}");
        }
    }


    static void kill_process()
    {
        Console.Write("enter the PID of the process to kill : .. ");
        int pid = Convert.ToInt16(Console.ReadLine());
        try
        {
            Process process = Process.GetProcessById(pid);
            process.Kill();
            Console.WriteLine($"Process with PID {pid} has been terminated.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n\nsomthing went wrong! error message : {ex.Message}");
        }
        
    }

    static void show_process_parent()
    {
        Console.Write("enter the PID of the process to recognize parent : .. ");
        try
        {
            int pid = Convert.ToInt16(Console.ReadLine());
            Process process = Process.GetProcessById(pid);
            Process parent = Process.GetProcessById((int)process.Handle);
            Console.WriteLine(parent);
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"\n\nsomthing went wrong! error message : {ex.Message}");
        }
    }
}