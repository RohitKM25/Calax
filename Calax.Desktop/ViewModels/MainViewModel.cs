using System.Collections.ObjectModel;
using System.Windows.Input;
using Calax.Desktop.Contracts.ViewModels;
using Calax.Desktop.Core.Contracts.Services;
using Calax.Desktop.Core.Models;
using Calax.Desktop.Services;
using Calax.Desktop.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Navigation;

namespace Calax.Desktop.ViewModels;

public class MainViewModel : ObservableRecipient
{
    public static readonly string OldVal = "Old";
    public static readonly string NewVal = "New";
    public static readonly string RegimesFieldName = "regimes";
    public static readonly string NumericalFieldName = "numfield";

    private readonly IDataService _dataService;
    public ObservableCollection<string> ForYears { get; private set; } = new ObservableCollection<string>();
    public List<SlabSet> SlabSets
    {
        get; private set;
    } = new List<SlabSet>();
    public ObservableCollection<string> Regimes { get; private set; } = new ObservableCollection<string>();

    private string _selectedForYear;
    public string SelectedForYear
    {
        get => _selectedForYear;
        set
        {
            var regimes = _dataService.GetAvailableForYearRegimes(value);
            Regimes.Clear();

            foreach (var item in regimes)
            {
                Regimes.Add(item);
            }
            if (Regimes.Count > 0)
            {
                SelectedRegime = Regimes[0];
            }
            SetProperty(ref _selectedForYear, value);
        }
    }
    private string _selectedRegime;
    public string SelectedRegime
    {
        get => _selectedRegime;
        set => SetProperty(ref _selectedRegime, value);
    }
    private double _totalGrossIncome;
    public double TotalGrossIncome
    {
        get => _totalGrossIncome;
        set => SetProperty(ref _totalGrossIncome, value);
    }
    private double _totalDeduction;
    public double TotalDeduction
    {
        get => _totalDeduction;
        set => SetProperty(ref _totalDeduction, value);
    }

    public MainViewModel(IDataService dataService)
    {
        _dataService = dataService;

        SetRegimeCommand = new RelayCommand<string>(
            (param) =>
            {
                if (SelectedRegime != param)
                {
                    SelectedRegime = param;
                }
            });
        ForYears.Clear();

        var foryears = _dataService.GetAvailableForYears();

        foreach (var item in foryears)
        {
            ForYears.Add(item);
        }
        Regimes.Clear();
        SlabSets = _dataService.GetSlabSets().ToList();
    }


    public ICommand SetRegimeCommand
    {
        get;
    }
    public static bool IsSelectedRegimeButton(string selectedRegime, string val)
    {
        return val == selectedRegime;
    }

    public static bool CheckFieldEnabled(string fieldName, object value)
    {
        return fieldName switch
        {
            "regimes" => (value as ObservableCollection<string>).Count > 0,
            "numfield" => (value as string) != null,
            _ => false,
        };
    }
}
