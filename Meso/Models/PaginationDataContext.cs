using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Meso.ViewModels;

namespace Meso.Models
{
    public class PaginationDataContext : BaseViewModel
    {
        //public delegate Task GetRecordsEventHandler();

        public event EventHandler GetRecordsEvent;

        public PaginationDataContext()
        {

        }

        public ICommand GetNextRecordSetCommand { get; set; }

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

        public List<int> PageSizes { get; set; } = new List<int>() {1,2,10,20,50,100};

        private int _selectedPageSize = 2;
        public int SelectedPageSize
        {
            get
            {
                return _selectedPageSize;
            }

            set
            {
                _selectedPageSize = value;
                GetRecordsEvent.Invoke(this,null);
                OnPropertyChanged();
            }
        }

        private int _currentPage = 1;
        public int CurrentPage  
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        public string SortColumn{ get; set; }
        public string SortOrder{ get; set; }
    }
}
