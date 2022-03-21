using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd_MVC.Models
{
    public partial class Voiture
    {
        public Voiture()
        {
            Reservation = new HashSet<Reservation>();
        }

        public int Idvoiture { get; set; }

        [Display(Name = "Libellé de la notoriété")]
        public int Idnotoriete { get; set; }

        [Display(Name = "Ville du dépot")]
        public int Iddepot { get; set; }
        public string Immatriculation { get; set; } = null!;
        public string? Marque { get; set; }
        public bool? Inactif { get; set; }

        public IEnumerable<SelectListItem> ListDepot { get; set; }
        public IEnumerable<SelectListItem> ListNotoriete { get; set; }

        public virtual Depot IddepotNavigation { get; set; } = null!;
        public virtual Notoriete IdnotorieteNavigation { get; set; } = null!;
        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
