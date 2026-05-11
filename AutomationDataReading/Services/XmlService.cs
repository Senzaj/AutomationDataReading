using System.Xml.Linq;
using AutomationDataReading.Models;
using AutomationDataReading.Models.Xml;
using AutomationDataReading.Services.Interfaces;

namespace AutomationDataReading.Services;

public class XmlService: IXmlReader<ActionRecord>
{
    private readonly string _filePath = 
        System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "sample_records.xml");
    
    public async Task<List<ActionRecord>> GetList()
    {
       return await Task.Run(GetRecordsAsync);
    }

    private Task<List<ActionRecord>> GetRecordsAsync()
    {
        try
        {
            var xmlDoc = XDocument.Load(_filePath);
            var records = xmlDoc.Root?.Elements("Record");
            return Task.FromResult(records!.Select(FormRecord).ToList());
        }
        catch (Exception exception)
        {
            return Task.FromException<List<ActionRecord>>(exception);
        } 
    }

    private ActionRecord FormRecord(XElement element)
    {
        return new ActionRecord
        {
            Title = element.Element("Title").Value,
            Category = element.Element("Category").Value,
            CreatedAt =  DateTime.Parse(element.Element("CreatedAt").Value),
            Source = SourceType.Xml
        };
    }
}