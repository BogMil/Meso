using GenericCSR;
using Meso.App_Start;
using Meso.Commands;
using Meso.Models;
using Meso.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Unity;
using static Meso.Models.PaginationDataContext;

namespace Meso.ViewModels
{
    public class MusterijeViewModel : BaseViewModel
    {
        public ICommand NextRecordSetCommand;
        public Action<object> GetNextRecordSet;

        public MusterijeViewModel()
        {
            InitializeCommand = SetInitializeCommand();

            PaginationContext = new PaginationDataContext
            {
                GetNextRecordSetCommand = GetNextRecordSetCommand()
            };

            PaginationContext.GetRecordsEvent += GetRecordsEventHandler();
        }

        private List<MusterijaQueryDto> _musterije;
        public List<MusterijaQueryDto> Musterije
        {
            get
            {
                return _musterije;
            }
            set
            {
                _musterije = value;
                OnPropertyChanged();
            }
        }

        public static async Task<MusterijeViewModel> CreateInstance()
        {
            var musterijeViewModel = new MusterijeViewModel();
            await musterijeViewModel.GetMusterijeAsync();
            return musterijeViewModel;
        }

        private async Task GetMusterijeAsync()
        {
            var getMusterijeTask = new Task<List<MusterijaQueryDto>>(GetMusterije);
            getMusterijeTask.Start();
            var data = await getMusterijeTask;
            Musterije = data.ToList();
        }

        public List<MusterijaQueryDto> GetMusterije()
        {
            var pager = new Pager() {
                CurrentPageNumber = PaginationContext.CurrentPage,
                NumberOfRowsToDisplay = PaginationContext.SelectedPageSize
            };

            OrderByProperties orderByProperties;

            if (PaginationContext.SortColumn != null)
                orderByProperties = new OrderByProperties()
                {
                    OrderByColumn = PaginationContext.SortColumn,
                    OrderDirection = PaginationContext.SortOrder == "Ascending" ? 
                        SortDirections.Ascending : SortDirections.Descending
                };
            else
                orderByProperties = new OrderByProperties();

            var service = UnityConfig.ContainerInstance.Resolve<IMusterijaService>();
            var data = service.GetJqGridData(pager, null, orderByProperties);
            return data.ToList();
        }

        private ICommand GetNextRecordSetCommand()
        {
            return new RelayCommand(x =>
            {
                PaginationContext.CurrentRecordsText = "test";
                MessageBox.Show("Test", "Test");
            });
        }

        public ICommand InitializeCommand { get; set; }
        private ICommand SetInitializeCommand()
        {
            return new RelayCommand(async x => { await GetMusterijeAsync(); });
        }
    
        public async void Sort (object sender,DataGridSortingEventArgs e)
        {
            e.Handled = true;

            PaginationContext.SortColumn = e.Column.SortMemberPath;
            PaginationContext.SortOrder = e.Column.SortDirection.ToString();
            
            await GetMusterijeAsync();

        }
        #region EventHandlers
        private EventHandler GetRecordsEventHandler()
        {
            return new EventHandler(async (o, ea) => await GetMusterijeAsync());
        }
        #endregion
    }


}
