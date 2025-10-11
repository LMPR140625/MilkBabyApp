using MilkBabyApp.Models;
using MilkBabyApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Data.Sqlite;
using System.Data;
using SQLite;

namespace MilkBabyApp.ViewModels
{
    public class ReportViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private List<REGISTROALIMENTO> _lstResult;
        private IDatabase _dbConn;
        private readonly string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "babymilk.db3");
        public ObservableCollection<REGISTROALIMENTO[]> registros { get; set; }

        public ReportViewModel()
        {
            registros = new ObservableCollection<REGISTROALIMENTO[]>((IEnumerable<REGISTROALIMENTO[]>)GetRegistrosAlimentacion());
        }

        

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
               
        public Task<bool> InsertRegistroAlimento(int cantidad, string unidad, string fechaHora)
        {
            throw new NotImplementedException();
        }

        public async Task<List<REGISTROALIMENTO>> GetRegistrosAlimentacion()
        {
            _dbConn = new Database(_dbPath);
            _lstResult = await _dbConn.GetRegistrosAlimentacion();

            return _lstResult;
        }
    }
}
