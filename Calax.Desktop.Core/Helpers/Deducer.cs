using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calax.Desktop.Core.Models;

namespace Calax.Desktop.Core.Helpers;
public static class Deducer
{
    public static string GetDeducedTax(IEnumerable<SlabSet> slabSets,string forYear, string regime, double totalGrossIncome, double totalDeduction)
    {
        var slabSet = GetSlabSetFromFYAndRegime(slabSets,forYear, regime);
        if (slabSet == null) return "0";
        var currentGTI = totalGrossIncome - totalDeduction;
        double calculatedTax = 0;
        var isExceded = false;
        var slabIndex = 0;
        while (!isExceded)
        {
            if (currentGTI >= slabSet.Slabs[slabIndex].Range.End)
            {
                var deducedVal = slabSet.Slabs[slabIndex].Range.Difference * slabSet.Slabs[slabIndex].Percent;
                calculatedTax += deducedVal;
                currentGTI -= (double)slabSet.Slabs[slabIndex].Range.Difference;
                slabIndex++;
            }
            else
            {
                calculatedTax += currentGTI * slabSet.Slabs[slabIndex].Percent;
                isExceded = true;
            }
        }
        return calculatedTax.ToString();
    }
    public static SlabSet GetSlabSetFromFYAndRegime(IEnumerable<SlabSet> slabSets, string forYear, string regime)
    {
        return slabSets.Where(s=>s.ForYear==forYear&&s.Regime==regime).FirstOrDefault();
    }
}
