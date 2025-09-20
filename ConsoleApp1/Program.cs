using System;
using System.Text;

class Program
{
    static unsafe void Main()
    {
        double value = 0;
        byte* p = (byte*)&value;

        p[0] = 1;

        char c = 'ƍ';
        byte[] cBytes = BitConverter.GetBytes(c);
        p[1] = cBytes[0];
        p[2] = cBytes[1];

        p[3] = (byte)'A';

        byte[] c2 = BitConverter.GetBytes('ƍ');
        p[4] = c2[0];

        int num = 2;
        byte[] intBytes = BitConverter.GetBytes(num);
        for (int i = 0; i < 4; i++)
            p[4 + i] = intBytes[i];

        p[7] = 3;

        Console.WriteLine("Double value: " + value);

        Console.Write("Memory bytes: ");
        for (int i = 0; i < sizeof(double); i++)
            Console.Write(p[i] + " ");
    }
}
