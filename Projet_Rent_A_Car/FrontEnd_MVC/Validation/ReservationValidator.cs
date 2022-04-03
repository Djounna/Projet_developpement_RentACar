using FluentValidation;
using FrontEnd_MVC.Models;

namespace FrontEnd_MVC.Validation
{
    public class ReservationValidator : AbstractValidator<Reservation>
    {
        public ReservationValidator(){

        RuleFor(reservation => reservation.KilometrageRetour).GreaterThan(reservation => reservation.KilometrageDepart);

            }
    }
}
