using System.Windows.Controls;
using Meso.Views;
using Meso.Commands;
using System.Windows.Input;
using System.Windows;

namespace Meso.ViewModels
{
    class Class1 : BaseViewModel
    {
        public Class1()
        {
            ChangePage = new RelayCommand(setPage);
        }

        private string _text;
        public string text {
            get
            {
                return _text;
            }

            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        public string testNameof { get; set; }

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

        public ICommand ChangePage { get; set; }

        private void setPage(object page)
        {
            System.Uri resource = new System.Uri(@"Views\"+ page + ".xaml", System.UriKind.RelativeOrAbsolute);
            CurrentView = Application.LoadComponent(resource) as Page;
        }
    }
}
