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

        #endregion

        public SettingsViewModel(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
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

        internal void GetData()
        {
            IsLoading = true;

            IsLoading = false;
        }

    }
}
