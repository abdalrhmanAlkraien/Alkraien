using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace adminSection.Models
{
    public class Ministry_and_Sponsor
    {
        [Key]
        public int? Ministry_Id { get; set; }
        public ICollection<Minsitry> ministry { get; set; }
        public int? Sponsor_ID { get; set; }
        public ICollection <The_Sponsor> Sponsor { get; set; }

        
    }
}