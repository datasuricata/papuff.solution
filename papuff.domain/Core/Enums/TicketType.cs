using System.ComponentModel.DataAnnotations;

namespace papuff.domain.Core.Enums {
    public enum TicketType {
        [Display(Description = "Simples")]
        Simple = 0,

        [Display(Description = "Vip")]
        Vip = 1,

        [Display(Description = "Plateia")]
        Audience = 2,

        [Display(Description = "Bastidores")]
        Backstage = 3,

        [Display(Description = "Camarote")]
        Cabin = 4,
    }
}