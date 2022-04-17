using Microsoft.AspNetCore.Mvc.Rendering;
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
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public int Iddepot1 { get; set; }

        
        [Display(Name = "Ville du second dépôt")]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public int Iddepot2 { get; set; }

        [Display(Name = "Prix du forfait")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Ce champ doit être un nombre supérieur à 0")]
        public decimal Prix { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public IEnumerable<SelectListItem> ListDepot { get; set; }

        public virtual Depot Iddepot1Navigation { get; set; } = null!;
        public virtual Depot Iddepot2Navigation { get; set; } = null!;
        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
