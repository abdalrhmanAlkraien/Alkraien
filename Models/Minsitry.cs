using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminSection.Models
{
    [Table("tbl_ministry")]
    public partial class Minsitry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Min_Id { get; set; }
        /// <summary>
        /// Min Id Att Here
        /// </summary>
        [Required(ErrorMessage ="You must Provide Name")]
        [Display(Name ="Full Name")]
        public string  Min_Full_Name { get; set; }
        /// <summary>
        /// Min Full Name Here
        /// </summary>

        [Required(ErrorMessage ="You must provide Email Address.")]
        [Display(Name ="Email Address")]
        [DataType(DataType.EmailAddress,ErrorMessage ="You must provide only Email Address in this feild")]
        public string  Min_Email { get; set; }
        /// <summary>
        /// Min Email Att Here
        /// </summary>

        [Required(ErrorMessage ="You must provide Password.")]
        [Display(Name ="Password")]
        [DataType(DataType.Password)]
        public string  Password { get; set; }
        /// <summary>
        /// Min Password Att Here
        /// </summary>

       



        [Required(ErrorMessage ="You must provide Phone Number")]
        [Display(Name ="Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string Min_Phone { get; set; }
        /// <summary>
        /// Min Phone Number Att Here
        /// </summary>
        [Display(Name = "Upload your image")]
        public string Min_Image_Path { get; set; }
        /// <summary>
        /// Min Image Path Att Here
        /// </summary>

        public string User_Type { get; set; }
        /// <summary>
        /// Min User Type Att Here
        /// </summary>
        /// 
        
        public virtual ICollection<Home_Child_Care> Home { get; set; }
        /// <summary>
        /// ministry and HomeCare  has relationship  one to many
        /// </summary>

        public int? Adm_Id { get; set; }
        public virtual ICollection<Admin> admin { get; set; }
        // <summary>
        // admin and Minsitry has relationship one to many
        // </summary>

        public int? Chi_Id { get; set; }
        public virtual ICollection<Child> child { get; set; }
        /// <summary>
        /// child and Minsitry has relationship  one to many
        /// </summary>
        /// 
        public int? Note_ID { get; set; }
        public virtual ICollection<Note> note { get; set; }
        /// <summary>
        /// Note and Minsitry has relationship  one to many
        /// </summary>
        /// 
        //public int? Support_ID { get; set; }
        //public virtual ICollection<Support> sponser { get; set; }

        public virtual ICollection<The_Sponsor> sponsor { get; set; }

        // the Suppoort realtion
        //public virtual ICollection<The_Supports> support { get; set; }

        public virtual Min_and_The_Supports min_support { get; set; }

    }
}