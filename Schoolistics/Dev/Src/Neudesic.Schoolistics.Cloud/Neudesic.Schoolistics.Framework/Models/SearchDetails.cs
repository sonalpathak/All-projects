using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Framework.Models
{
    public class SearchDetails : TableEntity
    {
        public SearchDetails()
        {
        }

        public string Username
        {
            get { return this.PartitionKey; }
            set { this.PartitionKey = value; }
        }

        public string SearchId
        {
            get { return this.RowKey; }
            set { this.RowKey = value; }
        }

        public string SearchName { get; set; }

        public string Keyword { get; set; }

        public string Accreditation { get; set; }

        public bool AdmissionsInProgress { get; set; }

        public int? Class { get; set; }

        public string City { get; set; }

        public int? Rating { get; set; }

        public double? MaximumDistance { get; set; }

        public int? MinimumBudget { get; set; }

        public int? MaximumBudget { get; set; }

        public bool? MidDayMeals { get; set; }

        public bool? DigitalClassroom { get; set; }

        public bool? DayBoarding { get; set; }

        public bool? SchoolBus { get; set; }

        public bool? Playground { get; set; }

        public bool? Transportation { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
