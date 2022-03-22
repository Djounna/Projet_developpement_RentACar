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
        public int? KilometrageDepart { get; set; }
        public int? KilometrageRetour { get; set; }
        public decimal CoefficientMultiplicateur { get; set; }

        public virtual Client IdclientNavigation { get; set; } = null!;
        public virtual Depot IddepotDepartNavigation { get; set; } = null!;
        public virtual Depot IddepotRetourNavigation { get; set; } = null!;
        public virtual Forfait? IdforfaitNavigation { get; set; }
        public virtual Voiture IdvoitureNavigation { get; set; } = null!;
    }
}
