using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Threading;
using System.ComponentModel;

using Timer_Project.database;

/*
 * UITLEG
 * code snippet in de vorm van een console app dat de processnaam achter het actieve window
 * uitleest. Het idee is om met een zekere frequentie de actieve applicatie te pollen (elke 5000 ms?).
 * Is nu voor diagnostics redenen op 1000ms gezet. Lijkt me wat te intensied
 * 
 * Als de active applicatie veranderd is tov de vorige polling wordt een nieuwe entry aangemaakt die
 * een processnaam, een starttijd en een voorlopige eindtijd meekrijgt.
 * 
 * Als de actieve applicatie niet veranderd is, wordt de entry alleen geupdate met een nieuwe voorlopige
 * eindtijd. Dit om in het geval van een crash zoveel mogelijk data al weggeschreven te hebben.
 * 
 * TODO:
 * -    Alle Console.WriteLines moeten PUT of PATCH achtige database-updates worden.
 * 
 * OPTIONEEL:
 * als je wil weten in welk VS project of op wlke website in de browser je
 * bezig ent geweest, kunnen we heel eenvoudig ook de titel van het actieve window zelf achterhalen.
 * 
 * OVERIGE GEDACHTES:
 * Moeten misschine zorgen dat we vaak pollen, en alles loggen maar pas bij het displayen van de
 * resultaten filteren for hele korte onderbrekeingen. 10 seconden de rekenmachine erbij en weer terug naar
 * VS zou je kunnen filteren in de presentatie laag.
 * 
 */

namespace Timer_Project
{
    class AppChecker : INotifyPropertyChanged
    {
        //Verander de waarde van currentApp altijd via CurrentApp. Als currentApp direct word aangepast, wordt de event PropertyChanged niet getriggert.
        private String app = "";
        private String currentApp
        {
            get { return app; }
            set
            {
                if(!app.Equals(value))
                {
                    app = value;
                    OnPropertyChanged("app");
                }
            }
        }
        public String CurrentApp
        {
            get { return currentApp; }
        }
        /// <summary>
        /// Fires when the current active app changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public AppChecker()
        {

        }
        public void Start()
        {
            Thread thread = new Thread(WriteActiveProcess);
            thread.Start();
        }

        private void WriteActiveProcess()
        {
            Process active = null;
            Program_DB to_database = new Program_DB();
            string begintijd = "";
            string eindtijd = "";

            while (true)
            // (true) kan vervangen worden door (onzeApplicatie == aan)
            {
                IntPtr hWnd = GetForegroundWindow();
                GetWindowThreadProcessId(hWnd, out int pid);
                Process temp = Process.GetProcessById(pid);

                if (temp == null)
                    active = null;
                else
                {
                    if (active == null || temp.ProcessName != active.ProcessName || temp.MainWindowTitle != active.MainWindowTitle)
                    {
                        // DB PUT:
                        // nieuwe entry met initialisatie

                        active = temp;
                        string applicatieNaam = active.MainWindowTitle.Split('-').Last().Split('\\').Last();

                        if (applicatieNaam == "")
                            applicatieNaam = active.ProcessName;
                        else if (applicatieNaam.StartsWith(" "))
                            applicatieNaam = applicatieNaam.Substring(1);


                        if (begintijd.ToString() != "")
                        {
                            to_database.update_entry(begintijd, eindtijd);
                        }


                        currentApp = applicatieNaam;
                        begintijd = DateTime.UtcNow.ToString();

                        to_database.new_entry(currentApp, begintijd, active.MainWindowTitle);

                        /*
                        Console.WriteLine(applicatieNaam);          // process naam                                         // DB PUT   NAAM
                        Console.WriteLine(active.MainWindowTitle);  // gehele naam van het window, inclusief extra info     // DB PUT   MISC
                        Console.WriteLine(DateTime.UtcNow);         // starttijd                                            // DB PUT   START
                        Console.WriteLine(DateTime.UtcNow); */        // eindtijd                                             // DB PUT   EIND
                    }
                    else
                    {
                        // DB PATCH:
                        // update eindtijd huidige entry
                        eindtijd = DateTime.UtcNow.ToString();
                        to_database.update_entry(begintijd, eindtijd);

                        Console.WriteLine(DateTime.UtcNow); // nieuwe eind-time                                             // DB PATCH EIND
                    }
                }
                Thread.Sleep(1000);
            }
            to_database.closeconn(); //later maken we dit reachable
        }

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }

        protected void OnPropertyChanged(String propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        // Credits:
        // https://stackoverflow.com/questions/17345202/get-the-current-active-application-name

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern Int32 GetWindowThreadProcessId(IntPtr hWnd, out int pid);
    }
}
