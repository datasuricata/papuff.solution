using System.ComponentModel.DataAnnotations;

namespace papuff.domain.Core.Enums {
    public enum TicketStage {
        [Display(Description = "Estoque")]
        InStock = 0,

        [Display(Description = "Aberto")]
        Opened = 1,

        [Display(Description = "Fechado")]
        Closed = 2,
    }
}