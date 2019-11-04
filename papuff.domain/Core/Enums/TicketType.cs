using System.ComponentModel.DataAnnotations;

namespace papuff.domain.Core.Enums {
    public enum TicketType {
        [Display(Description = "Simples")]
        Simple = 1,

        [Display(Description = "Vip")]
        Vip = 2,

        [Display(Description = "Plateia")]
        Audience = 3,

        [Display(Description = "Bastidores")]
        Backstage = 4,

        [Display(Description = "Camarote")]
        Cabin = 5,
    }
}