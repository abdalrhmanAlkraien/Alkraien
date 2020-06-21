using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminSection.Models
{
    public class test
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "you must provide your full name")]
        [Display(Name = "Full Name")]
        public string name { get; set; }
        [Required(ErrorMessage = "you must provide password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Visitor_Password { get; set; }
        [Required(ErrorMessage = "you must upload your Image")]
        [Display(Name = "upload Image")]
        public string Visitor_Image_Path { get; set; }

        [Required(ErrorMessage = "you must provide phone number")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public int Visitor_Phone { get; set; }
        public DateTime date { get; set; }
    }
}