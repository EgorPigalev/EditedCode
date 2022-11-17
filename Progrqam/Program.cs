using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FindInitialDistribution
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Нахождение первоначального распределения транспортной задачи по методу северо-заподного угла");
                    FileWork fileWork = new FileWork(); //// Избегание использования статистических классов
                    Decision decision = new Decision(); //// Избегание использования статистических классов
                    char userResponse; //// Изменено название на более понятное
                    Console.Clear();
                    fileWork.CalcInitialNameFile();
                    decision.arrayDistribution = fileWork.GetData();
                    if (fileWork.CheckProverka(decision.arrayDistribution)) //// Убрано явное сравнение с true 
                    {
                        Console.ReadKey();
                        break;
                    }
                    //// Изменено название на более понятное, а также тип на более правильный (использование double которое занимает больше памяти)
                    int summaObjectiveFunction; // Переменная для хранения значения целевой функции
                    summaObjectiveFunction = decision.GetDistribution();
                    Console.Clear();
                    fileWork.CalcSaveNameFile();
                    Console.WriteLine("Результат распределения занесён в файл " + fileWork.pathEnd);// вывод матрицы с рапределёнными поставками
                    fileWork.CalcRecord(decision.arrayDistribution, summaObjectiveFunction);
                    while (true)
                    {
                        try
                        {
                            Console.Write("\nПовторить программу?\nДа(Y)/Нет(N)\nОтвет: ");
                            userResponse = Convert.ToChar(Console.ReadLine());
                            break;
                        }
                        catch
                        {
                            Console.WriteLine("Введены некорректные данные!");
                        }
                    }
                    if (!(userResponse.Equals('Y') || userResponse.Equals('y') || userResponse.Equals('н') || userResponse.Equals('Н')))
                    {
                        break;
                    }
                    else if (!(userResponse.Equals('N') || userResponse.Equals('N') || userResponse.Equals('Т') || userResponse.Equals('т')))
                    {
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                    }
                }
            }
            catch
            {
                Console.WriteLine("В работе программы произошла ошибка");
            }
        }
    }
}

public class FileWork //// Изменено название на нотацию паскаля
{
    private string pathStart; // Путь к исходному файлу
    public string pathEnd; // Путь к конечному файлу
    public void CalcInitialNameFile() //// Изменено название метода
    {
        char userResponse; // Переменная для диалога
        while (true)
        {
            Console.WriteLine("После нажатия Enter, Вам необходимо указать csv файл где хранятся входные данные");
            Console.ReadKey();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".csv";
            openFileDialog.Filter = "Text documents (.csv)|*.csv";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pathStart = openFileDialog.FileName;
                break;
            }
            else
            {
                while (true)
                {
                    try
                    {
                        Console.Write("\nЖелаете повторить выбор входных данных?\nДа(Y)/Нет(N)\nОтвет: ");
                        userResponse = Convert.ToChar(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Введены некорректные данные!");
                    }
                }
                if (userResponse.Equals('N') || userResponse.Equals('N') || userResponse.Equals('Т') || userResponse.Equals('т'))
                {
                    return;
                }
            }
        }
    }
    public void CalcSaveNameFile() //// Изменено название метода
    {
        char userResponse; // Переменная для диалога с пользователем
        while (true)
        {
            Console.WriteLine("После нажатия Enter, Вам необходимо указать csv файл куда будет сохранён результат");
            Console.ReadKey();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".csv";
            saveFileDialog.Filter = "Text documents (.csv)|*.csv";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                pathEnd = saveFileDialog.FileName;
                break;
            }
            else
            {
                while (true)
                {
                    try
                    {
                        Console.Write("\nЖелаете повторить выбор файла?\nДа(Y)/Нет(N)\nОтвет: ");
                        userResponse = Convert.ToChar(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Введены некорректные данные!");
                    }
                }
                if (userResponse.Equals('N') || userResponse.Equals('N') || userResponse.Equals('Т') || userResponse.Equals('т'))
                {
                    return;
                }
            }
        }
    }
    public string[,] GetData() // Считывание данных из файла
    {
        string[] lines = File.ReadAllLines(pathStart); //// var используется тогда, когда тип переменной очевиден
        string[][] text = new string[lines.Length][];
        for (int i = 0; i < text.Length; i++)
        {
            text[i] = lines[i].Split(';');
        }
        string[,] arrayElements = new string[text.Length, text[text.Length - 1].Length]; //// Изменено название переменной на более понятное
        int j = 0;
        int k;
        foreach (string[] line in text)
        {
            k = 0;
            foreach (string s in line)
            {
                arrayElements[j, k] = s;
                k++;
            }
            j++;
        }
        return arrayElements;
    }
    public bool CheckProverka(string[,] a) // Проверка входных данных на правильность
    {
        for (int i = 0; i < a.GetLength(0); i++)
        {
            for (int j = 0; j < a.GetLength(1); j++)
            {
                if (i == 0 && j == 0) //// добавлены фигурные скобки (требование использовать конструкию if с фигурными скобками)
                {
                    j++;
                }
                try
                {
                    if (Convert.ToDouble(a[i, j]) <= 0)
                    {
                        Console.WriteLine("В таблицы распределений, не могут быть отрицательные значения или 0");
                        return true;
                    }
                }
                catch
                {
                    Console.WriteLine("Во входных данных присутствуют некоректные данные, исправьте их и попробуйте снова");
                    return true;
                }
            }
        }
        return false;
    }
    public void CalcRecord(string[,] distributionArray, int summaObjectiveFunction) // Запись данных в csv файл
    {
        for (int i = 0; i < distributionArray.GetLength(0); i++)
        {
            for (int j = 0; j < distributionArray.GetLength(1); j++)
            {
                File.AppendAllText(pathEnd, distributionArray[i, j] + ";", Encoding.UTF8);
            }
            File.AppendAllText(pathEnd, Environment.NewLine);
        }
        File.AppendAllText(pathEnd, Environment.NewLine);
        File.AppendAllText(pathEnd, "\nF =;" + summaObjectiveFunction + " у. д. е.;", Encoding.UTF8);
        File.AppendAllText(pathEnd, Environment.NewLine);
        //// Изменено название на более понятное
        int recordNumber = 1; // Переменная хранящая номер записи
        for (int i = 0; i < distributionArray.GetLength(0); i++) // Циклы для прохождения по массиву распределения
        {
            for (int j = 0; j < distributionArray.GetLength(1); j++)
            {
                if (i == 0 && j == 0) //// добавлены фигурные скобки (требование использовать конструкию if с фигурными скобками)
                {
                    j++;
                }
                //// Изменено название на более понятное
                string cellContent = distributionArray[i, j]; // Переменная для хранения содержимого ячейки
                //// Изменено название на более понятное
                int numberChar = 0; // Количесво символов / в ячейке
                int index = 0; // Переменная, которая хранит индекс символа |
                for (int e = 0; e < cellContent.Length; e++)
                {
                    if (cellContent[e] == '|')
                    {
                        numberChar++;
                        index = e;
                    }
                }
                if (numberChar != 0) // Если в ячейке есть символ |
                {
                    // Вывод записи кому и с кем нужно заключить договор
                    File.AppendAllText(pathEnd, recordNumber + ") " + i + " поставщику требуется заключить договор с " + j + " магазином на поставку " + cellContent.Substring(index + 1) + " единиц продукции\n", Encoding.UTF8);
                    File.AppendAllText(pathEnd, Environment.NewLine);
                    recordNumber++;
                }
            }
        }
    }
}
public class Decision //// Изменено название на нотацию паскаля
{
    //// Изменено название переменной на более понятное
    public string[,] arrayDistribution; // Матрица которая хранит таблицу, где будет хранится распределение
    //// Удалены не используемые переменные
    public int GetDistribution() //Выполнения распредления и подсчёта суммы
    {
        int summaObjectiveFunction = 0; // Переменная для хранения суммы
        for (int i = 1; i < arrayDistribution.GetLength(0); i++) // Цикл по строкам массива
        {
            for (int j = 1; j < arrayDistribution.GetLength(1); j++) // Цикл по столбцам массива
            {
                if (Convert.ToInt32(arrayDistribution[0, j]) != 0) // Проверка, если в ячейке спрос равен 0, то идём дальше
                {
                    if (Convert.ToInt32(arrayDistribution[i, 0]) != 0) // Проверка, если в ячейке предложение равно 0, то переходим на новую строчку
                    {
                        if (Convert.ToInt32(arrayDistribution[i, 0]) > Convert.ToInt32(arrayDistribution[0, j])) // Проверка, если предложение больше спроса
                        {
                            summaObjectiveFunction = summaObjectiveFunction + Convert.ToInt32(arrayDistribution[i, j]) * Convert.ToInt32(arrayDistribution[0, j]); // К сумме прибавляем произведение цены с поставкой
                            arrayDistribution[i, j] = arrayDistribution[i, j] + " | " + arrayDistribution[0, j]; // В ячеке добавляем символ / и максимальную поставку
                            arrayDistribution[i, 0] = Convert.ToString(Convert.ToInt32(arrayDistribution[i, 0]) - Convert.ToInt32(arrayDistribution[0, j])); // Вычитаем из предложения сделанную поставку
                            arrayDistribution[0, j] = "0"; // Зануляем спрос, так как весь спрос удовлетворён
                        }
                        else if (Convert.ToInt32(arrayDistribution[i, 0]) < Convert.ToInt32(arrayDistribution[0, j]))// Проверка, если предложение меньше спроса
                        {
                            summaObjectiveFunction = summaObjectiveFunction + (Convert.ToInt32(arrayDistribution[i, j]) * Convert.ToInt32(arrayDistribution[i, 0])); // К сумме прибавляем произведение цены с поставкой
                            arrayDistribution[i, j] = arrayDistribution[i, j] + " | " + arrayDistribution[i, 0]; // В ячекй добавляем символ / и максимальную поставку
                            arrayDistribution[0, j] = Convert.ToString(Convert.ToInt32(arrayDistribution[0, j]) - Convert.ToInt32(arrayDistribution[i, 0])); // Вычитаем из спроса сделанную поставку
                            arrayDistribution[i, 0] = "0"; // Зануляем предложение, так как у данного поставщика закончилась продукция
                            // Переходим на новую строку на тот же магазин
                            if (i != i - 1)
                            {
                                i++;
                                j--;
                            }
                        }
                        else
                        {
                            summaObjectiveFunction = summaObjectiveFunction + (Convert.ToInt32(arrayDistribution[i, j]) * Convert.ToInt32(arrayDistribution[i, 0])); // К сумме прибавляем произведение цены с поставкой
                            arrayDistribution[i, j] = arrayDistribution[i, j] + " | " + arrayDistribution[i, 0]; // В ячекй добавляем символ / и максимальную поставку
                            arrayDistribution[i, 0] = "0"; // Зануляем предложение, так как у данного поставщика закончилась продукция
                            arrayDistribution[0, j] = "0"; // Зануляем спрос, так как весь спрос удовлетворён
                                           // Переходим на новую строку на тот же магазин
                            if (i != i - 1)
                            {
                                i++;
                                j--;
                            }
                        }
                    }
                    else //// добавлены фигурные скобки (требование использовать конструкию if с фигурными скобками)
                    {
                        i++;
                    }
                }
            }
        }
        return summaObjectiveFunction;
    }
}