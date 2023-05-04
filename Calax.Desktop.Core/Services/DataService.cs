using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calax.Desktop.Core.Contracts.Services;
using Calax.Desktop.Core.Models;

namespace Calax.Desktop.Core.Services;
public class DataService : IDataService
{
    private List<SlabSet> _slabSets;

    public DataService()
    {
        _slabSets = new List<SlabSet>();
    }

    public IEnumerable<string> GetAvailableForYearRegimes(string forYear)
    {
        return _slabSets.ToList().Where(s => s.ForYear==forYear).Select(s=>s.Regime);
    }
    public IEnumerable<string> GetAvailableForYears()
    {
        return _slabSets.ToList().Select(s => s.ForYear).Distinct();
    }
    public IEnumerable<SlabSet> GetSlabSets()
    {
        return _slabSets;
    }
    public void SetSlabSets(IEnumerable<SlabSet> slabSets) 
    {
        _slabSets = slabSets.ToList();
    }
}
