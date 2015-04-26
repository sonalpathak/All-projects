using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Framework.Models
{
    public class UserInfo :TableEntity
    {

        public UserInfo()
        {
        }

        public string UserId 
        {
            get { return this.RowKey; }
            set { this.RowKey = value; }
        }

        public string Username 
        {
            get { return this.PartitionKey; }
            set { this.PartitionKey = value; }
        }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public string FullName { get; set; }

        public string DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public string Occupation { get; set; }

        public string UserType { get; set; }

        public string Gender { get; set; }

        public string Education { get; set; }

        public string Image { get; set; }

        public DateTime Created { get; set; }

        public string Address { get; set; }

        public bool IsSocialUser { get; set; }
    }
}
