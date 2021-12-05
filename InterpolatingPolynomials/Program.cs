using System;

namespace InterpolatingPolynomials
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            double[] xArray = new double[0];
            double[] yArray = new double[0];

            Console.Write("File path: ");
            var path = Console.ReadLine();
            readFile(path, ref xArray, ref yArray);

            Newtons(xArray, yArray);
            Console.WriteLine("\nLagrange Form");
            Lagrange(xArray, yArray);

            Console.ReadKey();
        }
        static void readFile(string FileName, ref double[] xArray, ref double[] yArray)
        {
            string[] lines = System.IO.File.ReadAllLines(FileName);

            if (lines.Length < 2)
                throw new FormatException();

            var xValues = lines[0].Split(' ');
            var yValues = lines[1].Split(' ');

            xArray = new double[xValues.Length];
            yArray = new double[yValues.Length];

            for (int i = 0; i < xValues.Length; i++)
            {
                xArray[i] = double.Parse(xValues[i]);
                yArray[i] = double.Parse(yValues[i]);
            }
        }
        public static void Newtons(double[] xArray, double[] yArray)
        {
            double[][] printArray = new double[xArray.Length][];

            double[] curArray = new double[xArray.Length];
            int depth = 0;
            yArray.CopyTo(curArray, 0);
            while (curArray.Length > 0)
            {
                double[] newArray = new double[curArray.Length - 1];
                for (int i = 1; i < curArray.Length; i++)
                {
                    newArray[i - 1] = (curArray[i] - curArray[i - 1]) / (xArray[i + depth] - xArray[i - 1]);
                }
                printArray[depth] = newArray;
                curArray = newArray;
                depth++;
            }

            Console.Write("x\ty\t");
            for (int i = 0; i < printArray[0].Length; i++)
                Console.Write("{0}\t", i + 1);
            Console.WriteLine();

            for (int i = 0; i < printArray.Length; i++)
            {
                Console.Write("{0:0.000}\t{1:0.000}\t", xArray[i], yArray[i]);
                for (int j = 0; j < printArray[i].Length; j++)
                {
                    Console.Write("{0:0.000}\t", printArray[j][i]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.Write(yArray[0]);


            for (int i = 0; i < printArray.Length - 1; i++)
            {
                var element = printArray[i][0];

                if (element >= 0)
                    Console.Write(" + ");
                else
                    Console.Write(" - ");
                Console.Write("{0:0.000}", Math.Abs(element));

                for (int j = 0; j <= i; j++)
                {
                    Console.Write("(x - {0})", xArray[j]);
                }
            }
            Console.WriteLine();
        }
        public static void Lagrange(double[] xArray, double[] yArray)
        {
            for (int i = 0; i < xArray.Length; i++)
            {
                if (i != 0)
                    Console.Write(" + ");
                Console.Write("{0} * ", yArray[i]);
                for (int j = 0; j < xArray.Length; j++)
                {
                    if (i == j)
                        continue;
                    Console.Write("(x - {0})", xArray[j]);
                }
                Console.Write(" / ");
                for (int j = 0; j < xArray.Length; j++)
                {
                    if (i == j)
                        continue;
                    Console.Write("({0} - {1})", xArray[i], xArray[j]);
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
    }
}
