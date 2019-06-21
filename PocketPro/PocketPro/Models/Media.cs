using SQLite;
using System;

namespace PocketPro.Models
{
    public class Media
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string Path { get; set; }
        public string ThumbPath { get; set; }

        public string Title { get; set; }
        public string Size { get; set; }
        public MediaType Type { get; set; }  
        public string Date { get; set; }
    }

    public enum MediaType
    {
        Picture, Video, Audio
    }
}