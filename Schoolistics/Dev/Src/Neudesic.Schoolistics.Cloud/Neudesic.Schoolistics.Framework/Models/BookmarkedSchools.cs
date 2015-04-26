using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Framework.Models
{
    public class BookmarkedSchools : TableEntity
    {
        public string Username
        {
            get { return this.PartitionKey; }
            set { this.PartitionKey = value; }
        }

        public string SchoolId
        {
            get { return this.RowKey; }
            set {this.RowKey = value; }
        }

        public string SchoolName { get; set; }

        public string City { get; set; }

        public string SchoolLogo { get; set; }

        public DateTime Created { get; set; }
    }
}
