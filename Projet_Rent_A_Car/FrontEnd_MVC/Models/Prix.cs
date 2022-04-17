using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd_MVC.Models
{
    public partial class Prix
    {

        [Display(Name = "Numéro du prix")]
        public int Idprix { get; set; }
        [Display(Name = "Référence du pays")]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public int Idpays { get; set; }
        public IEnumerable<SelectListItem> ListPays { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        [Display(Name = "Valeur du prix")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Ce champ doit être un nombre supérieur à 0")]
        public decimal PrixKm { get; set; }

        public virtual Pays IdpaysNavigation { get; set; } = null!;
    }
}
