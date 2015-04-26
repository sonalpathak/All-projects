using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Entities
{
    public class SearchDetails
    {
        public string Username { get; set; }

        public string SearchId { get; set; }

        public string SearchName { get; set; }

        public string Keyword { get; set; }

        public string Accreditation { get; set; }

        public bool AdmissionsInProgress { get; set; }

        public string Class { get; set; }

        public string City { get; set; }

        public int Rating { get; set; }

        public int MaximumDistance { get; set; }

        public int MinimumBudget { get; set; }

        public int MaximumBudget { get; set; }       

        public bool MidDayMeals { get; set; }

        public bool DigitalClassroom { get; set; }

        public bool DayBoarding { get; set; }

        public bool SchoolBus { get; set; }

        public bool Playground { get; set; }

        public bool Transportation { get; set; }
    }
}
