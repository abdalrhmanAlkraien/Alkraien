using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminSection.Models
{
    [MetadataType(typeof(AdminMetaData))]
    public partial class Admin
    {
        [NotMapped]
        public string Adm_Confirm_Password { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
        [NotMapped]
        public string loginErrorMessage { get; set; }
    }
    public class AdminMetaData
    {
       // [Required(ErrorMessage ="you must provied confirm password")]
        [Display(Name ="Confirm Password")]
        [DataType(DataType.Password)]
        public string Adm_Confirm_Password { get; set; }
       // [Required(ErrorMessage = "You must provied your image")]

        public HttpPostedFileBase ImageFile { get; set; }
        public string loginErrorMessage { get; set; }

    }
}