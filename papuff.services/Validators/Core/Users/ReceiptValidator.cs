using FluentValidation;
using papuff.domain.Core.Wallets;

namespace papuff.services.Validators.Core.Users {
    public class ReceiptValidator : AbstractValidator<Receipt> {
        public ReceiptValidator() {
            RuleFor(r => r.Account)
                .NotEmpty().NotNull().WithMessage("Informe a conta.");

            RuleFor(r => r.Agency)
                .NotEmpty().NotNull().WithMessage("Informe a agencia.");

            RuleFor(r => r.DateDue)
                .Must(x => x > 0).WithMessage("Data de pagamento deve ser maior que 0.");

            RuleFor(r => r.WalletId)
                .NotEmpty().NotNull().WithMessage("Deve vincular com a carteira.");
        }
    }
}