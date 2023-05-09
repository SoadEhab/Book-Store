using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DataModels;

namespace BLL
{
    public class UserVM
    {
        public int UserID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string E_mail { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
    }
}
