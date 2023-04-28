using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calax.UWP.Models
{
    public class InitialiseDialogModel : PageModelBase
    {
        private string _slabSetsFolderPath { get; set; }
        public string SlabSetsFolderPath
        {
            get => _slabSetsFolderPath;

            set
            {
                _slabSetsFolderPath = value;
                _notifyPropertyChanged();
            }
        }
    }
}
