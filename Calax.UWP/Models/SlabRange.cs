using Newtonsoft.Json;
using Windows.UI.Xaml.Controls;

namespace Calax.UWP.Models
{
    public class SlabRange
    {
        public SlabRange(double start, double? end)
        {
            this.Start = start;
            this.End = end;
        }

        public double Start { get; set; }
        public double? End { get; set; }
        [JsonIgnore]
        public double Difference { get => (End ?? double.PositiveInfinity) - Start; }
        [JsonIgnore]
        public string Display { get => $"{Start / 100000}-{End / 100000}"; }
    }
}

