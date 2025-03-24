namespace VichMat2
{

    public interface IIterations
    {
        float[] Zeidel(float[,] A, float[] B, float epsilon, bool normCheck);
    }
    public class ZeidelIterations : IIterations
    {
        public float Criterion2Count(float[,] alpha, float epsilon)
        {
            int n = alpha.GetLength(0);

            // Формирование матрицы alpha2
            float[,] alpha2 = new float[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j > i)
                    {
                        alpha2[i, j] = 0;
                    }
                    else
                    {
                        alpha2[i, j] = alpha[i, j];
                    }
                }
            }

            float alphaNorm = NormCount(alpha);
            float alpha2Norm = NormCount(alpha2);

            return ((1 - alphaNorm) * epsilon) / alpha2Norm;
        }

        public float[] Iterations(float[] x_old, float epsilon, float[,] alpha, float[] beta)
        {
            int iterationCount = 0;
            int n = alpha.GetLength(0);
            float[] x_new = new float[n];
            //float criterion = epsilon;
            float criterion = Criterion2Count(alpha, epsilon);
            while (true)
            {
                iterationCount++;
                for (int i = 0; i < n; i++)
                {
                    float temp = x_new[i];
                    x_new[i] = beta[i];
                    for (int j = 0; j < n; j++)
                    {
                        if (i != j)
                        {
                            x_new[i] += alpha[i, j] * (j < i ? x_new[j] : x_old[j]);
                        }
                    }
                    Console.WriteLine($"Значение x[{i}] после итерации: {x_new[i]}");
                   // x_old[i] = temp;
                }
                float maxDif = 0;
                for (int i = 0; i < x_new.Length; i++)
                {
                    float dif = Math.Abs(x_new[i] - x_old[i]);
                    if (dif > maxDif)
                    {
                        maxDif = dif;
                    }
                }
                if (maxDif <= criterion)
                    break;

                Array.Copy(x_new, x_old, n);
            }
            Console.WriteLine($"Количество итераций: {iterationCount}");
            return x_new;
        }

        public float NormCount(float[,] alpha)
        {
            float norm = 0;
            int n = alpha.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                float strSum = 0;
                for (int j = 0; j < n; j++)
                {
                    strSum += Math.Abs(alpha[i, j]);
                }
                if (strSum > norm)
                {
                    norm = strSum;
                }
            }
            Console.WriteLine($"Норма альфа матрицы: {norm}");
            if (norm >= 1)
            {
                Console.WriteLine("Норма больше единицы, метод может не сойтись.");
            }
            return norm;
        }

        public float[] Zeidel(float[,] A, float[] B, float epsilon, bool normCheck)
        {
            int n = B.Length;
            if (A.GetLength(0) != n || A.GetLength(1) != n)
                throw new ArgumentException("Матрица A должна быть квадратной и соответствовать размерности вектора B.");

            // Создание матрицы альфа
            float[,] alpha = new float[n, n];
            for (int i = 0; i < n; i++)
            {
                float diagonalElement = A[i, i];
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        alpha[i, j] = 0;
                    }
                    else
                    {
                        alpha[i, j] = -A[i, j] / diagonalElement;
                    }
                }
            }

            // Проверка нормы матрицы альфа
            if (normCheck)
            {
                NormCount(alpha);
            }

            // Создание матрицы бета
            float[] beta = new float[n];
            for (int i = 0; i < n; i++)
            {
                beta[i] = B[i] / A[i, i];
            }

            // Решение системы
            // Начальное приближение
            float[] x_old = new float[n];
            for (int i = 0; i < n; i++)
            {
                x_old[i] = beta[i];
            }

            float[] x_new = new float[n];
            x_new = Iterations(x_old, epsilon, alpha, beta);

            Console.WriteLine("Решение матрицы:");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(x_new[i]);
            }

            return x_new;
        }
    }
}