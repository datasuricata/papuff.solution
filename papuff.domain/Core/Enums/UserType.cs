using System.ComponentModel.DataAnnotations;

namespace papuff.domain.Core.Enums {
    public enum UserType {
        [Display(Description = "Cliente")]
        Customer = 1,

        [Display(Description = "Empresa")]
        Enterprise = 2,

        [Display(Description = "Operador")]
        Operator = 3,

        [Display(Description = "Ninja das Sombras")]
        Root = 999,
    }
}
