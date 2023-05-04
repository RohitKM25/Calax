using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calax.Desktop.Contracts.Services;
using Calax.Desktop.Core.Contracts.Services;
using Calax.Desktop.Core.Models;

namespace Calax.Desktop.Services;
public class FileDataService : IFileDataService
{
    private readonly IFileService _fileService;
    private readonly IDataService _dataService;
    private readonly ISlabSetsFolderPathSettingsService _slabSetsFolderPathSettingsService;
    public FileDataService(IFileService fileService,ISlabSetsFolderPathSettingsService slabSetsFolderPathSettingsService,IDataService dataService)
    {
        _slabSetsFolderPathSettingsService = slabSetsFolderPathSettingsService ;
        _fileService = fileService;
        _dataService = dataService;
    }
    private async void _initializeData()
    {
       var slabSets = new List<SlabSet>() {
        new SlabSet("Old", "2023-24", new List<Slab>() {
            new Slab(new SlabRange(0,250000),0),
            new Slab(new SlabRange(250000,500000),.05),
            new Slab(new SlabRange(500000,1000000),.20),
            new Slab(new SlabRange(1000000,null),.30)
        }),new SlabSet("New", "2023-24", new List<Slab>() {
            new Slab(new SlabRange(0,300000),0),
            new Slab(new SlabRange(300000,600000),.05),
            new Slab(new SlabRange(600000,900000),.10),
            new Slab(new SlabRange(900000,1200000),.15),
            new Slab(new SlabRange(1200000,1500000),.20),
            new Slab(new SlabRange(1500000,null),.30),
        })};
        await SaveSlabSetsToFolder(slabSets);
        //Todo: implement saveslabsetstofolder and data init
    }

    public Task AddNewSlabSetToFolder(SlabSet slabSet) => throw new NotImplementedException();
    public async Task<IEnumerable<SlabSet>> GetSlabSetsFromFolder()
    {
        var slabSets = new List<SlabSet>();
        foreach (var filePath in Directory.GetFiles(_slabSetsFolderPathSettingsService.SlabSetsFolderPath))
        {
            SlabSet? slabSet = null;
            await Task.Run(() => {
                slabSet = _fileService.Read<SlabSet>(_slabSetsFolderPathSettingsService.SlabSetsFolderPath, filePath);
                slabSet.StoredFileName = filePath;
            });
            if (slabSet != null) slabSets.Add(slabSet);
        }
        return slabSets;
    }
    public async Task SaveSlabSetsToFolder(IEnumerable<SlabSet> slabSets)
    {
        foreach (var slabSet in slabSets)
        {
            if (slabSet.StoredFileName == null) continue;
            await Task.Run(() =>
            {
                _fileService.Save(Path.GetDirectoryName(slabSet.StoredFileName), Path.GetFileName(slabSet.StoredFileName), slabSet);
            });
        }
    }

    public async Task InitializeAsync()
    {
        _dataService.SetSlabSets(await GetSlabSetsFromFolder());
    }
}
