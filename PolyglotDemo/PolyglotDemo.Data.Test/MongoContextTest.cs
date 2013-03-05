using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PolyglotDemo.Model;
using System.Linq;

namespace PolyglotDemo.Data.Test
{
    /// <summary>
    /// Summary description for MongoContextTest
    /// </summary>
    [TestClass]
    public class MongoContextTest
    {
        [TestInitialize]
        public void init()
        {
            DatabaseInitialize.DatabaseInitializeFactory("Mongo");
        }

        [TestMethod]
        public void WhenGetFileStructureThenResultNotNull()
        {
            var dataContext = new MongoDataContext();
            var result = dataContext.GetFileStructure();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void WhenGetFileStructureThenResultHasManyItems()
        {
            var dataContext = new MongoDataContext();
            var result = dataContext.GetFileStructure();
            Assert.AreNotEqual<int>(0, result.Count()); 
        }

        /*[TestMethod]
        public void WhenGetOrganizationFirstResultHasNameEqualToUGM()
        {
            var dataContext = new DataContext();
            var result = dataContext.GetOrganizations();

            Assert.AreEqual<string>("UGM", result.First().Name);
        }*/

        [TestMethod]
        public void GetFileStructureHasHaragethFileStructure()
        {
            var dataContext = new MongoDataContext();
            RootDirectory result = dataContext.GetFileStructure("harageth").FirstOrDefault();
            Assert.AreEqual<string>("harageth", result.un);
        }

        [TestMethod]
        public void WhenGetFileStructurThenHasFiles()
        {
            var dataContext = new MongoDataContext();
            var result = dataContext.GetFileStructure("harageth").FirstOrDefault();
            int fileCount = result.files.Count();
            Assert.AreNotEqual<int>(0, fileCount);
        }

        [TestMethod]
        public void WhenGetFileStructurThenHasSpecificFile()
        {
            var dataContext = new MongoDataContext();
            var result = dataContext.GetFileStructure("harageth").FirstOrDefault();
            List<string> files = result.files;
            string fileCount = files.Find(x => x.Equals("temp.txt")); ;
            Assert.AreEqual<string>("temp.txt", fileCount);
        }
    }
}
