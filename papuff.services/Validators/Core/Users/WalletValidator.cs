using FluentValidation;
using papuff.domain.Core.Wallets;

namespace papuff.services.Validators.Core.Users {
    public class WalletValidator : AbstractValidator<Wallet> {
        public WalletValidator () {
            RuleFor (r => r.UserId).NotNull ().NotEmpty ()
                .WithMessage ("Usuário não identificado, contate o suporte.");
        }
    }
}