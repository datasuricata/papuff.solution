using FluentValidation;
using papuff.domain.Arguments.Sieges;

namespace papuff.services.Validators.Core.Sieges {
    public class SiegeValidator : AbstractValidator<SiegeRequest> {
        public SiegeValidator() {
            RuleFor(r => r.OwnerId).NotNull()
                .WithMessage("Um proprietário deve ser atribuido.");

            //RuleFor(r => r.Seconds).LessThan(28800‬‬)
            //    .WithMessage("Deve ser menor que 20 dias");

            RuleFor(r => r.Seconds).GreaterThan(5)
                .WithMessage("Deve ser no minimo 5 segundos");

            RuleFor(r => r.OwnerId).NotNull().NotEmpty()
               .WithMessage("Usuário não identificado, contate o suporte.");
        }
    }
}