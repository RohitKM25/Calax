using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Calax.UWP.Models
{
    public class CalculatorPageModel :PageModelBase
    {

        private ObservableCollection<Deducer> _deducers { get; set; }
        public ObservableCollection<Deducer> Deducers
        {
            get => _deducers;

            set
            {if (_deducers == value) return;
                _deducers = value;
                var currForYears = new ObservableCollection<string>(value.Select(d => d.SlabSet.ForYear).Distinct());
                if (!Helper.EnumerableEquals(ForYears, currForYears))
                    ForYears = currForYears;
                _notifyPropertyChanged();
            }
        }
        private ObservableCollection<string> _forYears { get; set; }
        public ObservableCollection<string> ForYears
        {
            get => _forYears;

            set
            {
                _forYears = value;
                IsForYearEnabled = value.Count > 0;
                _notifyPropertyChanged();
            }
        }
        private ObservableCollection<string> _regimes { get; set; }
        public ObservableCollection<string> Regimes
        {
            get => _regimes;

            set
            {
                _regimes = value;
                IsRegimesEnabled = value.Count > 0;
                _notifyPropertyChanged();
            }
        }
        private string _selectedForYear { get; set; }
        public string SelectedForYear
        {
            get => _selectedForYear;

            set

            { 
                if (_selectedForYear==value) return;
            _selectedForYear = value;
                SetCalculatedTax(TotalGrossIncome, TotalDeduction);
                var currRegimes = new ObservableCollection<string>(Deducers.Where(d => d.SlabSet.ForYear == value).Select(d => d.SlabSet.Regime));
                var b = !Helper.EnumerableEquals(Regimes, currRegimes);
                if (b)
                    Regimes = currRegimes;
                _notifyPropertyChanged();
            }
        }
        private string _selectedRegime { get; set; }
        public string SelectedRegime
        {
            get => _selectedRegime;

            set
            {
                if (_selectedRegime == value)  return;

                _selectedRegime = value;
                SetCalculatedTax(TotalGrossIncome, TotalDeduction);
                IsNumFieldsEnabled = SelectedRegime!=null;
                _notifyPropertyChanged();
            }
        }
        private Deducer GetDeducerFromSelections()
        {
            return SelectedForYear != null && SelectedForYear != "" && SelectedRegime != null && SelectedRegime != ""
                ? Deducers.ToList().Find(d=>d.SlabSet.Regime.ToLower()==SelectedRegime.ToLower()&&d.SlabSet.ForYear.ToLower()==SelectedForYear.ToLower())
                : null;
        }
        private void SetCalculatedTax(double gti, double td)
        {
            var tax = GetDeducerFromSelections()?.Deduce(gti, td);
            if (tax != null)
            {
                CalculatedTax = (double)tax;
            }
        }
        public void ClearFields()
        {
            SelectedForYear = null;
            SelectedRegime = null;
            TotalGrossIncome = 0;
            TotalDeduction = 0;
        }
        private double _totalGrossIncome { get; set; }
        public double TotalGrossIncome
        {
            get => _totalGrossIncome;

            set
            {
                _totalGrossIncome = value;
                SetCalculatedTax(value, TotalDeduction);
                _notifyPropertyChanged();
            }
        }
        private double _totalDeduction { get; set; }
        public double TotalDeduction
        {
            get => _totalDeduction;

            set
            {
                _totalDeduction = value;
                SetCalculatedTax(TotalGrossIncome, value);
                _notifyPropertyChanged();
            }
        }
        private double _calculatedTax { get; set; }
        public double CalculatedTax
        {
            get => _calculatedTax;

            set
            {
                _calculatedTax = value;
                _notifyPropertyChanged();
            }
        }
        private bool _isForYearEnabled { get; set; }
        public bool IsForYearEnabled
        {
            get => _isForYearEnabled;

            set
            {
                _isForYearEnabled = value;
                _notifyPropertyChanged();
            }
        }
        private bool _isRegimesEnabled { get; set; }
        public bool IsRegimesEnabled
        {
            get => _isRegimesEnabled;

            set
            {
                _isRegimesEnabled = value;
                _notifyPropertyChanged();
            }
        }
        private bool _isNumFieldsEnabled { get; set; }
        public bool IsNumFieldsEnabled
        {
            get => _isNumFieldsEnabled;

            set
            {
                _isNumFieldsEnabled = value;
                _notifyPropertyChanged();
            }
        }
    }
}
