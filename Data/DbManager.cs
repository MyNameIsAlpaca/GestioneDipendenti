using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using GestioneDipendenti.Dipendenti;
using System.Configuration;
using NLog;
using UtilityLib;

namespace GestioneDipendenti.Data
{
    internal class DbManager
    {
        Utility utility = new Utility();
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private SqlConnection sqlCnn = new();

        private SqlCommand sqlCommand = new SqlCommand();

        public bool IsDbValid = false;

        public DbManager(string dbCnnString)
        {
            try
            {
                sqlCnn.ConnectionString = dbCnnString;
                sqlCnn.Open();
                IsDbValid = true;
                sqlCnn.Close();
            }
            catch 
            {
                throw;
            }
        }

        public void CheckDb()
        {
            IsDbValid = false;

            try
            {
                if(sqlCnn.State == System.Data.ConnectionState.Closed) 
                {
                    sqlCnn.Open();
                    IsDbValid = true;
                }
                else if (sqlCnn.State == System.Data.ConnectionState.Open)
                {
                    IsDbValid = true;
                }
            } 
            catch(Exception ex)
            {
                throw;
            }
        }

        public void fillEmployeesListWithDb(List<Employees>employeesList)
        {
            try
            {
                CheckDb();
                using (SqlCommand  sqlCommand = new SqlCommand())
                {
                    int successImport = 0;

                    sqlCommand.CommandText = "select * from [dbo].[AnagraficaGenerica]";

                    sqlCommand.Connection = sqlCnn;

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            string id = reader["Matricola"].ToString();

                            var EmployeesNeedAdd = employeesList.SingleOrDefault(r => r.EmployeesId.ToLower() == id.ToLower());

                            if(EmployeesNeedAdd != null)
                            {
                                Console.Clear();
                                utility.errorStyle($"Il dipendente con matricola {id} è già presente nel database");
                                Console.ReadLine();
                            }
                            else
                            {
                                string fullName = reader["Nominativo"].ToString();

                                string ruolo = reader["Ruolo"].ToString();

                                string reparto = reader["Reparto"].ToString();

                                int eta = int.Parse(reader["Eta"].ToString());

                                string indirizzo = reader["Indirizzo"].ToString();

                                string citta = reader["Citta"].ToString();

                                string provincia = reader["Provincia"].ToString();

                                string cap = reader["Cap"].ToString();

                                string telefono = reader["Telefono"].ToString();

                                string[] nameSplit = fullName.Split(' ');

                                string firstName = nameSplit[0];

                                string lastName = nameSplit[1];

                                Employees employees = new Employees(firstName, lastName, eta, indirizzo, citta, provincia, cap, telefono, id, reparto, ruolo);

                                successImport++;

                                employeesList.Add(employees);

                            }

                        }
                        if(successImport > 0)
                        {
                            Console.Clear();
                            utility.successStyle($"Hai importato correttamente {successImport} dipendenti");
                        }
                        else
                        {
                            Console.Clear();
                            utility.errorStyle("Non hai importato nessun dipendente");
                        }

                    }

                }
                
                Logger.Info("Importatazione tramite database");
            } 
            catch(Exception ex)
            {
                Logger.Error($"L'importazione del db ha riscontrato un errore: {ex}");
                throw;
            }
        }

        public void fillActivityListWithDb(List<Employees> employeesList, List<ActivityEmp> activityList)
        {
            try
            {
                CheckDb();
                using(SqlCommand  cmd = new SqlCommand())
                {
                    int successImport = 0;

                    sqlCommand.CommandText = "select * from [dbo].AttivitaDipendente";

                    sqlCommand.Connection = sqlCnn;

                    using(SqlDataReader reader =  sqlCommand.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            string matricola = reader["Matricola"].ToString();
                            string data = reader["DataAttivita"].ToString();
                            string attivita = reader["Attivita"].ToString();
                            string hourString = reader["Ore"].ToString();
                            int hour = int.Parse(hourString);

                            ActivityEmp activity = new ActivityEmp(data, attivita, hour, matricola);

                            activityList.Add(activity);

                            foreach (var employees in employeesList)
                            {
                                if (employees.EmployeesId == activity.EmployeerId)
                                {
                                    employees.activityEmp.Add(activity);
                                }
                            }

                            successImport++;
                        }
                    }
                    if (successImport > 0)
                    {
                        Console.Clear();
                        utility.successStyle($"Hai importato correttamente {successImport} attivita");
                    }
                    else
                    {
                        Console.Clear();
                        utility.errorStyle("Non hai importato nessuna attivita");
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Error($"L'importazione del db ha riscontrato un errore: {ex}");
                throw;
            }
        }
    }
}
