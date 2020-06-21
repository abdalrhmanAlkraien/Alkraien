using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminSection.Models
{
    [Table("tbl_Admin")]
    public partial class Admin
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        
        [ScaffoldColumn(false)]
        public int Adm_ID { get; set; }
        /// <summary>
        /// admin Id att here
        /// </summary>
        /// 

        [Required(ErrorMessage ="You must provied full name")]
        [Display(Name ="Full Name")]
        public string Adm_Name { get; set; }
        /// <summary>
        /// admin full name att here
        /// </summary>
        /// 

        [Required(ErrorMessage ="You must provied Email Address")]
        [Display(Name ="Email Address")]
        [DataType(DataType.EmailAddress)]
        public string  Adm_Email { get; set; }
        /// <summary>
        /// admin Email adrress att here
        /// </summary>

        [Required(ErrorMessage ="You must provied phone number")]
        [Display(Name ="Phone number")]
        [DataType(DataType.PhoneNumber)]
        public string Adm_Phone { get; set; }
        /// <summary>
        /// admin phone number att here
        /// </summary>
        /// 
        [Required(ErrorMessage = "you must provide Gender.")]
        [Display(Name = "Gender")]
        public string Adm_Gender { get; set; }
        /// <summary>
        /// Admin Gender ATT Here
        /// </summary>
        /// 

        [Display(Name ="Upload your image")]
        public string Adm_Image_Path { get; set; }
        /// <summary>
        /// admin image path att here
        /// </summary>
        /// 

        [Required(ErrorMessage ="you must provied your password")]
        [Display(Name ="Password")]
        [DataType(DataType.Password)]
        public string Adm_Password { get; set; }
        /// <summary>
        /// admin password att here
        /// </summary>
        /// 

        public string User_Type { get; set; }
        /// <summary>
        /// admin user type att here
        /// </summary>


       // public int? Chi_Id { get; set; }
        public virtual ICollection<Child> chi { get; set; }
        /// <summary>
        /// admin and child has relationship  one to many
        /// </summary>

        public AdminParent adm_par { get; set; }
        /// <summary>
        /// admin and parent has relationship  many to many
        /// </summary>

        public ICollection<Note> note { get; set; }
        /// <summary>
        /// admin and note has relationship  one to many
        /// </summary>
        /// 

        public int? Home_ID { get; set; }
        public Home_Child_Care home { get; set; }
        /// <summary>
        /// admin and home has relationship  one to many 
        /// </summary>

        public int? Min_id { get; set; }
        public virtual Minsitry min { get; set; }
        /// <summary>
        /// admin and Minsitry has relationship  one to many
        /// </summary>



    }
}