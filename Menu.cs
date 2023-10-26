using GestioneDipendenti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UtilityLib;

namespace GestioneDipendenti
{
    internal class Menu
    {
        public void openMenu()
        {
            Utility utility = new UtilityLib.Utility();

            EmployeesList employeesList = new EmployeesList();

            EmployeesActivity employeesActivity = new EmployeesActivity();

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
                        utility.titleStyle("Importazione dati");
                        Console.WriteLine("Importazione in corso...");
                        employeesList.fillListEmployeersTxt();
                        employeesActivity.fillListActivityTxt();
                        
                        break;

                    case "2":
                        Console.Clear();
                        utility.titleStyle("Visualizzazione Dati");
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
