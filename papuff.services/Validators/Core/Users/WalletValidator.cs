using FluentValidation;
using papuff.domain.Core.Users;

namespace papuff.services.Validators.Core.Users {
    public class WalletValidator : AbstractValidator<Wallet> {
        public WalletValidator() {
            RuleFor(r => r.Account).NotNull().NotEmpty()
                .WithMessage("");

            RuleFor(r => r.Agency).NotNull().NotEmpty()
                .WithMessage("");

            RuleFor(r => r.DateDue).GreaterThan(0)
                .WithMessage("");

            RuleFor(r => r.Document).NotNull().NotEmpty()
                .WithMessage("");

            RuleFor(r => r.Type).IsInEnum()
                .WithMessage("");

            RuleFor(r => r.UserId).NotNull().NotEmpty()
                .WithMessage("");
        }
    }
}
