using CommunityToolkit.Mvvm.Input;
using MilkBabyApp.Services;
using MilkBabyApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MilkBabyApp.ViewModels
{
    public partial class MainViewModel : INotifyPropertyChanged
    {
        private  IDatabase _dbConn;
        private readonly string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "babymilk.db3");
        private string _unitSelected;
        private int _qtySelected;
        public ObservableCollection<string> Units { get; set; }
        public ObservableCollection<int> Qty{ get; set; }

        public TimeSpan _currentTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        public TimeSpan _selectedTime;
        public TimeSpan _selectedTime3;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainViewModel()
        {
            Units = new ObservableCollection<string> { "Onza", "Pieza", "Mililitro" };
            Qty = new ObservableCollection<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            UnitSelected = Units.FirstOrDefault();
            QtySelected = Qty.FirstOrDefault();
        }       

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public TimeSpan SelectedTime
        {
            get => _selectedTime;
            set
            {
                if (_selectedTime != value)
                {
                    _selectedTime = value;
                    OnPropertyChanged();
                    SelectedTime3 = _selectedTime.Add(new TimeSpan(3, 0, 0));
                }
            }
        }

        public TimeSpan SelectedTime3
        {
            get => _selectedTime3;
            set
            {
                if (_selectedTime3 != value)
                {
                    _selectedTime3 = value;
                    OnPropertyChanged();

                }
            }
        }

        public TimeSpan CurrentTime
        {
            get => _currentTime;
            set
            {
                if (_currentTime != value)
                {
                    _currentTime = value;
                    OnPropertyChanged();
                    SelectedTime = _currentTime;
                }
            }
        }

        public string UnitSelected
        {
            get => _unitSelected;
            set
            {
                if (_unitSelected != value)
                {
                    _unitSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        public int QtySelected
        {
            get => _qtySelected;
            set
            {
                if(_qtySelected != value)
                {
                    _qtySelected = value;
                    OnPropertyChanged();
                }
            }
        }

        [RelayCommand]
        private async Task SaveAliment()
        {
            _dbConn = new Database(_dbPath);
            bool result = await _dbConn.InsertRegistroAlimento(_qtySelected,_unitSelected, SelectedTime.ToString());
            if (result) await Shell.Current.DisplayAlert("Exito", "Guardado correctamente", "Salir");
            else await Shell.Current.DisplayAlert("Alerta", "Problemas al guardar", "Salir");

        }
    }
}
