using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AutomationDataReading.Models;
using AutomationDataReading.Services;
using AutomationDataReading.Services.Interfaces;

namespace AutomationDataReading;

public partial class MainWindow : Window
{
    private readonly IExcelReader<UserWorkloadRecord> _excelReader = new ExcelService();
    private readonly IXmlReader<ActionRecord> _xmlReader = new XmlService();
    
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void loadExcel_OnClick(object sender, RoutedEventArgs e)
    {
        var records = await _excelReader.GetList();
        RecordsGrid.ItemsSource = records;
    }

    private async void LoadXml_OnClick(object sender, RoutedEventArgs e)
    {
        var records = await _xmlReader.GetList();
        
    }

    private void LoadSql_OnClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("load_sql");
    }

    private void SearchField_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var textBox = (TextBox)sender;
        
    }

    private void Filters_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = (ComboBox)sender;
        
    }
}