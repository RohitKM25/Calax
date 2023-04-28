using Newtonsoft.Json;
using System.Collections.Generic;

namespace Calax.UWP.Models
{
    public class SlabSet
    {
        public string StoredFileName { get; set; }
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
        public string Display { get => $"{Regime} Regime FY {ForYear}"; }
    }
}
