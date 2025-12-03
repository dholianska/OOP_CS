namespace P01_BillsPaymentSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(80)]
        [Column(TypeName = "VARCHAR(80)")]
        public string Email { get; set; }

        [Required]
        [MaxLength(25)]
        [Column(TypeName = "VARCHAR(25)")]
        public string Password { get; set; }

        // Зв'язок
        public ICollection<PaymentMethod> PaymentMethods { get; set; } = new HashSet<PaymentMethod>();
    }
}