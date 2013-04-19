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
        public void WhenGetFileStructureThenHasSpecificFile()
        {
            var dataContext = new MongoDataContext();
            var result = dataContext.GetFileStructure("harageth").FirstOrDefault();
            List<string> files = result.files;
            string fileCount = files.Find(x => x.Equals("temp.txt"));
            Assert.AreEqual<string>("temp.txt", fileCount);
        }

        [TestMethod]
        public void AddFileToStructureThen()
        {
            var dataContext = new MongoDataContext();
            var result = dataContext.GetFileStructure("harageth").FirstOrDefault();
            int items = result.files.Count;
            dataContext.AppendFile("thirdFile.txt", "harageth");
            var result2 = dataContext.GetFileStructure("harageth").FirstOrDefault();
            int appendedItems = result2.files.Count;
            Assert.AreNotEqual(items, appendedItems);
        }

        [TestMethod]
        public void AddFileToStructureThenSomething()
        {
            var dataContext = new MongoDataContext();
            var result = dataContext.GetFileStructure("harageth").FirstOrDefault();
            int items = result.files.Count;
            dataContext.AppendFile("thirdFile.txt", "harageth");
            var result2 = dataContext.GetFileStructure("harageth").FirstOrDefault();
            int appendedItems = result2.files.Count;
            Assert.AreEqual(items+1, appendedItems);
        }

        [TestMethod]
        public void AddFolderToStructureThenPreviousCountAndCurrentCountNotEqual()
        {
            var dataContext = new MongoDataContext();
            var result = dataContext.GetFileStructure("harageth").FirstOrDefault();
            int i = result.folders.Count;
            Folder insertFolder = new Folder();
            insertFolder.folderName = "ATestFolder";
            result.folders.Add(insertFolder);

            dataContext.UpdateFileStructure(result);

            var result2 = dataContext.GetFileStructure("harageth").FirstOrDefault();
            int j = result2.folders.Count;
            Assert.AreNotEqual(i,j);

        }

        [TestMethod]
        public void AddFolderToStructureThenListHasOneMoreItem()
        {
            var dataContext = new MongoDataContext();
            var result = dataContext.GetFileStructure("harageth").FirstOrDefault();
            int i = result.folders.Count;
            Folder insertFolder = new Folder();
            insertFolder.folderName = "ATestFolder";
            result.folders.Add(insertFolder);

            dataContext.UpdateFileStructure(result);

            var result2 = dataContext.GetFileStructure("harageth").FirstOrDefault();
            int j = result2.folders.Count;
            Assert.AreEqual(i+1, j);
        }

        [TestMethod]
        public void AddFolderToStructureThenListHasSpecificItem()
        {
            var dataContext = new MongoDataContext();
            var result = dataContext.GetFileStructure("harageth").FirstOrDefault();
            int i = result.folders.Count;
            Folder insertFolder = new Folder();
            insertFolder.folderName = "ATestFolder";
            result.folders.Add(insertFolder);

            dataContext.UpdateFileStructure(result);

            var result2 = dataContext.GetFileStructure("harageth").FirstOrDefault();
            Folder newFolder = result2.folders.Find(x => x.folderName == "ATestFolder");
            Assert.AreEqual<string>(newFolder.folderName, "ATestFolder");
        }

        //////////////////////////////

        [TestMethod]
        public void AddFileToStructureThenPreviousCountAndCurrentCountNotEqual()
        {
            var dataContext = new MongoDataContext();
            var result = dataContext.GetFileStructure("harageth").FirstOrDefault();
            int i = result.files.Count;
            string insertFile = "ATestFile.txt";
            result.files.Add(insertFile);

            dataContext.UpdateFileStructure(result);

            var result2 = dataContext.GetFileStructure("harageth").FirstOrDefault();
            int j = result2.files.Count;
            Assert.AreNotEqual(i, j);

        }

        [TestMethod]
        public void AddFileToStructureThenListHasOneMoreItem()
        {
            var dataContext = new MongoDataContext();
            var result = dataContext.GetFileStructure("harageth").FirstOrDefault();
            int i = result.files.Count;
            string insertFile = "ATestFile.txt";
            result.files.Add(insertFile);

            dataContext.UpdateFileStructure(result);

            var result2 = dataContext.GetFileStructure("harageth").FirstOrDefault();
            int j = result2.files.Count;
            Assert.AreEqual(i + 1, j);
        }

        [TestMethod]
        public void AddFileToStructureThenListHasSpecificItem()
        {
            var dataContext = new MongoDataContext();
            var result = dataContext.GetFileStructure("harageth").FirstOrDefault();
            string insertFolder = "ATestFile.txt";
            result.files.Add(insertFolder);

            dataContext.UpdateFileStructure(result);

            var result2 = dataContext.GetFileStructure("harageth").FirstOrDefault();
            string newFile = result2.files.Find(x => x == "ATestFile.txt");
            Assert.AreEqual<string>(newFile, "ATestFile.txt");
        }



    }
}
