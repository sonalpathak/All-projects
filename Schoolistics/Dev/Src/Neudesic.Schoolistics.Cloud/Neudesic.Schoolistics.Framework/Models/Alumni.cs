using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Framework.Models
{
    public class Alumni:TableEntity
    {
        public string SchoolId
        {
            get { return this.PartitionKey; }
            set { this.PartitionKey = value; }
        }

        public string AlumniId
        {
            get { return this.RowKey; }
            set { this.RowKey = value; }
        }

        public string AlumniName { get; set; }

        public string Position { get; set; }

        public string Image { get; set; }
    }
}
