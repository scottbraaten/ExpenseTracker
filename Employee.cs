using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Braaten_CourseProject_Part2
{
    [Serializable]
    public abstract class Employee
    {
        private string firstName;
        private string lastName;
        private string ssn;
        private DateTime hireDate;
        private Benefits benefits;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public string SSN
        {
            get { return ssn; }
            set { ssn = value; }
        }
        public DateTime HireDate
        {
            get { return hireDate; }
            set { hireDate = value; }
        }
        public Benefits BenefitsEmp
        {
            get { return benefits; }
            set { benefits = value; }
        }

        public Employee()
        {
            FirstName = null; LastName = null; SSN = null; HireDate = new DateTime(); BenefitsEmp = new Benefits();
        }

        public Employee(string firstName, string lastName, string ssn, DateTime hireDate, Benefits benefits)
        {
            FirstName = firstName; LastName = lastName; SSN = ssn; HireDate = hireDate; BenefitsEmp = benefits;
        }

        public override string ToString()
        {
            return $"firstName={FirstName}, lastName={LastName}, ssn={SSN}, hireDate={HireDate.ToShortDateString()}" +
                $", healthIns={BenefitsEmp.HealthInsurance}, lifeIns={BenefitsEmp.LifeInsurance}" +
                $", vacation={BenefitsEmp.Vacation}";
        }

        public abstract double CalculatePay();
    }
}
