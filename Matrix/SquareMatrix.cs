using System;

namespace Matrix
{
    public class SquareMatrix
    {
        public int Count { get; protected set; }
        protected int[,] MatrixElements { get; set; }
        protected SquareMatrix AdditionalMatrix { get; set; }
        public SquareMatrix ReverseMatrix { get; protected set; }
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
            for (int row = 0; row < Count; row++)
            {
                for (int column = 0; column < Count; column++)
                {
                    MatrixElements[row, column] = randomizer.Next(min, max);
                }
            }
        }

        public void Transpone()
        {
            for(int row=0;row<Count;row++)
                for(int column=0;column<Count;column++)
                {
                    var temp = this.MatrixElements[row, column];
                    this.MatrixElements[row, column] = this.MatrixElements[column, row];
                    this.MatrixElements[column, row] = temp;
                }
        }

        public int this[int row, int column] => MatrixElements[row, column];
        #endregion AdditionalMethods


        #region PrivateMethods

        private int CountDeterminant()
        {
            Determinant = 0;
            if (this.AdditionalMatrix == null)
                for (int column = 0; column < Count; column++)
                {
                    Determinant += this[0, column] * this.FindAlgerbricAddition(0, column);
                }
            else
                for (int column = 0; column < Count; column++)
                {
                    Determinant += this[0, column] * this.AdditionalMatrix[0, column];
                }
            return Determinant;
        }

        private int FindAlgerbricAddition(int row,int column)
        {
            if (this.Count == 1)
                return this[0, 0];
            var minor = new SquareMatrix(Count-1);
            var power = -1;
            if ((row + column) % 2 == 0)
                power = 1;
            var tempAddForRow = 0;
            for(int _row =0;_row<Count;_row++)
            { 
                if(row==_row)
                {
                    tempAddForRow = -1;
                    continue;
                }
                var tempAddForColumn = 0;
                for (int _column=0;_column<Count;_column++)
                {
                    if(column==_column)
                    {
                        tempAddForColumn = -1;
                        continue;
                    }
                    minor.MatrixElements[_row + tempAddForRow, _column + tempAddForColumn] = this[_row, _column];
                }
            }
            return minor.CountDeterminant()*power;
        }

        private void FindAdditionalMatrix()
        {
            var resultMatrixElements = new int[Count, Count];
            for(int row=0;row<Count;row++)
            {
                for (int column = 0; column < Count; column++)
                    resultMatrixElements[row, column] = FindAlgerbricAddition(row, column);
            }
            AdditionalMatrix = new SquareMatrix(resultMatrixElements);
        }

        private int ExtendedEuclideanAlgorithm(int a, int b, out int x, out int y)
        {
            if (a == 0)
            {
                x = 0; y = 1;
                return b;
            }
            int x1, y1;
            int d = ExtendedEuclideanAlgorithm(b % a, a, out x1, out y1);
            x = y1 - (b / a) * x1;
            y = x1;
            return d;
        }

        private int FindReverseDeterminant(int module)
        {
            int x, y;
            int g = ExtendedEuclideanAlgorithm(Determinant, module, out x, out y);
            if (g == 1)
                x = (x % module + module) % module;
            return x;
        }

        public void FindReverseMatrix(int module)
        {
            FindAdditionalMatrix();
            AdditionalMatrix.Transpone();
            AdditionalMatrix *= FindReverseDeterminant(module);
            ReverseMatrix = AdditionalMatrix;
        }

        #endregion PrivateMethods
        #region OverloadOperations
        public static SquareMatrix operator +(SquareMatrix first, SquareMatrix second)
        {
            var length = first.Count;
            var resultMatrixElements = new int[length, length];
            for (int row = 0; row < length; row++)
            {
                for (int column = 0; column < length; column++)
                    resultMatrixElements[row, column] = first[row, column] + second[row, column];
            }
            return new SquareMatrix(resultMatrixElements);
        }

        public static SquareMatrix operator /(SquareMatrix target, int diviser)
        {
            int[,] resultMatrixElements = new int[target.Count, target.Count];
            for (int row = 0; row < target.Count; row++)
            {
                for (int column = 0; column < target.Count; column++)
                    resultMatrixElements[row, column] /= diviser;
            }
            return new SquareMatrix(resultMatrixElements);
        }

        public static SquareMatrix operator %(SquareMatrix target, int module)
        {
            int[,] resultMatrixElements = new int[target.Count, target.Count];
            for (int row = 0; row < target.Count; row++)
            {
                for (int column = 0; column < target.Count; column++)
                    resultMatrixElements[row, column] %= module;
            }
            return new SquareMatrix(resultMatrixElements);
        }

        public static SquareMatrix operator *(SquareMatrix target, int multiplexor)
        {
            int[,] resultMatrixElements = new int[target.Count, target.Count];
            for (int row = 0; row < target.Count; row++)
            {
                for (int column = 0; column < target.Count; column++)
                    resultMatrixElements[row, column] *= multiplexor;
            }
            return new SquareMatrix(resultMatrixElements);
        }

        public static Vector operator *(SquareMatrix matrix, Vector vector)
        {
            if (vector.Count != matrix.Count)
                throw new ArgumentException("Vector count and matrix count are not equals!");
            int[] resultVectorElements = new int[vector.Count];
            for (int row = 0; row < vector.Count; row++)
            {
                var temp = 0;
                for (int column = 0; column < matrix.Count; column++)
                {
                    temp += vector[row] * matrix[column, row];
                }
                resultVectorElements[row] = temp;
            }

            return new Vector(resultVectorElements);
        }


        public static SquareMatrix operator *(SquareMatrix first, SquareMatrix second)
        {
            if (first.Count != second.Count)
                throw new ArgumentException("Matrixes count are not equals!");
            int[,] resultMatrixElements = new int[first.Count, second.Count];
            for (int firstrow = 0; firstrow < first.Count; firstrow++)
            {
                var temp = 0;
                for (int column = 0; column < first.Count; column++)
                {
                    for (int secondrow = 0; secondrow < second.Count; secondrow++)
                    {
                        temp += first[firstrow, secondrow] * second[secondrow, column];
                    }
                    resultMatrixElements[firstrow, column] = temp;
                }

            }
            return new SquareMatrix(resultMatrixElements);
        }
    }

    #endregion OverloadOperations
}
