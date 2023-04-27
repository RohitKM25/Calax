using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calax.UWP.Models
{
    public class MainPageViewModel
    {
        private ObservableCollection<Deducer> _deducers = new ObservableCollection<Deducer>();
        private ObservableCollection<string> _fys = new ObservableCollection<string>();

        public MainPageViewModel(List<Deducer> deducers, string slabSetsFolderPath)
        {
            _deducers = new ObservableCollection<Deducer>(deducers);
            _fys = new ObservableCollection<string>(_deducers.Select((d)=>d.SlabSet.ForYear).Distinct());
            SlabSetsFolderPath = slabSetsFolderPath;
        }

        public ObservableCollection<Deducer> Deducers { get { return this._deducers; } }
        public ObservableCollection<string> FYs { get { return this._fys; } }
        public string SlabSetsFolderPath { get; set; }
    }
}
