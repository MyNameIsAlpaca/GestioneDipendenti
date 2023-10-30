using GestioneDipendenti.Dipendenti;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using UtilityLib;


namespace GestioneDipendenti.Data
{
    internal class EmployeesList
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public List<Employees> employeesList = new List<Employees>();

        Utility utility = new Utility();
        public void fillListEmployeersTxt()
        {
            int DataImported = 0;
            try
            {
                string relPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                string filePath = Path.Combine(relPath, "DataEmployees", "Employees.txt");

                if(File.Exists(filePath))
                {
                    string[] fData = File.ReadAllLines(filePath);

                    string[] DataClean;

                    foreach (string row in fData)
                    {
                        DataClean = row.Split(';');

                        string[] nameSplit = DataClean[1].Split(' ');

                        if (nameSplit.Length == 3) {

                        nameSplit[1] = nameSplit[1] + ' ' + nameSplit[2];
                        
                        }

                        if (nameSplit.Length == 4)
                        {

                            nameSplit[1] = nameSplit[1] + ' ' + nameSplit[2] + ' ' + nameSplit[3];

                        }


                        if (utility.testInt(DataClean[4]))
                        {
                            int age = int.Parse(DataClean[4]);

                            string cap = DataClean[8];

                            Employees employees = new Employees(nameSplit[0], nameSplit[1], age , DataClean[5], DataClean[6], DataClean[7], cap, DataClean[9], DataClean[0], DataClean[3], DataClean[2]);

                            DataImported++;

                            employeesList.Add(employees);
                        }
                        else
                        {

                        }

                    }
                    if (employeesList.Count() > 0)
                    {
                        Logger.Info("Importazione dipendenti avvenuta");
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Importazione completata");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"Hai importato correttamente {DataImported} dati");
                        Console.ReadLine();
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        utility.errorStyle("Non è stato importato nulla");
                        Console.ReadLine();
                        Logger.Error("Non è stato importato nulla");
                    }
                }
                else
                {
                    Logger.Error("Nessun file di importazione");
                    utility.errorStyle("Non è stato trovato nessun file di importazione");
                    Console.ReadLine();
                    Console.Clear();
                }

            } catch(Exception ex)
            {
                Console.Clear();
                utility.errorStyle($"è stato riscontrato il seguente errore: {ex}");
                Logger.Error(ex);
            }

        }

    }

    internal class EmployeesActivity
    {
        public List<ActivityEmp> activityList = new List<ActivityEmp>();

        Utility utility = new Utility();

        EmployeesList employeesList = new EmployeesList();

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public void fillListActivityTxt()
        {
            int DataImported = 0;

            string relPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string filePath = Path.Combine(relPath, "DataEmployees", "EmployeesActivities.txt");

            try
            {
                if (File.Exists(filePath))
                {

                    string[] fData = File.ReadAllLines(filePath);

                    string[] DataClean;

                    foreach(string str in fData)
                    {
                        DataClean = str.Split(';');

                        if (utility.testInt(DataClean[2]))
                        {
                            int hour = int.Parse(DataClean[2]);

                            ActivityEmp activity = new ActivityEmp(DataClean[0], DataClean[1], hour, DataClean[3]);

                            DataImported++;

                            activityList.Add(activity);

                        }
                        else
                        {

                        }
                    }
                    if (activityList.Count > 0)
                    {
                        Logger.Info("Importazione attività avvenuta");
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Importazione completata");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"Hai importato correttamente {DataImported} dati");
                        Console.ReadLine();
                        Console.Clear();
                    }
                    else
                    {
                        Logger.Info("Importato un file vuoto");
                        Console.Clear();
                        utility.errorStyle("Hai importato un file vuoto");
                        Console.ReadLine();
                        Console.Clear();
                    }
                }
                else
                {
                    utility.errorStyle("Nessun file di importazione attività trovato");
                    Logger.Error("Nessun file di importazione attività trovato");
                }

            }
            catch(Exception ex)
            {
                Console.Clear();
                utility.errorStyle($"è stato riscontrato il seguente errore: {ex}");
                Logger.Error(ex);
            }


        }
    }
}
