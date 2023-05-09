using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DataModels;

namespace BLL
{
    public class AuthorVM
    {
        public int AuthorID { get; set; }
        public string AuthorName { get; set; }
        public string Description { get; set; }
    }
}
