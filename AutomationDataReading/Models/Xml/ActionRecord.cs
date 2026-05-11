namespace AutomationDataReading.Models.Xml;

public class ActionRecord
{
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public SourceType Source {get; set; } 
}