using System;
public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Waiting for a removeable device...");
        while (true)
        {
            if (usb_listenter())
            {
                startMSPaint();
                break;
            }
            //System.Threading.Thread.Sleep(1000);

        }
    }
    static bool usb_listenter()
    {
        DriveInfo[] drives = DriveInfo.GetDrives();
        foreach (DriveInfo drive in drives)
        {
            if (drive.DriveType == DriveType.Removable)
            {
                return true;
            }
        }
        return false;
    }

    static void startMSPaint()
    {
        try
        {
            System.Diagnostics.Process.Start("mspaint.exe");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Something went wrong while trying to start MS Pain. error message : " + ex.ToString());
        }
    }

}

