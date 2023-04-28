

namespace Calax.UWP.Models
{
    public class Deducer
    {
        public SlabSet SlabSet { get; set; }

        public Deducer(SlabSet slabSet)
        {
            SlabSet = slabSet;
        }

        public double Deduce(double gti, double td)
        {
            double currentGTI = gti-td;
            double calculatedTax = 0;
            bool isExceded = false;
            int slabIndex = 0;
            while (!isExceded)
            {
                if (currentGTI >= SlabSet.Slabs[slabIndex].Range.End)
                {
                    double deducedVal = SlabSet.Slabs[slabIndex].Range.Difference * SlabSet.Slabs[slabIndex].Percent;
                    calculatedTax += deducedVal;
                    currentGTI -= (double)SlabSet.Slabs[slabIndex].Range.Difference;
                    slabIndex++;
                }
                else
                {
                    calculatedTax += currentGTI * SlabSet.Slabs[slabIndex].Percent; isExceded = true;
                }
            }
            return calculatedTax;
        }
    }
}
