using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace SitaTechAssessment
{
    public class Departments : Parcels
    {
        public Parcels parcels = new Parcels();

        //Properties of a department
        public string Name { get; set; }
        public string WeightLimit { get; set; }
        public string ValueLimit { get; set; }
        public int Packages { get; set; }

        public Departments()
        {

        }
        public Departments(string name, string weightlimit, string valuelimit)
        {
            Name = name;
            WeightLimit = weightlimit;
            ValueLimit = valuelimit;
        }

        //overrride ToString method for departments for the tidy presentation to the user
        public override string ToString()
        {
            string output = "- Name : " + Name;
            return output;
        }

        //Checks each property of a given department is valid and not null
        public bool CheckDepartments(Departments d)
        {
            if (Regex.IsMatch(d.Name, @"^[\sa-zA-Z]+$") &&
                Regex.IsMatch(d.WeightLimit, @"^[\s0-9<>=]+$") &&
                d.ValueLimit != null)
            {
                if (d.Packages >= 0)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }

        public bool CheckDepartmentDuplicate(Departments d, List<Departments> departments)
        {
            foreach (Departments dep in departments)
            {
                if (d.Name == dep.Name)
                {
                    return false;
                }
                else if (d.WeightLimit == dep.WeightLimit)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
