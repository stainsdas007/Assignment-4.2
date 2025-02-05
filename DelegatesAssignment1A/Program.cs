using System;
using System.Collections.Generic;

namespace DelegateAssignment1A
{
    // Represents a bank account with deposit, withdrawal, and balance check functionality.
    public class BankAccount
    {
        public int AccountNumber { get; } // Read-only account number
        public string Name { get; } // Account holder's name
        public string PhoneNumber { get; } // Contact number
        public decimal Balance { get; private set; } // Balance (modifiable only within this class)

        // Constructor to initialize the bank account
        public BankAccount(int accountNumber, string name, string phoneNumber, decimal initialBalance = 0)
        {
            AccountNumber = accountNumber;
            Name = name;
            PhoneNumber = phoneNumber;
            Balance = initialBalance;
        }

        // Deposits money into the account
        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                Console.WriteLine($"Deposited {amount:C} into account {AccountNumber} ({Name}). New balance: {Balance:C}");
            }
            else
            {
                Console.WriteLine("Invalid deposit amount.");
            }
        }

        // Withdraws money from the account if there are sufficient funds
        public void Withdraw(decimal amount)
        {
            if (amount > 0 && Balance >= amount)
            {
                Balance -= amount;
                Console.WriteLine($"Withdrew {amount:C} from account {AccountNumber} ({Name}). New balance: {Balance:C}");
            }
            else
            {
                Console.WriteLine("Insufficient funds or invalid amount.");
            }
        }

        // Displays the current account balance
        public void CheckBalance()
        {
            Console.WriteLine($"Account {AccountNumber} ({Name}, {PhoneNumber}) balance: {Balance:C}");
        }
    }

    // Manages multiple bank accounts and transactions
    public class Bank
    {
        private Dictionary<int, BankAccount> accounts = new Dictionary<int, BankAccount>(); // Stores bank accounts

        // Delegate for handling deposit and withdrawal transactions
        public delegate void TransactionDelegate(BankAccount account, decimal amount);

        // Creates a new bank account
        public void CreateAccount(int accountNumber, string name, string phoneNumber, decimal initialBalance = 0)
        {
            if (!accounts.ContainsKey(accountNumber))
            {
                accounts[accountNumber] = new BankAccount(accountNumber, name, phoneNumber, initialBalance);
                Console.WriteLine($"Account {accountNumber} created successfully for {name} ({phoneNumber}).");
            }
            else
            {
                Console.WriteLine("Account number already exists.");
            }
        }

        // Performs a transaction (Deposit or Withdraw) using a delegate
        public void PerformTransaction(int accountNumber, decimal amount, TransactionDelegate transaction)
        {
            if (accounts.TryGetValue(accountNumber, out BankAccount account))
            {
                Console.WriteLine($"Performing transaction for {account.Name} ({account.AccountNumber}).");
                transaction(account, amount); // Calls the method assigned to the delegate (Deposit/Withdraw)
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        // Checks the balance of a given account
        public void CheckBalance(int accountNumber)
        {
            if (accounts.TryGetValue(accountNumber, out BankAccount account))
            {
                account.CheckBalance();
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }
    }

    // Main class to handle user interactions
    public class DelegateAssignment1A
    {
        public static void Main()
        {
            Bank bank = new Bank();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nBanking System Menu:");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Check Balance");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine(); // Read user input

                switch (choice)
                {
                    case "1":
                        try
                        {
                            Console.Write("Enter account number: ");
                            int accNum = int.Parse(Console.ReadLine());
                            Console.Write("Enter name: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter phone number: ");
                            string phoneNumber = Console.ReadLine();
                            Console.Write("Enter initial amound to start accound: ");
                            decimal initBalance = decimal.Parse(Console.ReadLine());
                            bank.CreateAccount(accNum, name, phoneNumber, initBalance);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Error: Please enter valid inputs.");
                        }
                        break;

                    case "2":
                        try
                        {
                            Console.Write("Enter account number: ");
                            int depAcc = int.Parse(Console.ReadLine());
                            Console.Write("Enter deposit amount: ");
                            decimal depAmount = decimal.Parse(Console.ReadLine());
                            bank.PerformTransaction(depAcc, depAmount, (acc, amt) => acc.Deposit(amt));
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Error: Please enter a valid amount.");
                        }
                        break;

                    case "3":
                        try
                        {
                            Console.Write("Enter account number: ");
                            int withAcc = int.Parse(Console.ReadLine());
                            Console.Write("Enter withdrawal amount: ");
                            decimal withAmount = decimal.Parse(Console.ReadLine());
                            bank.PerformTransaction(withAcc, withAmount, (acc, amt) => acc.Withdraw(amt));
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Error: Please enter a valid amount.");
                        }
                        break;

                    case "4":
                        try
                        {
                            Console.Write("Enter account number: ");
                            int balAcc = int.Parse(Console.ReadLine());
                            bank.CheckBalance(balAcc);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Error: Please enter a valid account number.");
                        }
                        break;

                    case "5":
                        exit = true;
                        Console.WriteLine("Exiting banking system. Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
