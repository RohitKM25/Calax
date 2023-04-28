using Newtonsoft.Json;

namespace Calax.UWP.Models
{
    public class Slab
    {
        public Slab(SlabRange range, double percent)
        {
            Range = range;
            Percent = percent;
        }

        public SlabRange Range { get; set; }
        public double Percent { get; set; }
        [JsonIgnore]
        public string PercentDisplay { get => $"{Percent*100}%"; }
    }
}
