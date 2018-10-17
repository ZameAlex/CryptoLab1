using System;
using System.Collections.Generic;
using System.Text;
using Matrix;
using static System.Math;

namespace Hill
{
    public class HillCipher
    {
        private SquareMatrix Key;
        private Dictionary<int, char> alphabet;
        private Dictionary<char, int> reverseAlphabet;
        private const int module = 29;

        private HillCipher()
        {
            alphabet = new Dictionary<int, char>();
            reverseAlphabet = new Dictionary<char, int>();
            var alpha = 'a';
            for (int number = 0; number < 26; number++, alpha++)
            {
                alphabet.Add(number, alpha);
                reverseAlphabet.Add(alpha, number);
            }
            alphabet.Add(26, ' ');
            alphabet.Add(27, '.');
            alphabet.Add(28, ',');
            reverseAlphabet.Add(' ', 26);
            reverseAlphabet.Add('.', 27);
            reverseAlphabet.Add(',', 28);
        }

        public HillCipher(SquareMatrix key) : this()
        {
            Key = new SquareMatrix(key);
            try
            {
                Key.FindReverseMatrix(module);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public HillCipher(string key):this()
        {
            key = key.ToLower();
            var length = (int)Floor(Sqrt(key.Length));
            int[,] temp = new int[length, length];
            for(int number=0;number<key.Length;number++)
            {
                temp[number / length, number % length] = reverseAlphabet[key[number]];
            }
            Key = new SquareMatrix(temp);
            try
            {
                Key.FindReverseMatrix(module);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public HillCipher(int number):this()
        {
            var length = (int)Floor(Sqrt(number));
            bool hasReverse = false;
            while(hasReverse!=true)
            {
                Key = new SquareMatrix(length, 0, module-1);
                try
                {
                    Key.FindReverseMatrix(module);
                    hasReverse = true;
                }
                catch (Exception)
                {
                }
            }
        }

        public HillCipher(int[,] key):this(new SquareMatrix(key))
        { }

        public string Encode(string message)
        {
            Key %= module;
            return Translate(message.ToLower(), Key);
        }

        public string Decode(string encodedMessage)
        {
            return Translate(encodedMessage.ToLower(), Key.ReverseMatrix);
        }


        private string Translate(string message, SquareMatrix matrixKey)
        {
            var builder = new StringBuilder(message);
            while (builder.Length % matrixKey.Count != 0)
            {
                builder.Append(' ');
            }
            var vectors = new Vector[builder.Length / matrixKey.Count];
            for (int vectorNumber = 0; vectorNumber < vectors.Length; vectorNumber++)
            {
                var temp = new int[matrixKey.Count];
                for (int number = 0; number < matrixKey.Count; number++)
                {
                    temp[number] = reverseAlphabet[builder[number]];
                }
                builder.Remove(0, matrixKey.Count);
                vectors[vectorNumber] = new Vector(temp);
            }
            StringBuilder translatedMessage = new StringBuilder();
            Vector res;
            foreach (var vector in vectors)
            {
                res = (matrixKey*vector) % module;
                for (int number = 0; number < res.Count; number++)
                    translatedMessage.Append(alphabet[res[number]]);
            }
            return translatedMessage.ToString();
        }

    }
}
