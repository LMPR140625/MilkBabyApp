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
            LstRegistros = (List<Registros>) await _databaseContext.GetAllAsync<Registros>();
            LstRegistros = LstRegistros.OrderByDescending(rec => rec.DiaHora).Take(5).ToList();
            IsLoading = false;
        }
    }
}
