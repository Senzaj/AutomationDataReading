using System.Windows;
using System.Windows.Controls;
using AutomationDataReading.Models;
using AutomationDataReading.Models.Excel;
using AutomationDataReading.Models.Sql;
using AutomationDataReading.Models.Xml;
using AutomationDataReading.Services;
using AutomationDataReading.Services.Interfaces;

namespace AutomationDataReading;

public partial class MainWindow : Window
{
    private readonly IExcelReader<UserWorkloadRecord> _excelReader = new ExcelService();
    private readonly IXmlReader<ActionRecord> _xmlReader = new XmlService();
    private readonly ISqlReader<ProfileRecord> _sqlProfilesReader = new SqlService();
    
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void loadExcel_OnClick(object sender, RoutedEventArgs e)
    {
        var records = await _excelReader.GetList();
        ExelRecordsGrid.ItemsSource = records;
        SwitchTable(SourceType.Excel);
    }

    private async void LoadXml_OnClick(object sender, RoutedEventArgs e)
    {
        var records = await _xmlReader.GetList();
        XmlRecordsGrid.ItemsSource = records;
        SwitchTable(SourceType.Xml);
    }

    private async void LoadSql_OnClick(object sender, RoutedEventArgs e)
    {
        var records = await _sqlProfilesReader.GetList();
        SqlRecordsGrid.ItemsSource = records;
        SwitchTable(SourceType.Sql);
    }

    private void SearchField_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        var textBox = (TextBox)sender;
        
    }

    private void Filters_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var comboBox = (ComboBox)sender;
        
    }

    private void SwitchTable(SourceType tableType)
    {
        switch (tableType)
        {
            case SourceType.Excel:
                ExelRecordsGrid.Visibility = Visibility.Visible;
                XmlRecordsGrid.Visibility = Visibility.Collapsed;
                SqlRecordsGrid.Visibility = Visibility.Collapsed;
                break;
            case SourceType.Xml:
                XmlRecordsGrid.Visibility = Visibility.Visible;
                ExelRecordsGrid.Visibility = Visibility.Collapsed;
                SqlRecordsGrid.Visibility = Visibility.Collapsed;
                break;
            case SourceType.Sql:
                SqlRecordsGrid.Visibility = Visibility.Visible;
                ExelRecordsGrid.Visibility = Visibility.Collapsed;
                XmlRecordsGrid.Visibility = Visibility.Collapsed;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(tableType), tableType, null);
        }
    }
}