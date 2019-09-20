using FluentValidation;
using papuff.domain.Core.Sieges;

namespace papuff.services.Validators.Core.Sieges {
    public class TicketValidator : AbstractValidator<Ticket> {
        public TicketValidator() {
            RuleFor(r => r.SiegeId)
                .NotEmpty().NotNull().WithMessage("Vincular a um cerco é obrigatório.");

            RuleFor(r => r.Hash)
                .NotEmpty().NotNull().WithMessage("Informar um hash é obrigatório.");

            RuleFor(r => r.DateDue)
                .Must(d => d > 0).WithMessage("Data de expiração deve ter ao menos um dia.");
        }
    }
}