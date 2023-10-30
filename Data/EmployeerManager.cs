using GestioneDipendenti.Dipendenti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using UtilityLib;

namespace GestioneDipendenti.Data
{
    internal class EmployeerManager
    {
        Utility utility = new UtilityLib.Utility();

        public void searchEmployee(List<Employees> employeesList)
        {
            utility.titleStyle("Ricerca dipendente");
            Console.WriteLine("Inserisci l'id del dipendente che vuoi ricercare");
            string userSearch = Console.ReadLine().ToLower();
            bool found = false;
            Console.Clear();
            foreach(Employees employee in employeesList)
            {
                if(employee.EmployeesId.ToLower() == userSearch)
                {
                    Console.WriteLine($"Id {employee.EmployeesId}\nNome: {employee.FirstName} {employee.LastName} - Età: {employee.Age}");
                    Console.WriteLine($"Ruolo: {employee.Role} - Reparto: {employee.Department}");
                    Console.WriteLine($"Indirizzo: {employee.Address}, {employee.City}, {employee.Province} ({employee.Cap})\n");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Informazioni di contatto:");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"Numero di telefono: {employee.PhoneNumber}");
                    found = true;
                }
            }
            if (!found)
            {
                Console.Clear();
                utility.errorStyle("Nessun dipendente trovato con quell'id");
            }
            Console.ReadLine();
            Console.Clear();
        }
        public void editEmployee(List<Employees> employeesList)
        {
            Console.WriteLine("sei nel modifica");

        }
        public void deleteEmployee(List<Employees> employeesList)
        {
            bool close = false;
            utility.titleStyle("Elimina dipendente");
            Console.WriteLine("Inserisci l'id del dipendente che vuoi eliminare");
            string userSearch = Console.ReadLine().ToLower();
            bool sure = false;
            bool found = false;
            Console.Clear();
            foreach (Employees employee in employeesList)
            {
                if (employee.EmployeesId.ToLower() == userSearch)
                {
                    Console.WriteLine($"Id {employee.EmployeesId}\nNome: {employee.FirstName} {employee.LastName} - Età: {employee.Age}");
                    Console.WriteLine($"Ruolo: {employee.Role} - Reparto: {employee.Department}");
                    Console.WriteLine($"Indirizzo: {employee.Address}, {employee.City}, {employee.Province} ({employee.Cap})\n");
                    Console.ForegroundColor = ConsoleColor.Green;
                    found = true;
                }
            }
            if (!found)
            {
                Console.Clear();
                utility.errorStyle("Nessun dipendente trovato con quell'id");
            }
            else
            {
                while (!close)
                {
                    utility.errorStyle("\nSei sicuro di voler rimuovere questo dipendente?");
                    Console.WriteLine("1) Si\n2) No");
                    string userSure = Console.ReadLine().ToLower();
                    switch (userSure)
                    {
                        case "1":
                            Console.Clear();
                            var itemToRemove = employeesList.Single(r => r.EmployeesId.ToLower() == userSearch);
                            employeesList.Remove(itemToRemove);
                            utility.errorStyle("Dipendente eliminato con successo");
                            close = true;
                            break;
                        case "2":
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
            }
        }
    }
}
