using System.ComponentModel.DataAnnotations;

namespace FrontEnd_MVC.Models
{
    public partial class Pays
    {
        public Pays()
        {
            Ville = new HashSet<Ville>();
        }

        [Display(Name = "Numéro du pays")]
        public int Idpays { get; set; }

        [Required(ErrorMessage = "Ce champ est obligatoire")]
        [MaxLength(50, ErrorMessage = "Ce champ doit contenir maximum 50 caractères")]

        [Display(Name = "Libellé du pays")]
        public string Nom { get; set; } = null!;

        public virtual ICollection<Ville> Ville { get; set; }

        [Display(Name = "Prix au km")]
        public virtual Prix Price { get; set; }

    }
}
