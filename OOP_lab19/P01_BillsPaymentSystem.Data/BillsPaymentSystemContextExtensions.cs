namespace P01_BillsPaymentSystem.Data
{
    using P01_BillsPaymentSystem.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class BillsPaymentSystemContextExtensions
    {
        public static void Seed(this BillsPaymentSystemContext context)
        {
            var users = CreateUsers();
            var bankAccounts = CreateBankAccounts();
            var creditCards = CreateCreditCards();

            context.Users.AddRange(users);
            context.BankAccounts.AddRange(bankAccounts);
            context.CreditCards.AddRange(creditCards);

            context.SaveChanges();

            var paymentMethods = CreatePaymentMethods(users, bankAccounts, creditCards);

            context.PaymentMethods.AddRange(paymentMethods);
            context.SaveChanges();
        }

        private static List<User> CreateUsers()
        {
            return new List<User>
            {
                new User { FirstName = "Olena", LastName = "Koval", Email = "olena.koval@test.com", Password = "pass1" },
                new User { FirstName = "Andrew", LastName = "Melnyk", Email = "andriy_m@test.com", Password = "pass2" },
                new User { FirstName = "Svitlana", LastName = "DelRey", Email = "svetlana.p@test.com", Password = "pass3" },
                new User { FirstName = "Ihor", LastName = "Savchuk", Email = "ihor_s@test.com", Password = "pass4" }
            };
        }

        private static List<BankAccount> CreateBankAccounts()
        {
            return new List<BankAccount>
            {
                new BankAccount { Balance = 15000.50m, BankName = "PryvatBank", SwiftCode = "PBANUA2X" },
                new BankAccount { Balance = 34500.00m, BankName = "Oschad", SwiftCode = "OSCADUAX" },
                new BankAccount { Balance = 1200.75m, BankName = "Ukresimbank", SwiftCode = "EXBSUA22" }
            };
        }

        private static List<CreditCard> CreateCreditCards()
        {
            return new List<CreditCard>
            {
                new CreditCard { Limit = 5000.00m, MoneyOwed = 1250.00m, ExpirationDate = new DateTime(2027, 10, 01) },
                new CreditCard { Limit = 10000.00m, MoneyOwed = 0.00m, ExpirationDate = new DateTime(2028, 05, 01) },
                new CreditCard { Limit = 7500.00m, MoneyOwed = 5000.00m, ExpirationDate = new DateTime(2026, 12, 01) }
            };
        }

        private static List<PaymentMethod> CreatePaymentMethods(List<User> users, List<BankAccount> accounts, List<CreditCard> cards)
        {
            var paymentMethods = new List<PaymentMethod>();

            paymentMethods.Add(new PaymentMethod
            {
                UserId = users[0].UserId,
                Type = PaymentType.BankAccount,
                BankAccountId = accounts[0].BankAccountId,
                CreditCardId = null
            });
            paymentMethods.Add(new PaymentMethod
            {
                UserId = users[0].UserId,
                Type = PaymentType.CreditCard,
                BankAccountId = null,
                CreditCardId = cards[0].CreditCardId
            });

            paymentMethods.Add(new PaymentMethod
            {
                UserId = users[1].UserId,
                Type = PaymentType.BankAccount,
                BankAccountId = accounts[1].BankAccountId,
                CreditCardId = null
            });

            paymentMethods.Add(new PaymentMethod
            {
                UserId = users[2].UserId,
                Type = PaymentType.CreditCard,
                BankAccountId = null,
                CreditCardId = cards[1].CreditCardId
            });

            paymentMethods.Add(new PaymentMethod
            {
                UserId = users[3].UserId,
                Type = PaymentType.BankAccount,
                BankAccountId = accounts[2].BankAccountId,
                CreditCardId = null
            });
            paymentMethods.Add(new PaymentMethod
            {
                UserId = users[3].UserId,
                Type = PaymentType.CreditCard,
                BankAccountId = null,
                CreditCardId = cards[2].CreditCardId
            });

            return paymentMethods;
        }
    }
}