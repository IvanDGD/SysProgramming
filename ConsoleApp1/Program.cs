using System.Net.Sockets;

class Bank
{
    private static int balance = 1000;
    private static object locker = new object();

    public void WithdrawMoney(int withdraw)
    {
        lock (locker)
        {
            if (withdraw <= balance)
            {
                Console.WriteLine($"From balance was withdraw {withdraw}$");
                balance -= withdraw;
            }
            else
            {
                Console.WriteLine("Not enough money");
            }
        }
    }
}

class Program
{
    static object locker = new object();
    static int sumResult = 0;
    static double avgResult = 0;
    static void Main()
    {
        #region Task
        //Bank card1 = new Bank();
        //Bank card2 = new Bank();

        //card1.WithdrawMoney(500);
        //card2.WithdrawMoney(750);
        #endregion

        #region additionalTask1
        int[] arr = { 1, 51, 3, 15, 64, 9, 23, 47, 89, 75 };

        Thread sumThread = new Thread(() => ArraySum(arr));
        Thread avgThread = new Thread(() => ArrayAvg(arr));

        sumThread.Start();
        sumThread.Join();

        avgThread.Start();
        avgThread.Join();
        #endregion
    }

    static void ArraySum(int[] array)
    {
        lock (locker)
        {
            int sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }
            sumResult = sum;
            Console.WriteLine(sumResult);
        }
    }

    static void ArrayAvg(int[] array)
    {
        lock (locker)
        {
            int sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum += array[i];
            }
            avgResult = (double)sum / array.Length;
            Console.WriteLine(avgResult);
        }
    }
}
