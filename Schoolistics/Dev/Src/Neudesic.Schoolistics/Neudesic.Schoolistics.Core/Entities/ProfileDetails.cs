using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Entities
{
   public  class ProfileDetails
    {
       [Required]
       public string Id { get; set; }
       [Required]
        public string Name { get; set; }
       [Required]
        public string Gender { get; set; }
       [Required]
        public string Education { get; set; }
       [Required]
        public string PhoneNo{ get; set; }
       [Required]
       public string MailId { get; set; }
           [Required]
        public string Occupation { get; set; }
       [Required]
        public string DOB { get; set; }
       [Required]
        public string Relation{get;set;}
       [Required]
        public string Password { get; set; }
       [Required]
        public string ReEnterPassword { get; set; }
        
       
    }
}
