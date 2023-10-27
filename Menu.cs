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

            DataInfo dataInfo = new DataInfo();

            Utility utility = new UtilityLib.Utility();

            EmployeesList employeesList = new EmployeesList();

            EmployeesActivity employeesActivity = new EmployeesActivity();

            DataManager dataManager = new DataManager();

            bool close = false;

            while(!close)
            {
                utility.titleStyle("Benvenuto");

                Console.WriteLine("Scegli cosa vuoi fare\n1) Importa dati\n2) Visualizza dati importati\n3) Seleziona dati specifici\n4) Serializza tutti i dati in JSON\n5) Esci");

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
                        while (!close)
                        {
                            utility.titleStyle("Dati specifici");
                            Console.WriteLine("Quali dati vuoi consultare?\n1) Età media del personale\n2) Età media per reparto\n3) Totale ore lavoro per reparto\n4) Totale ore lavoro per nominativo\n5) Totale ore straordinarie\n6) Totale ore straordinarie per nominativo\n7) Totale ore ferie\n8) Totale ore ferie per nominativo\n9) Ore prefestive con data e nominativo\nOppure F per tornare indietro");
                            userChoose = Console.ReadLine();
                            switch (userChoose)
                            {
                                case "1":
                                    Console.Clear();
                                    dataInfo.avgAge(employeesList.employeesList, employeesActivity.activityList);

                                    Console.ReadLine();
                                    Console.Clear();
                                    break;
                                case "2":
                                    Console.Clear();
                                    dataInfo.avgAgeDep(employeesList.employeesList, employeesActivity.activityList);
                                    Console.Clear();
                                    break;
                                case "3":
                                    Console.Clear();
                                    dataInfo.totalHourDep(employeesList.employeesList, employeesActivity.activityList);
                                    Console.Clear();
                                    break;
                                case "4":
                                    Console.Clear();
                                    dataInfo.totalHourName(employeesList.employeesList, employeesActivity.activityList);
                                    Console.Clear();
                                    break;
                                case "5":
                                    Console.Clear();
                                    dataInfo.totalExtra(employeesList.employeesList, employeesActivity.activityList);
                                    Console.Clear();
                                    break;
                                case "6":
                                    Console.Clear();
                                    dataInfo.totalExtraName(employeesList.employeesList, employeesActivity.activityList);
                                    Console.Clear();
                                    break;
                                case "7":
                                    Console.Clear();
                                    dataInfo.totalHourHolidays(employeesList.employeesList, employeesActivity.activityList);
                                    Console.Clear();
                                    break;
                                case "8":
                                    Console.Clear();
                                    dataInfo.totalHourHolidaysName(employeesList.employeesList, employeesActivity.activityList);
                                    Console.Clear();
                                    break;
                                case "9":
                                    Console.Clear();
                                    dataInfo.preHolidaysCalc(employeesList.employeesList, employeesActivity.activityList);
                                    Console.Clear();
                                    break;
                                case "f":
                                    Console.Clear();
                                    close = true;
                                    break;
                            }
                        }
                        close = false;
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
