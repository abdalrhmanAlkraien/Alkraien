using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminSection.Models
{
    public partial class The_Supports
    {
        [Key]
        [ForeignKey("visitor")]
        public int Supports_Id { get; set; }

        [Required(ErrorMessage = "You must provide full name")]
        [Display(Name = "Full Name")]

        public string Supports_FullName { get; set; }
        [Required(ErrorMessage = "You Must provide Email Address")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Supports_Email { get; set; }

        [Required(ErrorMessage = "you must provide Phone number ")]
        [Display(Name = "Phone number")]
        [DataType(DataType.PhoneNumber)]
        public int Supports_Phone_Number { get; set; }
        [Required(ErrorMessage = "You must provide password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Supports_Password { get; set; }
        [Required(ErrorMessage = "You must provide location")]
        [Display(Name = "Location")]
        public string Sponsor_Location { get; set; }
        public string User_Type { get; set; }

        [Display(Name = "Upload image")]
        public string Supports_Image_Path { get; set; }

        public virtual Visitor visitor { get;set;}

        //public virtual Minsitry min { get; set; }
        //public int Min_id { get; set; }

        public virtual ICollection<volunteer> vol { get; set; }
        public virtual Min_and_The_Supports min_support { get; set; }

        public virtual ICollection<Child> child { get; set; }
        public virtual ICollection<Note> note { get; set; }



    }
}