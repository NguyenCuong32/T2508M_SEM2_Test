using System;
using BankSystem.Domain.Models;
using BankSystem.Domain.Interfaces;

namespace BankSystem.UI
{
    /// <summary>
    /// Manages the user interface and interactions via Console.
    /// Follows Single Responsibility Principle (SRP).
    /// </summary>
    public class ConsoleInterface
    {
        public void Start()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool exit = false;

            while (!exit)
            {
                RenderHeader("BANK ACCOUNT SYSTEM");
                Console.WriteLine("1. Create Normal Account (VND)");
                Console.WriteLine("2. Create Exchange Account (USD -> VND)");
                Console.WriteLine("3. Exit");
                Console.WriteLine();
                Console.Write("Select an option (1-3): ");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        CreateNormalAccountFlow();
                        break;
                    case "2":
                        CreateExchangeAccountFlow();
                        break;
                    case "3":
                        exit = true;
                        RenderFooter("Thank you for using Bank Account System!");
                        break;
                    default:
                        RenderError("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void CreateNormalAccountFlow()
        {
            RenderHeader("CREATE NORMAL ACCOUNT");
            
            decimal initialBalance = ReadDecimal("Enter initial balance (VND) [Try 25000000]: ", minValue: 0);

            BankAccount account = new NormalAccount(initialBalance);
            RenderSuccess($"Normal Account created successfully with balance: {BankAccount.FormatVnd(account.Balance)} đ");
            
            ManageAccountFlow(account);
        }

        private void CreateExchangeAccountFlow()
        {
            RenderHeader("CREATE EXCHANGE ACCOUNT");

            decimal amountUsd = ReadDecimal("Enter amount in USD [Try 1000]: ", minValue: 0);
            decimal exchangeRate = ReadDecimal("Enter exchange rate (USD to VND) [Try 25000]: ", minValue: 1);

            BankAccount account = new ExchangeAccount(amountUsd, exchangeRate);
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n[Calculation: {amountUsd:N2} USD × {exchangeRate:N0} = {BankAccount.FormatVnd(account.Balance)} VND]");
            Console.ResetColor();

            RenderSuccess($"Exchange Account created successfully with calculated balance: {BankAccount.FormatVnd(account.Balance)} đ");

            ManageAccountFlow(account);
        }

        private void ManageAccountFlow(BankAccount account)
        {
            bool back = false;
            while (!back)
            {
                RenderHeader($"MANAGE {account.AccountType.ToUpper()}");
                Console.WriteLine("1. Check Balance");
                Console.WriteLine("2. Make Bank Transfer");
                Console.WriteLine("3. Back to Main Menu");
                Console.WriteLine();
                Console.Write("Select an option (1-3): ");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n--- Check Balance ---");
                        account.CheckBalance();
                        Console.ResetColor();
                        PressAnyKey();
                        break;

                    case "2":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n--- Bank Transfer ---");
                        Console.ResetColor();
                        decimal transferAmount = ReadDecimal("Enter amount to transfer (VND) [Try 1000000]: ", minValue: 0.01m);

                        if (account.Transfer(transferAmount, out string message))
                        {
                            RenderSuccess(message);
                        }
                        else
                        {
                            RenderError(message);
                        }
                        PressAnyKey();
                        break;

                    case "3":
                        back = true;
                        break;

                    default:
                        RenderError("Invalid option. Please try again.");
                        break;
                }
            }
        }

        #region Helpers for console formatting and reading inputs

        private void RenderHeader(string title)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("====================================================");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"                 {title}                 ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("====================================================");
            Console.ResetColor();
            Console.WriteLine();
        }

        private void RenderSuccess(string message)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[SUCCESS] {message}");
            Console.ResetColor();
        }

        private void RenderError(string message)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] {message}");
            Console.ResetColor();
            PressAnyKey();
        }

        private void RenderFooter(string message)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("====================================================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($" {message} ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("====================================================");
            Console.ResetColor();
        }

        private decimal ReadDecimal(string prompt, decimal minValue)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (decimal.TryParse(input, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal val) && val >= minValue)
                {
                    return val;
                }
                // Try parsing with comma/dot swap just in case local settings are different
                if (decimal.TryParse(input?.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out val) && val >= minValue)
                {
                    return val;
                }
                
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Invalid numeric input. Please enter a value >= {minValue}.");
                Console.ResetColor();
            }
        }

        private void PressAnyKey()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }

        #endregion
    }
}
