using System;
using System.Collections.Generic;
using Matrix;
using Hill;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            HillCipher hill = new HillCipher(26);
            var encodedMessage = hill.Encode("ACT");
            Console.WriteLine(encodedMessage);
            var decodedMessage = hill.Decode(encodedMessage);
            Console.WriteLine(decodedMessage);
            Console.WriteLine(decodedMessage);
        }
    }
}
