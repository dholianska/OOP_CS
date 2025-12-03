namespace P01_BillsPaymentSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CreditCard
    {
        public int CreditCardId { get; set; }

        public decimal Limit { get; set; }

        public decimal MoneyOwed { get; set; }

        [NotMapped]
        public decimal LimitLeft => this.Limit - this.MoneyOwed;

        public DateTime ExpirationDate { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("debt repayment must be positive");
            }
            this.MoneyOwed -= amount;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("withdrawal amount must be positive");
            }

            if (this.LimitLeft >= amount)
            {
                this.MoneyOwed += amount;
                return true;
            }

            return false;
        }
    }
}