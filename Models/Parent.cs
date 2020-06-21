using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace adminSection.Models
{
    [Table("tbl_Parent")]
    public partial class Parent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       
        [Key]
        
        [ScaffoldColumn(false)]
        public int Parent_Id { get; set; }
        /// <summary>
        /// End Id Att
        /// </summary>
        /// 
        [Required(ErrorMessage = "you must provide parent First Name")]
        [Display(Name = "Full Name")]
        public string Parent_Full_Name{ get; set; }

        /// <summary>
        /// End Full Name Att
        /// </summary>
        /// 
       
        [Required(ErrorMessage = "you must provide parent Email Address")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Correct Input")]
        [StringLength(100, ErrorMessage = "at least 10 Char", MinimumLength = 10)]
        public string Parent_Email { get; set; }
        /// <summary>
        /// End Email Att
        /// </summary>
        /// 
        [Required(ErrorMessage = "you must provide parent phone number")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public int Parent_Phone { get; set; }
        /// <summary>
        /// End Phone Number Att
        /// </summary>
        
        [Required(ErrorMessage = "you must provide parent password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Parent_Password { get; set; }
        /// <summary>
        /// End Password Att
        /// </summary>
        /// 
        



        public string Parent_Location { get; set; }
        /// <summary>
        /// End Location Att
        /// </summary>
        /// 
        [Required(ErrorMessage = "you must provide parent status")]
        [Display(Name = "Parent status")]

        public string Parent_status { get; set; }
        /// <summary>
        /// End parent status Att
        /// </summary>
        /// 

        public string Par_User_Type { get; set; }
        /// <summary>
        /// Parent User Type Att Here
        /// </summary>

        public virtual ICollection<Child> child { get; set; }
        /// <summary>
        /// Child and parent has relationship  one to many
        /// </summary>
        public AdminParent adm_par { get; set; }
        /// <summary>
        /// admin and parent has relationship  many to many
        /// </summary>

        public ICollection<Note> note { get; set; }
        /// <summary>
        /// parent and note has relationship  one to many
        /// </summary>
        //public ICollection<Home_and_parent> home_and_parent { get; set; }
        ///// <summary>
        ///// home and parent has relationship  many to many
        ///// </summary>

        //public ICollection<Home_Parent> home_parent { get; set; }

    }
}