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
    
    public partial class tblTipoLeitor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblTipoLeitor()
        {
            this.tblLeitor = new HashSet<tblLeitor>();
        }
    
        public int IDTipoLeitor { get; set; }
        public string TipoLeitor { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblLeitor> tblLeitor { get; set; }
    }
}
