using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminSection.Models
{
    [Table("tbl_Home")]
    public partial class Home_Child_Care
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [ScaffoldColumn(false)]
        public int Home_ID { get; set; }
        /// <summary>
        /// Home Care ID Att Here
        /// </summary>
        /// 

        [Required(ErrorMessage ="You must Provide Home Name")]
        [Display(Name ="Home Name")]
        public string  Home_Name { get; set; }
        /// <summary>
        /// Home Care Name Here
        /// </summary>
        /// 

        [Required(ErrorMessage ="You must provide Home Location")]
        [Display(Name ="Home Location")]
        public string Home_Location { get; set; }
        /// <summary>
        /// Home Care Location Here
        /// </summary>
        /// 

        [Required(ErrorMessage ="You must provide age group for child")]
        [Display(Name ="Age Group")]
        //[Range(1,18,ErrorMessage ="the age out of range")] Droop Box between 1-5 6-9 9-12 12-18
        public int  Home_Age_Group { get; set; }
        /// <summary>
        /// Home Care Age Group Att Here
        /// </summary>
        /// 

        [Required(ErrorMessage ="you must provide Gender group for child")]
        [Display(Name ="Gender Group")]
        public string Home_Child_Gender { get; set; }// drop box in view for male or female group
        /// <summary>
        /// Home Care Gender Type Here
        /// </summary>
        /// 

        [Required(ErrorMessage ="You must provide Email address for home care")]
        [Display(Name ="Email Address")]
        [DataType(DataType.EmailAddress,ErrorMessage ="you must provide only E-mail Address in this feild")]
        public string Home_Email { get; set; }
        /// <summary>
        /// Home Care Email Att Here
        /// </summary>

        [Required(ErrorMessage ="You must provide Phone Number for Home Care")]
        [Display(Name ="Phone Number")]
        [DataType(DataType.PhoneNumber,ErrorMessage ="You must provide only Phone Number in this feild")]
        public string Home_Phone { get; set; }
        /// <summary>
        /// Home Care Phone Number Att Here
        /// </summary>

        [Display(Name ="Website For Home",Description ="Can you Adding website if the home have website")]
        public string Home_Website { get; set; }

        /// <summary>
        /// Home Care Wibsite Here If they have
        /// </summary>
        

        public int? Min_Id { get; set; }
        public virtual Minsitry ministry { get; set; }

        /// <summary>
        /// ministry and HomeCare has relationship  one to many
        /// </summary>
        /// 

        public int? Adm_Id { get; set; }
        public virtual ICollection<Admin> admin { get; set; }
        // <summary>
        // admin and home has relationship one to many 
        // </summary>
        public int? Chi_Id { get; set; }
        public virtual ICollection<Child> child { get; set; }
        /// <summary>
        /// child and home has relationship  one to many 
        /// </summary>
        /// 
        public int? Note_ID { get; set; }
        public virtual ICollection<Note> note { get; set; }
        /// <summary>
        /// Note and Home has relationship  one to many
        /// </summary>
        /// 

        //public ICollection<Home_and_parent> home_and_parent { get; set; }
        ///// <summary>
        ///// home and parent has relationship  many to many
        ///// </summary>
        //public ICollection<Home_Parent> home_parent { get; set; }
    }
}