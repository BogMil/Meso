using Meso.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Meso.ViewModels
{
    class Page2DC : BaseViewModel
    {
        public Page2DC()
        {
            //var x = new RelayCommand(s =>MessageBox.Show("asd","asd"), s => true);
            ChangePage = new RelayCommand(s=> MessageBox.Show("asd", "asd"));
        }

        public ICommand ChangePage { get; set; }

        private void showMessageBog(object param)
        {
            MessageBox.Show("asd", "asd");
        }
    }
}
