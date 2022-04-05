using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab2.Models
{
    public class Storage
    {
        public int StorageId { get; set; }
        public string NameStorage { get; set; }
        public ICollection<Technic> Technics { get; set; }
        public Storage()
        {
            Technics = new List<Technic>();
        }
    }
}