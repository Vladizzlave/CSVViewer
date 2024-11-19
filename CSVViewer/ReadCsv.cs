using System;
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
