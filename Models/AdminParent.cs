using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace adminSection.Models
{
    public class AdminParent
    {
        [Key]
        public int? Adm_id { get; set; }
        public ICollection<Admin> admin { get; set; }
        /// <summary>
        /// admin and parent has relationship  many to many
        /// </summary>
        public int? Par_Id { get; set; }
        public ICollection<Parent> parent { get; set; }
        /// <summary>
        /// admin and parent has relationship  many to many
        /// </summary>
    }
}