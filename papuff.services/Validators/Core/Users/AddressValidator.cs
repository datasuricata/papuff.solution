using FluentValidation;
using papuff.domain.Core.Users;

namespace papuff.services.Validators.Core.Users {
    public class AddressValidator : AbstractValidator<Address> {
        public AddressValidator() {
            RuleFor(r => r.AddressLine).NotNull().NotEmpty()
                .WithMessage("Linha de endereço deve ser informado.");

            RuleFor(r => r.City).NotNull().NotEmpty()
                .WithMessage("Informe uma cidade.");

            RuleFor(r => r.Country).NotNull().NotEmpty()
                .WithMessage("Informe o país.");

            RuleFor(r => r.PostalCode).NotNull().NotEmpty()
                .WithMessage("Informe o código postal.");

            RuleFor(r => r.StateProvince).NotNull().NotEmpty()
                .WithMessage("Estado é obrigatório");
        }
    }
}