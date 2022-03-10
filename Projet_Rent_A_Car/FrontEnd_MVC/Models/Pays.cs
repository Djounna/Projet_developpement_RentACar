﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd_MVC.Models
{
    public partial class Pays
    {
        public Pays()
        {
            Ville = new HashSet<Ville>();
        }

        public int Idpays { get; set; }

        [Required(ErrorMessage = "This field is mandatory")]
        [MaxLength(50, ErrorMessage = "this field shouldn'thave more than 50 character")]
        public string Nom { get; set; } = null!;

        public virtual ICollection<Ville> Ville { get; set; }
    

}
}
