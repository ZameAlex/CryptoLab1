using System;

namespace Matrix
{
    public class SquareMatrix
    {
        public int Count { get; protected set; }
        public int[,] MatrixElements { get; protected set; }
        public int Determinant { get; protected set; }

        #region Constructors
        public SquareMatrix(int n)
        {
            Count = n;
            MatrixElements = new int[Count, Count];
        }

        public SquareMatrix(int n, int min, int max)
        {
            Count = n;
            MatrixElements = new int[Count, Count];
            RandomInitialization(min, max);
        }


        public SquareMatrix(int[,] elements)
        {
            if (elements.GetLength(0) == elements.GetLength(1))
            {
                Count = elements.GetLength(0);
                MatrixElements = new int[Count, Count];
                Array.Copy(elements, MatrixElements, elements.Length);
            }
            else
                throw new ArgumentException("Matrix is not square!");
        }
        #endregion Constructors


        #region AdditionalMethods
        public void RandomInitialization(int min, int max)
        {
            Random randomizer = new Random((int)DateTime.Now.Ticks);
            for (int raw = 0; raw < Count; raw++)
            {
                for (int column = 0; column < Count; column++)
                {
                    MatrixElements[raw, column] = randomizer.Next(min, max);
                }
            }
        }

        public int this[int raw, int column] => MatrixElements[raw, column];
        #endregion AdditionalMethods


        #region PrivateMethods
        private void CountDeterminant()
        {

        }
        #endregion PrivateMethods
        #region OverloadOperations
        public static SquareMatrix operator +(SquareMatrix first, SquareMatrix second)
        {
            var length = first.Count;
            var resultMatrixElements = new int[length, length];
            for (int raw = 0; raw < length; raw++)
            {
                for (int column = 0; column < length; column++)
                    resultMatrixElements[raw, column] = first[raw, column] + second[raw, column];
            }
            return new SquareMatrix(resultMatrixElements);
        }

        public static SquareMatrix operator /(SquareMatrix target, int diviser)
        {
            int[,] resultMatrixElements = new int[target.Count, target.Count];
            for (int raw = 0; raw < target.Count; raw++)
            {
                for (int column = 0; column < target.Count; column++)
                    resultMatrixElements[raw, column] /= diviser;
            }
            return new SquareMatrix(resultMatrixElements);
        }

        public static SquareMatrix operator %(SquareMatrix target, int module)
        {
            int[,] resultMatrixElements = new int[target.Count, target.Count];
            for (int raw = 0; raw < target.Count; raw++)
            {
                for (int column = 0; column < target.Count; column++)
                    resultMatrixElements[raw, column] %= module;
            }
            return new SquareMatrix(resultMatrixElements);
        }

        public static SquareMatrix operator*(SquareMatrix target,int multiplexor)
        {
            int[,] resultMatrixElements = new int[target.Count, target.Count];
            for (int raw = 0; raw < target.Count; raw++)
            {
                for (int column = 0; column < target.Count; column++)
                    resultMatrixElements[raw, column] *= multiplexor;
            }
            return new SquareMatrix(resultMatrixElements);
        }

        public static SquareMatrix operator *(SquareMatrix matrix, int[] vector)
        {
            int[,] resultMatrixElements = new int[matrix.Count, matrix.Count];
            for (int raw = 0; raw < matrix.Count; raw++)
            {
                for (int column = 0; column < matrix.Count; column++)

            }
            return new SquareMatrix(resultMatrixElements);
        }

        #endregion OverloadOperations

    }
}
