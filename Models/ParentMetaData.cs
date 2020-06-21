using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminSection.Models
{
    [MetadataType(typeof(ParentMetaData))]
    public partial class Parent
    {
        [NotMapped]
        public string Parent_Confirm_Password { get; set; }
        /// <summary>
        /// Confitm password Att here
        /// </summary>
        [NotMapped]
        public string loginErrorMessage { get; set; }
        /// <summary>
        /// Login Error Message Att here
        /// </summary>

        public string Par_Image_Path { get; set; }
        /// <summary>
        /// Image Path Att Here
        /// </summary>

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
        /// <summary>
        /// Image File Att Here
        /// </summary>
    }
    public class ParentMetaData
    {
        //[Required(ErrorMessage = "you must provide parent confirm Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        //[Compare("Parent_Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string Parent_Confirm_Password { get; set; }



        /// <summary>
        /// End Confirm Password Att
        /// </summary>
        /// 
        public string loginErrorMessage { get; set; }
        /// <summary>
        /// End Login Error Message
        /// </summary>
        ///

        
        [Display(Name ="Upload Ypur Image")]
        public string Par_Image_Path { get; set; }
        /// <summary>
        /// End Image Path
        /// </summary>
        ///
        public HttpPostedFileBase ImageFile { get; set; }
        /// <summary>
        /// End Image File
        /// </summary>
        ///

    }
}