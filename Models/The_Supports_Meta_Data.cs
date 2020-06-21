using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminSection.Models
{
    [MetadataType(typeof(The_Supports_Meta_Data))]
    public partial class The_Supports
    {
        [NotMapped]
        public string The_Supports_Confirm_Password { get; set; }
        [NotMapped]
        public HttpPostedFileBase The_Supports_ImageFile { get; set; }
        [NotMapped]
        public string The_Supports_loginErrorMessage { get; set; }

        public int Accept_Voulnteer { get; set; }
        public int Num_Of_Child { get; set; }
    }
    public class The_Supports_Meta_Data
    {
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string The_Supports_Confirm_Password { get; set; }

        public HttpPostedFileBase The_Supports_ImageFile { get; set; }
        public string The_Supports_loginErrorMessage { get; set; }

        public int Accept_Voulnteer { get; set; }
        public int Num_Of_Child { get; set; }
    }
}