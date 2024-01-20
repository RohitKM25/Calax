using System.Collections.ObjectModel;
using Calax.Desktop.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Calax.Desktop.ViewModels;

public class NewSlabSetViewModel : ObservableRecipient
{
    private string _forYear;
    public string ForYear
    {
        get => _forYear;
        set => SetProperty(ref _forYear, value);
    }
    private string _regime;
    public string Regime
    {
        get => _regime;
        set => SetProperty(ref _regime, value);
    }
    public ObservableCollection<Slab> Slabs { get; set; } = new ObservableCollection<Slab>() { new Slab(new SlabRange(0, 5), .05), new Slab(new SlabRange(5, 10), .1) };
    public NewSlabSetViewModel()
    {
    }

    public void AddSlab()
    {
        Slabs.Add(new Slab());
    }
}
