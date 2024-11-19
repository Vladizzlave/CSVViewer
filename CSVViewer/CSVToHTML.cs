using CSVViewer;
using System;
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
