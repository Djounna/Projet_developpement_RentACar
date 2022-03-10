using System;
using System.Collections.Generic;
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

        [Display(Name ="Libelle de la notoriété")]
        [Required(ErrorMessage = "This field is mandatory")]
        [MaxLength(50, ErrorMessage = "this field shouldn'thave more than 50 character")]
        public string Libelle { get; set; }

        [Range(0,100,ErrorMessage ="This field should be between 0 and 100")]
        [Display(Name = "Coefficient multplicateur du prix")]
        public double CoefficientMultiplicateur { get; set; }

        [Display(Name = "Est Inactif")]
        public bool? Inactif { get; set; }

        public virtual ICollection<Voiture> Voiture { get; set; }
    }
}
