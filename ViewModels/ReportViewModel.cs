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

namespace MilkBabyApp.ViewModels
{
    public partial class ReportViewModel : BaseViewModel
    {
        private readonly DatabaseContext _databaseContext;

        #region Properties
        [ObservableProperty]
        public int ozDay;

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

        #endregion
        public ReportViewModel(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
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

            // Trash month

            MonthTrash = lstResult
                .Where(rec => rec.Mes == DateTime.Now.Month && rec.Anio == DateTime.Now.Year && rec.Dia == DateTime.Now.Day)
                .Count();
            IsLoading = false;
        }
    }
}
