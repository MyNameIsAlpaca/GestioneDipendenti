using GestioneDipendenti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UtilityLib;
using NLog;
using System.Data.SqlClient;
using System.Configuration;

namespace GestioneDipendenti
{
    internal class Menu
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public void openMenu()
        {
            DbManager dbManager = new DbManager(ConfigurationManager.AppSettings["CnnDbString"]);

            DataInfo dataInfo = new DataInfo();

            Utility utility = new UtilityLib.Utility();

            EmployeesList employeesList = new EmployeesList();

            EmployeesActivity employeesActivity = new EmployeesActivity();

            DataManager dataManager = new DataManager();

            EmployeerManager employeerManager = new EmployeerManager();

            bool close = false;

            while(!close)
            {
                utility.titleStyle("Benvenuto");

                Console.WriteLine("Scegli cosa vuoi fare\n1) Importa dati\n2) Visualizza dati importati\n3) Seleziona dati specifici\n4) Gestione dipendenti\n5) Serializza tutti i dati in JSON\n6) Aggiornamento DataBase\n7) Esci");

                string userChoose = Console.ReadLine();

                switch (userChoose)
                {
                    case "1":
                        Console.Clear();
                        while (!close)
                        {
                        utility.titleStyle("Importazione dati");
                        Console.WriteLine("Che tipo di dati vuoi importare?\n1) Dati dipendenti\n2) Dati attività\n3) Importa dati dipendente da database\n4) Importa dati attività da database\nOppure F per tornare indietro");
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
                                    employeesActivity.fillListActivityTxt(employeesList.employeesList);
                                    close = true;
                                    break;
                                case "3":
                                    Console.Clear();
                                    dbManager.fillEmployeesListWithDb(employeesList.employeesList);
                                    Console.ReadLine();
                                    break;
                                case "4":
                                    Console.Clear();
                                    dbManager.fillActivityListWithDb(employeesList.employeesList, employeesActivity.activityList);
                                    Console.ReadLine();
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
                            if(employeesList.employeesList.Count == 0 || employeesActivity.activityList.Count == 0)
                            {
                                utility.errorStyle("Non sono presenti abbastanza dati per avere dei dati specifici");
                                Console.ReadLine();
                                Console.Clear();
                                break;
                            }
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
                        while (!close)
                        {
                            utility.titleStyle("Gestione dipendenti");
                            if(employeesList.employeesList.Count == 0)
                            {
                                utility.errorStyle("Nessun dipendente presente nel database");
                                Console.ReadLine();
                                Console.Clear();
                                break;
                            }
                            Console.WriteLine("Cosa vuoi fare?\n1) Visualizza dipendente\n2) Modifica dipendente\n3) Elimina dipendente\n Oppure F per tornare indietro");
                            switch (Console.ReadLine().ToLower())
                            {
                                case "1":
                                    Console.Clear();
                                    employeerManager.searchEmployee(employeesList.employeesList);
                                    break;
                                case "2":
                                    Console.Clear();
                                    employeerManager.editEmployee(employeesList.employeesList);
                                    break;
                                case "3":
                                    Console.Clear();
                                    employeerManager.deleteEmployee(employeesList.employeesList);
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
                    case "5":
                        Console.Clear();
                        utility.titleStyle("Serializzazione dati");
                        if (employeesList.employeesList.Count == 0)
                        {
                            utility.errorStyle("Non sono presenti dati da serializzare");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                        dataManager.exportJson(employeesList.employeesList);
                        Console.Clear();
                        utility.successStyle("Esportazione completata");
                        break;
                    case "6":
                        Console.Clear();
                        utility.titleStyle("Aggiornamento Database");
                        while (!close)
                        {
                        Console.WriteLine("Cosa desideri fare?\n1) Modifica dipendente\n2) Elimina dipendente\n Oppure F per uscire");
                            switch (Console.ReadLine().ToLower())
                            {
                                case "1":
                                    employeerManager.editEmployeeInDb();
                                    break;
                                case "2":
                                    Console.Clear();
                                    Console.WriteLine("Sei nell'elimina");
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
                    case "7":
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
