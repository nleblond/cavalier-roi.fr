﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;

using WS.Models;
using WS.Models.IN;
using WS.Models.OUT;

namespace WS.BLL
{
    public static class CommandesManager
    {

        


        public static List<Commande> GetCommmandes(Int32? _Id = null, String _DtMin = null, String _DtMax = null, Int32? _ProduitId = null, String _ProduitReference = null, Int32? _EleveId = null, String _ReferenceTransaction = null, String _ReferenceExterne = null, Int32? _StatutId = null)
        {

            DBModelsParameters _DB = new WS.Models.DBModelsParameters();
    
            List<CommandeResult> _CommandeResults = _DB.GetCommandes(
                                   id: (_Id == null ? -1 : _Id),
                                   dtMin: (String.IsNullOrEmpty(_DtMin) ? null : _DtMin.Replace("/", "-")),
                                   dtMax: (String.IsNullOrEmpty(_DtMax) ? null : _DtMax.Replace("/", "-")),
                                   produitId: (_ProduitId == null ? -1 : _ProduitId),
                                   produitReference: (String.IsNullOrEmpty(_ProduitReference) ? null : _ProduitReference),
                                   eleveId: (_EleveId == null ? -1 : _EleveId),
                                   referenceTransaction: (String.IsNullOrEmpty(_ReferenceTransaction) ? null : _ReferenceTransaction),
                                   referenceExterne: (String.IsNullOrEmpty(_ReferenceExterne) ? null : _ReferenceExterne),
                                   statutId: (_StatutId == null ? -1 : _StatutId)
                              ).ToList();

            List<Commande> _Commandes = new List<Commande>();
            foreach (CommandeResult _Current in _CommandeResults)
            {
                if (_Commandes.Any(c => c.Id == _Current.Id) == false)
                {
                    Commande _NewCommande = new Commande();
                    _NewCommande.Id = Int32.Parse(_Current.Id.ToString());
                    _NewCommande.DtCreation = _Current.DtCreation;

                    _NewCommande.DtModification = _Current.DtModification;
                    _NewCommande.DtValidation = _Current.DtValidation;

                    _NewCommande.Prix = _Current.Total;
                    _NewCommande.ReferenceTransaction = _Current.ReferenceTransaction.Trim();
                    _NewCommande.ReferenceExterne = _Current.ReferenceExterne.Trim();

                    if (_Current.StatutId != null)
                    {
                        _NewCommande.Statut = new Statut();
                        _NewCommande.Statut.Id = Int32.Parse(_Current.StatutId.ToString());
                        _NewCommande.Statut.Libelle = _Current.StatutLibelle;
                    }

                    if (_Current.AdresseId != null)
                    {
                        _NewCommande.Adresse = new Adresse();
                        _NewCommande.Adresse.Id = Int32.Parse(_Current.AdresseId.ToString());
                        _NewCommande.Adresse.Destinataire = _Current.Destinataire;
                        _NewCommande.Adresse.Ligne1 = _Current.Ligne1;
                        _NewCommande.Adresse.Ligne2 = _Current.Ligne2;
                        _NewCommande.Adresse.CodePostal = _Current.CodePostal;
                        _NewCommande.Adresse.Ville = _Current.Ville;
                        _NewCommande.Adresse.Telephone = _Current.Telephone;
                        _NewCommande.Adresse.Email = _Current.Email;
                    }

                    _NewCommande.Lignes = new List<Ligne>();
                    Double _PrixLignes = 0;
                    foreach (CommandeResult _Current2 in _CommandeResults.FindAll(c => c.Id == _Current.Id && c.LigneId != null) as List<CommandeResult>)
                    {
                        _PrixLignes += Double.Parse(_Current2.Prix.ToString());

                        Ligne _NewLigne = new Ligne();
                        _NewLigne.Id = Int32.Parse(_Current2.LigneId.ToString());
                        _NewLigne.Quantite = Int16.Parse(_Current2.Quantite.ToString());
                        _NewLigne.Reduction = _Current2.Reduction;
                        _NewLigne.Prix = Double.Parse(_Current2.Prix.ToString());

                        Produit _NewProduit = new Produit();
                        _NewProduit.Id = Int32.Parse(_Current2.ProduitId.ToString());
                        _NewProduit.Libelle = _Current2.ProduitLibelle;
                        _NewProduit.Reference = _Current2.ProduitReference;
                        _NewLigne.Produit = _NewProduit;

                        _NewCommande.Lignes.Add(_NewLigne);
                    }

                    if (_Current.FraiId != null)
                    {
                        _NewCommande.Frai = new Frai();
                        _NewCommande.Frai.Id = Int32.Parse(_Current.FraiId.ToString());
                        _NewCommande.Frai.Libelle = _Current.FraiLibelle;

                        if (_Current.FraiPrix == null) {
                            //envoi "exceptionnel"
                            _NewCommande.Frai.Prix = _Current.Total - _PrixLignes;
                        }
                        else
                        {
                            //envoi "classique"
                            _NewCommande.Frai.Prix = _Current.FraiPrix;
                        }
                    }

                    if (_Current.EleveId != null)
                    {
                        _NewCommande.Eleve = new Eleve();
                        _NewCommande.Eleve.Id = Int32.Parse(_Current.EleveId.ToString());
                        _NewCommande.Eleve.Nom = _Current.Nom;
                        _NewCommande.Eleve.Prenom = _Current.Prenom;
                    }

                    _Commandes.Add(_NewCommande);
                }
            }

            return _Commandes;
        }



        public static Int32? AddCommande(DateTime? _DtCreation = null, DateTime? _DtValidation = null, Int32? _StatutId = null, Int32? _EleveId = null, Double? _Prix = null, Int32? _FraiId = null, String _ReferenceTransaction = null, String _ReferenceExterne = null, Adresse _Adresse = null, List<Ligne> _Lignes = null)
        {
            DBModelsParameters _DB = new WS.Models.DBModelsParameters();


            //ajout de l'adresse
            Int32? _NewAdresseId = _DB.AddAdresse(
                                    destinataire: _Adresse.Destinataire.Trim(),
                                    ligne1: _Adresse.Ligne1.Trim(),
                                    ligne2: (String.IsNullOrEmpty(_Adresse.Ligne2) ? null : _Adresse.Ligne2.Trim()),
                                    codePostal: _Adresse.CodePostal.Trim(),
                                    ville: _Adresse.Ville.Trim(),
                                    pays: _Adresse.Pays.Trim(),
                                    telephone: _Adresse.Telephone.Trim(),
                                    email: _Adresse.Email.Trim()
            ).FirstOrDefault().Value;

            //ajout de la commande
            Int32? _NewCommandeId = _DB.AddCommande(
                                    dtCreation: _DtCreation,
                                    dtValidation: _DtValidation,
                                    statutId: (_StatutId == null ? -1 : _StatutId),
                                    eleveId: (_EleveId == null ? -1 : _EleveId),
                                    prix: (_Prix == null ? -1 : _Prix),
                                    fraiId: (_FraiId == null ? -1 : _EleveId),
                                    referenceTransaction: (String.IsNullOrEmpty(_ReferenceTransaction) ? null : _ReferenceTransaction.Trim()),
                                    referenceExterne: (String.IsNullOrEmpty(_ReferenceExterne) ? null : _ReferenceExterne.Trim()),
                                    adresseId: (_NewAdresseId == null ? -1 : _NewAdresseId)
            ).FirstOrDefault().Value;

            //ajout des lignes
            foreach(Ligne _Current in _Lignes)
            {
                Int32? _NewLigneId = _DB.AddLigne(
                                        produitId: _Current.Produit.Id,
                                        commandeId: _NewCommandeId,
                                        quantite: _Current.Quantite,
                                        statutId: _StatutId,
                                        prix: _Current.Prix,
                                        reduction: _Current.Reduction
                ).FirstOrDefault().Value;
            }

            //envoi du mail de confirmation
            String _EmailConfirmation = String.Empty;
            _EmailConfirmation += "<html>";
            _EmailConfirmation += "<body>";
            _EmailConfirmation += "<img src=\"http://www.cavalier-roi.fr/Content/Images/LogoMail.jpg\" />";
            _EmailConfirmation += "<br /><hr /><br />";
            if (_StatutId == 3)
            {
                _EmailConfirmation += "Votre commande #" + _NewCommandeId.ToString() + " a bien été prise en compte et votre paiement " + _ReferenceTransaction + " a bien été effectué !";
            }
            else if (_StatutId == 2)
            {
                _EmailConfirmation += "Votre commande #" + _NewCommandeId.ToString() + " a bien été prise en compte !";
            }
            _EmailConfirmation += "<br /><br />";
            _EmailConfirmation += "<table cellpadding=\"2\" cellspacing=\"2\" border=\"1\">";
            _EmailConfirmation += "<tr>";
            _EmailConfirmation += "     <th>Numéro de produit</th>";
            _EmailConfirmation += "     <th>Libellé de produit</th>";
            _EmailConfirmation += "     <th>Quantité</th>";
            foreach (Ligne _Current in _Lignes)
            {
                _EmailConfirmation += "<tr>";
                _EmailConfirmation += "     <td>" + (!String.IsNullOrEmpty(_Current.Produit.Reference) ? _Current.Produit.Reference : _Current.Produit.Id.ToString()) + "</td>";
                _EmailConfirmation += "     <td>" + _Current.Produit.Libelle + "</td>";
                _EmailConfirmation += "     <td>" + _Current.Quantite + "</td>";
                _EmailConfirmation += "</tr>";
            }
            _EmailConfirmation += "</table>";
            _EmailConfirmation += "<br /><br />";
            _EmailConfirmation += "Vous pouvez retrouver toutes vos commandes dans la partie \"Mon Compte\" du site de l'École du cavalier roi : <a href=\"" + WS.Constants.SITE_URL + "/MonCompte\" target=\"_blank\">" + WS.Constants.SITE_URL + "/MonCompte</a>.";
            _EmailConfirmation += "<br /><br />";
            if (_StatutId == 3)
            {
                _EmailConfirmation += "Merci de contacter au plus vite l'École du cavalier roi à <a href=\"mailto:" + WS.Constants.COMMANDES_EMAIL + "\" target=\"_blank\">" + WS.Constants.COMMANDES_EMAIL + "</a> pour régler le paiement.";
            }
            else if (_StatutId == 2)
            {
                _EmailConfirmation += "Vous recevrez votre facture directement par mail ou en la demandant à <a href=\"mailto:" + WS.Constants.COMMANDES_EMAIL + "\" target=\"_blank\">" + WS.Constants.COMMANDES_EMAIL + "</a>";
                _EmailConfirmation += "<br /><br />";
                _EmailConfirmation += "Pour plus d'informations, n'hésitez pas à contacter l'École du cavalier roi à <a href=\"mailto:" + WS.Constants.COMMANDES_EMAIL + "\" target=\"_blank\">" + WS.Constants.COMMANDES_EMAIL + "</a>.";
            }
            _EmailConfirmation += "<br /><br />";
            _EmailConfirmation += "L'École du cavalier roi";
            _EmailConfirmation += "<br /><br />";
            _EmailConfirmation += "</body>";
            _EmailConfirmation += "</html>";
            Eleve _Eleve = ElevesManager.GetEleves(_Id: _EleveId)[0];
            Tools.SendMail(WS.Constants.COMMANDES_EMAIL, _Eleve.Email, "Confirmation de commande", _EmailConfirmation, true, WS.Constants.MAILSERVER_URL, WS.Constants.MAILSERVER_PORT, WS.Constants.COMMANDES_USERNAME, WS.Constants.COMMANDES_PASSWORD, WS.Constants.COMMANDES_CC, WS.Constants.COMMANDES_CCI);

            return _NewCommandeId;
        }


        public static Int32 DelCommande(Int32? _Id = null, String _Real = "N")
        {
            DBModelsParameters _DB = new WS.Models.DBModelsParameters();

            return _DB.DelCommande(_Id, _Real);
        }

        public static Int32 UpdCommande(Int32? _Id = null, Int32? _StatutId = null, String _ReferenceTransaction = null, String _ReferenceExterne = null)
        {
            DBModelsParameters _DB = new WS.Models.DBModelsParameters();

            return _DB.UpdCommande(
                                    id: (_Id == null ? -1 : _Id),
                                    statutId: (_StatutId == null ? -1 : _StatutId),
                                    referenceTransaction: (String.IsNullOrEmpty(_ReferenceTransaction) ? null : _ReferenceTransaction),
                                    referenceExterne: (String.IsNullOrEmpty(_ReferenceExterne) ? null : _ReferenceExterne)
                                );
        }







    }
}