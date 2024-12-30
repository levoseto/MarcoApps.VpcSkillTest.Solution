namespace MarcoApps.VpcSkillTest.Services.Mobile.Services
{
    using MarcoApps.VpcSkillTest.Services.Mobile.Models;
    using SQLite;

    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;
        private readonly string _DatabaseName = "refacciones.db";

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection($"{dbPath}/{_DatabaseName}");
            _database.CreateTableAsync<Taller>().Wait();
            _database.CreateTableAsync<Mecanico>().Wait();
            _database.CreateTableAsync<Vehiculo>().Wait();
            _database.CreateTableAsync<Refaccion>().Wait();
            _database.CreateTableAsync<Inventario>().Wait();
            _database.CreateTableAsync<Solicitud>().Wait();
            _database.CreateTableAsync<Envio>().Wait();
            _database.CreateTableAsync<Instalacion>().Wait();
        }

        public SQLiteAsyncConnection GetConnection() => _database;
    }
}