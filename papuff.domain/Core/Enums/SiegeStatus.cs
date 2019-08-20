using System.ComponentModel.DataAnnotations;

namespace papuff.domain.Core.Enums {
    public enum SiegeStatus {

        [Display(Description = "Invisivel")]
        Invisible = 0,

        [Display(Description = "Disponivel")]
        Available = 1,

        [Display(Description = "Aberto")]
        Opened = 2,

        [Display(Description = "Fechado")]
        Closed = 3,
    }
}
