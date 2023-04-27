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
        public double Difference { get => (End ?? double.PositiveInfinity) - Start; }
    }
}

