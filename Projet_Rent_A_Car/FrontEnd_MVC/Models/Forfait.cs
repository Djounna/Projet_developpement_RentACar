using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd_MVC.Models
{
    public partial class Forfait
    {
        public Forfait()
        {
            Reservation = new HashSet<Reservation>();
        }

        [Display(Name = "Numéro du forfait")]
        public int Idforfait { get; set; }

        [Display(Name = "Ville du premier dépôt")]
        public int Iddepot1 { get; set; }

        [Display(Name = "Ville du second dépôt")]
        public int Iddepot2 { get; set; }

        [Display(Name = "Prix du forfait")]
        public decimal Prix { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public IEnumerable<SelectListItem> ListDepot { get; set; }

        public virtual Depot Iddepot1Navigation { get; set; } = null!;
        public virtual Depot Iddepot2Navigation { get; set; } = null!;
        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
