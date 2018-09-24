using GenericCSR;
using Meso.App_Start;
using Meso.Commands;
using Meso.Models;
using Meso.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using Meso.Views.Musterija;
using Unity;

namespace Meso.ViewModels
{
    public class MusterijeViewModel : BaseViewModel
    {
        public ICommand test { get; set; }
        public ICommand Open { get; set; }

        public MusterijeViewModel()
        {
            InitializeCommand = SetInitializeCommand();
            test = new CustomCommand(testtest);
            Open = new CustomCommand(open);
        }

        private void open(object o)
        {
            Window window = new Window
            {
                Title = "My User Control Dialog",
                Content = new MusterijaAddDialog()
            };

            window.ShowDialog();}

        private void testtest(object o)
        {
            
        }

        private List<MusterijaQueryDto> _musterije;
        public List<MusterijaQueryDto> Musterije
        {
            get => _musterije;
            set
            {
                _musterije = value;
                OnPropertyChanged();
            }
        }

        private MusterijaQueryDto _musterija;
        public MusterijaQueryDto Musterija
        {
            get => _musterija;
            set
            {
                _musterija = value;
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
            var service = UnityConfig.ContainerInstance.Resolve<IMusterijaService>();
            var data = service.GetJqGridData(new Pager(), null, new OrderByProperties());
            return data.ToList();
        }


        public ICommand InitializeCommand { get; set; }
        private ICommand SetInitializeCommand()
        {
            return new RelayCommand(async x => { await GetMusterijeAsync(); });
        }
    }


}
