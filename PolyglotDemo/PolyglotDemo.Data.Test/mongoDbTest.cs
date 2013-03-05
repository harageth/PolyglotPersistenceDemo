using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using PolyglotDemo.Model;
using System.Linq;


namespace PolyglotDemo.Data.Test
{
    [TestClass]
    public class mongoDbTest
    {
        [TestMethod]
        [Ignore]
        public void Insert()
        {
            const string connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase("PolyglotDemo");
            var directories = database.GetCollection<RootDirectory>("UserDirectories");
            directories.Insert(new RootDirectory() { un = "harageth" });
        }

        [TestMethod]
        [Ignore]
        public void InsertChildRecord()
        {
            const string connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase("PolyglotDemo");
            var directories = database.GetCollection<RootDirectory>("UserDirectories");
            directories.Remove(new QueryDocument());
            directories.Insert(new RootDirectory()
            {
                _id = ObjectId.GenerateNewId().ToString(),
                un = "harageth",
                folders = new List<Folder>() { new Folder() { folderName = "firstFolder", files = new List<string>() { "temp.txt", "file.txt" } }, new Folder() { folderName = "secondFolder", files = new List<string>() { "temp.txt", "file.txt" } }, new Folder() { folderName = "thirdFolder", files = new List<string>() { "temp.txt", "file.txt" } } },
                files = new List<string>() { "temp.txt", "file.txt" }

            });
        }

        [TestMethod]
        [Ignore]
        public void Read()
        {
            const string connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase("PolyglotDemo");
            var directories = database.GetCollection<RootDirectory>("UserDirectories");
            //directories.Remove(new QueryDocument());
            directories.Insert(new RootDirectory()
            {
                _id = ObjectId.GenerateNewId().ToString(),
                un = "harageth",
                folders = new List<Folder>() { new Folder() { folderName = "firstFolder", files = new List<string>() { "temp.txt", "file.txt" } }, new Folder() { folderName = "secondFolder", files = new List<string>() { "temp.txt", "file.txt" } }, new Folder() { folderName = "thirdFolder", files = new List<string>() { "temp.txt", "file.txt" } } },
                files = new List<string>() { "temp.txt", "file.txt" }

            }); 

            var fileStruct = directories.Find(Query.EQ("un", "harageth")).FirstOrDefault();
            Console.WriteLine(fileStruct.un);
        }
    }
}
