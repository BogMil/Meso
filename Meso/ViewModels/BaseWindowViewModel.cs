using Meso.Commands;
using Meso.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Meso.ViewModels
{
    class BaseWindowViewModel
    {
        public Page CurrentView { get; set; } = new Page1();

        public BaseWindowViewModel()
        {

        }

        public ICommand ChangePage(object test)
        {
            return new RelayCommand(x => this.CurrentView = new Page1(), x => true);   
        }

    }
}
