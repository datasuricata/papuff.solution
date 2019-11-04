using FluentValidation;
using papuff.domain.Core.Generals;
using papuff.domain.Core.Users;

namespace papuff.services.Validators.Core.Users {
    public class DocumentValidator : AbstractValidator<Document> {
        public DocumentValidator () {
            RuleFor (r => r.Type).IsInEnum ()
                .WithMessage ("Tipo do documento é obrigatório.");

            RuleFor (r => r.Value).NotEmpty ().NotNull ()
                .WithMessage ("Informe os dados do documento.");

            RuleFor (r => r.ImageUri).NotEmpty ().NotNull ();
        }
    }
}