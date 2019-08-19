using System.ComponentModel.DataAnnotations;

namespace papuff.domain.Core.Enums {

    public enum PaymentType {
        [Display(Description = "Não Definido")]
        Uninformed = 0,

        [Display(Description = "Pagamento com Crédito")]
        Credit = 1,

        [Display(Description = "Pagamento com Débito")]
        Debit = 2,

        [Display(Description = "Pagamento com Boleto Bancário")]
        BankSlip = 3
    }
}
