using FluentValidation;
using papuff.domain.Core.Users;

namespace papuff.services.Validators.Core.Users {
    public class UserValidator : AbstractValidator<User> {
        public UserValidator() {
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Endereço de e-mail é obrigatório.")
                .EmailAddress().WithMessage("É necessário um e-mail válido.");
        }

        //private static bool ClienteMaiorDeIdade(DateTimeOffset dataNascimento) {
        //    return dataNascimento <= DateTimeOffset.Now.AddYears(-18);
        //}
    }
}
