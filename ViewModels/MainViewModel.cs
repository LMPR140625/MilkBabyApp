using CommunityToolkit.Mvvm.Input;
using Microsoft.Data.Sqlite;
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
using System.Text.Json;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using MilkBabyApp.Data;
using MilkBabyApp.Models;
using SQLite;

namespace MilkBabyApp.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        private readonly DatabaseContext _databaseContext;

        [ObservableProperty]
        private string _unitSelected;
        [ObservableProperty]
        public int _qtySelected;

        [ObservableProperty]
        List<string> units = new();

        [ObservableProperty]
        public bool isLoading = true;
        [ObservableProperty]
        public string? searchText;

        [ObservableProperty]
        private TimeSpan selectedTime;
        [ObservableProperty]
        private TimeSpan selectedTime3;

        [ObservableProperty]
        private TimeSpan currentTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

        public MainViewModel(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            this.Units.Add("Onza");
            this.Units.Add("Pieza");
            this.Units.Add("Mililitro");
        }


        public int Qty
        {
            get => _qtySelected;
            set
            {
                if (_qtySelected != value)
                {
                    _qtySelected = value;
                    OnPropertyChanged();
                }
            }
        }
        [RelayCommand]
        private async Task SaveAliment()
        {
            bool result = false;
            try
            {
                result = await _databaseContext.AddItemAsync<Registros>(
                    new Registros() { Id = Guid.NewGuid(),  Cantidad = Qty , DiaHora = SelectedTime.ToString(), Unidad = UnitSelected });

                UpdateDetail();
            }
            catch (SqliteException ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (result) 
            {

                await Shell.Current.DisplayAlert("Exito", "Guardado correctamente", "Salir");
                
            } 
            else await Shell.Current.DisplayAlert("Alerta", "Problemas al guardar", "Salir");

        }

        internal void GetData()
        {
            IsLoading = true;
            SelectedTime = CurrentTime;
            SelectedTime3 = SelectedTime.Add(new TimeSpan(3,0,0));
            IsLoading = false;
        }

        internal void UpdateDetail()
        {
            SelectedTime = CurrentTime;
            SelectedTime3 = SelectedTime.Add(new TimeSpan(3, 0, 0));
        }
    }
}
