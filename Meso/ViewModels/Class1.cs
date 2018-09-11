using System.Windows.Controls;
using Meso.Views;
using Meso.Commands;
using System.Windows.Input;

namespace Meso.ViewModels
{
    class Class1 : BaseViewModel
    {
        public Class1()
        {
            ChangePage = new RelayCommand(x => this.CurrentView = new Page1(), x => true);
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

        public Page CurrentView { get; set; } = new Page2();

        public ICommand ChangePage { get; set; }
    }
}
