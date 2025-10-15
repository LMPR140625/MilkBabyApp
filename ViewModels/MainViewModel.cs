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
        public string unitSelected;

        [ObservableProperty]
        public int qtySelected;

        [ObservableProperty]
        List<string> units = new();

        [ObservableProperty]
        public bool isLoading = true;

        [ObservableProperty]
        public string? searchText;

        [ObservableProperty]
        public TimeSpan selectedTime;

        [ObservableProperty]
        public TimeSpan selectedTime3;

        [ObservableProperty]
        public int qty;

        [ObservableProperty]
        private TimeSpan currentTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

        public MainViewModel(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            this.Units.Add("Onza");
            this.Units.Add("Pieza");
            this.Units.Add("Mililitro");
        }
        
        [RelayCommand]
        private async Task SaveAliment()
        {
            bool result = false;
            UpdateDetail();
            try
            {
                var lst = await _databaseContext.GetAllAsync<Registros>();
                result = await _databaseContext.AddItemAsync<Registros>(
                    new Registros() { Id = Guid.NewGuid()
                                    , IdRegistro = lst.Count() + 1 
                                    ,  Cantidad = Qty
                                    , HoraMinutos = SelectedTime.ToString()
                                    , Unidad = UnitSelected
                                    , Dia = DateTime.Now.Day
                                    , Mes = DateTime.Now.Month
                                    , Anio = DateTime.Now.Year
                                    , DateRecord = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year});

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

        internal async void GetData()
        {
            IsLoading = true;
            //await _databaseContext.DeleteAllAsync<Registros>();
            var lstResult = await _databaseContext.GetAllAsync<Registros>();
            var time = await _databaseContext.GetAllAsync<TimeSettings>();

            if(lstResult.Count() > 0)
            {
                Registros lastRecord = lstResult.OrderByDescending(rec => rec.IdRegistro).Take(1).First();

                //SelectedTime = CurrentTime;
                SelectedTime = TimeSpan.Parse(lastRecord.HoraMinutos);
                SelectedTime3 = SelectedTime.Add(new TimeSpan
                    (time.Select(ti => ti.TimeInterval).First(), 0, 0));
            }
            
            IsLoading = false;
        }

        internal void UpdateDetail()
        {
            SelectedTime = CurrentTime;
            SelectedTime3 = SelectedTime.Add(new TimeSpan(3, 0, 0));
        }
    }
}
