using System.ComponentModel.DataAnnotations;

namespace papuff.domain.Core.Enums {
    public enum VisibilityType {
        [Display(Description = "Publico")]
        Public = 0,

        [Display(Description = "Privado")]
        Private = 1,
    }
}
