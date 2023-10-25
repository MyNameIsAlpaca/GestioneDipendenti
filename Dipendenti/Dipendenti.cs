using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneDipendenti.Dipendenti
{
    public abstract class Person
    {
        public string FirstName {  get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public int Cap {  get; set; }

        public int PhoneNumber { get; set; }

        public Person(string firstName, string lastName, int age, string address, string city, string province, int cap, int phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Address = address;
            City = city;
            Province = province;
            Cap = cap;
            PhoneNumber = phoneNumber;
        }
    }

    public class Employees : Person
    {
        public string EmployeesId { get; set; }

        public string Department {  get; set; }

        public string Role { get; set; }

        public Employees (string firstName, string lastName, int age, string address, string city, string province, int cap, int phoneNumber, string employeesId, string department, string role)
            : base(firstName, lastName, age, address, city, province, cap, phoneNumber)
        {
            EmployeesId = employeesId;
            Department = department;
            Role = role;
        }
    }

    public class Activity
    {
        public string Date { get; set; }

        public string Type { get; set; }

        public int Time { get; set; }

        public string EmployeerId { get; set; }

        public Activity (string date, string type, int time, string employeerId)
        {
            this.Date = date;
            this.Type = type;
            this.Time = time;
            this.EmployeerId = employeerId;
        }
    }
}
