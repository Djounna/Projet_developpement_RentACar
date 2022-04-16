using System.ComponentModel.DataAnnotations;

namespace FrontEnd_MVC.Models
{
    public partial class Notoriete
    {
        public Notoriete()
        {
            Voiture = new HashSet<Voiture>();
        }

        public int Idnotoriete { get; set; }

        [Display(Name = "Libellé de la notoriété")]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        [MaxLength(50, ErrorMessage = "Ce champ doit contenir maximum 50 caractères")]
        public string Libelle { get; set; }

        [Range(0, 5, ErrorMessage = "Ce champ doit contenir un chiffre entre 0 et 5")]
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        [Display(Name = "Coefficient multplicateur du prix")]
        public decimal CoefficientMultiplicateur { get; set; }

        [Display(Name = "Est Inactif")]
        public bool? Inactif { get; set; }

        public virtual ICollection<Voiture> Voiture { get; set; }
    }
}
