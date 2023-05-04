using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calax.Desktop.Contracts.Services;
using Calax.Desktop.Core.Contracts.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;

namespace Calax.Desktop.Services;
internal class SlabSetsFolderPathSettingsService : ISlabSetsFolderPathSettingsService
{
    public SlabSetsFolderPathSettingsService(ILocalSettingsService localSettingsService)
    {
        _localSettingsService = localSettingsService;
    }

    private const string SettingsKey = "SlabSetsFolderPath";
    public string SlabSetsFolderPath
    {
        get; set;
    } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"CalaxSlabSets");

    private readonly ILocalSettingsService _localSettingsService;

    private async Task<string> LoadSlabSetsFolderPathFromSettingsAsync()
    {
        var path = await _localSettingsService.ReadSettingAsync<string>(SettingsKey);

        return path?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CalaxSlabSets");
    }

    private async Task SaveSlabSetsFolderPathInSettingsAsync(string path)
    {
        await _localSettingsService.SaveSettingAsync(SettingsKey, path);
    }
    public async Task InitializeAsync()
    {
        SlabSetsFolderPath = await LoadSlabSetsFolderPathFromSettingsAsync();
        await Task.CompletedTask;
    }
    public async Task SetSlabSetsFolderPathAsync(string path)
    {
        SlabSetsFolderPath = path;

        await SaveSlabSetsFolderPathInSettingsAsync(path);
    }
}
