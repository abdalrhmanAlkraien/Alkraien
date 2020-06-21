using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace adminSection.Models
{
    public class Min_and_The_Supports
    {
        Min_and_The_Supports()
        {
            this.ministry = new List<Minsitry>();
            this.supports = new List<The_Supports>();
        }
        [Key]
        public int? Ministry_Id { get; set; }
        public ICollection<Minsitry> ministry { get; set; }
        public int? Sponsor_ID { get; set; }
        public ICollection<The_Supports> supports { get; set; }
    }
}