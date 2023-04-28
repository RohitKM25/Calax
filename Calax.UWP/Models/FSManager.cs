using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;

namespace Calax.UWP.Models
{
    public static class FSManager
    {
        public static string GetSlabSetsFolderTokenSetting()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            return localSettings.Values.Keys.Contains("SlabSets Folder Token") ? localSettings.Values["SlabSets Folder Token"] as string : "";
        }

        public static async void SetSlabSetsFolderSetting(string currFolderToken)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            string prevToken = localSettings.Values["SlabSets Folder Token"] as string;
            localSettings.Values["SlabSets Folder Token"] = currFolderToken;
            var currStorageItem = await Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFolderAsync(currFolderToken);
            if (prevToken == null)
            {
                localSettings.Values["SlabSets Folder Token"] = currFolderToken;
                InitSettingsAndFolder();
            }
            else
            {
                var prevStorageItem = await Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFolderAsync(prevToken);
                foreach (var prevStorageFile in (await prevStorageItem.GetFilesAsync()))
                {
                    await prevStorageFile.CopyAsync(currStorageItem);
                    await prevStorageFile.DeleteAsync();
                }
            }
        }

        public static async void InitSettingsAndFolder()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            var oldregimefy2023to24 = new SlabSet("Old", "2023-24", new List<Slab>() {
            new Slab(new SlabRange(0,250000),0),
            new Slab(new SlabRange(250000,500000),.05),
            new Slab(new SlabRange(500000,1000000),.20),
            new Slab(new SlabRange(1000000,null),.30)
        });
            var newregimefy2023to24 = new SlabSet("New", "2023-24", new List<Slab>() {
            new Slab(new SlabRange(0,300000),0),
            new Slab(new SlabRange(300000,600000),.05),
            new Slab(new SlabRange(600000,900000),.10),
            new Slab(new SlabRange(900000,1200000),.15),
            new Slab(new SlabRange(1200000,1500000),.20),
            new Slab(new SlabRange(1500000,null),.30),
        });
            var oldregimefy2023to24Json = JsonConvert.SerializeObject(oldregimefy2023to24);
            var newregimefy2023to24Json = JsonConvert.SerializeObject(newregimefy2023to24);
            var folderToken = localSettings.Values["SlabSets Folder Token"] as string;
            var storageItem = await Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFolderAsync(folderToken);

            var old2324 = await storageItem.CreateFileAsync("Old 2023-24.json");
            var new2324 = await storageItem.CreateFileAsync("New 2023-24.json");

            var oldregimefy2023to24Buffer = Windows.Security
                .Cryptography
                .CryptographicBuffer
                .ConvertStringToBinary(
                    oldregimefy2023to24Json, 
                    Windows.Security.Cryptography.BinaryStringEncoding.Utf8
                );
            var newregimefy2023to24Buffer = Windows.Security.
                Cryptography.
                CryptographicBuffer.
                ConvertStringToBinary(
                    newregimefy2023to24Json, 
                    Windows.Security.Cryptography.BinaryStringEncoding.Utf8
                );

            await FileIO.WriteBufferAsync(old2324, oldregimefy2023to24Buffer);
            await FileIO.WriteBufferAsync(new2324, newregimefy2023to24Buffer);
        }

        public static async Task<List<SlabSet>> GetSlabSetsFromFiles()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            var folderToken = localSettings.Values["SlabSets Folder Token"] as string;
            var storageItem = await Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFolderAsync(folderToken);
            List<SlabSet> slabSets = new List<SlabSet>();
            foreach (var storageFile in await storageItem.GetFilesAsync())
            {
                string json = await FileIO.ReadTextAsync(storageFile);
                var slabSet = JsonConvert.DeserializeObject<SlabSet>(json);
                slabSet.StoredFileName = storageFile.Name;
                slabSets.Add(slabSet);
            }
            return slabSets;
        }

        public static async Task<List<Deducer>> GetDeducersFromFiles()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            var folderToken = localSettings.Values["SlabSets Folder Token"] as string;
            var storageItem = await Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFolderAsync(folderToken);
            List<Deducer> deducers = new List<Deducer>();
            foreach (var storageFile in await storageItem.GetFilesAsync())
            {
                string json = await FileIO.ReadTextAsync(storageFile);
                deducers.Add(JsonReducer.ReduceJson(json));
            }
            return deducers;
        }

        public static async void PickFolder()
        {
            var folderPicker = new Windows.Storage.Pickers.FolderPicker();
            folderPicker.FileTypeFilter.Add("*");

            StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                var token = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(folder);
                FSManager.SetSlabSetsFolderSetting(token);
            }
        }

        public static async Task<string> GetFolderPath()
        {
            return (await Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.GetFolderAsync(FSManager.GetSlabSetsFolderTokenSetting())).Path;
        }

        public static void ClearData()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Clear();
            localSettings.Values.Clear();
        }
    }
}
