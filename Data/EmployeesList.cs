using GestioneDipendenti.Dipendenti;
using System;
using System.Collections.Generic;
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
        public List<Employees> employeesList = new List<Employees>();

        Utility utility = new Utility();
        public void fillListEmployeersTxt()
        {

            string relPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string filePath = Path.Combine(relPath, "DataEmployees", "Employees.txt");

            string[] fData = File.ReadAllLines(filePath);

            string[] DataClean;

            foreach (string row in fData)
            {
                DataClean = row.Split(';');

                string[] nameSplit = DataClean[1].Split(' ');

                if (utility.testInt(DataClean[4]))
                {
                    int age = int.Parse(DataClean[4]);

                    if (utility.testInt(DataClean[8]))
                    {
                        int cap = int.Parse(DataClean[8]);

                        if (utility.testInt(DataClean[9]))
                        {
                            int phoneNumber = int.Parse(DataClean[9]);

                            Employees employees = new Employees(nameSplit[0], nameSplit[1], age , DataClean[5], DataClean[6], DataClean[7], cap, phoneNumber, DataClean[0], DataClean[4], DataClean[3]);

                            employeesList.Add(employees);
                        }
                    }
                    else
                    {

                    }
                }
                else
                {

                }

            }

        }

    }

    internal class EmployeesActivity
    {
        public List<Activity> activityList = new List<Activity>();

        Utility utility = new Utility();

        public void fillListActivityTxt()
        {
            string relPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string filePath = Path.Combine(relPath, "DataEmployees", "EmployeesActivities.txt");

            string[] fData = File.ReadAllLines(filePath);

            string[] DataClean;

            foreach(string str in fData)
            {
                DataClean = str.Split(';');

                if (utility.testInt(DataClean[2]))
                {
                    int hour = int.Parse(DataClean[2]);

                    Activity activity = new Activity(DataClean[0], DataClean[1], hour, DataClean[3]);

                    activityList.Add(activity);
                }
                else
                {

                }

            }
        }
    }
}
