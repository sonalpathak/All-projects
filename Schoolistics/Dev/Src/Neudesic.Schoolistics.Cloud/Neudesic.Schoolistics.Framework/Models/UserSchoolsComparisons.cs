using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Framework.Models
{
    public class UserSchoolsComparisons : TableEntity
    {
        public string ComparisonId
        {
            get { return this.RowKey; }
            set { this.RowKey = value; }
        }

        public string Username 
        {
            get { return this.PartitionKey; }
            set { this.PartitionKey = value; }
        }

        public DateTime Created { get; set; }
    }
}
