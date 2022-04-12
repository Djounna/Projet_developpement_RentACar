using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd_MVC.Models
{
    public partial class Client
    {
        public Client()
        {
            Reservation = new HashSet<Reservation>();
        }

        public int Idclient { get; set; }
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        [MaxLength(50, ErrorMessage = "Ce champ doit contenir maximum 50 caractères")]
        public string Nom { get; set; } = null!;
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        [MaxLength(50, ErrorMessage = "Ce champ doit contenir maximum 50 caractères")]
        public string Prenom { get; set; } = null!;
        [Required(ErrorMessage = "Ce champ est obligatoire")]
        [MaxLength(50, ErrorMessage = "Ce champ doit contenir maximum 50 caractères")]
        [EmailAddress(ErrorMessage ="Ce champ doit contenir une adresse courriel")]
        [Display(Name = "Addresse courriel")]
        public string Mail { get; set; } = null!;

        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}
