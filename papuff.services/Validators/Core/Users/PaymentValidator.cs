using FluentValidation;
using papuff.domain.Core.Wallets;

namespace papuff.services.Validators.Core.Users {
    public class PaymentValidator : AbstractValidator<Payment> {
        public PaymentValidator() {
            RuleFor(r => r.WalletId)
                .NotEmpty().NotNull().WithMessage("");

            RuleFor(r => r.Card)
                .NotEmpty().NotNull().WithMessage("");

            RuleFor(r => r.Code)
                .NotEmpty().NotNull().WithMessage("");

            RuleFor(r => r.Document)
                .NotEmpty().NotNull().WithMessage("");
        }
    }
}
