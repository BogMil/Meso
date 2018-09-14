using System.Windows.Input;
using Meso.ViewModels;

namespace Meso.Models
{
    public class PaginationDataContext : BaseViewModel
    {
        private string _currentRecordsText = "1-10";
        public string CurrentRecordsText
        {
            get => _currentRecordsText;
            set
            {
                _currentRecordsText = value;
                OnPropertyChanged();
            }
        }

        public ICommand GetNextRecordSetCommand { get; set; }

    }
}
