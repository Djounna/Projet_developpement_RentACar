using FluentValidation;
using FrontEnd_MVC.Models;

namespace FrontEnd_MVC.Validation
{
    public class ReservationValidator : AbstractValidator<Reservation>
    {
        public ReservationValidator(){

        //RuleFor(reservation => reservation.KilometrageRetour).GreaterThan(reservation => reservation.KilometrageDepart);
        //RuleFor(reservation => reservation.DateDepart).GreaterThan(DateTime.Now);
        //RuleFor(reservation => reservation.DateRetour).GreaterThan(reservation => reservation.DateDepart);

            }
    }
}
