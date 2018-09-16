using System;
using System.Collections.Generic;
using System.Text;

namespace Matrix
{
    public class Vector
    {
        public int Count { get; protected set; }
        public int[] VectorElements { get; set; }

        #region Constructors
        public Vector(int n)
        {
            Count = n;
            VectorElements = new int[Count];
        }

        public Vector(int n, int min, int max)
        {
            Count = n;
            VectorElements = new int[Count];
        }


        public Vector(int[] elements)
        {
            Count = elements.Length;
            VectorElements = new int[Count];
            Array.Copy(elements, VectorElements, elements.Length);
        }
        #endregion Constructors



        #region AdditionalMethods
        public int this[int number] => VectorElements[number];
        #endregion AdditionalMethods


        #region OverloadOperations

        public static Vector operator %(Vector target, int module)
        {
            int[] resultVectorElements = new int[target.Count];
            for (int count = 0; count < target.Count; count++)
            {
                resultVectorElements[count] = target[count] % module;
            }
            return new Vector(resultVectorElements);
        }

        public static Vector operator *(Vector target, int multiplexor)
        {
            int[] resultVectorElements = new int[target.Count];
            for (int count = 0; count < target.Count; count++)
            {
                resultVectorElements[count] = target[count] * multiplexor;
            }
            return new Vector(resultVectorElements);
        }

        public static Vector operator *(Vector vector, SquareMatrix matrix)
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

        #endregion OverloadOperations


    }
}
