using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace adminSection.Models
{
    [Table("tbl_volunteer")]
    public class volunteer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Volunteer_Id { get; set; }
        /// <summary>
        /// volunteer_Id Att Section
        /// Section success
        /// 
        /// </summary>
        [Required(ErrorMessage = "you must provide title of volunteer")]
        [Display(Name = "volunteer Title")]
        public string Volunteer_Title{ get; set; }
        /// <summary>
        ///  volunteer_Name Att Secction
        ///  Section success
        /// </summary>
        /// 

        [Required(ErrorMessage = "you must provide Decription of volunteer")]
        [Display(Name = "volunteer Decription")]
        public string Volunteer_Decription { get; set; }
        /// <summary>
        /// volunteer_Decription Section
        /// Section success
        /// </summary>

        [Required(ErrorMessage = "you must provide type of volunteer")]
        [Display(Name = "Volunteer Type")]
        public string Volunteer_Type { get; set; }
        /// <summary>
        /// volunteer_Type Att Section
        /// sectioin success
        /// </summary>
        /// 

        [Display(Name = "Volunteer Status")]
        public string Volunteer_status { get; set; }
        /// <summary>
        /// volunteer_status Section
        /// Section Success
        /// </summary>
        /// 
        [Display(Name = "the reason of refuse")]
        
        public string the_reason_of_refuse { get; set; }
        /// <summary>
        /// the_reason_of_refuse Section
        /// Section Success 
        /// </summary>

        public virtual Visitor vistor { get; set; }

        public int Visitro_ID { get; set; }
        /// <summary>
        /// Visior with volunteer has relation one to many
        /// section success
        /// sectioin success
        /// </summary>

        public virtual The_Supports support { get; set; }








    }
}