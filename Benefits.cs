using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Braaten_CourseProject_Part2
{
    [Serializable]
    public class Benefits
    {
        private string healthInsurance;
        private int lifeInsurance;
        private int vacation;
        public string HealthInsurance
        {
            get { return healthInsurance; }
            set { healthInsurance = value; }
        }
        public int LifeInsurance
        {
            get { return lifeInsurance; }
            set { lifeInsurance = value; }
        }
        public int Vacation
        {
            get { return vacation; }
            set { vacation = value; }
        }

        public Benefits()
        {
            HealthInsurance = null; LifeInsurance = 0; Vacation = 0;
        }

        public Benefits(string healthInsurance, int lifeInsurance, int vacation)
        {
            HealthInsurance = healthInsurance; LifeInsurance = lifeInsurance; Vacation = vacation;
        }

        public override string ToString()
        {
            return $"{HealthInsurance} {LifeInsurance} {Vacation}";
        }
    }
}
