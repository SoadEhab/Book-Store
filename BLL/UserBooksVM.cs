using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DataModels;

namespace BLL
{
    public class UserBooksVM
    {
        public int User_BookID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> BookID { get; set; }
        public bool IsBought { get; set; }
        public Nullable<System.DateTime> Bought_Date { get; set; }

    }
}
