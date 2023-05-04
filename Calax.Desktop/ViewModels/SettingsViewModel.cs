using System.Reflection;
using System.Windows.Input;

using Calax.Desktop.Contracts.Services;
using Calax.Desktop.Helpers;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.UI.Xaml;

using Windows.ApplicationModel;

namespace Calax.Desktop.ViewModels;

public class SettingsViewModel : ObservableRecipient
{
    private readonly IThemeSelectorService _themeSelectorService;
    private readonly ISlabSetsFolderPathSettingsService _slabSetsFolderPathSettingsService;
    private ElementTheme _elementTheme;
    private string _slabSetsFolder;
    private string _versionDescription;

    public ElementTheme ElementTheme
    {
        get => _elementTheme;
        set => SetProperty(ref _elementTheme, value);
    }

    public string SlabSetsFolder
    {
        get => _slabSetsFolder;
        set => SetProperty(ref _slabSetsFolder, value);
    }

    public string VersionDescription
    {
        get => _versionDescription;
        set => SetProperty(ref _versionDescription, value);
    }

    public ICommand SwitchThemeCommand
    {
        get;
    }

    public SettingsViewModel(IThemeSelectorService themeSelectorService,ISlabSetsFolderPathSettingsService slabSetsFolderPathSettingsService)
    {
        _themeSelectorService = themeSelectorService;
        _slabSetsFolderPathSettingsService = slabSetsFolderPathSettingsService;
        _elementTheme = _themeSelectorService.Theme;
        _slabSetsFolder = _slabSetsFolderPathSettingsService.SlabSetsFolderPath;
        _versionDescription = GetVersionDescription();

        SwitchThemeCommand = new RelayCommand<ElementTheme>(
            async (param) =>
            {
                if (ElementTheme != param)
                {
                    ElementTheme = param;
                    await _themeSelectorService.SetThemeAsync(param);
                }
            });
    }

    private static string GetVersionDescription()
    {
        Version version;

        if (RuntimeHelper.IsMSIX)
        {
            var packageVersion = Package.Current.Id.Version;

            version = new(packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision);
        }
        else
        {
            version = Assembly.GetExecutingAssembly().GetName().Version!;
        }

        return $"{"AppDisplayName".GetLocalized()} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
    }
}
