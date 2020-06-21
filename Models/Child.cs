using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminSection.Models
{
    [Table("tbl_child")]
    public partial class Child
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Chi_Id { get; set; }
        /// <summary>
        /// child Id att
        /// </summary>
        [Required(ErrorMessage ="you must provied child first name")]
        [Display(Name ="Child First Name")]
        public string  Chi_First_Name { get; set; }
        /// <summary>
        /// First name att
        /// </summary>
        [Required(ErrorMessage ="you must provied child last Name")]
        [Display(Name ="Child Last Name")]
        public string Chi_Last_Name { get; set; }
        /// <summary>
        /// Last Name Att
        /// </summary>
        [Required(ErrorMessage ="you must provied child age")]
        [Display(Name ="Child Age")]
        [Range(0,18, ErrorMessage = "Correct Input")]
        public int Age { get; set; }
        /// <summary>
        /// Child Age Att
        /// </summary>
        [Required(ErrorMessage ="you must provied child gender")]
        [Display(Name ="Child Gender")]
        public string Gender { get; set; }
        /// <summary>
        /// Child Gender Att
        /// </summary>
        [Display(Name ="Child School")]
        public string Chi_School { get; set; }
        /// <summary>
        /// Child School
        /// </summary>

        [Required(ErrorMessage ="you must provied parent status")]
        [Display(Name ="Child Parent Status")]
        public string Chi_parent_status { get; set; }
        /// <summary>
        /// Child Parent Status
        /// </summary>
        /// 
        [Display(Name ="Child Image Path")]
        public string Chi_Image_Path { get; set; }
        /// <summary>
        /// Child Image Status
        /// </summary>
        ///



        public int? Adm_ID { get; set; }
        public Admin admin { get; set; }

        /// <summary>
        /// admin and child has relationship  one to many
        /// </summary>

        public virtual Parent parent { get; set; }
        public int Par_ID { get; set; }
        /// <summary>
        /// parent and child has relationship  one to many
        /// </summary>


        public ICollection<Note> note { get; set; }
        /// <summary>
        /// note and child has relationship  one to many
        /// </summary>

        public int? Home_ID { get; set; }
        public virtual Home_Child_Care Home { get; set; }
        /// <summary>
        /// child and home has relationship  one to many 
        /// </summary>

        public int? Min_id { get; set; }
        public virtual Minsitry min { get; set; }
        /// <summary>
        /// child and Minsitry has relationship  one to many
        /// </summary>
        /// 

        public int? The_SupportsID { get; set; }
        public The_Supports supports { get; set; }
        /// <summary>
        /// child and The supports has relationship  one to many
        /// </summary>
        ///

    }
}