using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestioneDipendenti.Dipendenti;
using NLog;
using UtilityLib;

namespace GestioneDipendenti.Data
{

    internal class DataManager
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        Utility utility = new Utility();
        bool close = false;

        public void showDataImported(List<Employees> employeesList, List<ActivityEmp> activityList)
        {
            while(!close)
            {
                utility.titleStyle("Visualizzazione dati importati");
                Console.WriteLine("Quali dati hai bisogno di visualizzare?\n1) Dati dipendenti\n2) Dati attività\n3) Oppure F per uscire");
                string userChoose = Console.ReadLine().ToLower();
                switch(userChoose)
                {
                    case "1":
                        Console.Clear();
                        if(employeesList.Count != 0)
                        {
                            foreach(Employees employee in employeesList)
                            {
                                Console.WriteLine($"Matricola: {employee.EmployeesId}, Nome: {employee.FirstName}, Cognome: {employee.LastName}, Ruolo: {employee.Role}, Reparto: {employee.Department}, Indirizzo: {employee.Address} {employee.City} {employee.Province} {employee.Cap}, Età: {employee.Age}, Telefono: {employee.PhoneNumber}\n");
                            }
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            utility.errorStyle("Nessun dato importato");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        break;
                    case "2":
                        Console.Clear();
                        if (activityList.Count != 0)
                        {
                            foreach(ActivityEmp activity in activityList)
                            {
                                Console.WriteLine($"Data: {activity.Date} - Matricola: {activity.EmployeerId} - Attività: {activity.Type} - Ore: {activity.Time}");
                            }
                            Console.ReadLine();
                            Console.Clear();
                        }
                        else
                        {
                            utility.errorStyle("Nessun dato importato");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        break;
                    case "f":
                        close = true;
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        utility.errorStyle("Scegli un'opzione valida");
                        break;
                }
            }
            close = false;
        }
    }
}
