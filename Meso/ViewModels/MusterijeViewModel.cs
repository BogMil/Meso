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
using System.Windows.Input;
using Unity;

namespace Meso.ViewModels
{
    public class MusterijeViewModel : BaseViewModel
    {
        public ICommand NextRecordSetCommand;
        public Action<object> GetNextRecordSet;

        public MusterijeViewModel()
        {
            InitializeCommand = Initialize();
            PaginationContext = new PaginationDataContext
            {
                GetNextRecordSetCommand = GetNextRecordSetCommand()
            };
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
            var x = await getMusterijeTask;
            Musterije = x.ToList();
        }

        public List<MusterijaQueryDto> GetMusterije()
        {
            var service = UnityConfig.ContainerInstance.Resolve<IMusterijaService>();
            var data = service.GetJqGridData(new Pager(), null, new OrderByProperties());
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
        private ICommand Initialize()
        {
            return new RelayCommand(async x => { await GetMusterijeAsync(); });
        }
    }


}
