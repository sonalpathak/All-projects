using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Framework.API_Models
{
    public class SchoolDetail
    {
        public string SchoolId { get; set; }
       
        public string SchoolName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }
        
        public string SchoolLogo { get; set; }

        public string Accreditation { get; set; }

        public bool AdmissionsInProgress { get; set; }

        public int SchoolStrength { get; set; }

        public int LowerClassRange { get; set; }

        public int UpperClassRange { get; set; }

        public string WebsiteLink { get; set; }

        public string Description { get; set; }

        public double UserRating { get; set; }

        public double Rating { get; set; }

        public int RatingsCount { get; set; }

        public bool MidDayMeals { get; set; }

        public bool DigitalClassroom { get; set; }

        public bool DayBoarding { get; set; }

        public bool SchoolBus { get; set; }

        public bool Playground { get; set; }

        public bool Transportation { get; set; }

        public bool IsFeatured { get; set; }
    }
}
