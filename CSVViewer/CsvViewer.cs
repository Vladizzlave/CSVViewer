using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSVViewer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к CSV файлу:");
            string filePath = Console.ReadLine();

            // Проверка существования файла
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Не найден csv файл.");
                return;
            }

            // Чтение и валидация файла
            Validator validator = new Validator();
            List<string[]> rows = validator.ValidateAndLoadFile(filePath);
            if (rows == null)
            {
                Console.WriteLine("Ошибка при обработке CSV файла.");
                return;
            }

            string[] headers = rows[0]; // Первая строка — заголовки
            rows.RemoveAt(0); // Удаляем заголовки из списка строк

            // Настройка стилей
            HtmlWriter writer = new HtmlWriter();
            Console.WriteLine("Введите шрифт (по умолчанию Arial):");
            string font = Console.ReadLine();
            font = string.IsNullOrEmpty(font) ? "Arial" : font;

            Console.WriteLine("Введите размер шрифта (по умолчанию 14):");
            string fontSizeInput = Console.ReadLine();
            int fontSize = int.TryParse(fontSizeInput, out int parsedFontSize) ? parsedFontSize : 14;

            Console.WriteLine("Введите цвет текста в формате RGB (#000000 - черный):");
            string color = Console.ReadLine();
            color = string.IsNullOrEmpty(color) ? "#000000" : color;

            writer.SetStyles(font, fontSize, color);

            // Генерация HTML
            Console.WriteLine("Введите путь для сохранения HTML файла:");
            string outputFilePath = Console.ReadLine();

            string htmlContent = writer.CreateHtmlFile(headers, rows);

            // Сохранение HTML файла
            try
            {
                writer.SaveHtml(outputFilePath, htmlContent);
                Console.WriteLine("HTML файл успешно создан.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения файла: {ex.Message}");
            }
        }
    }

    class Validator
    {
        public List<string[]> ValidateAndLoadFile(string filePath)
        {
            try
            {
                var lines = File.ReadAllLines(filePath);
                if (lines.Length == 0)
                {
                    Console.WriteLine("Файл пуст.");
                    return null;
                }

                var headers = lines[0].Split(';');
                if (headers.Length == 0)
                {
                    Console.WriteLine("Отсутствуют заголовки.");
                    return null;
                }

                var rows = new List<string[]>();
                foreach (var line in lines)
                {
                    var values = line.Split(';');
                    if (values.Length != headers.Length)
                    {
                        Console.WriteLine($"Ошибка: строка '{line}' не соответствует структуре заголовков.");
                        return null;
                    }
                    rows.Add(values);
                }

                return rows;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
                return null;
            }
        }
    }

    class HtmlWriter
    {
        private string font = "Arial";
        private int fontSize = 14;
        private string color = "#000000";

        public void SetStyles(string font, int fontSize, string color)
        {
            this.font = font;
            this.fontSize = fontSize;
            this.color = color;
        }

        public string CreateHtmlFile(string[] headers, List<string[]> rows)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<style>");
            sb.AppendLine($"table {{ font-family: {font}; font-size: {fontSize}px; color: {color}; border-collapse: collapse; width: 100%; }}");
            sb.AppendLine("th, td { border: 1px solid #dddddd; text-align: left; padding: 8px; }");
            sb.AppendLine("tr:nth-child(even) { background-color: #f2f2f2; }");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<table>");

            // Генерация заголовков
            sb.AppendLine("<tr>");
            foreach (var header in headers)
            {
                sb.AppendLine($"<th>{header}</th>");
            }
            sb.AppendLine("</tr>");

            // Генерация строк данных
            foreach (var row in rows)
            {
                sb.AppendLine("<tr>");
                foreach (var cell in row)
                {
                    sb.AppendLine($"<td>{cell}</td>");
                }
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</table>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        }

        public void SaveHtml(string filePath, string content)
        {
            File.WriteAllText(filePath, content, Encoding.UTF8);
        }
    }
}