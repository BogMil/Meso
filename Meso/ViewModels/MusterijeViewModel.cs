using GenericCSR;
using Meso.App_Start;
using Meso.Commands;
using Meso.Models;
using Meso.Services;
using Meso.Services.Interfaces;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Unity;

namespace Meso.ViewModels
{
    class MusterijeViewModel : BaseViewModel
    {
        private List<MusterijaQueryDto> _Musterije { get; set; }
        public List<MusterijaQueryDto> Musterije
        {
            get
            {
                return _Musterije;
            }
            set
            {
                _Musterije = value;
                OnPropertyChanged();
            }
        }

        public PaginationDataContext dc { get; set; } = new PaginationDataContext();

        public List<MusterijaQueryDto> GetMusterije()
        {
            var service = UnityConfig.ContainerInstance.Resolve<IMusterijaService>();
            var data=service.GetJqGridData(new Pager(), null, new OrderByProperties());
            return data.ToList();
        }

        public static async Task<MusterijeViewModel> CreateInstance()
        {
            var x = new MusterijeViewModel();
            await x.GetMusterijeAsync();
            return x;
        } 

        private async Task GetMusterijeAsync()
        {
            Task<List<MusterijaQueryDto>> task = new Task<List<MusterijaQueryDto>>(GetMusterije);
            task.Start();
            var x = await task;
            Musterije = x.ToList();
        }

    }

    public class PaginationDataContext
    {
        public string Text { get; set; } = "iz drugog";
        public ICommand testCommand { get; set; } = new RelayCommand(x=> MessageBox.Show("Test","Test"));

    }
}
