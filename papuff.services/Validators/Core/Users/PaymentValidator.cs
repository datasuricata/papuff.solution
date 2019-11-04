using FluentValidation;
using papuff.domain.Core.Wallets;

namespace papuff.services.Validators.Core.Users {
    public class PaymentValidator : AbstractValidator<Payment> {
        public PaymentValidator () {
            RuleFor (r => r.WalletId)
                .NotNull ().NotEmpty ().WithMessage ("Carteira de destino é obrigatória, contate o suporte.");

            RuleFor (r => r.Card)
                .NotNull ().NotEmpty ().WithMessage ("Informe o número do cartão.");

            RuleFor (r => r.Code)
                .NotNull ().NotEmpty ().WithMessage ("Informe o código de segurança.");

            RuleFor (r => r.Document)
                .NotNull ().NotEmpty ().WithMessage ("Cpf é obrigatório.");
        }
    }
}