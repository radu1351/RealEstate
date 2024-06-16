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
                _database.CreateTableAsync<House>().Wait();
                _database.CreateTableAsync<User>().Wait(); 
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

        public async Task<int> SaveUserAsync(User user)
        {
            try
            {
                return await _database.InsertAsync(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving user: {ex}");
                return 0;
            }
        }

        public async Task<int> deleteUserAsync(User user)
        {
            try
            {
                return await _database.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user: {ex}");
                return 0;
            }
        }

        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            try
            {
                return await _database.Table<User>()
                                      .Where(u => u.Email == email && u.Password == password)
                                      .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

    }
}
