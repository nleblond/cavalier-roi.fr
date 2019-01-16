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
    
    public partial class CommandeResult
    {
        public Nullable<int> Id { get; set; }
        public string ReferenceTransaction { get; set; }
        public string ReferenceExterne { get; set; }
        public string DtCreation { get; set; }
        public Nullable<int> EleveId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public Nullable<int> ProduitId { get; set; }
        public string ProduitLibelle { get; set; }
        public string ProduitReference { get; set; }
        public Nullable<int> LigneId { get; set; }
        public Nullable<int> Quantite { get; set; }
        public Nullable<double> Prix { get; set; }
        public Nullable<double> Reduction { get; set; }
        public Nullable<double> Total { get; set; }
        public Nullable<int> StatutId { get; set; }
        public string StatutLibelle { get; set; }
        public Nullable<int> FraiId { get; set; }
        public string FraiLibelle { get; set; }
        public Nullable<double> FraiPrix { get; set; }
        public Nullable<int> AdresseId { get; set; }
        public string Ligne1 { get; set; }
        public string Ligne2 { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Destinataire { get; set; }
        public string DtModification { get; set; }
        public string DtValidation { get; set; }
        public string TrackingNumber { get; set; }
    }
}
