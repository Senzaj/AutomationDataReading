namespace AutomationDataReading.Models;

public class UserWorkloadRecord
{
    public string Name { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public double Hours { get; set; }
    public DateTime Date { get; set; }
    public SourceType Source {get; set; } 
}