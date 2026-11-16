using System.Globalization;
using System.Text;

namespace Pos.Infrastructure.Exporters;

public class CsvFinalScoreExporter
{
    public string Export(string studentName, double finalScore)
    {
        var sb = new StringBuilder();
        sb.AppendLine("student,final_score");
        sb.AppendLine($"{Escape(studentName)},{finalScore.ToString(CultureInfo.InvariantCulture)}");
        return sb.ToString();
    }

    private static string Escape(string value)
    {
        if (value.Contains(',') || value.Contains('"') || value.Contains('\n'))
            return $"\"{value.Replace("\"", "\"\"")}\"";
        return value;
    }
}