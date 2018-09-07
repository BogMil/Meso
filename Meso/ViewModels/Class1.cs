using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;

namespace Meso.ViewModels
{
    class Class1 : BaseViewModel
    {
        public Class1()
        {
            MesoEntities x = new MesoEntities();
            _text = x.Test.FirstOrDefault().test1;
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

    }
}
