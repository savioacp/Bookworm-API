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
    
    public partial class tblLeitor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblLeitor()
        {
            this.tblEmprestimo = new HashSet<tblEmprestimo>();
            this.tblFavoritos = new HashSet<tblFavoritos>();
            this.tblReserva = new HashSet<tblReserva>();
        }
    
        public int IDLeitor { get; set; }
        public Nullable<int> IDTipoLeitor { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public Nullable<System.DateTime> DataNasc { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public System.DateTime DataCadastro { get; set; }
        public string Senha { get; set; }
        public string Salt { get; set; }
        public byte[] ImagemLeitor { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblEmprestimo> tblEmprestimo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblFavoritos> tblFavoritos { get; set; }
        public virtual tblTipoLeitor tblTipoLeitor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblReserva> tblReserva { get; set; }
    }
}
