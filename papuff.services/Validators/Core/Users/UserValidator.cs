using FluentValidation;
using papuff.domain.Core.Generals;
using papuff.domain.Core.Users;
using System;

namespace papuff.services.Validators.Core.Users {
    public class UserValidator : AbstractValidator<User> {
        public UserValidator() {
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Endereço de e-mail é obrigatório.")
                .EmailAddress().WithMessage("É necessário um e-mail válido.");

            //RuleFor(r => r.General).SetValidator(new GeneralValidator());

            //RuleFor(r => r.General)
            //    .Must(GeneralForUser).WithMessage("Usuário deve ser maior de 18 anos.");
        }

        private static bool GeneralForUser(General general) {
            if (!string.IsNullOrEmpty(general.UserId))
                return BeOver18(general.BirthDate);

            return false;
        }

        private static bool BeOver18(DateTime birthDate) {
            return birthDate <= DateTime.Now.AddYears(-18);
        }
    }
}
