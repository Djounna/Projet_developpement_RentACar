using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd_MVC.Models
{
    public partial class Reservation
    {
        public int Idreservation { get; set; }

        [Display(Name = "Client")]
        public int Idclient { get; set; }

        [Display(Name = "Voiture")]
        public int Idvoiture { get; set; }


        [Display(Name = "Ville de départ")]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public int IddepotDepart { get; set; }

        [Display(Name = "Ville de retour")]
        public int? IddepotRetour { get; set; }

        [Display(Name = "Prix du forfait ")]
        public int? Idforfait { get; set; }
        public DateTime DateReservation { get; set; }
        public DateTime DateDepart { get; set; }
        public DateTime DateRetour { get; set; }
        [Display(Name = "Km au depart")]
        [Range(0, Double.PositiveInfinity, ErrorMessage = "Ce champ doit être > 0")] 
        public int? KilometrageDepart { get; set; }
        [Display(Name = "Km au retour")]
        [Range(0, Double.PositiveInfinity, ErrorMessage = "Ce champ doit être > 0")]
        public int? KilometrageRetour { get; set; }
        public decimal CoefficientMultiplicateur { get; set; }

        public bool? Penalite { get; set; }

        [Display(Name = "Pays de départ")]
        public int IdpaysDepart { get; set; }  // Nécessaire pour Effectuer Réservation
        [Display(Name = "Pays de retour")]
        public int? IdpaysRetour { get; set; }  // Nécessaire pour cloturer réservation

        public int? IddepotRetourPrevu { get; set; } // Nécessaire pour Cloture Location

        public decimal? PrixTotal { get; set; }

        public IEnumerable<SelectListItem> ListPays { get; set; }
        public IEnumerable<SelectListItem> ListDepotDepart { get; set; }
        public IEnumerable<SelectListItem> ListDepotRetour { get; set; }
        //public IEnumerable<SelectListItem> ListDepotForfait { get; set; }  // Pas nécessaire
        public IEnumerable<SelectListItem> ListVoitureDisponible { get; set; }



        public virtual Client IdclientNavigation { get; set; } = null!;
        public virtual Depot IddepotDepartNavigation { get; set; } = null!;
        public virtual Depot? IddepotRetourNavigation { get; set; } = null!;
        public virtual Forfait? IdforfaitNavigation { get; set; }
        public virtual Voiture IdvoitureNavigation { get; set; } = null!;
    }
}
