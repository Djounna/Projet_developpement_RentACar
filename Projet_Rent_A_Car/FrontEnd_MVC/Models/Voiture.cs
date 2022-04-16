using Microsoft.AspNetCore.Mvc.Rendering;
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

        [MaxLength(9, ErrorMessage = "Ce champ doit contenir maximum 9 caractères")]
        [MinLength(9, ErrorMessage = "Ce champ doit contenir au minimum 9 caractères")]
        [Required(ErrorMessage = "This field is mandatory")]
        public string Immatriculation { get; set; } = null!;
        [MaxLength(50, ErrorMessage = "Ce champ doit contenir maximum 50 caractères")]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        public string Marque { get; set; }

        [Display(Name = "Est Inactif")]
        public bool? Inactif { get; set; }

        public IEnumerable<SelectListItem> ListDepot { get; set; }
        public IEnumerable<SelectListItem> ListNotoriete { get; set; }

        public virtual Depot IddepotNavigation { get; set; } = null!;
        public virtual Notoriete IdnotorieteNavigation { get; set; } = null!;
        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
