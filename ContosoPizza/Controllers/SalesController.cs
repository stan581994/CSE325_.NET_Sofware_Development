using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class SalesController : ControllerBase
{
    public SalesController()
    {
    }

    // GET all sales records
    [HttpGet]
    public ActionResult<List<SalesData>> GetAllSales() =>
        SalesService.GetAllSales();

    // GET total sales amount
    [HttpGet("total")]
    public ActionResult<decimal> GetTotalSales() =>
        SalesService.GetTotalSales();

    // GET sales summary report as text
    [HttpGet("summary")]
    public ActionResult<string> GetSalesSummaryReport()
    {
        var report = SalesService.GenerateSalesSummaryReport();
        return Content(report, "text/plain");
    }

    // POST to generate and save sales summary report to file
    [HttpPost("generate-report")]
    public async Task<ActionResult<string>> GenerateAndSaveSalesSummaryReport([FromBody] string? filePath = null)
    {
        var fileName = filePath ?? "sales_summary_report.txt";
        var result = await SalesService.GenerateAndSaveSalesSummaryReport(fileName);
        return Ok(result);
    }

    // POST to add a new sales record
    [HttpPost]
    public IActionResult AddSalesRecord(SalesData salesData)
    {
        if (string.IsNullOrEmpty(salesData.FileName))
            return BadRequest("FileName is required");

        if (salesData.Amount <= 0)
            return BadRequest("Amount must be greater than 0");

        SalesService.AddSalesRecord(salesData);
        return CreatedAtAction(nameof(GetAllSales), salesData);
    }
}
