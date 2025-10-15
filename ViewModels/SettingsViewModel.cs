using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Data.Sqlite;
using MilkBabyApp.Data;
using MilkBabyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkBabyApp.ViewModels
{
    public partial class SettingsViewModel : BaseViewModel
    {
        private readonly DatabaseContext _databaseContext;

        #region Properties
        [ObservableProperty]
        public bool isLoading = true;

        [ObservableProperty]
        public int timeInterval;

        private TimeSettings _timeSettings;

        #endregion

        public SettingsViewModel(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        internal async void GetData()
        {
            IsLoading = true;
            var time = await _databaseContext.GetAllAsync<TimeSettings>();

            if (time.Count() == 0)
            {
                var res = await _databaseContext.AddItemAsync<TimeSettings>
                    (new TimeSettings { Id = new Guid(), TimeInterval = 3});
                TimeInterval = 3;
            } else
            {
                TimeInterval = time.Select(time => time.TimeInterval).First();
                _timeSettings = time.First();
            }
            IsLoading = false;
        }

        [RelayCommand]
        private async Task DeleteData()
        {
            bool result = false;
            try
            {
                result = await _databaseContext.DropData<Registros>();
            }
            catch (SqliteException ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (result) await Shell.Current.DisplayAlert("Exito", "Se elimino la tabla de Registros", "Salir");
            else await Shell.Current.DisplayAlert("Alerta", "Problemas al eliminar", "Salir");

        }

        [RelayCommand]
        private async Task UpdateTimeInterval()
        {
            await _databaseContext.UpdateItemAsync<TimeSettings>(new TimeSettings { Id = _timeSettings.Id, TimeInterval = TimeInterval });
        }
        
    }
}
