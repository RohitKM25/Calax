using Newtonsoft.Json;
using System.Collections.Generic;

namespace Calax.Desktop.Core.Models;

public class SlabSet
{
    public SlabSet(string regime, string forYear, List<Slab> slabs)
    {
        Regime = regime;
        ForYear = forYear;
        Slabs = slabs;
    }

    public string Regime { get; set; }
    public string ForYear { get; set; }
    public List<Slab> Slabs { get; set; }
    [JsonIgnore]
    public string Display => $"{Regime} Regime FY {ForYear}";
    [JsonIgnore]
    public string StoredFileName
    {
        get; set;
    }
}
