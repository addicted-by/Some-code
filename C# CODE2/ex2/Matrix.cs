using System;
using System.Collections.Generic;

namespace ex2
{

    public class Matrix
    {
        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < _Dimension; i++)
            {
                for (int j = 0; j < _Dimension; j++)
                    output += $"| {_data[i, j]} |";
                output += '\n';
            }
            return output;
        }

        
        public Int64 GetHashCode()
        {
            const Int64 mod = (Int64)10e12;
            Int64 p = 1;
            Int64 prod = 7;
            Int64 hash = 0;
            for (int i = 0; i < _Dimension * _Dimension; ++i)
            {
                hash = (hash + (Int64)(_data[i / 4, i % 4] * p)) % mod;
                p = (p * prod) % mod;
            }
            return hash;
        }

        public const int _Dimension = 4;
        private double[,] _data = new double[_Dimension, _Dimension];

        public static int Dimension
        {
            get => _Dimension;
            
        }

        public double Determinant
        {
            get
            {
                double det = 1;
                Matrix M = new Matrix();
                for (int i = 0; i < _Dimension; i++)
                    for (int j = 0; j < _Dimension; j++)
                        M[i, j] = _data[i, j];
                for (int i = 0; i < _Dimension; ++i)
                {
                    int j = i;
                    while (j < _Dimension && M[j, i] == 0)
                        ++j;
                    if (j == _Dimension)
                        return 0;
                    if (j != i)
                    {
                        det *= -1;
                        Changerows(M, i, j);
                    }
                    double leader = M[i,i];
                    for (int k = i; k < _Dimension; ++k)
                    {
                        M[i,k] /= leader;
                    }
                    det *= leader;
                    for (int k = i + 1; k < _Dimension; ++k)
                    {
                        leader = M[k,i];
                        for (j = i; j < _Dimension; ++j)
                        {
                            M[k, j] -= M[i, j] * leader;
                        }
                    }
                }
                return det;
            }
              /*  int i, j, k, x, n = 0;
                double result;
                bool flag = false;
                Matrix draft = new Matrix();
                for (i = 0; i < _Dimension; i++)
                    for (j = 0; j < _Dimension; j++)
                        draft[i, j] = _data[i, j];
                for (i = 0; i < _Dimension; i++)
                {
                    for (j = 0; j < _Dimension; j++)
                    {
                        if (!draft[i, j].Equals(0))
                        {
                            flag = true;
                            break;
                        }
                    }

                    if (!flag) continue;
                    n++;
                    flag = false;
                }

                if (n == _Dimension)
                {
                    result = 1;
                    for (i = 0; i < _Dimension; i++)
                    {
                        if (draft[i, i].Equals(0))
                        {
                            x = 0;
                            while (x < _Dimension && draft[x, i].Equals(0))
                                x += 1;
                            for (j = 0; j < _Dimension; j++)
                                draft[i, j] += draft[x, j];
                            for (j = 0; j < _Dimension; j++)
                                draft[x, i] = draft[i, j] - draft[x, j];
                            for (j = 0; j < _Dimension; j++)
                                draft[i, j] -= draft[x, j];
                            result *= -1;
                        }

                        for (i = 0; i < _Dimension; i++)
                        {
                            x = -1;
                            for (j = i; j < _Dimension; j++)
                                if (draft[j, i].Equals(1))
                                    x = j;
                            if (x == -1)
                            {
                                result *= draft[i, i];
                                for (j = i + 1; j < _Dimension; j++)
                                    draft[j, i] /= draft[i, i];
                            }
                            else if (x != i)
                            {
                                result *= -1;

                                double[] buff = new double[_Dimension];
                                for (k = 0; k < _Dimension; k++)
                                {
                                    buff[k] = draft[i, k];
                                    draft[i, k] = draft[x, k];
                                    draft[x, k] = buff[k];
                                }
                            }

                            for (j = i + 1; j < _Dimension; j++)
                                for (k = i + 1; k < _Dimension; k++)
                                    draft[j, k] -= draft[j, i] * draft[i, k];
                        }
                    }
                }
                else
                    return 0;
                return result;
            }*/
        }

        public double this[int row, int column]
        {
            get =>
                (row < _Dimension && column < _Dimension) ?
                    _data[row, column] : throw new IndexOutOfRangeException();

            set => _data[row, column] = value;
        }
        public Matrix Inverse()
        {
            if (Determinant.Equals(0)) throw new ArithmeticException();
            Matrix a = new Matrix(_data);
            Matrix output = Matrix.EMatrix();
            for (int i = 0; i < _Dimension; ++i)
            {
                int j = i;
                while (a[j,i] == 0)
                        ++j;
                if (i != j)
                {
                    Changerows(a, i, j);
                    Changerows(output, i, j);
                }
                double leader = a[i,i];
                for (j = 0; j < _Dimension; ++j)
                {
                    a[i,j] /= leader;
                    output[i,j] /= leader;
                }
                for (int k = i + 1; k < _Dimension; ++k)
                {
                    leader = a[k, i];
                    for (j = 0; j < _Dimension; ++j)
                    {
                        a[k, j] -= a[i, j] * leader;
                        output[k, j] -= output[i, j] * leader;
                    }
                }
            }

            for (int i = (int)_Dimension - 1; i >= 0; --i)
            {
                for (int k = i - 1; k >= 0; --k)
                {
                    double leader = a[k, i];
                    for (int j = 0; j < _Dimension; ++j)
                    {
                        a[k, j] -= a[i, j] * leader;
                        output[k, j] -= output[i, j] * leader;
                    }
                }
            }
            return output;
        }

        private void Changerows(Matrix a, int i, int j)
        {
            for (int k = 0; k < _Dimension; k++)
            {
                double t = a[i, k];
                a[i, k] = a[j, k];
                a[j, k] = t;
            }
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            Matrix output = new Matrix();
            for (int i = 0; i < _Dimension; i++)
                for (int j = 0; j < _Dimension; j++)
                    output[i, j] = a[i, j] + b[i, j];
            return output;
        }

        public static Matrix operator ++(Matrix a)
        {
            for (int i = 0; i < _Dimension; i++)
                a[i, i]++;
            return a;
        }
        public static Matrix operator -(Matrix a)
        {
            for (int i = 0; i < _Dimension; ++i)
                for (int j = 0; j < _Dimension; ++j)
                   a[i, j] = -a[i, j];
            return a;
        }

        public static Matrix operator -(Matrix a, Matrix b)
        {
            return a + (-b);
        }

        public static Matrix operator --(Matrix a)
        {
            for (int i = 0; i < _Dimension; i++)
                a[i, i]--;
            return a;
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            Matrix output = new Matrix();
            for (int k = 0; k < _Dimension; k++)
                for (int i = 0; i < _Dimension; i++)
                    for (int j = 0; j < _Dimension; j++)
                        output[k, i] += a[k, j] * b[j, i];
            return output;
        }
        public static Matrix operator *(Matrix a, double b)
        {
            return a * b;
        }
        public static Matrix operator /(Matrix a, Matrix b)
        {
            return a * b.Inverse();
        }

        public static bool operator ==(Matrix a, Matrix b)
        {
            return a.Determinant == b.Determinant;
        }


        public static bool operator !=(Matrix a, Matrix b)
        {
            return !(a.Determinant == b.Determinant);
        }

        public static bool operator >(Matrix a, Matrix b)
        {
            return a.Determinant > b.Determinant;
        }

        public static bool operator <(Matrix a, Matrix b)
        {
            return a.Determinant < b.Determinant;
        }

        public Matrix(params double[] data)
        {
            for (int i = 0; i < _Dimension; i++)
                for (int j = 0; j < _Dimension; j++)
                    this[i, j] = data[j + i * _Dimension];
        }

        public Matrix(List<double> data)
        {
            for (int i = 0; i < _Dimension; i++)
                for (int j = 0; j < _Dimension; j++)
                    this[i, j] = data[j + i * _Dimension];
        }

        public Matrix(double[,] data)
        {
            _data = data;
        }

        public Matrix(double data)
        {
            for (int i = 0; i < _Dimension; i++)
                for (int j = 0; j < _Dimension; j++)
                    this[i, j] = (i == j) ? data : 0;
        }

        public Matrix()
        {
            for (int i = 0; i < _Dimension; i++)
                for (int j = 0; j < _Dimension; j++)
                    this[i, j] = 0;
        }

        public static Matrix EMatrix()
        {
            Matrix output = new Matrix();
            for (int i = 0; i < _Dimension; i++)
                output[i, i] = 1;
            return output;
        }
    }


    class Program
    {
        static void Main()
        {

            string input = "";
            do
            {
                Matrix m1, m2;
                List<double> data = new List<double>();
                Console.Write("Menu:\n" +
                              "1. Matrix addition\n" +
                              "2. Matrix substraction\n" +
                              "3. Matrix multiply\n" +
                              "4. Matrix divide\n" +
                              "5. Matrix determinant\n" +
                              "6. Inverse matrix\n" +
                              "7. Compare matrix\n" +
                              "8. Exit\n" +
                              "№: ");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        data.Clear();
                        Console.WriteLine("Write first matrix's elements in one row:");
                        input = Console.ReadLine();
                        if (input == null)
                            throw new NullReferenceException();
                        foreach (string item in input.Split(' '))
                            data.Add(double.Parse(item));
                        m1 = new Matrix(data);
                        data.Clear();
                        Console.WriteLine("Write second matrix's elements in one row:");
                        input = Console.ReadLine();
                        if (input == null)
                            throw new NullReferenceException();
                        Console.WriteLine(input.Split(' '));
                        foreach (string item in input.Split(' '))
                            data.Add(double.Parse(item));
                        m2 = new Matrix(data);
                        Console.WriteLine("The first:\n"
                                          + m1
                                          + "The second\n:\n"
                                          + m2
                                          + "\nTotal:\n" + (m1 + m2));
                        break;
                    case "2":
                        data.Clear();
                        Console.WriteLine("Write first matrix's elements in one row:");
                        input = Console.ReadLine();
                        if (input == null)
                            throw new NullReferenceException();
                        foreach (string item in input.Split(' '))
                            data.Add(double.Parse(item));
                        m1 = new Matrix(data);
                        data.Clear();
                        Console.WriteLine("Write second matrix's elements in one row:");
                        input = Console.ReadLine();
                        if (input == null)
                            throw new NullReferenceException();
                        foreach (string item in input.Split(' '))
                            data.Add(double.Parse(item));
                        m2 = new Matrix(data);
                        Console.WriteLine("The first:\n"
                                          + m1
                                          + "The second\n:\n"
                                          + m2
                                          + "\nTotal:\n" + (m1 - m2));
                        break;
                    case "3":
                        data.Clear();
                        Console.WriteLine("Write first matrix's elements in one row:");
                        input = Console.ReadLine();
                        if (input == null)
                            throw new NullReferenceException();
                        foreach (string item in input.Split(' '))
                            data.Add(double.Parse(item));
                        m1 = new Matrix(data);
                        data.Clear();
                        Console.WriteLine("Write second matrix's elements in one row:");
                        input = Console.ReadLine();
                        if (input == null)
                            throw new NullReferenceException();
                        foreach (string item in input.Split(' '))
                            data.Add(double.Parse(item));
                        m2 = new Matrix(data);
                        Console.WriteLine("The first:\n"
                                          + m1
                                          + "The second\n:\n"
                                          + m2
                                          + "\nTotal:\n" + (m1 * m2));
                        break;
                    case "4":
                        data.Clear();
                        Console.WriteLine("Write first matrix's elements in one row:");
                        input = Console.ReadLine();
                        if (input == null)
                            throw new NullReferenceException();
                        foreach (string item in input.Split(' '))
                            data.Add(double.Parse(item));
                        m1 = new Matrix(data);
                        data.Clear();
                        Console.WriteLine("Write second matrix's elements in one row:");
                        input = Console.ReadLine();
                        if (input == null)
                            throw new NullReferenceException();
                        foreach (string item in input.Split(' '))
                            data.Add(double.Parse(item));
                        m2 = new Matrix(data);
                        Console.WriteLine("The first:\n"
                                          + m1
                                          + "The second\n:\n"
                                          + m2
                                          + "\nTotal:\n" + (m1 / m2));
                        break;
                    case "5":
                        data.Clear();
                        Console.WriteLine("Write matrix's elements in one row:");
                        input = Console.ReadLine();
                        if (input == null)
                            throw new NullReferenceException();
                        foreach (string item in input.Split(' '))
                            data.Add(double.Parse(item));
                        m1 = new Matrix(data);
                        Console.WriteLine("Matrix's determinant:" + m1.Determinant);
                        break;
                    case "6":
                        data.Clear();
                        Console.WriteLine("Write matrix's elements in one row:"); input = Console.ReadLine();
                        if (input == null)
                            throw new NullReferenceException();
                        foreach (string item in input.Split(' '))
                            data.Add(double.Parse(item));
                        m1 = new Matrix(data);
                        Console.WriteLine("Inverse: " + m1.Inverse());
                        break;
                    case "7":
                        data.Clear();
                        Console.WriteLine("Write first matrix's elements in one row:");
                        input = Console.ReadLine();
                        if (input == null)
                            throw new NullReferenceException();
                        foreach (string item in input.Split(' '))
                            data.Add(double.Parse(item));
                        m1 = new Matrix(data);
                        data.Clear();
                        Console.WriteLine("Write second matrix's elements in one row:");
                        input = Console.ReadLine();
                        if (input == null)
                            throw new NullReferenceException();
                        foreach (string item in input.Split(' '))
                            data.Add(double.Parse(item));
                        m2 = new Matrix(data);
                        Console.WriteLine("The first " +
                                          (
                                              (m1 == m2) ? "equal second" :
                                              (m1 > m2) ? "determinant bigger than second" :
                                              (m1 < m2) ? "determinant smaller than second" :
                                              "not equal second"
                                          )
                        );
                        break;
                    case "8":
                        Console.WriteLine("Exit...");
                        return;
                    default:
                        Console.WriteLine("Incorrect value");
                        break;
                }
            } while (input != "8");
        }
    }
}