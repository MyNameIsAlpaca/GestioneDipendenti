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
                Console.WriteLine($"L'età media nel reparto {value.Key}: {value.Value} anni\n");
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
                Console.WriteLine($"Il totale di ore nel reparto {value.Key} è di {value.Value} ore\n");
            }

            Console.ReadLine();
        }
        public void totalHourName(List<Employees> employeesList, List<ActivityEmp> activityList)
        {
            var query = from employees in employeesList
                        join activity in activityList on employees.EmployeesId equals activity.EmployeerId
                        where activity.Type != "Ferie"
                        select new { nominativo = employees.FirstName + ' ' + employees.LastName, HourTime = activity.Time };

            Dictionary<string, int> queryDic = query.GroupBy(a => a.nominativo).ToDictionary(g => g.Key, g => Convert.ToInt32(g.Sum(a => a.HourTime)));

            foreach (var value in queryDic)
            {
                Console.WriteLine($"Il totale di ore del dipendente: {value.Key} è di {value.Value} ore\n");
            }

            Console.ReadLine();
        }
        public void totalExtra(List<Employees> employeesList, List<ActivityEmp> activityList)
        {
            var query = from employees in employeesList
                        join activity in activityList on employees.EmployeesId equals activity.EmployeerId
                        where activity.Type != "Ferie" && activity.Time > 8
                        select activity.Time;
            int cleanQuery = 0;

            foreach(var value in query)
            {
                int valueClean = value - 8;

                cleanQuery = cleanQuery + valueClean;
            }

            Console.WriteLine($"Il totale delle ore extra è {cleanQuery}\n");

            Console.ReadLine();
        }
        public void totalExtraName(List<Employees> employeesList, List<ActivityEmp> activityList)
        {
            var query = from employees in employeesList
                        join activity in activityList on employees.EmployeesId equals activity.EmployeerId
                        where activity.Type != "Ferie" && activity.Time > 8
                        select new { nominativo = employees.FirstName + ' ' + employees.LastName, HourTime = activity.Time };
            
            Dictionary<string, int> queryDic = query.GroupBy(a => a.nominativo).ToDictionary(g => g.Key, g => Convert.ToInt32(g.Sum(a => a.HourTime - 8)));

            foreach(var value in queryDic)
            {
                Console.WriteLine($"Il totale ore extra del dipendente {value.Key} è di {value.Value}\n");
            }

            Console.ReadLine();
        }
        public void totalHourHolidays(List<Employees> employeesList, List<ActivityEmp> activityList)
        {
            var holidayTime = from employees in employeesList
                        join activity in activityList on employees.EmployeesId equals activity.EmployeerId
                        where activity.Type == "Ferie"
                        select activity.Time;
            int holidayTimeTotal = holidayTime.Sum();

            Console.WriteLine($"Le ore di ferie totali è {holidayTimeTotal}\n");
            Console.ReadLine();
        }
        public void totalHourHolidaysName(List<Employees> employeesList, List<ActivityEmp> activityList)
        {
            var query = from employees in employeesList
                        join activity in activityList on employees.EmployeesId equals activity.EmployeerId
                        where activity.Type == "Ferie"
                        select new { nominativo = employees.FirstName + ' ' + employees.LastName, HourTime = activity.Time };

            Dictionary<string, int> queryDic = query.GroupBy(a => a.nominativo).ToDictionary(g => g.Key, g => Convert.ToInt32(g.Sum(a => a.HourTime)));

            foreach(var value in queryDic)
            {
                Console.WriteLine($"Le ore di ferie effettuate da {value.Key} sono: {value.Value}\n");
            }
            Console.ReadLine();
        }
        public void preHolidaysCalc(List<Employees> employeesList, List<ActivityEmp> activityList)
        {
            var query = from employees in employeesList
                        join activity in activityList on employees.EmployeesId equals activity.EmployeerId
                        where activity.Type == "Pre Festivo"
                        select new { nominativo = employees.FirstName + ' ' + employees.LastName, HourTime = activity.Time, fromDate = activity.Date };

            foreach ( var value in query )
            {
                Console.WriteLine($"{value.fromDate}, {value.nominativo}, {value.HourTime}\n");
            }

            Console.ReadLine();
        }
    }
}
