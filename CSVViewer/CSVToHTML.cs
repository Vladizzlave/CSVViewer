using System;
using System.IO;

class CSVToHTML
{
    public static void ConvertCSVToHTML(string csvFilePath, string htmlFilePath)
    {
        // Проверяем, существует ли CSV файл
        if (File.Exists(csvFilePath))
        {
            // Открываем CSV файл для чтения
            using (StreamReader reader = new StreamReader(csvFilePath))
            // Открываем HTML файл для записи
            using (StreamWriter writer = new StreamWriter(htmlFilePath))
            {
                // Записываем начало HTML-документа и таблицы
                writer.WriteLine("<html><body><table border='1'>");

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    writer.WriteLine("<tr>"); // Начинаем новую строку таблицы
                    string[] values = line.Split(';'); // Или ',' если в CSV запятая
                    foreach (var value in values)
                    {
                        writer.WriteLine($"<td>{value}</td>"); // Добавляем ячейки
                    }
                    writer.WriteLine("</tr>"); // Закрываем строку таблицы
                }

                // Завершаем HTML документ
                writer.WriteLine("</table></body></html>");
            }

            Console.WriteLine("HTML файл успешно создан.");
        }
        else
        {
            Console.WriteLine("CSV файл не найден.");
        }
    }

    static void Main(string[] args)
    {
        string csvFilePath = @"C:\Users\user\Desktop\Csv.csv"; // Укажи путь к CSV
        string htmlFilePath = @"C:\Users\user\Desktop\Html.html"; // Путь к HTML
        ConvertCSVToHTML(csvFilePath, htmlFilePath); // Конвертируем CSV в HTML
    }
}