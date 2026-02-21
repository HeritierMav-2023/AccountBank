using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBank
{
    internal class Program
    {
        
            static void Main()
            {
                var account = new Account();
                var printer = new StatementPrinter();

                account.Deposit(1000);
                account.Deposit(500);
                account.Withdraw(200);

                printer.Print(account.GetStatement());
            }
        
    }

    /// <summary>
    ///  Represents a single transaction in the bank account, including the date, amount, and resulting balance after the transaction.
    /// </summary>
    public class Transaction
    {
        public DateTime Date { get; }
        public int Amount { get; }
        public int BalanceAfter { get; }

        public Transaction(DateTime date, int amount, int balanceAfter)
        {
            Date = date;
            Amount = amount;
            BalanceAfter = balanceAfter;
        }
    }

    /// <summary>
    /// Represents a simple bank account that allows deposits, withdrawals, and provides a statement of transactions.
    /// </summary>
    public class Account
    {
        private readonly List<Transaction> _transactions = new List<Transaction>();
        private int _balance = 0;

        public void Deposit(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be positive.");

            _balance += amount;
            _transactions.Add(new Transaction(DateTime.Now, amount, _balance));
        }

        public void Withdraw(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be positive.");

            if (amount > _balance)
                throw new InvalidOperationException("Insufficient funds.");

            _balance -= amount;
            _transactions.Add(new Transaction(DateTime.Now, -amount, _balance));
        }

        public IEnumerable<Transaction> GetStatement()
        {
            return _transactions;
        }
    }

    /// <summary>
    ///  Responsible for printing the account statement in a readable format, showing the date, amount, and balance after each transaction.
    /// </summary>
    public class StatementPrinter
    {
        public void Print(IEnumerable<Transaction> transactions)
        {
            Console.WriteLine("DATE\t\tAMOUNT\tBALANCE");

            foreach (var t in transactions)
            {
                Console.WriteLine($"{t.Date:dd/MM/yyyy}\t{t.Amount}\t{t.BalanceAfter}");
            }
        }
    }


}
