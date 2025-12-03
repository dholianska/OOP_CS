namespace P01_BillsPaymentSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BankAccount
    {
        public int BankAccountId { get; set; }
        public decimal Balance { get; set; }

        [Required]
        [MaxLength(50)]
        public string BankName { get; set; }

        [Required]
        [MaxLength(20)]
        [Column(TypeName = "VARCHAR(20)")]
        public string SwiftCode { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("it must be positive");
            }
            this.Balance += amount;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("it must be positive");
            }

            if (this.Balance >= amount)
            {
                this.Balance -= amount;
                return true;
            }

            return false;
        }
    }
}