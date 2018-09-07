using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meso.ViewModels
{
    class Class1 : BaseViewModel
    {
        private string _text = nameof(testNameof);
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

        //public event PropertyChangedEventHandler PropertyChanged;
        //private void OnPropertyRaised(string propertyname)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        //}
    }
}
