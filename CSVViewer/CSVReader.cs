
using System;
using System.IO;

class CSVReader
{
    public static void ReadCSV(string filePath)
    {
        // Открываем файл для чтения
        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            // Читаем файл построчно
            while ((line = reader.ReadLine()) != null)
            {
                // Разделяем строку на значения, разделитель может быть ';' или ','
                string[] values = line.Split(';'); // Например, ';' если это разделитель в CSV
                foreach (var value in values)
                {
                    Console.Write(value + "\t"); // Выводим значения в консоль для проверки
                }
                Console.WriteLine(); // Переходим на новую строку для каждой строки файла
            }
        }
    }

    static void Pyth (string[] args)
    {
        // Указываем путь к CSV файлу
        string filePath = @"C:\Users\\user\Desktop\Csv.csv";
        ReadCSV(filePath); // Вызываем функцию для чтения файла
    }

}

