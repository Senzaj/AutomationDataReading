namespace AutomationDataReading.Services.Interfaces;

public interface IXmlReader<T>
{
    public Task<List<T>> GetList();
}