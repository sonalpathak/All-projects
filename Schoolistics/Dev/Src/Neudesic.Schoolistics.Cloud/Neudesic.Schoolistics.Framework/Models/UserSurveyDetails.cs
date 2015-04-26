using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Framework.Models
{
    public class UserSurveyDetails:TableEntity
    {
        public string SurveyId
        {
            get { return this.PartitionKey; }
            set { this.PartitionKey = value; }
        }

        public string UserName
        {
            get { return this.RowKey; }
            set { this.RowKey = value; }
        }
        public DateTime Created { get; set; }
    }
}
