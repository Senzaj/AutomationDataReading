namespace AutomationDataReading.Services.Interfaces;

public interface IExcelReader<T>
{
    public Task<List<T>> GetList();
}