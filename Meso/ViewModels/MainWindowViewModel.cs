using System;
using Meso.Commands;
using Meso.Views;
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
            ChangePage = new RelayCommand(SetPage);
            SetMusterijePage = new RelayCommand(SetMstrPage);
        }

        private Page _currentView = new Page2();
        public Page CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        private void SetPage(object page)
        {
            var resource = new Uri(@"Views\" + page + ".xaml", UriKind.RelativeOrAbsolute);
            CurrentView = Application.LoadComponent(resource) as Page;
        }

        private void  SetMstrPage(object page)
        {
            var resource = new Uri(@"Views\Musterija\" + page + ".xaml", UriKind.RelativeOrAbsolute);
            CurrentView = Application.LoadComponent(resource) as Page;
            if (CurrentView == null) throw new Exception("Nije definisan view");
            //CurrentView.DataContext = await MusterijeViewModel.CreateInstance();
        }
    }
}
