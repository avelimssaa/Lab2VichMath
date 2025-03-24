using Lab2VichMath;

namespace VichMat2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // Пример системы уравнений:
            float[,] A = {
            { 3, 5, 1 },
            { 1.799999f, 3, 7 },
            { 8, 1, 1 }
        };

            IGauss gauss = new GaussElimination();

            float[] B = { 12, 13.599998f, 18 };

            // Решаем систему:
            float[] x = gauss.SolveWithoutPivoting(A, B);

            // Выводим решение:
            Console.WriteLine("Решение без выбора главного элемента:");
            for (int i = 0; i < x.Length; i++)
            {
                Console.WriteLine("x[" + i + "] = " + x[i]);
            }

            x = gauss.SolveWithColumnPivoting(A, B);
            Console.WriteLine("Решение с выбором главного элемента:");
            for (int i = 0; i < x.Length; i++)
            {
                Console.WriteLine("x[" + i + "] = " + x[i]);
            }


            //float[,] C = {
            //    { 5, 30, 6 },
            //    { -3, 4, 20 },
            //    { 10, 2, 1 }
            //};
            //float[] D = { 53, 61, 15 };

            float[,] C = {
                { 10, 2, 1 },
                { 5, 30, 6 },
                { -3, 4, 20 }
            };
            float[] D = { 15, 53, 61 };

            IIterations iterations = new ZeidelIterations();
            //iterations.Zeidel(C, D, 0.0001f, false);
            float epsilon = (float)Math.Pow(10, float.MinValue);
            iterations.Zeidel(C, D, epsilon, true);


            IRun run = new RunThrought();

            float[,] E = new float[,]
            {
                {3, -1, 0 },
                {-1, 4, -1 },
                {0, 2, -1 }
            };

            float[] F = { 4, 4, 2 };

            float[] solution = run.Solve(E, F);
            Console.WriteLine("Решение:");
            for (int i = 0; i < solution.Length; i++)
            {
                Console.WriteLine($"x[{i + 1}] = {solution[i]}"); // Вывод: x1=2, x2=1, x3=0
            }
        }
    }
}