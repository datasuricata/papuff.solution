using System.ComponentModel.DataAnnotations;

namespace papuff.domain.Core.Enums {
    public enum BuildingType {

        [Display(Description = "Sem Definição")]
        Undefined = 0,

        [Display(Description = "Casa")]
        House = 1,

        [Display(Description = "Sobrado")]
        Townhouse = 2,

        [Display(Description = "Apartamento")]
        Apartment = 3,

        [Display(Description = "Comercial")]
        Commercial = 4,
    }
}
