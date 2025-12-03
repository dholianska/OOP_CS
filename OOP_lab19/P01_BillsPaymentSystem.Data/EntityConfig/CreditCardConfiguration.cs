namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_BillsPaymentSystem.Models;

    public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder.HasKey(cc => cc.CreditCardId);

            builder.Ignore(cc => cc.LimitLeft);

            builder.HasOne(cc => cc.PaymentMethod)
                .WithOne(pm => pm.CreditCard)
                .HasForeignKey<PaymentMethod>(pm => pm.CreditCardId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}