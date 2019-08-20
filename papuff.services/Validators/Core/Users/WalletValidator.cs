using FluentValidation;
using papuff.domain.Core.Users;

namespace papuff.services.Validators.Core.Users {
    public class WalletValidator : AbstractValidator<Wallet> {
        public WalletValidator() {
        }
    }
}
