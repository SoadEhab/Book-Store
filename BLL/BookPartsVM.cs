using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DataModels;

namespace BLL
{
    public class BookPartsVM
    {
        public int Book_PartID { get; set; }
        public string Title { get; set; }
        public Nullable<int> BookID { get; set; }
        public string Image { get; set; }
        public Nullable<int> BookNumber { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> Published_Date { get; set; }
        public Nullable<int> Author_ID { get; set; }
    }
}
