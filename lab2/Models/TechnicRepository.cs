using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace lab2.Models
{
    public interface IRepository : IDisposable
    {
        object Storages { get; set; }

        List<Technic> GetTechnicList();
        Technic GetTechnic(int? id);
        List<Storage> GetStorageList();
        Storage GetStorage(int? id);
        List<Technic> Include(int? storage, string name);
        void Create(Technic item);
        void Create_storage(Storage item);
        void Update(Technic item);
        void Delete(int id);
        void Delete_storage(int id);
        void Save();
    }
    public class TechnicRepository : IRepository
    {
        private TechnicContext db;
        public TechnicRepository()
        {
            this.db = new TechnicContext();
        }
        public List<Technic> GetTechnicList()
        {
            return db.Technics.ToList();
        }
        public List<Storage> GetStorageList()
        {
            return db.Storages.ToList();
        }
        public List<Technic> Include(int? storage, string name)
        {
            IQueryable<Technic> technics = db.Technics.Include(p => p.Storage);
            if (storage != null && storage != 0)
            {
                technics = technics.Where(p => p.StorageId == storage);
            }
            if (!String.IsNullOrEmpty(name) && !name.Equals("Все"))
            {
                technics = technics.Where(p => p.Name == name);
            }
            return technics.ToList();
        }
        public Technic GetTechnic(int? id)
        {
            return db.Technics.Find(id);
        }
        public Storage GetStorage(int? id)
        {
            return db.Storages.Find(id);
        }

        public void Create(Technic p)
        {
            db.Technics.Add(p);
        }
        public void Create_storage(Storage s)
        {
            db.Storages.Add(s);
        }
        public void Update(Technic p)
        {
            db.Entry(p).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Technic p = db.Technics.Find(id);
            if (p != null)
                db.Technics.Remove(p);
        }
        public void Delete_storage(int id)
        {
            Storage s = db.Storages.Find(id);
            if (s != null)
                db.Storages.Remove(s);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public object Storages { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}