//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bookworm_API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblGeneroProduto
    {
        public int IDGeneroProduto { get; set; }
        public Nullable<int> IDGenero { get; set; }
        public Nullable<int> IDProduto { get; set; }
    
        public virtual tblGenero tblGenero { get; set; }
        public virtual tblProduto tblProduto { get; set; }
    }
}
