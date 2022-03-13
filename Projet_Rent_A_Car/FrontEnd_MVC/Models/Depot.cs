using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd_MVC.Models
{
    public partial class Depot
    {
        public Depot()
        {
            ForfaitIddepot1Navigation = new HashSet<Forfait>();
            ForfaitIddepot2Navigation = new HashSet<Forfait>();
            ReservationIddepotDepartNavigation = new HashSet<Reservation>();
            ReservationIddepotRetourNavigation = new HashSet<Reservation>();
            Voiture = new HashSet<Voiture>();
        }

        [Display(Name="Numéro du dépôt")]
        public int Iddepot { get; set; }

        [Display(Name = "Libellé de la ville")]
        public int Idville { get; set; }
        
        public IEnumerable<SelectListItem> ListVille { get; set; }

        [Display(Name = "Est Inactif")]
        public bool? Inactif { get; set; }

        public virtual Ville IdvilleNavigation { get; set; } = null!;
        public virtual ICollection<Forfait> ForfaitIddepot1Navigation { get; set; }
        public virtual ICollection<Forfait> ForfaitIddepot2Navigation { get; set; }
        public virtual ICollection<Reservation> ReservationIddepotDepartNavigation { get; set; }
        public virtual ICollection<Reservation> ReservationIddepotRetourNavigation { get; set; }
        public virtual ICollection<Voiture> Voiture { get; set; }
    }
}
