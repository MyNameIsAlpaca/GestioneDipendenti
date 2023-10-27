using GestioneDipendenti.Dipendenti;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestioneDipendenti.Data
{
    internal class DataInfo
    {
        public void avgAge(List<Employees> employeesList, List<ActivityEmp> activityList)
        {

            int avgAge = Convert.ToInt32(employeesList.Select(x => x.Age).Average());

            Console.WriteLine($"L'età media del personale è: {avgAge}");

        }
        public void avgAgeDep(List<Employees> employeesList, List<ActivityEmp> activityList)
        {
            var avgAgeD = employeesList.GroupBy(a => a.Department).ToDictionary(g=> g.Key, g=> Convert.ToInt32(g.Average(a => a.Age)));

            foreach(var value  in avgAgeD)
            {
                Console.WriteLine($"L'età media nel reparto {value.Key}: {value.Value} anni");
            }

            Console.ReadLine();
        }
        public void totalHourDep(List<Employees> employeesList, List<ActivityEmp> activityList)
        {

            var query = from employees in employeesList
                        join activity in activityList on employees.EmployeesId equals activity.EmployeerId
                        where activity.Type != "Ferie"
                        select new { department = employees.Department, HourTime = activity.Time };

            Dictionary<string, int> queryDic = query.GroupBy(a => a.department).ToDictionary(g => g.Key, g => Convert.ToInt32(g.Sum(a => a.HourTime)));

            foreach (var value in queryDic)
            {
                Console.WriteLine($"Il totale di ore nel reparto {value.Key} è di {value.Value} ore");
            }

            Console.ReadLine();
        }
        public void totalHourName(List<Employees> employeesList, List<ActivityEmp> activityList)
        {

        }
        public void totalExtra(List<Employees> employeesList, List<ActivityEmp> activityList)
        {

        }
        public void totalExtraName(List<Employees> employeesList, List<ActivityEmp> activityList)
        {

        }
        public void totalHourHolidays(List<Employees> employeesList, List<ActivityEmp> activityList)
        {

        }
        public void totalHourHolidaysName(List<Employees> employeesList, List<ActivityEmp> activityList)
        {

        }
        public void preHolidaysCalc(List<Employees> employeesList, List<ActivityEmp> activityList)
        {

        }
    }
}
