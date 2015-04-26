using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Framework.Models
{
    public class UserSchoolComment : TableEntity
    {
        public UserSchoolComment()
        { }
        
        public string SchoolId
        {
            get { return this.PartitionKey; }
            set { this.PartitionKey = value; }
        }

        public string CommentId
        {
            get { return this.RowKey; }
            set { this.RowKey = value; }
        }

        public string Username { get; set; }

        public string Comment { get; set; }

        public DateTime Created { get; set; }
    }
}
