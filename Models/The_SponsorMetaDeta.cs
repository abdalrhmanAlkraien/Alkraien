using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminSection.Models
{
    [MetadataType(typeof(The_SponsorMetaDeta))]
    public partial class The_Sponsor
    {
        [NotMapped]
        public string Sponsor_Confirm_Password { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
        [NotMapped]
        public string loginErrorMessage { get; set; }
    }
    public class The_SponsorMetaDeta
    {
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string Sponsor_Confirm_Password { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
        public string loginErrorMessage { get; set; }
    }
}