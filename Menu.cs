using GestioneDipendenti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UtilityLib;
using NLog;

namespace GestioneDipendenti
{
    internal class Menu
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public void openMenu()
        {

            Utility utility = new UtilityLib.Utility();

            EmployeesList employeesList = new EmployeesList();

            EmployeesActivity employeesActivity = new EmployeesActivity();

            DataManager dataManager = new DataManager();

            bool close = false;

            while(!close)
            {
                utility.titleStyle("Benvenuto");

                Console.WriteLine("Scegli cosa vuoi fare\n1) Importa dati\n2) Visualizza dati importati\n3) Seleziona dati speicifici\n4) Serializza tutti i dati in JSON\n5) Esci");

                string userChoose = Console.ReadLine();

                switch (userChoose)
                {
                    case "1":
                        Console.Clear();
                        while (!close)
                        {
                        utility.titleStyle("Importazione dati");
                        Console.WriteLine("Che tipo di dati vuoi importare?\n1) Dati dipendenti\n2) Dati attività\nOppure F per tornare indietro");
                        userChoose = Console.ReadLine().ToLower();
                            switch (userChoose)
                            {
                                case "1":
                                    Console.Clear();
                                    utility.titleStyle("Importazione dati");
                                    Console.WriteLine("Importazione in corso...");
                                    employeesList.fillListEmployeersTxt();
                                    close = true;
                                    break;
                                case "2":
                                    Console.Clear();
                                    utility.titleStyle("Importazione dati");
                                    Console.WriteLine("Importazione in corso...");
                                    employeesActivity.fillListActivityTxt();
                                    close = true;
                                    break;
                                case "f":
                                    Console.Clear();
                                    close = true;
                                    break;
                                default:
                                    Console.Clear();
                                    utility.errorStyle("Scegli un'opzione valida");
                                    break;
                            }
                        }
                        close = false;
                        break;

                    case "2":
                        Console.Clear();
                        dataManager.showDataImported(employeesList.employeesList, employeesActivity.activityList);
                        break;

                    case "3":
                        Console.Clear();
                        utility.titleStyle("Selezione dati");
                        break;
                    case "4":
                        Console.Clear();
                        utility.titleStyle("Serializzazione dati");
                        break;
                    case "5":
                        Console.Clear();
                        close = true;
                        break;
                    default:
                        Console.Clear();
                        utility.errorStyle("Scegli un'opzione valida");
                        break;
                }

            }

        }
    }
}
