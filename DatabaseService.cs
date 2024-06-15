using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Imobiliare
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            try
            {
                _database.CreateTableAsync<House>().Wait();  // This ensures the table is created
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating table: {ex}");
            }
        }

        public async Task<List<House>> GetHousesAsync()
        {
            try
            {
                return await _database.Table<House>().ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching houses: {ex}");
                return new List<House>();
            }
        }

        public async Task<int> SaveHouseAsync(House house)
        {
            try
            {
                return await _database.InsertAsync(house);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving house: {ex}");
                return 0;
            }
        }

        public async Task<int> DeleteHouseAsync(House house)
        {
            try
            {
                return await _database.DeleteAsync(house);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting house: {ex}");
                return 0;
            }
        }
    }
}
