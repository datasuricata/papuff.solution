using FluentValidation;
using papuff.domain.Core.Generals;

namespace papuff.services.Validators.Core.Generals {
    public class GeneralValidator : AbstractValidator<General> {

        public GeneralValidator() {
            RuleFor(r => r.Name).NotNull().NotEmpty()
                .WithMessage("Informe seu nome completo.");

            RuleFor(r => r.UserId).NotNull().NotEmpty()
                .WithMessage("Usuário não identificado, contate o suporte.");
        }
    }
}
