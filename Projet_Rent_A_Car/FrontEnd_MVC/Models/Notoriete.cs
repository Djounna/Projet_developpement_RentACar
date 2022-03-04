using System;
using System.Collections.Generic;

namespace FrontEnd_MVC.Models
{
    public partial class Notoriete
    {
        public Notoriete()
        {
            Voiture = new HashSet<Voiture>();
        }

        public int Idnotoriete { get; set; }
<<<<<<< HEAD
        public string Libelle { get; set; } = null!;
        public decimal CoefficientMultiplicateur { get; set; }
=======
        public string? Libelle { get; set; }

        
        public decimal? CoefficientMultiplicateur { get; set; }
>>>>>>> Antoine
        public bool? Inactif { get; set; }

        public virtual ICollection<Voiture> Voiture { get; set; }
    }
}
