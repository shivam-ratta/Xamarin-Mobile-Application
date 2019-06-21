using PocketPro.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SQLite;

namespace PocketPro.Services
{
    class Datastores
    {
        public static void SetDatabase(string databasePath)
        {
            Database = new SQLiteAsyncConnection(databasePath);

            Database.CreateTableAsync<Note>().Wait();
            Database.CreateTableAsync<Mileage>().Wait();
            Database.CreateTableAsync<Media>().Wait();

        }

        public static SQLiteAsyncConnection Database { get; set; }

        public static IDataStore<Note> Notes => DependencyService.Get<IDataStore<Note>>() ?? new NoteDataStore();

        public static IDataStore<Media> Pictures => DependencyService.Get<IDataStore<Media>>() ?? new MediaStore(MediaType.Picture);
        public static IDataStore<Media> Videos => DependencyService.Get<IDataStore<Media>>() ?? new MediaStore(MediaType.Video);
        public static IDataStore<Media> Audio => DependencyService.Get<IDataStore<Media>>() ?? new MediaStore(MediaType.Audio);

        public static IDataStore<Mileage> Mileage => DependencyService.Get<IDataStore<Mileage>>() ?? new MileageStore();

    }
}
