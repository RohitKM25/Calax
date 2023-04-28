using Calax.UWP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Calax.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InitialiseDialog : Page
    {
        public InitialiseDialogModel ViewModel { get; set; }
        public InitialiseDialog()
        {
            this.InitializeComponent();
            ViewModel = new InitialiseDialogModel();
        }

        private async void _slabSetsFolderChooseFolderButton_Click(object sender, RoutedEventArgs e)
        {

            var folderPicker = new Windows.Storage.Pickers.FolderPicker();
            folderPicker.FileTypeFilter.Add("*");

            StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                var token = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(folder);
                FSManager.SetSlabSetsFolderSetting(token);
            }
            var storageItem = await Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFolderAsync(FSManager.GetSlabSetsFolderTokenSetting());
            ViewModel.SlabSetsFolderPath = storageItem.Path;
        }
    }
}
