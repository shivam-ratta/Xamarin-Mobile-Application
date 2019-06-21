using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PocketPro.Models;
using SQLite;

namespace PocketPro.Services
{
    public class NoteDataStore : IDataStore<Note>
    {
        private List<Note> items;
        private SQLiteAsyncConnection _database;

        public NoteDataStore()
        {
            _database = Datastores.Database;

            items = new List<Note>();
        }

        public async Task<int> AddItemAsync(Note item)
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
         

        public async Task<bool> UpdateItemAsync(Note item)
        {
            var oldItem = items.Where((Note arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<int> DeleteItemAsync(Note note)
        {
            return await _database.DeleteAsync(note);
        }

        public async Task<Note> GetItemAsync(int id)
        {
            return await _database.Table<Note>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Note>> GetItemsAsync(bool forceRefresh = false)
        {
            return await _database.QueryAsync<Note>("SELECT * FROM [Note]");
        }
    }
}