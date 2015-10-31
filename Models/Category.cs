using GridMvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace QuotationsApp.Models
{
    public class Category
    {
        [NotMappedColumnAttribute]
        public int CategoryID { get; set; }
        [DisplayName("Type of Quote")]
        [GridColumn(Title = "Quote Type")]
        public string Name { get; set; }
        public virtual ICollection<Quotation> Categories { get; set; }

        public static explicit operator string (Category v)
        {
            throw new NotImplementedException();
        }
    }
}