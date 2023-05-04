using Calax.Desktop.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace Calax.Desktop.Views;

public sealed partial class NewSlabSetPage : Page
{
    public NewSlabSetViewModel ViewModel
    {
        get;
    }

    public NewSlabSetPage()
    {
        ViewModel = App.GetService<NewSlabSetViewModel>();
        InitializeComponent();
    }
}
