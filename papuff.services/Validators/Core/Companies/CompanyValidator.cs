using FluentValidation;
using papuff.domain.Core.Companies;

namespace papuff.services.Validators.Core.Companies {
    public class CompanyValidator : AbstractValidator<Company> {
        public CompanyValidator() {
            RuleFor(r => r.CNPJ).NotNull().NotEmpty()
                .WithMessage("CNPJ é obrigatório.");

            RuleFor(r => r.Email).EmailAddress()
                .WithMessage("Email é obrigatório.");

            RuleFor(r => r.Name).NotNull().NotEmpty()
                .WithMessage("Informe o nome fantasia da empresa.");

            RuleFor(r => r.Registration).NotNull().NotEmpty()
                .WithMessage("Informe o registro estadual.");

            RuleFor(r => r.UserId).NotNull().NotEmpty()
                .WithMessage("Usuário não identificado, contate o suporte.");
        }
    }
}
