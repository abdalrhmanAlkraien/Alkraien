using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminSection.Models
{
    [MetadataType(typeof(Child_Meta_Data))]
    public partial class Child
    {
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
        /// <summary>
        /// image File att here
        /// </summary>
    }
    public class Child_Meta_Data
    {
     //   [Required(ErrorMessage ="You must provied child image")]
        [Display(Name ="Upload Child Image")]
        public HttpPostedFileBase ImageFile { get; set; }
        /// <summary>
        /// image File att here
        /// </summary>
    }
}