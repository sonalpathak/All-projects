using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Framework.Models
{
    public class School : TableEntity
    {
        public School()
        { }

        public string SchoolId
        {
            get { return this.RowKey; }
            set { this.RowKey = value; }
        }

        public string SchoolName { get; set; }

        public string Address { get; set; }

        public string City
        {
            get { return this.PartitionKey; }
            set { this.PartitionKey = value; }
        }

        public string SchoolLogo { get; set; }


        public string Accreditation { get; set; }

        public bool AdmissionsInProgress { get; set; }

        public string Phone { get; set; }

        public int SchoolStrength { get; set; }

        public int LowerClassRange { get; set; }

        public string Email { get; set; }

        public int UpperClassRange { get; set; }

        public string WebsiteLink { get; set; }

        public string Residential { get; set; }

        public string Description { get; set; }

        public double Rating { get; set; }

        public int RatingsCount { get; set; }

        public bool MidDayMeals { get; set; }

        public bool DigitalClassroom { get; set; }

        public bool DayBoarding { get; set; }

        public bool SchoolBus { get; set; }

        public string SchoolHead { get; set; }

        public string Mandal { get; set; }

        public string GirlsBoysCoEd { get; set; }

        public bool Playground { get; set; }

        public bool Transportation { get; set; }

        public bool IsFeatured { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

    }
}
