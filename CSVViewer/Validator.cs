using System;
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

