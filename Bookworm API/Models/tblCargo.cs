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
    
    public partial class tblCargo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblCargo()
        {
            this.tblFuncionario = new HashSet<tblFuncionario>();
        }
    
        public int IDCargo { get; set; }
        public string NomeCargo { get; set; }
        public int NivelAcesso { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblFuncionario> tblFuncionario { get; set; }
    }
}
