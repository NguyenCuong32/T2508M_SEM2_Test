using BankSystem;

// Demo Data
// Normal account: 25.000.000 VND
var normalAccount = new NormalAccount("ACC001", "Nguyen Van A", 25_000_000m);

// Exchange account: 1.000 USD, rate 25.000 VND/USD → balance = 25.000.000 VND
var exchangeAccount = new ExchangeAccount("ACC002", "Tran Thi B", 1_000m, 25_000m);

Console.OutputEncoding = System.Text.Encoding.UTF8;

while (true)
{
    Console.Clear();
    PrintHeader();

    Console.WriteLine("  [1] Normal Account   (VND)");
    Console.WriteLine("  [2] Exchange Account (USD → VND)");
    Console.WriteLine("  [0] Exit");
    Console.WriteLine(new string('─', 45));
    Console.Write("  Choose account type: ");

    string? choice = Console.ReadLine();

    if (choice == "0") break;

    IAccount? selectedAccount = choice switch
    {
        "1" => normalAccount,
        "2" => exchangeAccount,
        _   => null
    };

    if (selectedAccount == null)
    {
        Console.WriteLine("  Invalid choice.");
        Pause();
        continue;
    }

    // Show extra info for exchange account
    if (selectedAccount is ExchangeAccount ea)
    {
        Console.WriteLine($"\n  Exchange Rate : {CurrencyFormatter.FormatVND(ea.ExchangeRate)} / USD");
        Console.WriteLine($"  Amount (USD)  : {ea.Amount:N0} USD");
    }

    var service = new AccountService(selectedAccount);
    Console.WriteLine($"\n  Account: {service.GetAccountInfo()}");
    Console.WriteLine(new string('─', 45));

    Console.WriteLine("\n  [1] Check Balance");
    Console.WriteLine("  [2] Bank Transfer");
    Console.Write("\n  Action: ");

    string? action = Console.ReadLine();

    Console.WriteLine();

    switch (action)
    {
        case "1":
            Console.Write("  ");
            service.CheckBalance();
            break;

        case "2":
            Console.Write("  Enter transfer amount (VND): ");
            string? input = Console.ReadLine();
            if (decimal.TryParse(input?.Replace(".", "").Replace(",", ""), out decimal amount))
            {
                Console.Write("  ");
                service.MakeTransfer(amount);
            }
            else
            {
                Console.WriteLine("  Invalid amount.");
            }
            break;

        default:
            Console.WriteLine("  Invalid action.");
            break;
    }

    Pause();
}

Console.WriteLine("\n  Goodbye!\n");

// ── Helpers ────────────────────────────────────────────────────────────────
static void PrintHeader()
{
    Console.WriteLine(new string('─', 45));
    Console.WriteLine("       🏦  BANK ACCOUNT SYSTEM");
    Console.WriteLine(new string('─', 45));
    Console.WriteLine();
}

static void Pause()
{
    Console.WriteLine();
    Console.Write("  Press any key to continue...");
    Console.ReadKey();
}
