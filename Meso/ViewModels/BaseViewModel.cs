﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using Meso.Models;

namespace Meso.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public PaginationDataContext PaginationContext { get; set; }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
