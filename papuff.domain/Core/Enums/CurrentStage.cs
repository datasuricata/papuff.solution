using System.ComponentModel.DataAnnotations;

namespace papuff.domain.Core.Enums {
    public enum CurrentStage {
        [Display(Description = "Pendente Aprovação")]
        Pending = 1,

        [Display(Description = "Aprovado")]
        Aproved = 2,

        [Display(Description = "Recusado")]
        Recused = 3,

        [Display(Description = "Bloqueado")]
        Blocked = 4
    }
}
