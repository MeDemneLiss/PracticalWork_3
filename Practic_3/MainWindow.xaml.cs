using System.Windows;
using System.Windows.Controls;
using Lib_1;
using LibMas;
using Microsoft.Win32;


namespace Practic_3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int[,] array;

        private void CreateDataGrid_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(quantityColumn.Text, out int column) && int.TryParse(quantityRow.Text, out int row))
            {
                if (column > 0 && row > 0)
                {
                    array = new int[row, column];
                    dataGrid.ItemsSource = VisualArray.ToDataTable(array).DefaultView;
                    finalAnswer.Clear();
                    return;
                }
            }
            Error();
        }
        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            int indexColumn = e.Column.DisplayIndex;
            int indexRow = e.Row.GetIndex();
            if (!int.TryParse(((TextBox)e.EditingElement).Text.Replace('.', ','), out int value))
            {
                MessageBox.Show("Введены неверные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            array[indexRow, indexColumn] = value;
        }

        private void FillDataGrid_Click(object sender, RoutedEventArgs e)
        {


            if (!int.TryParse(quantityColumn.Text, out int column)) return;
            if (!int.TryParse(quantityRow.Text, out int row)) return;
            if (column > 0 && row > 0)
            {
                array = new int[row, column];
                MatrixLogic.FillRandomValues(array);
                dataGrid.ItemsSource = VisualArray.ToDataTable(array).DefaultView;
            }
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            finalAnswer.Clear();

            if (array == null || array.Length == 0)
            {
                Error();
            }
            else
            {
                SolutionTask.RowIdenticalValues(array, out string result);
                finalAnswer.Text = result;
            }
        }
        private void ClearDataGrid_Click(object sender, RoutedEventArgs e)
        {
            MatrixLogic.ClearMatrix(array);
            dataGrid.ItemsSource = VisualArray.ToDataTable(array).DefaultView;
            quantityColumn.Clear();
            quantityRow.Clear();
            finalAnswer.Clear();
        }

        private void SaveMaerix_Click(object sender, RoutedEventArgs e)
        {
            if (array == null)
            {
                Error();
                return;
            }
            SaveFileDialog save = new SaveFileDialog
            {
                DefaultExt = ".txt",
                Filter = "Все файлы (*.*)|*.*|Текстовые файлы|*.txt",
                FilterIndex = 2,
                Title = "Сохранение таблицы"
            };
            if (save.ShowDialog() == true)
            {
               MatrixLogic.SaveMatrix(save.FileName, array);
            }
        }
        private void OpenMatrix_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog open = new OpenFileDialog
            {
                Filter = "Все файлы (*.*)|*.*|Текстовые файлы|*.txt",
                FilterIndex = 2,
                Title = "Открытие таблицы"
            };
            if (open.ShowDialog() == true)
            {
                if (open.FileName != string.Empty)
                {
                    MatrixLogic.OpenMatrix(open.FileName, out array);                 
                    quantityColumn.Text = array.GetLength(1).ToString();
                    quantityRow.Text = array.GetLength(0).ToString();

                    dataGrid.ItemsSource = VisualArray.ToDataTable(array).DefaultView;
                }
            }
        }
        private void Info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Самсаков Андрей Александрович ИСП-31\nПрактическая работа№3\nДана целочисленная матрица размера M × N. Найти номер первой из ее строк, содержащих равное количество положительных и отрицательных элементов (нулевые элементы матрицы не учитываются). Если таких строк нет, то вывести 0.", "Информация о программе", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private static void Error()
        {
            MessageBox.Show("Вы не создали матрицу, укажите корректное количество колонок, строк и нажмите кнопку Заполнить", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

}