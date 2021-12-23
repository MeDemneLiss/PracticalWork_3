namespace Lib_1
{
    public class SolutionTask
    {
        public static void RowIdenticalValues(int[,] matrix, out string numberRow)
        {
            numberRow = "Ошибка";
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                int minusNumber = 0;
                int plusNumber = 0;
                for (int j = 0; j < matrix.GetLength(1) ; j++)
                {
                    if (matrix[i, j] > 0)
                    {
                        plusNumber++;
                    }
                    if (matrix[i, j] < 0)
                    {
                        minusNumber++;
                    }
                }
                if (minusNumber == plusNumber)
                {
                    numberRow = (i + 1).ToString();
                    return;
                }
                else
                {
                    numberRow = "0";
                }
            }
        }
    }
}
