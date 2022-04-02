using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd_MVC.Models
{
    public partial class Reservation
    {
        public int Idreservation { get; set; }

        public int Idclient { get; set; }

        [Display (Name = "Voiture") ]
        public int Idvoiture { get; set; }
        

        [Display(Name = "Ville de départ")]
        [Required(ErrorMessage = "This field is mandatory")]
        public int IddepotDepart { get; set; }

        [Display(Name = "Ville de retour")]
        public int IddepotRetour { get; set; }

        public int? Idforfait { get; set; }
        public DateTime DateReservation { get; set; }
        public DateTime DateDepart { get; set; }
        public DateTime? DateRetour { get; set; }
        [Display(Name = "Km au depart")]
        public int? KilometrageDepart { get; set; }
        [Display(Name = "Km au retour")]
        public int? KilometrageRetour { get; set; }
        public decimal CoefficientMultiplicateur { get; set; }

        
        [Display(Name = "Pays de départ")] 
        public int IdpaysDepart { get; set; }  // Nécessaire pour Effectuer Réservation

        
        public IEnumerable<SelectListItem> ListPays { get; set; }
        public IEnumerable<SelectListItem> ListDepotDepart { get; set; }
        public IEnumerable<SelectListItem> ListDepotRetour { get; set; }
        //public IEnumerable<SelectListItem> ListDepotForfait { get; set; }  // Pas nécessaire
        public IEnumerable<SelectListItem> ListVoitureDisponible { get; set; }



        public virtual Client IdclientNavigation { get; set; } = null!;
        public virtual Depot IddepotDepartNavigation { get; set; } = null!;
        public virtual Depot IddepotRetourNavigation { get; set; } = null!;
        public virtual Forfait? IdforfaitNavigation { get; set; }
        public virtual Voiture IdvoitureNavigation { get; set; } = null!;
    }
}
