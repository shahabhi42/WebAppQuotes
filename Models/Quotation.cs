using GridMvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QuotationsApp.Models
{
    public class Quotation
    {
        [GridHiddenColumn]
        public int QuotationID { get; set; }
        [Required(ErrorMessage = "Quote is required")]
        public string Quote { get; set; }
        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [GridColumn(Title = "Date Added")]
        [DisplayName("Date Added")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public int CategoryID { get; set; }
        [NotMappedColumnAttribute]
        public virtual Category Category { get; set; }
    }
    public class QuotationsAppCSISDBContext : DbContext
    {
        public DbSet<Quotation> Quotations { get; set; }

        public System.Data.Entity.DbSet<QuotationsApp.Models.Category> Categories { get; set; }
    }
}