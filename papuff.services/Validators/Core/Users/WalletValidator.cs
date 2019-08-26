using FluentValidation;
using papuff.domain.Core.Users;

namespace papuff.services.Validators.Core.Users {
    public class WalletValidator : AbstractValidator<Wallet> {
        public WalletValidator() {
            RuleFor(r => r.Account).NotNull().NotEmpty()
                .WithMessage("Conta é obrigatória.");

            RuleFor(r => r.Agency).NotNull().NotEmpty()
                .WithMessage("Agencia é obrigatória.");

            RuleFor(r => r.DateDue).GreaterThan(0)
                .WithMessage("Data de processamento deve ser maior que zero.");

            RuleFor(r => r.Document).NotNull().NotEmpty()
                .WithMessage("Informe os dados do seu documento.");

            RuleFor(r => r.Type).IsInEnum()
                .WithMessage("Selecione um tipo de documento valido.");

            RuleFor(r => r.UserId).NotNull().NotEmpty()
               .WithMessage("Usuário não identificado, contate o suporte.");
        }
    }
}
