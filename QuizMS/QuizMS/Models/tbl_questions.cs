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
    
    public partial class tbl_questions
    {
        public int question_id { get; set; }
        public string q_txt { get; set; }
        public string opa { get; set; }
        public string opb { get; set; }
        public string opc { get; set; }
        public string opd { get; set; }
        public string cop { get; set; }
        public Nullable<int> q_fk_ct_id { get; set; }
        public Nullable<int> ch_id { get; set; }
    
        public virtual tbl_category tbl_category { get; set; }
        public virtual chapter chapter { get; set; }
    }
}
