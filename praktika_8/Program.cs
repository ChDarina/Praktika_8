using System;
using System.IO;
using System.Text;

namespace praktika_8
{
    class Program
    {
        static public int InputNumber(string line, int left = 0, int right = 1)
        {
            int num = 0;
            try
            {
                num = Convert.ToInt32(line);
                if (num >= left && num <= right) return num;
                else
                {
                    Console.WriteLine("В строке должно быть число от {0} до {1}!", left, right);
                }
            }
            catch
            {
                Console.WriteLine("Файл заполнен неверно!");
            }
            Console.ReadKey();
            Environment.Exit(0);
            return -1;
        }
        static int [,] ReadfromFile(ref int count, ref int PeekNum)
        {
            string line, temp;
            int[,] matrix = null;
            using (FileStream sf = new FileStream("input.txt", FileMode.OpenOrCreate)) { }
            using (StreamReader reader = new StreamReader("input.txt", Encoding.Default))
            {
                if (reader.Peek() == -1)
                {
                    Console.WriteLine("Файл пуст");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                try
                {
                    line = reader.ReadLine();
                    string[] text = line.Split(' ');
                    PeekNum = text.Length;
                    int summa = 0;
                    count++;
                    for (int i = 1; reader.Peek() != -1; i++)
                    {
                        temp = reader.ReadLine();
                        text = temp.Split(' ');
                        if (text.Length == PeekNum)
                        {
                            count++;
                            line += " " + temp;
                        }
                        else
                        {
                            Console.WriteLine("\nФайл заполнен неверно. В строках должно быть одинаковое количество столбцов");
                            Console.ReadKey();
                            Environment.Exit(0);
                        }
                    }
                    matrix = new int[count, PeekNum];
                    text = line.Split(' ');
                    for (int i = 0; i < count; i++)
                    {
                        for (int j = 0, tempnum; j < PeekNum; j++)
                        {
                            tempnum = InputNumber(text[j + PeekNum * i]);
                            matrix[i, j] = tempnum;
                            summa += tempnum;
                        }
                        if (summa == 2) summa = 0;
                        else
                        {
                            Console.WriteLine("\nФайл заполнен неверно. В строках должно быть всего 2 единицы");
                            Console.ReadKey();
                            Environment.Exit(0);
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("\nФайл заполнен неверно");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                return matrix;
            }
        }
        static void Main(string[] args)
        {
            int count = 0, PeekNum = 0, k = 2, degree=0;
            int[,] matrix = null;
            matrix = ReadfromFile(ref count, ref PeekNum);
            Console.WriteLine("INPUT.TXT\n");
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < PeekNum; j++)
                    Console.Write(matrix[i, j].ToString() + " ");
                Console.WriteLine();
            }
            for (int i = 0; i < PeekNum; i++)
            {
                for (int j = 0; j < count; j++)
                    degree += matrix[j, i];
                if (k < degree) k = degree;
                degree = 0;
            }
            using (FileStream sf = new FileStream("output.txt", FileMode.OpenOrCreate)) { }
            using (StreamWriter writer = new StreamWriter("output.txt"))
            {
                writer.Write(k);
            }
            Console.WriteLine("\nOUTPUT.TXT\n" + k);
            Console.ReadKey();
        }
    }
}
