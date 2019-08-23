using FluentValidation;
using papuff.domain.Core.Sieges;

namespace papuff.services.Validators.Core.Sieges {
    public class AdsValidator : AbstractValidator<Advertising> {
        public AdsValidator() {
            RuleFor(r => r.Title).NotNull().NotEmpty()
                .WithMessage("Titulo da propaganda é obrigatório.");

            RuleFor(r => r.Description).NotNull().NotEmpty()
                .WithMessage("Descrição da propagandaé obrigatória.");

            RuleFor(r => r.ContentUri).NotNull().NotEmpty()
                .WithMessage("Url do conteúdo é obrigatório.");
        }
    }
}
