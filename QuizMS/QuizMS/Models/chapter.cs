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
    
    public partial class chapter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public chapter()
        {
            this.tbl_questions = new HashSet<tbl_questions>();
        }
    
        public int ch_id { get; set; }
        public string ch_name { get; set; }
        public Nullable<int> ad_id { get; set; }
        public Nullable<int> cat_id { get; set; }
    
        public virtual tbl_admin tbl_admin { get; set; }
        public virtual tbl_category tbl_category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_questions> tbl_questions { get; set; }
    }
}
