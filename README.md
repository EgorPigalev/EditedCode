# Программа для вычисления первоначального распределения по методу северо-заподного угла
Данная программа предназначена для вычисления суммы и поставок в транспортной задаче по методу северо-заподного угла. [Более детально о данном методе можно прочитать здесь.](http://galyautdinov.ru/post/metod-severo-zapadnogo-ugla) В качестве входной информации программа принимает .csv файл, далее происходит подсчёт суммы и распределения поставок, результат заносится в новый .csv файл. Для выбора файла в котором находятся входные данные, а также файла для сохранения результатов предусмотрены диалоговые окна. Процесс решения на экране не отображается.
## Начало работы
````C#
public int GetDistribution()
    {
        int summaObjectiveFunction = 0;
        for (int i = 1; i < arrayDistribution.GetLength(0); i++)
        {
            for (int j = 1; j < arrayDistribution.GetLength(1); j++)
            {
                if (Convert.ToInt32(arrayDistribution[0, j]) != 0)
                {
                    if (Convert.ToInt32(arrayDistribution[i, 0]) != 0)
                    {
                        if (Convert.ToInt32(arrayDistribution[i, 0]) > Convert.ToInt32(arrayDistribution[0, j]))
                        {
                            summaObjectiveFunction = summaObjectiveFunction + Convert.ToInt32(arrayDistribution[i, j]) * Convert.ToInt32(arrayDistribution[0, j]);
                            arrayDistribution[i, j] = arrayDistribution[i, j] + " | " + arrayDistribution[0, j];
                            arrayDistribution[i, 0] = Convert.ToString(Convert.ToInt32(arrayDistribution[i, 0]) - Convert.ToInt32(arrayDistribution[0, j]));
                            arrayDistribution[0, j] = "0";
                        }
                        else if (Convert.ToInt32(arrayDistribution[i, 0]) < Convert.ToInt32(arrayDistribution[0, j]))
                        {
                            summaObjectiveFunction = summaObjectiveFunction + (Convert.ToInt32(arrayDistribution[i, j]) * Convert.ToInt32(arrayDistribution[i, 0]));
                            arrayDistribution[i, j] = arrayDistribution[i, j] + " | " + arrayDistribution[i, 0];
                            arrayDistribution[0, j] = Convert.ToString(Convert.ToInt32(arrayDistribution[0, j]) - Convert.ToInt32(arrayDistribution[i, 0]));
                            arrayDistribution[i, 0] = "0";
                            if (i != i - 1)
                            {
                                i++;
                                j--;
                            }
                        }
                        else
                        {
                            summaObjectiveFunction = summaObjectiveFunction + (Convert.ToInt32(arrayDistribution[i, j]) * Convert.ToInt32(arrayDistribution[i, 0]));
                            arrayDistribution[i, j] = arrayDistribution[i, j] + " | " + arrayDistribution[i, 0];
                            arrayDistribution[i, 0] = "0";
                            arrayDistribution[0, j] = "0";
                            if (i != i - 1)
                            {
                                i++;
                                j--;
                            }
                        }
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }
        return summaObjectiveFunction;
    }
````
Для непосредственного начала работы следует скачать архивную копию данного проекта. Для этого необходимо проделать следующие действия:
+ Открыть репозиторий на сайте GitHub;
+ Нажать на кнопку "Code"
+ Выбрать пункт "Download ZIP"

![logo](https://github.com/EgorPigalev/EditedCode/blob/master/Progrqam/Image/download%20location.png)

### Необходимые условия
Для установики данного программного обеспечения необходимо наличие программы ***Microsoft Visual Studio***.
Для работы с данной прогораммой применяются следующие требования к системе:
| Параметр | Требования|
| --- | --- |
| Процессор | 64-разрядный процессор 1,8 ГГц или более |
| ОЗУ | не менее 2 ГБ |
| Жесткий диск | до 210 ГБ (минимум 800 МБ) |
| Видеоадаптер | минимальное разрешение 720p (1280 на 720 пикселей) |

**Visual Studio 2022** (последняя версия) поддерживается в следующих операционных системах x64-разрядных систем:
+ ***Windows 11*** версии 21H2 или более поздней: Домашняя, Pro, Pro для образовательных учреждений, Pro для рабочих станций, Корпоративная и для образовательных учреждений;
+ ***Windows 10*** версии 1909 или более поздних версий: Домашняя, Профессиональная, для образовательных учреждений и Корпоративная;
+ ***Windows Server 2022***: Standard и Datacenter;
+ ***Windows Server 2019***: Standard и Datacenter;
+ ***Windows Server 2016***: Standard и Datacenter.
### Установка
Для установки данного программного обеспечения необходимо необходимо проделать следующие действия:
1. Взять скаченую архивную копию программы
2. Разархивировать архив:
   + нажать ПКМ на архив;
   + выбрать нужный архиватор;
   + нажать распаковать в папку;
   
![logo](https://github.com/EgorPigalev/EditedCode/blob/master/Progrqam/Image/unzipping.png)

3. Запустить проект спомощью программы ***Microsoft Visual Studio***.

![logo](https://github.com/EgorPigalev/EditedCode/blob/master/Progrqam/Image/launch%20program.png)
## Авторы
Люди которые принимали участие в разработке проекта:
* **Пигалев Егор** - [PigalevEgor](https://github.com/EgorPigalev)
