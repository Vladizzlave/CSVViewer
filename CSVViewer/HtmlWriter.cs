using System;
using System.Text;
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
