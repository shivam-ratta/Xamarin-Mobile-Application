using System;
using System.Collections.Generic;

namespace PocketPro.Models
{
    public class Documents
    {
        public List<Media> Pictures { get; set; }
        public List<Media> Videos { get; set; }
        public List<Media> Audio { get; set; }
        public List<Note> Notes { get; set; }

    }
}