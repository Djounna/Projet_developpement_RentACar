using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrontEnd_MVC.Models
{
    public partial class Ville
    {
        public Ville()
        {
            Depot = new HashSet<Depot>();
        }

        [Display(Name = "Numéro de la ville")]
        public int Idville { get; set; }

        [Display(Name = "Libellé du pays")] 
        public int Idpays { get; set; }
        public IEnumerable<SelectListItem> ListPays { get; set; }

        [Display(Name = "Libellé de la ville")]
        public string Nom { get; set; } = null!;

        public virtual Pays IdpaysNavigation { get; set; } = null!;

        public virtual ICollection<Depot> Depot { get; set; }
    }
}
