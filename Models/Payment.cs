using System.ComponentModel.DataAnnotations;

namespace VirvisShopFinal.Models
{
    public class Payment
    {

        
        [Required(ErrorMessage = "El nombre del titular es obligatorio")]
        public string cardHolderName { get; set; }

        [Required(ErrorMessage = "El número de tarjeta es obligatorio")]
        [CreditCard(ErrorMessage = "Número de tarjeta inválido")]
        public string cardNumber { get; set; }

        [Required(ErrorMessage = "La fecha de expiración es obligatoria")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/([0-9]{2})$", ErrorMessage = "Formato de fecha inválido (MM/AA)")]
        public string expriryDate { get; set; }

        [Required(ErrorMessage = "El CVV es obligatorio")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "El CVV debe tener 3 dígitos")]
        public string cvv { get; set; }

        

        //public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;


    }
}
