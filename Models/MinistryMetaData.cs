using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminSection.Models
{
    [MetadataType(typeof(MinistryMetaData))]
    public partial class Minsitry
    {
        [NotMapped]
        public string Min_Confirm_Password { get; set; }
        /// <summary>
        /// ministry confirm password att  here
        /// </summary>
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
        /// <summary>
        /// ministry Image File here
        /// </summary>
        [NotMapped]
        public string loginErrorMessage { get; set; }
        /// <summary>
        /// Login error message here
        /// </summary>

    }
    public class MinistryMetaData
    {
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string Min_Confirm_Password { get; set; }
        /// <summary>
        /// ministry confirm password  here
        /// </summary>
        public HttpPostedFileBase ImageFile { get; set; }
        /// <summary>
        /// image file att here
        /// </summary>

        public string loginErrorMessage { get; set; }
        /// <summary>
        /// login error message att here
        /// </summary>

    }
}