using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminSection.Models
{
    [Table("tbl_Note")]
    public class Note
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [ScaffoldColumn(false)]
        public int Note_Id { get; set; }
        /// <summary>
        /// note id end
        /// </summary>
        [Required(ErrorMessage ="You must provide this note title")]
        [Display(Name ="Header")]
        public string Note_title { get; set; }
        /// <summary>
        /// note of title end
        /// </summary>
        
        [Required(ErrorMessage ="You must provide note content")]
        [Display(Name = "Content")]
        public string note_content { get; set; }
        /// <summary>
        /// 
        ///Note Content end
        /// </summary>
        public DateTime Note_Date { get; set; }

        /// <summary>
        /// Date Of note here
        /// </summary>
        /// 
        public string Posted { get; set; }
        /// <summary>
        /// who posted of note
        /// posted Of note here
        /// </summary>
        ///
        public virtual Child child { get; set; }
        public int Child_id { get; set; }
        /// <summary>
        /// Child and note has relationship  one to many
        /// </summary>

        public virtual Admin admin { get; set; }
        public int Admin_id { get; set; }
        /// <summary>
        /// admin and note has relationship  one to many
        /// </summary>




        public int ?Parent_Id { get; set; }
        
        
        public  Parent parent { get; set; }
        /// <summary>
        /// Parent and note has relationship  one to many
        /// </summary>
        /// 

        public int? Min_ID { get; set; }


        public Minsitry min { get; set; }
        /// <summary>
        /// Ministry and note has relationship  one to many
        /// </summary>
        ///

        public int? Home_ID { get; set; }


        public Home_Child_Care home { get; set; }
        /// <summary>
        /// Home and note has relationship  one to many
        /// </summary>
        ///  

        public int Support_ID { get; set; }
        public The_Supports supports { get; set; }
    }
}