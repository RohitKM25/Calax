using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;

namespace Calax.Desktop.Contracts.Services;
public  interface ISlabSetsFolderPathSettingsService
{
    string SlabSetsFolderPath
    {
    get;
    }

    Task InitializeAsync();

    Task SetSlabSetsFolderPathAsync(string path);
}
