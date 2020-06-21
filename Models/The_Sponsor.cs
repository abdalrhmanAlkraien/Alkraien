using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminSection.Models
{
    [Table("The_Sponsor")]
    public partial class The_Sponsor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[ForeignKey("Visitor")]
        [Key]
        [ForeignKey("visitor")]
        public int Sponsor_Id { get; set; }
        [Required(ErrorMessage = "you ust provide full name")]
        [Display(Name = "Full Name")]
        public string Sponsor_FullName { get; set; }

        [Required(ErrorMessage = "you must provied phone number")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string Sponsor_Phone { get; set; }

        [Required(ErrorMessage = "You must provide Email Address")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Sponsor_Email { get; set; }

        [Required(ErrorMessage = "You must provide Password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Sponsor_Password { get; set; }
        [Required(ErrorMessage = "you must provide Location")]
        [Display(Name = "You must provide Location")]
        public string Sponsor_Location { get; set; }

        [Required(ErrorMessage = "You provide image path")]
        [Display(Name = "image path")]
        public string Sponsor_Image_Path { get; set; }

        public string User_Type{ get; set; }

        
        public int? V_ID { get; set; }
        public virtual Visitor visitor { get; set; }

        public virtual Minsitry min { get; set; }
        public int Min_id { get; set; }

        

        

    }
}