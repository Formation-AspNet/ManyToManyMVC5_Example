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
    
    public partial class JobTag
    {
        public JobTag()
        {
            this.JobPosts = new HashSet<JobPost>();
        }
    
        public int Id { get; set; }
        public string Tag { get; set; }
    
        public virtual ICollection<JobPost> JobPosts { get; set; }
    }
}
