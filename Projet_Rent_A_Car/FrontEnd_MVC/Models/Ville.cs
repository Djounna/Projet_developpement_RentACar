using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

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
        [MaxLength(50, ErrorMessage = "Ce champ doit contenir maximum 50 caractères")]
        public string Nom { get; set; } = null!;

        public virtual Pays IdpaysNavigation { get; set; } = null!;

        public virtual ICollection<Depot> Depot { get; set; }
    }
}
