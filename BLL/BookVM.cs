using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DataModels;

namespace BLL
{
    public class BookVM
    {
        public int BookID { get; set; }
        public string BookName { get; set; }
        public Nullable<System.DateTime> Published_Date { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<int> AuthorID { get; set; }
    }
}
