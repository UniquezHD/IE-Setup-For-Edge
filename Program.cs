using System;
using System.IO;
using System.IO.Compression;
using System.Globalization;
using System.Security.Principal;
using System.Xml;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
using Spectre.Console;
using System.Threading;
using Microsoft.Win32;

namespace ie_Setup_By_UniquezHD
{
    class Program
    {
        static void Main(string[] args)
        {
            if (IsAdmin())
            {
                MenuInit();
            }
            else
            {
                MessageBox.Show("Run As Administrator :)", "Made By UniquezHD", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(0);
            }

        }

        static bool IsAdmin()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator);
        }

        static void MenuInit()
        {
            Console.Clear();
            Console.Title = "IE Setup For Edge Made By UniquezHD";

            Misc.Credits();

            Console.ForegroundColor = ConsoleColor.White;

            var controls = new Text("Arrow ↑\nArrow ↓\nEnter To Select");
            controls.Alignment = Justify.Center;

            var panel = new Spectre.Console.Panel(controls);
            panel.Border = BoxBorder.Rounded;
            panel.Header("[cyan1 bold]Controls[/]");
            panel.Header.Centered();

            var style = new Style().Foreground(Color.Cyan1);
            AnsiConsole.Write(panel);
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .PageSize(10)
                .HighlightStyle(style)
                .AddChoices(new[]
                {
                        "Easy Download",
                        "Setup Edge",
                        "Add Site",
                        "Help",
                        "About",
                        "Quit"
                }));

            Menu(choice);
        }

        static void Menu(string option)
        {
            var edgeInfo = FileVersionInfo.GetVersionInfo(@"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe");

            if (option == "Quit")
            {
                Environment.Exit(0);
            }

            if(option == "Easy Download")
            {
                DownloadFiles(edgeInfo.FileVersion);
            }

            else if (option == "Setup Edge")
            { 
                SetupEdge(edgeInfo);
            }

            else if (option == "Add Site")
            {
                var site = AnsiConsole.Ask<string>("Type [cyan1]Website Url or IP[/]");

                AddSites(site);

                Console.ReadKey();

                MenuInit();
            }

            else if(option == "About")
            {
                About();
            }

            else if (option == "Help")
            {
                Help();
            }
        }

        static void DownloadFiles(string edgeVersion)
        {
            string zipPath = "extract.rar";
            string extractPath = Environment.CurrentDirectory;
            string winRar = @"C:\Program files\WinRar\WinRAR.exe";

            if (File.Exists(winRar))
            {
                CleanUp();
                Console.WriteLine("Clean Up Finished");
                Console.WriteLine("");
                Console.WriteLine($"Detected Edge {edgeVersion}");
                Console.WriteLine("");
                Download(edgeVersion);

                Process p = new Process();
                p.StartInfo.FileName = winRar;
                p.StartInfo.CreateNoWindow = false;
                p.StartInfo.Arguments = string.Format("x -o+ \"{0}\"  \"{1}\"", zipPath, extractPath);
                p.EnableRaisingEvents = true;
                p.Start();

                Thread.Sleep(1000);

                ZipFile.ExtractToDirectory("MicrosoftEdgePolicyTemplates.zip", extractPath);
                Console.WriteLine("");
                Console.WriteLine("Extracted Files");
                Console.WriteLine("");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();

                MenuInit();
            }
            else
            {
                string errorMsg = $"This Feature Requires Winrar Installed in: {winRar}\nPress Any Key To Open Download Page";

                var error = new Text(errorMsg);
                error.Alignment = Justify.Center;

                var panel = new Spectre.Console.Panel(error);
                panel.Border = BoxBorder.Rounded;
                panel.Header("[red bold]Error[/]");
                panel.Header.Centered();

                AnsiConsole.Write(panel);

                Console.ReadKey();
                Process.Start("https://www.win-rar.com/start.html?&L=0");

                MenuInit();
            }         
        }

        static void CleanUp()
        {
            if (File.Exists("MicrosoftEdgePolicyTemplates.zip"))
            {
                File.Delete("MicrosoftEdgePolicyTemplates.zip");
            }
            if (File.Exists("extract.rar"))
            {
                File.Delete("extract.rar");
            }
            if (File.Exists("VERSION"))
            {
                File.Delete("VERSION");
            }
            if (Directory.Exists("examples"))
            {
                Directory.Delete("examples", true);
            }
            if (Directory.Exists("html"))
            {
                Directory.Delete("html", true);
            }
            if (Directory.Exists("mac"))
            {
                Directory.Delete("mac", true);
            }
            if (Directory.Exists("windows"))
            {
                Directory.Delete("windows", true);
            }
        }

        static void Download(string edgeVersion)
        {
            string dlName = "extract.rar";

            using (WebClient wc = new WebClient())
            {
                if (edgeVersion == "100.0.1185.29")
                {
                    wc.DownloadFile("https://msedge.sf.dl.delivery.mp.microsoft.com/filestreamingservice/files/ea3be9c0-cac3-498e-bdb3-aeb581d81edf/MicrosoftEdgePolicyTemplates.cab", dlName);
                    Console.WriteLine($"Donwloading {edgeVersion} Policy Files");
                }
                else if (edgeVersion == "99.0.1150.55")
                {
                    wc.DownloadFile("https://msedge.sf.dl.delivery.mp.microsoft.com/filestreamingservice/files/539f6f79-75c5-46d6-9376-3b64aa7db608/MicrosoftEdgePolicyTemplates.cab", dlName);
                    Console.WriteLine($"Donwloading {edgeVersion} Policy Files");
                }
                else if (edgeVersion == "99.0.1150.55")
                {
                    wc.DownloadFile("https://msedge.sf.dl.delivery.mp.microsoft.com/filestreamingservice/files/539f6f79-75c5-46d6-9376-3b64aa7db608/MicrosoftEdgePolicyTemplates.cab", dlName);
                    Console.WriteLine($"Donwloading {edgeVersion} Policy Files");
                }
                else if (edgeVersion == "99.0.1150.46")
                {
                    wc.DownloadFile("https://msedge.sf.dl.delivery.mp.microsoft.com/filestreamingservice/files/014b67c2-219d-4240-85d6-7cf3f2cf73ba/MicrosoftEdgePolicyTemplates.cab", dlName);
                    Console.WriteLine($"Donwloading {edgeVersion} Policy Files");
                }
                else
                {
                    string errorMsg = $"Your Edge Version: {edgeVersion} Is Not Compatible With Easy Download Press Any Key To Open Download Page";

                    var error = new Text(errorMsg);
                    error.Alignment = Justify.Center;

                    var panel = new Spectre.Console.Panel(error);
                    panel.Border = BoxBorder.Rounded;
                    panel.Header("[red bold]Error[/]");
                    panel.Header.Centered();

                    AnsiConsole.Write(panel);

                    Console.ReadKey();
                    Process.Start("https://www.microsoft.com/en-us/edge/business/download");
                    Console.ReadKey();
                }
            }
            Console.WriteLine("Download Finished");
        }

        static void SetupEdge(FileVersionInfo edgeVersion)
        {
            CultureInfo systemLanguage = CultureInfo.InstalledUICulture;

            var panel = new Spectre.Console.Panel($"{edgeVersion}");
            panel.Border = BoxBorder.Rounded;
            panel.Header("[cyan1 bold]Edge Information[/]");
            panel.Header.Centered();

            AnsiConsole.Write(panel);

            Console.WriteLine("");
            Console.WriteLine("Starting Setup");
            Console.WriteLine("");

            CheckFiles(systemLanguage);

            MoveFiles(systemLanguage);

            SetupRegedit();

            UpdateGroupPolicy();

            Console.WriteLine("");
            Console.WriteLine("Setup Finished");
            Console.WriteLine("");
            Console.WriteLine("Press any key to continue");

            MenuInit();
        }

        static void SetupRegedit()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Edge");
            if (key == null)
            {
                Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Edge");
            }
            try
            {
                RegistryKey key1 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Edge", true); 
                key1.SetValue("InternetExplorerIntegrationLocalFileAllowed", 0);
                key1.Close();

                RegistryKey key2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Edge", true);
                key2.SetValue("InternetExplorerIntegrationSiteList", "C:\\sites.xml");
                key2.Close();
               
                RegistryKey key3 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Edge", true);
                key3.SetValue("InternetExplorerIntegrationEnhancedHangDetection", 1);
                key3.Close();

                RegistryKey key4 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Edge", true);
                key4.SetValue("SendIntranetToInternetExplorer", 0);
                key4.Close();
              
                RegistryKey key5 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Edge", true);
                key5.SetValue("InternetExplorerIntegrationLevel", 1);
                key5.Close();

                RegistryKey key6 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Edge", true);
                key6.SetValue("EnterpriseModeSiteListManagerAllowed", 1);
                key6.Close();

                RegistryKey key7 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Policies\Microsoft\Edge", true);
                key7.SetValue("InternetExplorerIntegrationTestingAllowed", 1);
                key7.Close();

            } catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("");
            Console.WriteLine("Registry Completed");
        }

        static void UpdateGroupPolicy()
        {
            Console.WriteLine("");
            Console.WriteLine("Force Updating Policy (this can take a while on business pc)");
            FileInfo execFile = new FileInfo("gpupdate.exe");
            Process proc = new Process();
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo.FileName = execFile.Name;
            proc.StartInfo.Arguments = "/force";
            proc.Start();
 
            while (!proc.HasExited)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }
        }

        static void AddSites(string input)
        {
            if (!File.Exists(@"C:\sites.xml"))
            {
                Console.WriteLine("Please Run Setup Again");
            }
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@"C:\sites.xml");

                XmlNode root = doc.SelectSingleNode("site-list");

                XmlAttribute url = doc.CreateAttribute("url");
                url.Value = input;
                XmlElement site = doc.CreateElement("site");
                site.Attributes.Append(url);
                root.AppendChild(site);

                XmlElement compatMode = doc.CreateElement("compat-mode");
                compatMode.InnerText = "Default";
                site.AppendChild(compatMode);

                XmlAttribute allowRedirect = doc.CreateAttribute("allow-redirect");
                allowRedirect.Value = "true";
                XmlElement openIn = doc.CreateElement("open-in");
                openIn.InnerText = "IE11";
                site.AppendChild(openIn);

                doc.Save(@"C:\sites.xml");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("");
            Console.WriteLine($"Added {input}");
            Console.WriteLine("");

            Console.WriteLine("Paste this in the edge search bar");
            Console.WriteLine("edge://compat/enterprise");
            Console.WriteLine("Then Press Force Update");
            Console.WriteLine("");
            Console.WriteLine("Press any key to continue");
        }

        static void About()
        {
            var panel = new Spectre.Console.Panel("Developer:  [cyan1 bold]UniquezHD[/]\nVersion:    1.0.1\nGithub:     https://github.com/Xiiqaz \nYoutube:    https://www.youtube.com/channel/UC9hhDXOInKNDlzmmJPcOCmg");
            panel.Border = BoxBorder.Rounded;
            panel.Header("[cyan1 bold]About[/]");
            panel.Header.Centered();

            AnsiConsole.Write(panel);
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();

            MenuInit();
        }

        static void Help()
        {
            var panel = new Spectre.Console.Panel("Step 1: Select [cyan1 bold]Easy Download[/] or download files at: https://www.microsoft.com/en-us/edge/business/download \nStep 2: Select [cyan1 bold]Setup Edge[/]\nStep 3: Select [cyan1 bold]Add Site[/] and type in the url or ip\nStep 4: Paste: [cyan1 bold]edge://compat/enterprise[/] in the edge search bar then press [cyan1 bold]Force Update[/]");
            panel.Border = BoxBorder.Rounded;
            panel.Header("[cyan1 bold]Help[/]");
            panel.Header.Centered();

            AnsiConsole.Write(panel);
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();

            MenuInit();
        }

        static void MoveFiles(CultureInfo languageFolder)
        {
            string startPath = $"windows/admx/{languageFolder}/";
            string endPath = $"C:\\Windows\\PolicyDefinitions\\{languageFolder}\\";

            try
            {
                File.Copy("windows/admx/msedge.admx", "C:\\Windows\\PolicyDefinitions\\msedge.admx");
                Console.WriteLine("Updated msedge.admx");
                File.Copy("windows/admx/msedgeupdate.admx", "C:\\Windows\\PolicyDefinitions\\msedgeupdate.admx");
                Console.WriteLine("Updated msedgeupdate.admx");
            } 
            catch (IOException e)
            {
                Console.WriteLine($"{e}");
                Console.ReadKey();
            }

            try
            {
                File.Copy($"{startPath}msedge.adml", $"{endPath}msedge.adml");
                Console.WriteLine("Updated msedge.adml");
                File.Copy($"{startPath}msedgeupdate.adml", $"{endPath}msedgeupdate.adml");
                Console.WriteLine("Updated msedgeupdate.adml");
            } 
            catch (IOException e)
            {
                Console.WriteLine($"{e}");
                Console.ReadKey();
            }
        }

        static void CheckFiles(CultureInfo languageFolder)
        {
            string endPath = $"C:\\Windows\\PolicyDefinitions\\{languageFolder}\\";

            if (File.Exists($"{endPath}msedge.adml"))
            {
                File.Delete($"{endPath}msedge.adml");
                Console.WriteLine("Deleted msedge.adml");
            }

            if (File.Exists($"{endPath}msedgeupdate.adml"))
            {
                File.Delete($"{endPath}msedgeupdate.adml");
                Console.WriteLine("Deleted msedgeupdate.adml");
            }

            if (File.Exists("C:\\Windows\\PolicyDefinitions\\msedge.admx"))
            {
                File.Delete("C:\\Windows\\PolicyDefinitions\\msedge.admx");
                Console.WriteLine("Deleted msedge.admx");
            }

            if (File.Exists("C:\\Windows\\PolicyDefinitions\\msedge.admx"))
            {
                File.Delete("C:\\Windows\\PolicyDefinitions\\msedge.admx");
                Console.WriteLine("Deleted msedge.admx");
            }

            if (File.Exists("C:\\Windows\\PolicyDefinitions\\msedgeupdate.admx"))
            {
                File.Delete("C:\\Windows\\PolicyDefinitions\\msedgeupdate.admx");
                Console.WriteLine("Deleted msedgeupdate.admx");
            }

            if (!File.Exists(@"C:\sites.xml"))
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    XmlElement root = doc.CreateElement("site-list");

                    XmlAttribute version = doc.CreateAttribute("version");
                    version.Value = "2";
                    root.Attributes.Append(version);
                    doc.AppendChild(root);

                    doc.Save(@"C:\sites.xml");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                Console.WriteLine("Created sites.xml");
            }

            if (!Directory.Exists("windows"))
            {
                string errorMsg = "windows Folder Missing\nPress Any Key To Open Download Page";

                var errorWindows = new Text(errorMsg);
                errorWindows.Alignment = Justify.Center;

                var panelErrorWindows = new Spectre.Console.Panel(errorWindows);
                panelErrorWindows.Border = BoxBorder.Rounded;
                panelErrorWindows.Header("[red bold]Error[/]");
                panelErrorWindows.Header.Centered();

                AnsiConsole.Write(panelErrorWindows);

                Console.ReadKey();
                Process.Start("https://www.microsoft.com/en-us/edge/business/download");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();

                MenuInit();
            }

            if (!Directory.Exists("html"))
            {
                string errorMsg = "html Folder Missing\nPress Any Key To Open Download Page";

                var errorHtml = new Text(errorMsg);
                errorHtml.Alignment = Justify.Center;

                var panelErrorHtml = new Spectre.Console.Panel(errorHtml);
                panelErrorHtml.Border = BoxBorder.Rounded;
                panelErrorHtml.Header("[red bold]Error[/]");
                panelErrorHtml.Header.Centered();

                AnsiConsole.Write(panelErrorHtml);
                Console.ReadKey();
                Process.Start("https://www.microsoft.com/en-us/edge/business/download");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();

                MenuInit();
            }
        }
    }
}
