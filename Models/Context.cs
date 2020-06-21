using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Web;

namespace adminSection.Models
{
    public  class Context:DbContext
    {
       // public  DbSet<Authorize> auth { get; set; }
        public  DbSet<Admin> admin { get; set; }
        /// <summary>
        /// Mapping with Database
        /// </summary>
        public DbSet<Child> child { get; set; }
        /// <summary>
        /// Mapping with Database
        /// </summary>
        public DbSet<Parent> parent { get; set; }
        /// <summary>
        /// Mapping with Database
        /// </summary>
        public DbSet<AdminParent> adm_par { get; set; }
        /// <summary>
        /// Mapping with Database
        /// </summary>
        public DbSet<Note> note { get; set; }
        /// <summary>
        /// Mapping with Database
        /// </summary>
        public DbSet<Minsitry> ministry { get; set; }
        /// <summary>
        /// Mapping with Database
        /// </summary>
        public DbSet<Home_Child_Care> home { get; set; }
        /// <summary>
        /// Mapping with Database
        /// </summary>
        /// 
         

        public DbSet<volunteer> voulunter { get; set; }
        /// <summary>
        /// Mapping with Database
        /// </summary>
        /// 

       // public DbSet<The_Sponsor> spo { get; set; }

        public DbSet<Visitor> visitor { get; set; }

        public DbSet<Ministry_and_Sponsor> min_Spo { get; set; }
        
        public DbSet<The_Supports> sup { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Visitor>()
        //.HasRequired(c => c.sponsor)
        //.WithRequiredPrincipal(a => a.visitor)
        //.Map(m => m.MapKey("Visitro_ID"));
        //}





    }

}