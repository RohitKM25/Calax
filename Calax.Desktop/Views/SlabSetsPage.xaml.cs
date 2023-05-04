using Calax.Desktop.ViewModels;

using CommunityToolkit.WinUI.UI.Controls;

using Microsoft.UI.Xaml.Controls;

namespace Calax.Desktop.Views;

public sealed partial class SlabSetsPage : Page
{
    public SlabSetsViewModel ViewModel
    {
        get;
    }

    public SlabSetsPage()
    {
        ViewModel = App.GetService<SlabSetsViewModel>();
        InitializeComponent();
    }

    private void OnViewStateChanged(object sender, ListDetailsViewState e)
    {
        if (e == ListDetailsViewState.Both)
        {
            ViewModel.EnsureItemSelected();
        }
    }
}
