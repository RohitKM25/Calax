using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Calax.UWP.Models
{
    public static class FSManager
    {
        public static string GetSlabSetsFolderSetting()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            return localSettings.Values.Keys.Contains("SlabSets Folder Path") ? localSettings.Values["SlabSets Folder Path"] as string : "";
        }

        public static void SetSlabSetsFolderSetting(string currPath)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            var prevPath = localSettings.Values["SlabSets Folder Path"] as string;
            localSettings.Values["SlabSets Folder Path"] =currPath;
            foreach (var filePath in Directory.GetFiles(prevPath, "*.json"))
            {
                var newFilePath = Path.Combine(currPath,Path.GetFileName(filePath));
                File.Create(newFilePath).Close();
                File.Copy(filePath, newFilePath);
                File.Delete(filePath);
            }
        }

        public static async Task InitSettingsAndFolder()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["SlabSets Folder Path"] = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var oldregimefy2023to24 = new SlabSet("Old", "2023-24", new List<Slab>() {
            new Slab(new SlabRange(0,250000),0),
            new Slab(new SlabRange(250000,500000),.05),
            new Slab(new SlabRange(500000,1000000),.20),
            new Slab(new SlabRange(1000000,null),.30)
        });
            var newregimefy2023to24 = new SlabSet("New", "2023-24", new List<Slab>() {
            new Slab(new SlabRange(0,250000),0),
            new Slab(new SlabRange(250000,500000),.05),
            new Slab(new SlabRange(500000,1000000),.20),
            new Slab(new SlabRange(1000000,null),.30)
        });
            var oldregimefy2023to24Json = JsonConvert.SerializeObject(oldregimefy2023to24);
            var newregimefy2023to24Json = JsonConvert.SerializeObject(newregimefy2023to24);
            var folderPath = localSettings.Values["SlabSets Folder Path"] as string;
            await File.WriteAllTextAsync(Path.Combine(folderPath, "Old 2023-24.json"), oldregimefy2023to24Json);
            await File.WriteAllTextAsync(Path.Combine(folderPath, "New 2023-24.json"), newregimefy2023to24Json);
        }

        public static async Task<List<Deducer>> GetDeducersFromFiles()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            var folderPath = localSettings.Values["SlabSets Folder Path"] as string;
            List<Deducer> deducers = new List<Deducer>();
            foreach (var filePath in Directory.GetFiles(folderPath, "*.json"))
            {
                deducers.Add(JsonReducer.ReduceJson(await File.ReadAllTextAsync(filePath)));
            }
            return deducers;
        }
    }
}
