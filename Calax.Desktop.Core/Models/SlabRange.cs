using Newtonsoft.Json;

namespace Calax.Desktop.Core.Models;

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
    public double Difference => (End ?? double.PositiveInfinity) - Start;
    [JsonIgnore]
    public string Display => $"{Start / 100000}-{End / 100000}";
}

