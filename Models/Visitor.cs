using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminSection.Models
{
    [Table("tbl_Visitor")]
    public partial class Visitor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        
        public int Visitro_ID { get; set; }
        /// <summary>
        /// ID Att Section
        /// Section success
        /// </summary>

        [Required(ErrorMessage ="you must provide your full name")]
        [Display(Name ="Full Name")]
        public string Visitor_FullName { get; set; }
        /// <summary>
        /// Full Name Att Section
        /// Section success
        /// </summary>
        /// 

        [Required(ErrorMessage ="You must provide your Email")]
        [Display(Name ="Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Visitor_Email { get; set; }
        /// <summary>
        /// Email Address Att Section
        /// section success
        /// </summary>
        /// 
        [Required(ErrorMessage ="you must provide password")]
        [Display(Name ="Password")]
        [DataType(DataType.Password)]
        public string  Visitor_Password { get; set; }
        /// <summary>
        /// Password Att Section
        /// section success
        /// </summary>
        /// 
        [Required(ErrorMessage ="you must provide phone number")]
        [Display(Name ="Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public int Visitor_Phone { get; set; }
        /// <summary>
        /// Phone Number Att Section
        /// Section success
        /// </summary>

        [Required(ErrorMessage ="you must upload your Image")]
        [Display(Name ="upload Image")]
        public string  Visitor_Image_Path { get; set; }
        /// <summary>
        /// Image Path Att Section
        /// Section success
        /// </summary>

        public string User_Type { get; set; }
        /// <summary>
        /// User type Section
        /// Section success
        /// </summary>
        /// 

        public virtual ICollection<volunteer> volunterr { get; set; }
        /// <summary>
        /// Visior with volunteer has relation one to many
        /// section success
        /// sectioin success
        /// </summary>
        /// 

        
    
        //public int? Sponsor_ID { get; set; }

        //public virtual The_Sponsor sponsor { get; set; }

        public int? Support_Id { get; set; }
        public virtual The_Supports support { get; set; }







    }
}