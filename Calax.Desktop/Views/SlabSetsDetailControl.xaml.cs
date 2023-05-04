using Calax.Desktop.Core.Models;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Calax.Desktop.Views;

public sealed partial class SlabSetsDetailControl : UserControl
{
    public SlabSet? ListDetailsMenuItem
    {
        get => GetValue(ListDetailsMenuItemProperty) as SlabSet;
        set => SetValue(ListDetailsMenuItemProperty, value);
    }

    public static readonly DependencyProperty ListDetailsMenuItemProperty = DependencyProperty.Register("ListDetailsMenuItem", typeof(SlabSet), typeof(SlabSetsDetailControl), new PropertyMetadata(null, OnListDetailsMenuItemPropertyChanged));

    public SlabSetsDetailControl()
    {
        InitializeComponent();
    }

    private static void OnListDetailsMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is SlabSetsDetailControl control)
        {
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}
