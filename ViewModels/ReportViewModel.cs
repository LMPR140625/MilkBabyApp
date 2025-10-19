using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Data.Sqlite;
using MilkBabyApp.Data;
using MilkBabyApp.Models;
using MilkBabyApp.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.Json;
using Microcharts;
using SkiaSharp;

namespace MilkBabyApp.ViewModels
{
    public partial class ReportViewModel : BaseViewModel
    {
        private readonly DatabaseContext _databaseContext;

        #region Properties
        [ObservableProperty]
        public int ozDay;
        [ObservableProperty]
        public int ozYesterday;
        [ObservableProperty]
        public int ozBeforeYesterday;

        [ObservableProperty]
        public int takesDay;

        [ObservableProperty]
        public int montlyConsume;

        [ObservableProperty]
        public int monthTrash;

        [ObservableProperty]
        public bool isLoading = true;

        [ObservableProperty]
        public List<Registros> lstRegistros;

        [ObservableProperty]
        public BarChart barChartDays;

        [ObservableProperty]
        public bool isRefreshing;


        #endregion

        public ReportViewModel(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        [RelayCommand]
        public async Task DeleteRecord(Registros item)
        {
            bool result = false;
            try
            {
                result = await _databaseContext.DeleteItemAsync<Registros>(item);
            }
            catch (SqliteException ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (result)
            {
                await RefreshData();
                await Shell.Current.DisplayAlert("Exito", "Se elimino el registro", "Salir");
            }
            else await Shell.Current.DisplayAlert("Alerta", "Problemas al eliminar", "Salir");
        }
        public List<Registros> GetRecordsLastFive()
        {
            return (List<Registros>)_databaseContext.GetAllAsync<Registros>().Result;
        }

        public async void GetData()
        {
            IsLoading = true;
            // Retrieve records
            var lstResult = (List<Registros>) await _databaseContext.GetAllAsync<Registros>();

            // Top 5
            LstRegistros = lstResult
                .Where(rec => rec.Mes == DateTime.Now.Month && rec.Anio == DateTime.Now.Year)
                .OrderByDescending(rec => rec.IdRegistro)
                .Take(5)
                .ToList();

            // ozDay
            OzDay = lstResult
                .Where(rec => rec.Mes == DateTime.Now.Month && rec.Anio == DateTime.Now.Year && rec.Dia == DateTime.Now.Day)
                .Select(rec => rec.Cantidad)
                .Sum();

            // Takes of day
            TakesDay = lstResult
                .Where(rec => rec.Mes == DateTime.Now.Month && rec.Anio == DateTime.Now.Year && rec.Dia == DateTime.Now.Day)
                .Count();

            // Month Consume
            MontlyConsume= lstResult
                .Where(rec => rec.Mes == DateTime.Now.Month && rec.Anio == DateTime.Now.Year )
                .Select(rec => rec.Cantidad)
                .Sum();

            // BarChart 3 last days
            // ozDay
            OzYesterday = lstResult
                .Where(rec => rec.Mes == DateTime.Now.Month && rec.Anio == DateTime.Now.Year && rec.Dia == ( DateTime.Now.Day - 1))
                .Select(rec => rec.Cantidad)
                .Sum();

            OzBeforeYesterday = lstResult
                .Where(rec => rec.Mes == DateTime.Now.Month && rec.Anio == DateTime.Now.Year && rec.Dia == (DateTime.Now.Day - 2))
                .Select(rec => rec.Cantidad)
                .Sum();

            ChartEntry[] entries = new ChartEntry[]
            {
                new ChartEntry(OzBeforeYesterday)
                {
                    Label = "Antier",
                    ValueLabel = OzBeforeYesterday.ToString(),
                    Color = SKColor.Parse("#EAEAEA")
                },
                new ChartEntry(OzYesterday)
                {
                    Label = "Ayer",
                    ValueLabel = OzYesterday.ToString(),
                    Color = SKColor.Parse("#EAEAEA")
                },
                new ChartEntry(OzDay)
                {
                    Label = "Hoy",
                    ValueLabel = OzDay.ToString(),
                    Color = SKColor.Parse("#EAEAEA")
                }
            };

            BarChartDays = new BarChart()
            {
                ValueLabelOption = ValueLabelOption.OverElement,
                LabelTextSize = 30,
                LabelOrientation = Orientation.Horizontal,
                SerieLabelTextSize = 25,
                ValueLabelOrientation = Orientation.Horizontal,
                ValueLabelTextSize = 25,
                LabelColor = SKColor.Parse("#EAEAEA"),   
                CornerRadius = 10,                
                BackgroundColor = SKColor.Parse("#6495ed"),
                Entries = entries,
            };


            
            IsLoading = false;
        }

        [RelayCommand]
        public async Task RefreshData()
        {
            IsRefreshing = true;

            await Task.Delay(500);
            GetData();
            IsRefreshing = false;
        }
        
    }
}
