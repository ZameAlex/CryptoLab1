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
        #region Constructors
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
        #endregion Constructors

        public string GetKey
        {
            get
            {
                StringBuilder key = new StringBuilder();
                for (int row = 0; row < Key.Count; row++)
                    for (int column = 0; column < Key.Count; column++)
                        key.Append(alphabet[Key[row, column]]);
                return key.ToString();
            }
        }

        public string Encode(string message)
        {
            if (String.IsNullOrWhiteSpace(message))
                message = "If you can keep your head when all about you Are losing theirs and blaming it on you, If you can trust yourself when all men doubt you, But make allowance for their doubting too";
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
