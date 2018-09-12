using Meso.Commands;
using Meso.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Meso.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        public ICommand ChangePage { get; set; }
        public ICommand SetMusterijePage { get; set; }

        public MainWindowViewModel()
        {
            ChangePage = new RelayCommand(setPage);
            SetMusterijePage = new RelayCommand(setMstrPage);
        }

        private Page _CurrentView { get; set; } = new Page2();
        public Page CurrentView
        {
            get
            {
                return _CurrentView;
            }
            set
            {
                _CurrentView = value;
                OnPropertyChanged();
            }
        }

        private void setPage(object page)
        {
            System.Uri resource = new System.Uri(@"Views\" + page + ".xaml", System.UriKind.RelativeOrAbsolute);
            CurrentView = Application.LoadComponent(resource) as Page;
        }

        private async void  setMstrPage(object page)
        {
            System.Uri resource = new System.Uri(@"Views\" + page + ".xaml", System.UriKind.RelativeOrAbsolute);
            CurrentView = Application.LoadComponent(resource) as Page;
            CurrentView.DataContext = await MusterijeViewModel.CreateInstance();
        }
    }
}
