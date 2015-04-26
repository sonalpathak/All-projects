using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Entities
{
    public class School
    {
        public string SchoolId { get; set; }

        public string SchoolName { get; set; }

        public string SchoolLogo { get; set; }

        public string Accreditation { get; set; }

        public bool AdmissionsInProgress { get; set; }

        public int SchoolStrength { get; set; }

        public string LowerClassRange { get; set; }

        public string UpperClassRange { get; set; }

        public string WebsiteLink { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public double UserRating { get; set; }

        public double Rating { get; set; }

        public int RatingsCount { get; set; }

        public bool MidDayMeals { get; set; }

        public bool DigitalClassroom { get; set; }

        public bool DayBoarding { get; set; }

        public bool SchoolBus { get; set; }

        public bool Playground { get; set; }

        public bool Transportation { get; set; }

        public List<Location> Locations { get; set; }

        public double Distance { get; set; }
        public bool IsFeatured { get; set; }
    }
}
