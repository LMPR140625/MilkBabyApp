using Microsoft.Data.Sqlite;
using MilkBabyApp.Models;
using SQLite;
using System.Collections.ObjectModel;
using System.Data;

namespace MilkBabyApp.Services
{
    public interface IDatabase
    {
        Task<bool> InsertRegistroAlimento(int cantidad, string unidad, string fechaHora);
        Task<List<Registros>> GetRegistrosAlimentacion();
    }
    public class Database : IDatabase
    {
        private readonly string _dbPath;
        public SQLiteAsyncConnection database;

        public Database(string dbPath)
        {
            _dbPath = dbPath;
            database = new SQLiteAsyncConnection(dbPath);
            InitilizeDatabase();
        }

        public void InitilizeDatabase() 
        {
            using (var connection = new SqliteConnection($"DataSource={_dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"CREATE TABLE IF NOT EXISTS REGISTROALIMENTO(
                                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Cantidad   INTEGER NOT NULL,
                                    Unidad TEXT NOT NULL,
                                    DiaHora TEXT NOT NULL                                    
                                        )";
                command.ExecuteNonQuery();
            }
            
        }
        public async Task<bool> Login()
        {
            string user = string.Empty;
            using var connection = new SqliteConnection(_dbPath);
            await connection.OpenAsync();
            var command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM User";

            using (var reader = command.ExecuteReader())
            {
                
                while (reader.Read())
                {
                    user = reader.GetString(0);
                }                
            }

            if (user.Length > 0) return true;
            else return false;
        }

        public async Task<bool> InsertRegistroAlimento(int cantidad, string unidad, string diaHora)
        {
            //using var connection = new SqliteConnection(_dbPath);
            Registros newRecord = new Registros();
            newRecord.Cantidad = cantidad;
            newRecord.Unidad = unidad;
            newRecord.DiaHora = diaHora;

            var result = await database.InsertOrReplaceAsync(newRecord);
            //await connection.OpenAsync();
            //var command = connection.CreateCommand();
            //command.CommandText = $"INSERT INTO REGISTROALIMENTO(Cantidad,Unidad,DiaHora) VALUES({cantidad},{unidad},{fechaHora})";

            //int result = command.ExecuteNonQuery();

            return true;
        }

        public async Task<List<Registros>> GetRegistrosAlimentacion()
        {
            List<Registros> lst = new List<Registros>();
            try
            {
                if (database.TableMappings.Count() > 0)
                {
                    lst.Add(new Registros { Cantidad = 2, DiaHora = "10:30", Unidad = "Oz" });
                }
                
            }
            catch (System.Reflection.TargetInvocationException ex) {
                if (ex.InnerException != null)
                {
                    // Log or inspect ex.InnerException for the specific error details
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    Console.WriteLine($"Inner Exception Type: {ex.InnerException.GetType().FullName}");
                    // Further details like StackTrace can also be examined
                }
                else
                {
                    Console.WriteLine("TargetInvocationException occurred without an inner exception.");
                }
            }
            return lst;          

            
        }
    }
}
