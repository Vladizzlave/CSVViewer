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
            CSVToHTML app = new CSVToHTML();
            app.Run();
        }
    }

    class CSVToHTML
    {
        public void Run()
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
            ReadCSV reader = new ReadCSV();

            if (!validator.ValidateFile(filePath))
            {
                Console.WriteLine("Ошибка при проверке CSV файла.");
                return;
            }

            var (headers, rows) = reader.LoadCSV(filePath);
            if (headers == null || rows == null)
            {
                Console.WriteLine("Ошибка при чтении CSV файла.");
                return;
            }

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

    class ReadCSV
    {
        public (string[] headers, List<string[]> rows) LoadCSV(string filePath)
        {
            try
            {
                var lines = File.ReadAllLines(filePath);
                if (lines.Length == 0)
                {
                    Console.WriteLine("Файл пуст.");
                    return (null, null);
                }

                var headers = lines[0].Split(';');
                var rows = new List<string[]>();

                for (int i = 1; i < lines.Length; i++)
                {
                    var values = lines[i].Split(';');
                    rows.Add(values);
                }

                return (headers, rows);
            }
            catch
            {
                Console.WriteLine("Ошибка чтения файла.");
                return (null, null);
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
            sb.AppendLine ("<style>");
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

    class Validator
    {
        public bool ValidateFile(string filePath)
        {
            try
            {
                var lines = File.ReadAllLines(filePath);
                if (lines.Length == 0)
                {
                    Console.WriteLine("Файл пуст.");
                    return false;
                }

                var headers = lines[0].Split(';');
                if (headers.Length == 0)
                {
                    Console.WriteLine("Отсутствуют заголовки.");
                    return false;
                }

                for (int i = 1; i < lines.Length; i++)
                {
                    var values = lines[i].Split(';');
                    if (values.Length != headers.Length)
                    {
                        Console.WriteLine($"Ошибка в строке {i + 1}: количество значений не соответствует заголовкам.");
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при проверке файла: {ex.Message}");
                return false;
            }
        }
    }
}