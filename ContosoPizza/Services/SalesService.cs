using ContosoPizza.Models;
using System.Text;

namespace ContosoPizza.Services;

public static class SalesService
{
    private static readonly List<SalesData> SalesRecords = new()
    {
        new SalesData { FileName = "january_sales.txt", Amount = 15750.50m, Date = DateTime.Parse("2024-01-31") },
        new SalesData { FileName = "february_sales.txt", Amount = 18920.75m, Date = DateTime.Parse("2024-02-29") },
        new SalesData { FileName = "march_sales.txt", Amount = 22340.25m, Date = DateTime.Parse("2024-03-31") },
        new SalesData { FileName = "april_sales.txt", Amount = 19875.00m, Date = DateTime.Parse("2024-04-30") },
        new SalesData { FileName = "may_sales.txt", Amount = 25680.90m, Date = DateTime.Parse("2024-05-31") }
    };

    public static List<SalesData> GetAllSales() => SalesRecords;

    public static string GenerateSalesSummaryReport()
    {
        var totalSales = SalesRecords.Sum(s => s.Amount);
        var report = new StringBuilder();

        report.AppendLine("Sales Summary");
        report.AppendLine("----------------------------");
        report.AppendLine($" Total Sales: {totalSales:C}");
        report.AppendLine();
        report.AppendLine(" Details:");

        foreach (var sale in SalesRecords.OrderBy(s => s.Date))
        {
            report.AppendLine($"  {sale.FileName}: {sale.Amount:C}");
        }

        return report.ToString();
    }

    public static async Task<string> GenerateAndSaveSalesSummaryReport(string filePath = "sales_summary_report.txt")
    {
        var reportContent = GenerateSalesSummaryReport();
        
        try
        {
            await File.WriteAllTextAsync(filePath, reportContent);
            return $"Sales summary report saved to: {filePath}";
        }
        catch (Exception ex)
        {
            return $"Error saving report: {ex.Message}";
        }
    }

    public static void AddSalesRecord(SalesData salesData)
    {
        SalesRecords.Add(salesData);
    }

    public static decimal GetTotalSales() => SalesRecords.Sum(s => s.Amount);
}
