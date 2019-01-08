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
    
    public partial class Evenements
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Evenements()
        {
            this.Contenus = new HashSet<Contenus>();
            this.Participations = new HashSet<Participations>();
            this.Reservations = new HashSet<Reservations>();
        }
    
        public int Id { get; set; }
        public Nullable<System.DateTime> DtDebut { get; set; }
        public Nullable<System.DateTime> DtFin { get; set; }
        public Nullable<System.DateTime> DtLimiteInscription { get; set; }
        public string Descriptif { get; set; }
        public Nullable<int> Maximum { get; set; }
        public string Libelle { get; set; }
        public string Photo { get; set; }
        public Nullable<double> Prix { get; set; }
        public Nullable<int> TypologieId { get; set; }
        public Nullable<int> EvenementParentId { get; set; }
        public string VisibledYN { get; set; }
        public string Logo { get; set; }
        public string Bandeau { get; set; }
        public string Lien { get; set; }
        public Nullable<int> Minimum { get; set; }
        public Nullable<int> Duree { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contenus> Contenus { get; set; }
        public virtual Typologies Typologie { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Participations> Participations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservations> Reservations { get; set; }
    }
}
