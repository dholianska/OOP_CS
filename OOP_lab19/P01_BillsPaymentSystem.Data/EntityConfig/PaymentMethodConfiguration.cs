namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_BillsPaymentSystem.Models;

    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(pm => pm.Id);

            builder.HasIndex(pm => new { pm.UserId, pm.BankAccountId, pm.CreditCardId })
                .IsUnique(true);

            builder.ToTable(t => t.HasCheckConstraint("CH_PaymentMethod_XOR_Check",
                "([BankAccountId] IS NULL AND [CreditCardId] IS NOT NULL) OR ([BankAccountId] IS NOT NULL AND [CreditCardId] IS NULL)"));
        }
    }
}