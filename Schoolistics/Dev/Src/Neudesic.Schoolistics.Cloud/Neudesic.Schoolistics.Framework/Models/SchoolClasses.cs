using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Framework.Models
{
    public class SchoolClasses : TableEntity
    {
        public SchoolClasses()
        { }

        public string SchoolId
        {
            get { return this.RowKey; }
            set { this.RowKey = value; }
        }

        public int MinimumBudget { get; set; }

        public int MaximumBudget { get; set; }

        public string Class
        {
            get { return this.PartitionKey; }
            set { this.PartitionKey = value; }
        }

    }
}
