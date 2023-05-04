using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calax.Desktop.Core.Models;

namespace Calax.Desktop.Contracts.Services;
public interface IFileDataService
{
    Task InitializeAsync();
    Task<IEnumerable<SlabSet>> GetSlabSetsFromFolder();

    Task SaveSlabSetsToFolder(IEnumerable<SlabSet> slabSets);

    Task AddNewSlabSetToFolder(SlabSet slabSet);
}
