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
                    Console.WriteLine("======================================================");
                    Console.WriteLine($"Id {employee.EmployeesId}\nNome: {employee.FirstName} {employee.LastName} - Età: {employee.Age} anni");
                    Console.WriteLine($"Ruolo: {employee.Role} - Reparto: {employee.Department}");
                    Console.WriteLine($"Indirizzo: {employee.Address}, {employee.City}, {employee.Province} ({employee.Cap})");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Informazioni di contatto:");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"Numero di telefono: {employee.PhoneNumber}");
                    Console.WriteLine("======================================================");
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
            bool found = false;

            bool close = false;

            utility.titleStyle("Modifica dipendente");

            Console.WriteLine("Inserisci l'id del dipendente che vuoi modificare");

            string userEdit = Console.ReadLine().ToLower();

            foreach(Employees employee in employeesList)
            {
                if(employee.EmployeesId.ToLower() == userEdit)
                {
                    found = true;

                    Console.Clear();
                    while (!close)
                    {
                        Console.WriteLine("======================================================");
                        Console.WriteLine($"Id {employee.EmployeesId}\nNome: {employee.FirstName} {employee.LastName} - Età: {employee.Age} anni");
                        Console.WriteLine($"Ruolo: {employee.Role} - Reparto: {employee.Department}");
                        Console.WriteLine($"Indirizzo: {employee.Address}, {employee.City}, {employee.Province} ({employee.Cap})");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Informazioni di contatto:");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"Numero di telefono: {employee.PhoneNumber}");
                        Console.WriteLine("======================================================");

                        Console.WriteLine("\nQuale campo desideri modificare?");
                        Console.WriteLine("1) Nome\n2) Età\n3) Indirizzo\n4) Ruolo\n5) Reparto\n6) Numero di telefono\nOppure F per uscire");

                        switch (Console.ReadLine().ToLower())
                        {
                            case "1":
                                Console.Clear();

                                Console.WriteLine("Inserisci il nome");
                                employee.FirstName = Console.ReadLine();

                                Console.Clear();

                                Console.WriteLine("Inserisci il cognome");
                                employee.LastName = Console.ReadLine();

                                Console.Clear();
                                utility.successStyle("Nome aggiornato correttamente");
                                break;
                            case "2":
                                Console.Clear();
                                Console.WriteLine("Inserisci l'età aggiornata");
                                string userAge = Console.ReadLine();
                                if (utility.testInt(userAge))
                                {
                                    Console.Clear();
                                    employee.Age = Int32.Parse(userAge);
                                    utility.successStyle("Età aggiornata correttamente");
                                }
                                else
                                {
                                    Console.Clear();
                                    utility.errorStyle("Età non valida");
                                }

                                break;
                            case "3":
                                Console.Clear();
                                Console.WriteLine("Inserisci l'indirizzo");
                                employee.Address = Console.ReadLine();

                                Console.Clear();
                                Console.WriteLine("Inserisci la città");
                                employee.City = Console.ReadLine();

                                Console.Clear();
                                Console.WriteLine("Inserisci la provincia");
                                employee.Province = Console.ReadLine();

                                Console.Clear();
                                Console.WriteLine("Inserisci il cap");
                                employee.Cap = Console.ReadLine();

                                Console.Clear();

                                utility.successStyle("Indirizzo aggiornato correttamente");

                                break;
                            case "4":
                                Console.Clear();
                                Console.WriteLine("Inserisci il ruolo aggiornato");
                                employee.Role = Console.ReadLine();
                                Console.Clear();
                                utility.successStyle("Ruolo aggiornato correttamente");
                                break;
                            case "5":
                                Console.Clear();
                                Console.WriteLine("Inserisci il reparto aggiornato");
                                employee.Department = Console.ReadLine();
                                Console.Clear();
                                utility.successStyle("Reparto aggiornato correttamente");
                                break;
                            case "6":
                                Console.Clear();
                                Console.WriteLine("Inserisci il numero di telefono");
                                string phoneNumber = Console.ReadLine();
                                Console.Clear();
                                employee.PhoneNumber = phoneNumber;
                                utility.successStyle("Numero di telefono aggiornato correttamente");
                                break;
                            case "f":
                                Console.Clear();
                                close = true;
                                break;
                        }

                    }
                    close = false;
                }
            }

            if (!found)
            {
                Console.Clear();
                utility.errorStyle("Nessun dipendente trovato con quell'id");
            }

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
                    Console.WriteLine("======================================================");
                    Console.WriteLine($"Id {employee.EmployeesId}\nNome: {employee.FirstName} {employee.LastName} - Età: {employee.Age} anni");
                    Console.WriteLine($"Ruolo: {employee.Role} - Reparto: {employee.Department}");
                    Console.WriteLine($"Indirizzo: {employee.Address}, {employee.City}, {employee.Province} ({employee.Cap})");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Informazioni di contatto:");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"Numero di telefono: {employee.PhoneNumber}");
                    Console.WriteLine("======================================================");
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
