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
            Console.WriteLine("Write message, please");
            var encodedMessage = hill.Encode(Console.ReadLine());
            Console.WriteLine($"Key: {hill.GetKey}");
            Console.WriteLine($"Encoded message:\n{encodedMessage}");
            var decodedMessage = hill.Decode(encodedMessage);
            Console.WriteLine($"Decoded message:\n{decodedMessage}");
            Console.ReadKey();
        }
    }
}
