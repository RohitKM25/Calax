using System.Collections.ObjectModel;

using Calax.Desktop.Contracts.ViewModels;
using Calax.Desktop.Core.Contracts.Services;
using Calax.Desktop.Core.Models;

using CommunityToolkit.Mvvm.ComponentModel;

namespace Calax.Desktop.ViewModels;

public class SlabSetsViewModel : ObservableRecipient, INavigationAware
{
    private readonly IDataService _dataService;
    private SlabSet? _selected;

    public SlabSet? Selected
    {
        get => _selected;
        set => SetProperty(ref _selected, value);
    }

    public ObservableCollection<SlabSet> SlabSets { get; private set; } = new ObservableCollection<SlabSet>();

    public SlabSetsViewModel(IDataService dataService)
    {
        _dataService = dataService;
    }

    public void OnNavigatedTo(object parameter)
    {
        SlabSets.Clear();

        var data = _dataService.GetSlabSets();

        foreach (var item in data)
        {
            SlabSets.Add(item);
        }
    }

    public void OnNavigatedFrom()
    {
    }

    public void EnsureItemSelected()
    {
        Selected ??= SlabSets.First();
    }
}
