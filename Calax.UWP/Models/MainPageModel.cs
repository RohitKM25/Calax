using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Calax.UWP.Models
{
    public class MainPageModel : PageModelBase
    {
        private string _navigationHeader { get; set; }
        public string NavigationHeader
        {
            get => _navigationHeader;

            set
            {
                _navigationHeader = value;
                _notifyPropertyChanged();
            }
        }
    }
}
