using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Calax.UWP.Models
{
    public class SlabSetsPageModel : PageModelBase
    {
        private IEnumerable<SlabSet> _slabSets { get; set; }
        public IEnumerable<SlabSet> SlabSets
        {
            get => _slabSets;

            set
            {
                _slabSets = value;
                _notifyPropertyChanged();
            }
        }
        private SlabSet _selectSlabSet { get; set; }
        public SlabSet SelectedSlabSet
        {
            get => _selectSlabSet;

            set
            {
                _selectSlabSet = value;
                _notifyPropertyChanged();
            }
        }
    }
}
