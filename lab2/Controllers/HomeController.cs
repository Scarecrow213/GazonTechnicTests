using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using lab2.Models;

namespace lab2.Controllers
{
    public class HomeController : Controller
    {
        IRepository repo;
        public HomeController(IRepository r)
        {
            repo = r;
        }
        public HomeController()
        {
            repo = new TechnicRepository();
        }
        public ActionResult Index(int? storage, string name)
        {
            var storages = repo.GetStorageList();
            // устанавливаем начальный элемент, который позволит выбрать всех 
            storages.Insert(0, new Storage { NameStorage = "Все", StorageId = 0 });
            TechnicListViewModel plvm = new TechnicListViewModel
            {
                Technics = repo.Include(storage, name),
                Storages = new SelectList(storages, "StorageId", "NameStorage"),
            };
            return View(plvm);
        }
        //добавление магазина 
        [HttpGet]
        public ActionResult CreateStorage()
        {
            return View("CreateStorage");
        }
        [HttpPost]
        public ActionResult CreateStorage(Storage storage)
        {
            repo.Create_storage(storage);
            repo.Save();

            return RedirectToAction("Index");
        }
        //Создание новой комплектующей 
        [HttpGet]
        public ActionResult CreateTechnic()
        {
            // Формируем список магазинов для передачи в представление 
            SelectList storages = new SelectList(repo.GetStorageList(), "StoragepId", "NameStorage");
            ViewBag.Storages = storages;
            return View();
        }

        [HttpPost]
        public ActionResult CreateTechnic(Technic technic)
        {
            //Добавляем комплектующую в таблицу
            repo.Create(technic);
            repo.Save();
            // перенаправляем на главную страницу 
            return RedirectToAction("Index");
        }

        //Редактирование записи 
        [HttpGet]
        public ActionResult EditTechnic(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            // Находим в бд комплектующую 
            Technic technic = repo.GetTechnic(id);
            if (technic != null)
            {
                // Создаем список магазинов для передачи в представление 
                SelectList storages = new SelectList(repo.GetStorageList(), "StorageId", "NameStorage", technic.StorageId);
                ViewBag.Storages = storages;
                return View(technic);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult EditTechnic(Technic technic)
        {
            repo.Update(technic);
            repo.Save();
            return RedirectToAction("Index");
        }

        //удаление комплектующей 
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Technic b = repo.GetTechnic(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            Storage storage = repo.GetStorage(b.StorageId);
            ViewBag.Storage = storage.NameStorage;
            return View(b);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            repo.Delete(id);
            repo.Save();
            return RedirectToAction("Index");
        }

        //Удаление магазина
        [HttpGet]
        public ActionResult Delete_storage()
        {
            // Формируем список магазинов для передачи в представление
            SelectList storages = new SelectList(repo.GetStorageList(), "StorageId", "NameStorage");
            ViewBag.Storages = storages;
            return View();
        }

        [HttpPost, ActionName("Delete_storage")]
        public ActionResult DeleteConfirmed_storage(Storage storage)
        {
            repo.Delete_storage(storage.StorageId);
            repo.Save();
            return RedirectToAction("Index");
        }

        public ActionResult TechnicView(int id)
        {
            Technic b = repo.GetTechnic(id);
            return View(b);
        }

        protected override void Dispose(bool disposing)
        {
            repo.Dispose();
            base.Dispose(disposing);
        }
    }
}