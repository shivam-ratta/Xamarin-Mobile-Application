using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PocketPro.Models;
using SQLite;

namespace PocketPro.Services
{
    public class MediaStore : IDataStore<Media>
    {
        private List<Media> items;
        private SQLiteAsyncConnection _database;

        public MediaStore(MediaType mediaType)
        {
            _database = Datastores.Database;

            items = new List<Media>();
        }

        public async Task<int> AddItemAsync(Media item)
        {
            return await _database.InsertAsync(item);
        }
         

        public async Task<bool> UpdateItemAsync(Media item)
        {
            var oldItem = items.Where((Media arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<int> DeleteItemAsync(Media media)
        {
            return await _database.DeleteAsync(media);
        }

        public async Task<Media> GetItemAsync(int id)
        {
            return await _database.Table<Media>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Media>> GetItemsAsync(bool forceRefresh = false)
        {
            return await _database.QueryAsync<Media>("SELECT * FROM [Media]");
        }
    }
}