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
    
    public partial class tblReserva
    {
        public int IDReserva { get; set; }
        public Nullable<int> IDProduto { get; set; }
        public Nullable<int> IDLeitor { get; set; }
        public System.DateTime DataReserva { get; set; }
    
        public virtual tblLeitor tblLeitor { get; set; }
        public virtual tblProduto tblProduto { get; set; }
    }
}
