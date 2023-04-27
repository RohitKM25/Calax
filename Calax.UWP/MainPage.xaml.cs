using Calax.UWP.Models;
using System;
using System.Collections.Generic;
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
        public MainPageViewModel ViewModel { get; set; }
        public MainPage()
        {
            this.InitializeComponent(); 
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
            Window.Current.SetTitleBar(AppTitleBar);

            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values.Clear();
            if (FSManager.GetSlabSetsFolderSetting() == "")
                FSManager.InitSettingsAndFolder().Wait();
            ViewModel = new MainPageViewModel(FSManager.GetDeducersFromFiles().Result, FSManager.GetSlabSetsFolderSetting());
        }

        private async void _slabSetsFolderChooseFolderButton_Click(object sender, RoutedEventArgs e)
        {
            var folderPicker = new Windows.Storage.Pickers.FolderPicker();
            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
            folderPicker.FileTypeFilter.Add("*");

            Windows.Storage.StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                FSManager.SetSlabSetsFolderSetting(folder.Path);
                this.ViewModel.SlabSetsFolderPath = folder.Path;
            }
        }
    }
}
