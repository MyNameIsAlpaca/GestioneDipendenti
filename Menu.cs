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

            employeesList.fillListEmployeersTxt();

            EmployeesActivity employeesActivity = new EmployeesActivity();

            employeesActivity.fillListActivityTxt();

            bool close = false;

            while(!close)
            {
                utility.titleStyle("Benvenuto");

            }

        }
    }
}
