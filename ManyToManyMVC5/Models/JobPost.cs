//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class JobPost
    {
        public JobPost()
        {
            this.JobTags = new HashSet<JobTag>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public int EmployerID { get; set; }
    
        public virtual Employer Employer { get; set; }
        public virtual ICollection<JobTag> JobTags { get; set; }
    }
}