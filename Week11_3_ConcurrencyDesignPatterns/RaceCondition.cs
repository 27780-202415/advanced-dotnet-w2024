using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week11_3_ConcurrencyDesignPatterns
{
    public class RaceCondition
    {
        public static async void SimulateRaceCondition()
        {
            BankAccount bankAccount1 = new BankAccount();
            BankAccount bankAccount2 = new BankAccount();
            BankAccount bankAccount3 = new BankAccount();
            BankAccount bankAccount4 = new BankAccount();


            //Single Thread
            Console.WriteLine("Test using a single thread...");
            bankAccount1.Deposit(500);
            bankAccount1.Withdraw(250);
            bankAccount1.Withdraw(400);

            Console.WriteLine("***************");

            Console.WriteLine("Test using multi-thread...");
            bankAccount2.Deposit(500);
            Parallel.Invoke(() =>
            {
                bankAccount2.Withdraw(250);

            }, () =>
            {
                bankAccount2.Withdraw(400);
            });


            Console.WriteLine("***************");

            Console.WriteLine("Test using multi-thread with lock...");
            bankAccount3.Deposit(500);
            Parallel.Invoke(() =>
            {
                bankAccount3.WithdrawWithLock(250);

            }, () =>
            {
                bankAccount3.WithdrawWithLock(400);
            });


            Console.WriteLine("***************");

            Console.WriteLine("Test using multi-thread with Monitor...");
            bankAccount4.Deposit(500);
            Parallel.Invoke(() =>
            {
                bankAccount4.WithdrawWithMonitor(250);

            }, () =>
            {
                bankAccount4.WithdrawWithMonitor(400);
            });

        }
    }


}
