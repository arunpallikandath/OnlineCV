//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineCV
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    
    public partial class CvMaster
    {
        public CvMaster()
        {
            this.Expertises = new HashSet<Expertise>();
            this.Experiences = new HashSet<Experience>();
            this.Skills = new HashSet<Skill>();
        }
    
        public int Id { get; set; }
         [Required(ErrorMessage = "Title is required.")]
        public string title { get; set; }
        [MaxLength(10,ErrorMessage="Mobile number should not be more that 10 numbers.")]
        public string mobile { get; set; }
        public string home_phone { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string email { get; set; }
        public string summary { get; set; }
         [Required(ErrorMessage = "CV name is required.")]
        public string cv_name { get; set; }
        [Required(ErrorMessage = "Full name is required.")]
        public string fullname { get; set; }
        public string photo_path { get; set; }
    
        public virtual ICollection<Expertise> Expertises { get; set; }
        public virtual ICollection<Experience> Experiences { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
    }
}