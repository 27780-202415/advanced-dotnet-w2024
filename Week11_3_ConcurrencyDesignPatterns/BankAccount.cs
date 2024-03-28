using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week11_3_ConcurrencyDesignPatterns
{
    public class BankAccount
    {
        // acts as a synchronization block/handle that is acquired and released
        // as needed based on access to a critical section of code
        private static readonly object syncLock = new object();

        public double Balance { get; private set; }

        public void Deposit(double amount)
        {
            this.Balance += amount;
            Console.WriteLine($"Deposit {amount}, Current Balance: {Balance}");
        }

        public void Withdraw(double amount)
        {
           
            Random r = new Random();
            int randomLoad = r.Next(0, 1000);

            if (amount > this.Balance)
            {

                Console.WriteLine($"Cannot withdraw {amount} dollars, there is not enough money in the account ({Balance})");
            }
            else
            {
                double bal = Balance;
                Task.Delay(randomLoad);
                this.Balance = bal - amount;
                Console.WriteLine($"Withdraw {amount}, Current Balance: {Balance}");
            }

        }

        public void WithdrawWithLock(double amount)
        {
            lock (syncLock)
            {
                Withdraw(amount);
            }
        }
        public void WithdrawWithMonitor(double amount)
        {
            Monitor.Enter(syncLock);
            try
            {
               Withdraw(amount);
            }
            finally
            {
                Monitor.Exit(syncLock);
            }
        }
    }
}
