using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminSection.Models
{
    [MetadataType(typeof(VisitorMetaData))]
    public partial class Visitor
    {
        [NotMapped]
        public string  Visitor_Confirm_Password { get; set; }
        /// <summary>
        /// Visitor confirm password att  section
        /// section success
        /// </summary>
        

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
        /// <summary>
        /// Visitor Image File section
        /// section success
        /// </summary>
        

        [NotMapped]
        public string loginErrorMessage { get; set; }
        /// <summary>
        /// login error message att section
        /// section success
        /// </summary>
    }
    public class VisitorMetaData
    {
        [Display(Name ="Confirm Password")]
        [DataType(DataType.Password)]
        public string Visitor_Confirm_Password { get; set; }
        /// <summary>
        /// Visitor confirm password att  section
        /// section success
        /// </summary>
        
        public HttpPostedFileBase ImageFile { get; set; }
        /// <summary>
        /// Visitor Image File section
        /// section success
        /// </summary>
        

        public string loginErrorMessage { get; set; }
        /// <summary>
        /// login error message att section
        /// section success
        /// </summary>
    }
}