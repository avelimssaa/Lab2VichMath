using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VichMat2
{

    public interface IGauss
    {
        float[] SolveWithoutPivoting(float[,] A, float[] b);
        float[] SolveWithColumnPivoting(float[,] A, float[] b);
    }
    public class GaussElimination : IGauss
    {
        public float[] SolveWithoutPivoting(float[,] A, float[] b)
        {
            int n = A.GetLength(0);
            float[,] augmentedMatrix = new float[n, n + 1];

            // Формирование расширенной матрицы
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    augmentedMatrix[i, j] = A[i, j];
                }
                augmentedMatrix[i, n] = b[i];
            }

            // Прямой ход
            for (int k = 0; k < n - 1; k++)
            {
                for (int i = k + 1; i < n; i++)
                {
                    float factor = augmentedMatrix[i, k] / augmentedMatrix[k, k];
                    for (int j = k; j <= n; j++)
                    {
                        augmentedMatrix[i, j] -= factor * augmentedMatrix[k, j];
                    }
                }
            }

            // Обратный ход
            float[] x = new float[n];
            for (int i = n - 1; i >= 0; i--)
            {
                float sum = augmentedMatrix[i, n];
                for (int j = i + 1; j < n; j++)
                {
                    sum -= augmentedMatrix[i, j] * x[j];
                }
                x[i] = sum / augmentedMatrix[i, i];
            }

            return x;
        }

        public float[] SolveWithColumnPivoting(float[,] A, float[] b)
        {
            int n = A.GetLength(0);
            float[,] augmentedMatrix = new float[n, n + 1];

            // Формирование расширенной матрицы
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    augmentedMatrix[i, j] = A[i, j];
                }
                augmentedMatrix[i, n] = b[i];
            }

            // Прямой ход с выбором главного элемента по столбцу
            for (int k = 0; k < n - 1; k++)
            {
                // Поиск максимального элемента в столбце k
                int maxIndex = k;
                float maxValue = Math.Abs(augmentedMatrix[k, k]);
                for (int i = k + 1; i < n; i++)
                {
                    if (Math.Abs(augmentedMatrix[i, k]) > maxValue)
                    {
                        maxValue = Math.Abs(augmentedMatrix[i, k]);
                        maxIndex = i;
                    }
                }

                // Перестановка строк
                if (maxIndex != k)
                {
                    for (int j = 0; j <= n; j++)
                    {
                        float temp = augmentedMatrix[k, j];
                        augmentedMatrix[k, j] = augmentedMatrix[maxIndex, j];
                        augmentedMatrix[maxIndex, j] = temp;
                    }
                }

                // Исключение
                for (int i = k + 1; i < n; i++)
                {
                    float factor = augmentedMatrix[i, k] / augmentedMatrix[k, k];
                    for (int j = k; j <= n; j++)
                    {
                        augmentedMatrix[i, j] -= factor * augmentedMatrix[k, j];
                    }
                }
            }

            // Обратный ход
            float[] x = new float[n];
            for (int i = n - 1; i >= 0; i--)
            {
                float sum = augmentedMatrix[i, n];
                for (int j = i + 1; j < n; j++)
                {
                    sum -= augmentedMatrix[i, j] * x[j];
                }
                x[i] = sum / augmentedMatrix[i, i];
            }

            return x;
        }
    }
}
