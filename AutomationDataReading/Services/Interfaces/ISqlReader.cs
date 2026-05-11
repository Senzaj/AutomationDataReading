namespace AutomationDataReading.Services.Interfaces;

public interface ISqlReader<T>
{
    public Task<List<T>> GetList();
}