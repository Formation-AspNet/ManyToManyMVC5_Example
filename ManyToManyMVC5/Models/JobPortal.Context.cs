﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ManyToManyMVC5.Models
{
    using System;
	using System.Data.Entity;
	using System.Data.Entity.Infrastructure;
   
    
    public partial class JobPortalEntities : DbContext
    {
        public JobPortalEntities()
            : base("name=JobPortalEntities")
        {
        }
    
       
    
        public DbSet<Employer> Employers { get; set; }
        public DbSet<JobPost> JobPosts { get; set; }
        public DbSet<JobTag> JobTags { get; set; }
    }
}
