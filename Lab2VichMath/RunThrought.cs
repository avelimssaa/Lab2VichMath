namespace Lab2VichMath
{
    public interface IRun
    {
        float[] Solve(float[,] A, float[] B);
    }
    public class RunThrought : IRun
    {
        public float[] Solve(float[,] A, float[] B)
        {
            int n = A.GetLength(0);

            // Формирование коэффициентов прогона
            float[] alpha = new float[n - 1];
            float[] beta = new float[n - 1];      
            float[] x = new float[n];         

            // Прямой ход
            alpha[0] = A[0, 1] / A[0, 0];
            beta[0] = B[0] / A[0, 0];

            for (int i = 1; i < n - 1; i++)
            {
                float denominator = A[i, i] - A[i, i - 1] * alpha[i - 1];

                alpha[i] = A[i, i + 1] / denominator;
                beta[i] = (B[i] - A[i, i - 1] * beta[i - 1]) / denominator;
            }

            // Обратный ход
            x[n - 1] = (B[n - 1] - A[n - 1, n - 2] * beta[n - 2]) / (A[n - 1, n - 1] - A[n - 1, n - 2] *  alpha[n - 2]);
            for (int i = n - 2; i >= 0; i--)
            {
                x[i] = beta[i] - alpha[i] * x[i + 1];
            }

            return x;
        }
    }
}
