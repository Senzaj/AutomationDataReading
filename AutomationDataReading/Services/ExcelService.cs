using AutomationDataReading.Models;
using AutomationDataReading.Models.Excel;
using AutomationDataReading.Services.Interfaces;
using ClosedXML.Excel;

namespace AutomationDataReading.Services;

public class ExcelService: IExcelReader<UserWorkloadRecord>
{
    private readonly string _filePath = 
        System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "sample_worklogs.xlsx");

    public async Task<List<UserWorkloadRecord>> GetList()
    {
        return await Task.Run(GetRecordsAsync);
    }

    private Task<List<UserWorkloadRecord>> GetRecordsAsync()
    {
        try
        {
            using var workbook = new XLWorkbook(_filePath);
            var worksheet = workbook.Worksheet(1);
            return Task.FromResult(worksheet.RowsUsed().Skip(1).Select(FormRecord).ToList());
        }
        catch (Exception exception)
        {
            return Task.FromException<List<UserWorkloadRecord>>(exception);
        }
    }

    private UserWorkloadRecord FormRecord(IXLRow row)
    {
        var cells = row.CellsUsed().ToArray();

        return new UserWorkloadRecord
        {
            Name = cells[0].GetText(),
            Department = cells[1].GetText(),
            Hours = cells[2].GetDouble(),
            Date = cells[3].GetDateTime(),
            Source = SourceType.Excel
        };
    }
}