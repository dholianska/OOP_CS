namespace P01_BillsPaymentSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public enum PaymentType
    {
        BankAccount = 1,
        CreditCard = 2
    }
    public class PaymentMethod
    {
        public int Id { get; set; }

        [Required]
        public PaymentType Type { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int? BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }

        public int? CreditCardId { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}