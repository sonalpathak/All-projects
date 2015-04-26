using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Framework.Models
{
    public class Survey : TableEntity
    {
        public string SurveyId
        {
            get { return this.PartitionKey; }
            set { this.PartitionKey = value; }
        }

        public string OptionId
        {
            get { return this.RowKey; }
            set { this.RowKey = value; }
        }
       
        public string Question { get; set; }

        public string Option { get; set; }

        public int Votes { get; set; }

        public bool IsClosed { get; set; }

        public DateTime Created { get; set; }
    }
}
