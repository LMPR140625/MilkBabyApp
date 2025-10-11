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
        Task<List<REGISTROALIMENTO>> GetRegistrosAlimentacion();
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
                                    IdAlimento INTEGER PRIMARY KEY AUTOINCREMENT,
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

        public async Task<bool> InsertRegistroAlimento(int cantidad, string unidad, string fechaHora)
        {
            //using var connection = new SqliteConnection(_dbPath);
            await database.InsertOrReplaceAsync(new REGISTROALIMENTO(cantidad,unidad,fechaHora));
            //await connection.OpenAsync();
            //var command = connection.CreateCommand();
            //command.CommandText = $"INSERT INTO REGISTROALIMENTO(Cantidad,Unidad,DiaHora) VALUES({cantidad},{unidad},{fechaHora})";

            //int result = command.ExecuteNonQuery();

            return true;
        }

        public async Task<List<REGISTROALIMENTO>> GetRegistrosAlimentacion()
        {
            List<REGISTROALIMENTO> lst = await database.Table<REGISTROALIMENTO>().ToListAsync();

            return lst;
        }
    }
}
