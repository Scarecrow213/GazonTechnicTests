using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab2.Models
{
    public class Technic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Hours { get; set; }
        public int PlannedHours { get; set; }
        public int Percentage { get; set; }
        public string img { get; set; }

        public int? StorageId { get; set; }
        public Storage Storage { get; set; }
    }
}