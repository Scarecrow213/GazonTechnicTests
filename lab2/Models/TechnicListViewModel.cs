using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace lab2.Models
{
    public class TechnicListViewModel
    {
        public IEnumerable<Technic> Technics { get; set; }
        public SelectList Storages { get; set; }
    }
}