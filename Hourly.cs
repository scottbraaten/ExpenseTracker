using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Braaten_CourseProject_Part2
{
    [Serializable]
    internal class Hourly : Employee
    {
        private double hourlyRate;
        private double hoursWorked;

        public double HourlyRate
        {
            get { return hourlyRate; }
            set { hourlyRate = value; }
        }

        public double HoursWorked
        {
            get { return hoursWorked; }
            set { hoursWorked = value; }
        }

        public Hourly(string firstName, string lastName, string ssn, DateTime hireDate, Benefits benefits, double hourlyRate, double hoursWorked) : base (firstName, lastName, ssn, hireDate, benefits)
        {
            HourlyRate = hourlyRate;
            HoursWorked = hoursWorked;
        }

        public override double CalculatePay()
        {
            return HourlyRate * HoursWorked;
        }

        public override string ToString()
        {
            return base.ToString() + ", hourlyRate=" + HourlyRate + ", hoursWorked=" + HoursWorked;
        }
    }
}
