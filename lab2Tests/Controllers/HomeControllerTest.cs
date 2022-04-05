using Microsoft.VisualStudio.TestTools.UnitTesting;
using lab2.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using lab2.Models;
using System.Web;
using System.Web.Mvc;

namespace lab2.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            var mock = new Mock<IRepository>();
            var Storage = 1;
            var Name = "Газонокосилка1";
            mock.Setup(a => a.Include(Storage, Name)).Returns(new List<Technic>());
            mock.Setup(a => a.GetStorageList()).Returns(new List<Storage>());
            HomeController controller = new HomeController(mock.Object);

            // Act
            ViewResult result = controller.Index(Storage, Name) as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }

        [TestMethod()]
        public void CreateStorageTest()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.CreateStorage() as ViewResult;

            Assert.AreEqual("CreateStorage", result.ViewName);
        }
        
        [TestMethod()]
        public void CreateTechnicTest()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(a => a.GetStorageList()).Returns(new List<Storage>() { new Storage() });
            HomeController controller = new HomeController(mock.Object);

            // Act
            ViewResult result = controller.CreateTechnic() as ViewResult;
            SelectList actual = result.ViewBag.Storages as SelectList;

            // Assert
            Assert.IsNotNull(actual);
        }

        [TestMethod()]
        public void CreateTechnicEqualTest()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(a => a.GetStorageList()).Returns(new List<Storage>() { new Storage() { StorageId = 2, NameStorage = "1Storage" } });
            HomeController controller = new HomeController(mock.Object);

            // Act
            ViewResult result = controller.CreateTechnic() as ViewResult;
            SelectList actual = result.ViewBag.Storages as SelectList;

            // Assert
            Assert.AreEqual("NameStorage", actual.DataTextField);
        }

        [TestMethod()]
        public void EditTechnic_technicTest()
        {
            var mock = new Mock<IRepository>();
            var id = 1;
            mock.Setup(a => a.GetTechnic(id)).Returns(new Technic());
            mock.Setup(a => a.GetStorageList()).Returns(new List<Storage>());
            HomeController controller = new HomeController(mock.Object);

            // Act
            ViewResult result = controller.EditTechnic(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }

        [TestMethod()]
        public void EditTechnic_storageListTest()
        {
            var mock = new Mock<IRepository>();
            var id = 2;
            mock.Setup(a => a.GetTechnic(id)).Returns(new Technic());
            mock.Setup(a => a.GetStorageList()).Returns(new List<Storage>());
            HomeController controller = new HomeController(mock.Object);

            // Act
            ViewResult result = controller.EditTechnic(id) as ViewResult;
            SelectList actual = result.ViewBag.Storages as SelectList;
            // Assert
            Assert.IsNotNull(actual);
        }

        [TestMethod()]
        public void Delete_technicTest()
        {
            var mock = new Mock<IRepository>();
            var id = 1;
            mock.Setup(a => a.GetTechnic(id)).Returns(new Technic()
            {
                Id = id,
                Name = "Газонокосилка1",
                Price = 14000,
                StorageId = id
            });
            mock.Setup(a => a.GetStorage(id)).Returns(new Storage());
            HomeController controller = new HomeController(mock.Object);
            // Act
            ViewResult result = controller.Delete(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }

        [TestMethod()]
        public void Delete_nameStorageTest()
        {
            var mock = new Mock<IRepository>();
            var id = 1;
            string name = "Склад1";
            mock.Setup(a => a.GetTechnic(id)).Returns(new Technic()
            {
                Id = id,
                Name = "Газонокосилка1",
                Price = 14000,
                StorageId = id
            });
            mock.Setup(a => a.GetStorage(id)).Returns(new Storage()
            {
                NameStorage = "Склад1"
            });
            HomeController controller = new HomeController(mock.Object);
            // Act
            ViewResult result = controller.Delete(id) as ViewResult;
            string actual = result.ViewBag.Storage as string;

            // Assert
            Assert.AreEqual(name, actual);
        }

        [TestMethod()]
        public void Delete_storageTest()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(a => a.GetStorageList()).Returns(new List<Storage>() { new Storage(), new Storage() });
            HomeController controller = new HomeController(mock.Object);

            // Act
            ViewResult result = controller.Delete_storage() as ViewResult;
            SelectList actual = result.ViewBag.Storages as SelectList;

            // Assert
            Assert.AreEqual(actual.Count(), 2);
        }

        [TestMethod()]
        public void TechnicViewTest()
        {
            var mock = new Mock<IRepository>();
            var id = 2;
            mock.Setup(a => a.GetTechnic(id)).Returns(new Technic()
            {
                Id = id,
                Name = "Газонокосилка1",
            });
            HomeController controller = new HomeController(mock.Object);
            // Act
            ViewResult result = controller.TechnicView(id) as ViewResult;

            // Assert
            Assert.IsNotNull(result.Model);
        }
    }
}