using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd_MVC.Models
{
    public partial class Prix
    {

        [Display(Name = "Numéro du prix")]
        public int Idprix { get; set; }
        [Display(Name = "Référence du pays")]
        public int Idpays { get; set; }
        public IEnumerable<SelectListItem> ListPays { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        [Display(Name = "Valeur du prix")]
        public decimal PrixKm { get; set; }

        public virtual Pays IdpaysNavigation { get; set; } = null!;
    }
}
