using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PolyglotDemo.Data;

namespace PolyglotDemo.Data.Test
{
    [TestClass]
    public class RedisContextTest
    {
        [TestInitialize]
        public void init()
        {
            DatabaseInitialize.DatabaseInitializeFactory("Redis");
        }

        [TestMethod]
        public void HasValueWhenSearchForKeyTempTxt()
        {
            var dataContext = new RedisDataContext();

            byte[] val = dataContext.ReadFile("harageth./temp.txt");
            Assert.IsNotNull(val);
        }

        [TestMethod]
        public void NoValueWhenSearchForRemovedKeyTempTxt()
        {
            RedisDataContext dataContext = new RedisDataContext();

            dataContext.DeleteFile("harageth./temp.txt");
            Assert.IsNull(dataContext.ReadFile("harageth./temp.txt"));
        }

        [TestMethod]
        public void ItemExistsWhenInsertNewFile()
        {
            RedisDataContext dataContext = new RedisDataContext();

            dataContext.InsertFile("./anotherFile.txt", "c:\\users\\harageth\\documents\\visual studio 2012\\Projects\\PolyglotDemo\\TesterFile.txt");
            Assert.IsNotNull(dataContext.ReadFile("./anotherFile.txt"));
        }

        [TestMethod]
        public void FileContainsDataAfterInsertNewFile()
        {
            RedisDataContext dataContext = new RedisDataContext();

            string value = dataContext.InsertFile("./anotherFile.txt", "c:\\users\\harageth\\documents\\visual studio 2012\\Projects\\PolyglotDemo\\TesterFile.txt");
            Assert.AreEqual<string>("This is a specific file that I am adding to the project to make sure that I can add all sorts of files directly to the database.",
                value);
        }
    }
}
