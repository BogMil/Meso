using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using System.Windows.Controls;
using Meso.Views;

namespace Meso.ViewModels
{
    class Class1 : BaseViewModel
    {
        public Class1()
        {
           
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

        public Page CurrentView { get; set; } = new Page1();
    }
}
