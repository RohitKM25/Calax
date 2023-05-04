using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calax.Desktop.Core.Models;

namespace Calax.Desktop.Core.Contracts.Services;
public interface IDataService
{
    void SetSlabSets(IEnumerable<SlabSet> slabSets);
    IEnumerable<SlabSet> GetSlabSets();
    IEnumerable<string> GetAvailableForYears();
    IEnumerable<string> GetAvailableForYearRegimes(string forYear);
}
