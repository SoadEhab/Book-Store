using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DataModels;

namespace BLL
{
    public class AdminVM
    {
        
        public int AdminID { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string E_mail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
