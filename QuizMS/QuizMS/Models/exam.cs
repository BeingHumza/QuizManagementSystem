//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuizMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class exam
    {
        public int e_id { get; set; }
        public string e_name { get; set; }
        public Nullable<int> e_score { get; set; }
        public Nullable<int> std_id { get; set; }
        public Nullable<int> cat_id { get; set; }
    
        public virtual tbl_students tbl_students { get; set; }
        public virtual tbl_category tbl_category { get; set; }
    }
}
