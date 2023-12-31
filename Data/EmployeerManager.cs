﻿using GestioneDipendenti.Dipendenti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using UtilityLib;
using System.Configuration;
using NLog;

namespace GestioneDipendenti.Data
{
    internal class EmployeerManager
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        DbManager dbManager = new DbManager(ConfigurationManager.AppSettings["CnnDbString"]);

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

        public void deleteEmployeeInDb()
        {
            bool close = false;

            Console.Clear();

            utility.titleStyle("Elimina dipendente dal database");

            while (!close)
            {
                utility.insertStyle("Inserisci la matricola del dipendente che desideri eliminare");

                string matricola = Console.ReadLine();

                if (dbManager.checkEmployeeInDb(matricola))
                {
                    Console.Clear();
                    while (!close)
                    {
                        utility.errorStyle($"Attenzione! Sei sicuro di voler eliminare il dipendente {matricola}?");
                        Console.WriteLine("1) Si\n2) No");
                        switch (Console.ReadLine())
                        {
                            case "1":
                                Console.Clear();
                                dbManager.deleteEmployeesFromDb(matricola);
                                Logger.Info($"È stato eliminato il dipendente {matricola}");
                                utility.successStyle("Eliminazione avvenuta con successo");
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
                }
                else
                {
                    Console.Clear();
                    utility.errorStyle("Non è presente nessun dipendente con quella matricola nel database");
                }
            }
            close = false;

        }

        public void editEmployeeInDb()
        {
            bool close = false;
            Console.Clear();
            utility.titleStyle("Modifica dipendente");
            while (!close)
            {
                utility.insertStyle("Inserisci la matricola del dipendente da modificare");
                Console.WriteLine("Oppure F per uscire");
                string matricola = Console.ReadLine().ToUpper();
                if (matricola == "F")
                {
                    Console.Clear();
                    close = true;
                    break;
                }
                else if (matricola.Length != 4)
                {
                    Console.Clear();
                    utility.errorStyle("Inserisci una matricola valida");
                }
                else
                {
                    if (dbManager.checkEmployeeInDb(matricola))
                    {
                        Console.Clear();
                        utility.titleStyle($"Modifica dipendente {matricola}");
                        Console.WriteLine("Quale campo desideri modificare?\n1) Nome\n2) Età\n3) Domicilio\n4) Ruolo\n5) Reparto\n6) Numero di telefono");
                        while (!close)
                        {
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    Console.Clear();
                                    utility.insertStyle("Inserisci il nome aggiornato");
                                    string name = Console.ReadLine();
                                    dbManager.updateEmployeesInDb(matricola, name, "Nominativo");
                                    Console.Clear();
                                    utility.successStyle("Nome dipendente aggiornato con successo");
                                    Logger.Info($"È stato modificato il nome del dipendente {matricola} in {name}");
                                    close = true;
                                    break;
                                case "2":
                                    Console.Clear();
                                    utility.insertStyle("Inserisci l'età aggiornato");
                                    string age = Console.ReadLine();
                                    dbManager.updateEmployeesInDb(matricola, age, "Eta");
                                    Console.Clear();
                                    Logger.Info($"È stata aggiornata l'età del dipendente {matricola} in {age}");
                                    utility.successStyle("Età del dipendente aggiornata con successo");
                                    close = true;
                                    break;
                                case "3":
                                    Console.Clear();
                                    while (!close)
                                    {
                                        Console.WriteLine("Quale parte dell'domicilio desideri aggiornare?\n1) Indirizzo\n2) Città\n3) Provincia\n4) Cap");
                                        switch (Console.ReadLine())
                                        {
                                            case "1":
                                                Console.Clear();
                                                utility.insertStyle("Inserisci l'indirizzo aggiornato");
                                                string address = Console.ReadLine();
                                                dbManager.updateEmployeesInDb(matricola, address, "Indirizzo");
                                                close = true;
                                                Console.Clear();
                                                Logger.Info($"È stato modificato l'indirizzo del dipendente {matricola} in {address}");
                                                utility.successStyle("Indirizzo aggiornato con successo");
                                                break;
                                            case "2":
                                                Console.Clear();
                                                utility.insertStyle("Inserisci la città aggiornato");
                                                string city = Console.ReadLine();
                                                Logger.Info($"È stata aggiornata la città del dipendente {matricola} in {city}");
                                                dbManager.updateEmployeesInDb(matricola, city, "Citta");
                                                close = true;
                                                Console.Clear();
                                                utility.successStyle("Città aggiornata con successo");
                                                break;
                                            case "3":
                                                Console.Clear();
                                                utility.insertStyle("Inserisci la provincia aggiornata");
                                                string province = Console.ReadLine();
                                                dbManager.updateEmployeesInDb(matricola, province, "Provincia");
                                                close = true;
                                                Console.Clear();
                                                Logger.Info($"È stata aggiornata la provincia del dipendente {matricola} in {province}");
                                                utility.successStyle("Provincia aggiornata con successo");
                                                break;
                                            case "4":
                                                Console.Clear();
                                                utility.insertStyle("Inserisci il cap aggiornato");
                                                string cap = Console.ReadLine();
                                                dbManager.updateEmployeesInDb(matricola, cap, "Cap");
                                                close = true;
                                                Console.Clear();
                                                Logger.Info($"È stato aggiornato il cap del dipendente {matricola} in {cap}");
                                                utility.successStyle("Cap aggiornato con successo");
                                                break;
                                            default:
                                                Console.Clear();
                                                utility.errorStyle("Scegli un'opzione valida");
                                                break;
                                        }
                                    }
                                    break;
                                case "4":
                                    Console.Clear();
                                    utility.insertStyle("Inserisci il ruolo aggiornato");
                                    string ruolo = Console.ReadLine();
                                    dbManager.updateEmployeesInDb(matricola, ruolo, "Ruolo");
                                    Console.Clear();
                                    Logger.Info($"È stata aggiornato il ruolo del dipendente {matricola} in {ruolo}");
                                    utility.successStyle("Ruolo dipendente aggiornato con successo");
                                    close = true;
                                    break;
                                case "5":
                                    Console.Clear();
                                    utility.insertStyle("Inserisci il reparto aggiornato");
                                    string reparto = Console.ReadLine();
                                    dbManager.updateEmployeesInDb(matricola, reparto, "Reparto");
                                    Console.Clear();
                                    Logger.Info($"È stata aggiornato il reparto del dipendente {matricola} in {reparto}");
                                    utility.successStyle("Reparto dipendente aggiornato con successo");
                                    close = true;
                                    break;
                                case "6":
                                    Console.Clear();
                                    utility.insertStyle("Inserisci il numero di telefono aggiornato");
                                    while (!close)
                                    {
                                        string numero = Console.ReadLine();
                                        if (!utility.testInt(numero) || numero.Length < 9 || numero.Length > 13)
                                        {
                                            Console.Clear();
                                            utility.errorStyle("Inserisci un numero valido");
                                        }
                                        else
                                        {
                                            dbManager.updateEmployeesInDb(matricola, numero, "Telefono");
                                            Console.Clear();
                                            Logger.Info($"È stata aggiornato il numero di telefono del dipendente {matricola} in {numero}");
                                            utility.successStyle("Numero di telefono del dipendente aggiornato con successo");
                                            close = true;
                                        }
                                    }
                                    break;
                                default:
                                    utility.errorStyle("Scegli un'opzione valida");
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Console.Clear();
                        utility.errorStyle("Nessun dipendente con quella matricola trovato nel database");
                    }
                }
            }
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
