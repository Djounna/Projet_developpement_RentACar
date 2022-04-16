using FluentValidation;
using Models;
using ProjetSGDBContext = DataAccessLayer.ProjetSGDBContext;

namespace API_RAC.Validation
{
    public class PaysValidation : AbstractValidator<Pays>
    {
        private ProjetSGDBContext _ProjetSGDBContext;

        public PaysValidation(ProjetSGDBContext projetSGDB)
        {
            _ProjetSGDBContext = projetSGDB;
        }
        /*
        private async Task<bool> AlreadyExist(string nom)
        {
            var pays = _ProjetSGDBContext.Pays.SingleOrDefault(p => p.Nom == nom);
            return !(pays != null);

        }
         public PaysValidation()
        {
            RuleFor(p => p.Nom)
                .NotNull().WithMessage("The field name shouldn't be empty")
                .Length(50).WithMessage("this field should have less than 51 character");

            RuleFor(p => p.Nom)
                .MustAsync((p, cancellation) => AlreadyExist(p))
                .When(y => !string.IsNullOrEmpty(y.Nom))
                .WithMessage("Le pays existe déjà");

            RuleFor(p => p.ReferencePrix)
                .NotNull().WithMessage("This field shouldn't be empty");



        }*/
    }
}
