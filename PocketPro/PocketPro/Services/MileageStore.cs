using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PocketPro.Models;
using SQLite;

namespace PocketPro.Services
{
    public class MileageStore : IDataStore<Mileage>
    {
        private List<Mileage> items;
        private SQLiteAsyncConnection _database;

        public MileageStore()
        {
            _database = Datastores.Database;

            items = new List<Mileage>();
        }

        public async Task<int> AddItemAsync(Mileage item)
        {
            //if (item.ID != 0)
            //{
            //    return database.UpdateAsync(item);
            //}
            //else
            //{

            return await _database.InsertAsync(item);
            //}
        }


        public async Task<bool> UpdateItemAsync(Mileage item)
        {
            var oldItem = items.Where((Mileage arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<int> DeleteItemAsync(Mileage note)
        {
            return await _database.DeleteAsync(note);
        }

        public async Task<Mileage> GetItemAsync(int id)
        {
            return await _database.Table<Mileage>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Mileage>> GetItemsAsync(bool forceRefresh = false)
        {
            return await _database.QueryAsync<Mileage>("SELECT * FROM [Mileage]");
        }
    }
}