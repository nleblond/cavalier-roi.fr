//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Emplacements
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Emplacements()
        {
            this.Publications = new HashSet<Publications>();
        }
    
        public int Id { get; set; }
        public string Libelle { get; set; }
        public string Visuel { get; set; }
        public string Key { get; set; }
        public Nullable<int> ModeId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Publications> Publications { get; set; }
        public virtual Modes Mode { get; set; }
    }
}
