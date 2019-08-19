using FluentValidation;
using papuff.domain.Core.Enums;
using papuff.domain.Core.Users;
using System.Linq;

namespace papuff.services.Validators.Security {
    public class SecurityValidator : AbstractValidator<User> {

        public SecurityValidator() {

            RuleFor(r => r.General)
                .Must(x => new[] { CurrentStage.Aproved, CurrentStage.Pending }.Contains(x.Stage))
                .WithMessage("Usuário sem acesso, contate o suporte.");
        }
    }
}
