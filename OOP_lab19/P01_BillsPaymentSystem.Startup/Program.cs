namespace P01_BillsPaymentSystem.Startup
{
    using P01_BillsPaymentSystem.Data;
    using P01_BillsPaymentSystem.Models;
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new BillsPaymentSystemContext())
            {
                context.Database.EnsureCreated();
                // context.Seed();

                Console.Write("enter ID of user for the details: ");
                if (int.TryParse(Console.ReadLine(), out int userId))
                {
                    PrintUserDetailsService(context, userId);
                }
                else
                {
                    Console.WriteLine("incorrect ID");
                }
                Console.WriteLine();

                PayBills(context, 1, 30000.00m); // недостатньо
                // PayBills(context, 1, 16000.00m);
            }
        }

        private static void PrintUserDetailsService(BillsPaymentSystemContext context, int userId)
        {
            var user = context.Users
                .Where(u => u.UserId == userId)
                .Include(u => u.PaymentMethods)
                    .ThenInclude(pm => pm.BankAccount)
                .Include(u => u.PaymentMethods)
                    .ThenInclude(pm => pm.CreditCard)
                .FirstOrDefault();

            if (user == null)
            {
                Console.WriteLine($"User with id {userId} not found!");
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"user: {user.FirstName} {user.LastName}");
            Console.WriteLine(new string('-', 30));

            var bankAccounts = user.PaymentMethods
                .Where(pm => pm.Type == PaymentType.BankAccount && pm.BankAccount != null)
                .Select(pm => pm.BankAccount)
                .OrderBy(ba => ba.BankAccountId)
                .ToList();

            Console.WriteLine("bank accounts:");
            if (bankAccounts.Any())
            {
                foreach (var ba in bankAccounts)
                {
                    Console.WriteLine($"--- ID: {ba.BankAccountId}");
                    Console.WriteLine($"--- balance: {ba.Balance:F2}");
                    Console.WriteLine($"--- bank: {ba.BankName}");
                    Console.WriteLine($"--- SWIFT: {ba.SwiftCode}");
                }
            }
            else
            {
                Console.WriteLine("--- there is no bank accounts");
            }
            Console.WriteLine();

            var creditCards = user.PaymentMethods
                .Where(pm => pm.Type == PaymentType.CreditCard && pm.CreditCard != null)
                .Select(pm => pm.CreditCard)
                .OrderBy(cc => cc.CreditCardId)
                .ToList();

            Console.WriteLine("credit cards:");
            if (creditCards.Any())
            {
                foreach (var cc in creditCards)
                {
                    Console.WriteLine($"--- ID: {cc.CreditCardId}");
                    Console.WriteLine($"--- limit: {cc.Limit:F2}");
                    Console.WriteLine($"--- money owed: {cc.MoneyOwed:F2}");
                    Console.WriteLine($"--- limit left: {cc.LimitLeft:F2}"); // Обчислювана властивість
                    Console.WriteLine($"--- expiration date: {cc.ExpirationDate:yyyy/MM}");
                }
            }
            else
            {
                Console.WriteLine("--- there is no credit cards");
            }
        }

        public static void PayBills(BillsPaymentSystemContext context, int userId, decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("payment amount must be positive");
                return;
            }

            var user = context.Users
                .Where(u => u.UserId == userId)
                .Include(u => u.PaymentMethods)
                    .ThenInclude(pm => pm.BankAccount)
                .Include(u => u.PaymentMethods)
                    .ThenInclude(pm => pm.CreditCard)
                .FirstOrDefault();

            if (user == null)
            {
                Console.WriteLine($"user with id {userId} not found!");
                return;
            }

            var paymentMethods = user.PaymentMethods
                .OrderBy(pm => pm.Type)
                .ThenBy(pm => pm.BankAccountId)
                .ThenBy(pm => pm.CreditCardId)
                .ToList();

            decimal totalAvailable = 0;
            foreach (var pm in paymentMethods)
            {
                if (pm.BankAccount != null)
                {
                    totalAvailable += pm.BankAccount.Balance;
                }
                if (pm.CreditCard != null)
                {
                    totalAvailable += pm.CreditCard.LimitLeft;
                }
            }

            if (totalAvailable < amount)
            {
                Console.WriteLine("total available balance (including limits) is less than the required amount");
                return;
            }

            // 4. Логіка списання
            decimal remainingAmount = amount;
            Console.WriteLine($"\nattempt to pay with the amount: {amount:F2}...");

            foreach (var pm in paymentMethods)
            {
                if (remainingAmount <= 0) break;

                bool success = false;

                if (pm.BankAccount != null)
                {
                    decimal available = pm.BankAccount.Balance;
                    decimal withdrawAmount = Math.Min(available, remainingAmount);

                    if (pm.BankAccount.Withdraw(withdrawAmount))
                    {
                        remainingAmount -= withdrawAmount;
                        Console.WriteLine($"- withdrawn {withdrawAmount:F2} from BankAccount ID {pm.BankAccountId}. left: {remainingAmount:F2}");
                        success = true;
                    }
                }

                if (pm.CreditCard != null && remainingAmount > 0)
                {
                    decimal availableLimit = pm.CreditCard.LimitLeft;
                    decimal withdrawAmount = Math.Min(availableLimit, remainingAmount);

                    if (pm.CreditCard.Withdraw(withdrawAmount))
                    {
                        remainingAmount -= withdrawAmount;
                        Console.WriteLine($"-> Знято {withdrawAmount:F2} з CreditCard ID {pm.CreditCardId}. Залишилось: {remainingAmount:F2}");
                        success = true;
                    }
                }
            }

            if (remainingAmount == 0)
            {
                context.SaveChanges();
                Console.WriteLine("\nbill payment completed successfully!");
            }
        }

    }

}