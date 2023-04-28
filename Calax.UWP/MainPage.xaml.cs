using Calax.UWP.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Calax.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPageModel ViewModel { get; set; }
        public readonly Dictionary<string, Type> Pages = new Dictionary<string, Type>() {
            { "Calculator",typeof(CalculatorPage)},
            { "Slab Sets",typeof(SlabSetsPage)},
            { "New Slab Set",typeof(NewSlabSetPage)},
        };
        public MainPage()
        {
            this.InitializeComponent();
            ViewModel = new MainPageModel();
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            Window.Current.SetTitleBar(AppTitleBar);
            if (FSManager.GetSlabSetsFolderTokenSetting() == "")
                _init();
            else
                SelectNavigationViewDefaultPage();
        }

        private void _mainNavigationView_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            var value = (args.SelectedItem as Microsoft.UI.Xaml.Controls.NavigationViewItem).Content as string;
            if (!args.IsSettingsSelected && Pages.ContainsKey(value))
            {
                NavigationViewContentFrame.Navigate(Pages[value]);
                ViewModel.NavigationHeader = value;
            }
        }
        private void _navigationViewContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        //private async void _slabSetsFolderChooseFolderButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var folderPicker = new Windows.Storage.Pickers.FolderPicker();
        //    folderPicker.FileTypeFilter.Add("*");

        //    StorageFolder folder = await folderPicker.PickSingleFolderAsync();
        //    if (folder != null)
        //    {
        //        var token = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(folder);
        //        FSManager.SetSlabSetsFolderSetting(token);
        //    }
        //}

        private async void _init()
        {
            ContentDialog dialog = new ContentDialog();

            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            dialog.XamlRoot = this.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "Choose Tax Slabs Folder";
            dialog.PrimaryButtonText = "Ok";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new InitialiseDialog();

            var result = await dialog.ShowAsync();
        tryagain:
            if (!(result == ContentDialogResult.Primary && FSManager.GetSlabSetsFolderTokenSetting() != null))
            {
                dialog.Title = "REQUIRED: Choose Tax Slabs Folder";
                result = await dialog.ShowAsync();
                goto tryagain;
            }
            SelectNavigationViewDefaultPage();
        }

        private void SelectNavigationViewDefaultPage()
        {

            MainNavigationView.SelectedItem = MainNavigationView.MenuItems.ToList().Find((ni) => (ni as Microsoft.UI.Xaml.Controls.NavigationViewItem).Tag as string == "Calculator");
        }

    }
}
