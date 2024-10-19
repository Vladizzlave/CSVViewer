using System;
using System.IO;

class CSVToHTML
{
    public static void ConvertCSVToHTML(string csvFilePath, string htmlFilePath)
    {
        
        if (File.Exists(csvFilePath))
        {
           
            using (StreamReader reader = new StreamReader(csvFilePath))
            
            using (StreamWriter writer = new StreamWriter(htmlFilePath))
            {
                
                writer.WriteLine("<html><body><table border='1'>");

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    writer.WriteLine("<tr>"); 
                    string[] values = line.Split(';'); 
                    foreach (var value in values)
                    {
                        writer.WriteLine($"<td>{value}</td>"); 
                    }
                    writer.WriteLine("</tr>"); 
                }

                
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
        string csvFilePath = @"C:\Users\st53\Desktop\Csv.csv"; 
        string htmlFilePath = @"C:\Users\st53\Desktop\Html.html"; 
        ConvertCSVToHTML(csvFilePath, htmlFilePath); 
    }
}