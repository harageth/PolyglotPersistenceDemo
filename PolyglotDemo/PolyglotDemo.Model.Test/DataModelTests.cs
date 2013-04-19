using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace PolyglotDemo.Model.Test
{
    [TestClass]
    public class DataModelTests
    {
        RootDirectory testRoot;

        [TestInitialize]
        public void init()
        {
            testRoot = new RootDirectory() { 
                un = "harageth",
                folders = new List<Folder>() { new Folder() { folderName = "firstFolder", files = new List<string>() { "temp1.txt", "file1.txt" }, folders = new List<Folder> { new Folder() { folderName = "firstFolder2"}} }, 
                          new Folder() { folderName = "secondFolder", files = new List<string>() { "temp2.txt", "file2.txt" } }, 
                          new Folder() { folderName = "thirdFolder", files = new List<string>() { "temp3.txt", "file3.txt" } } },
                files = new List<string>() { "temp.txt", "file.txt" }

            };
        }
        /*
         * firstFolder -> temp1.txt, file1.txt
         * secondFolder -> temp2.txt, file2.txt
         * thirdFolder -> temp3.txt, file3.txt
         * 
         * temp.txt
         * file.txt
         * */


        [TestMethod]
        public void OnSplitArrayIsOrganizedAsExpected()
        {
            string virtualPath = "/folder1/folder2";
            string[] splitPath = virtualPath.Split('/');

            Assert.AreEqual("", splitPath[0]);

        }

        [TestMethod]
        public void WhenAddFileToRootReturnTrue()
        {
            string newFile = "something.txt";
            int val = testRoot.files.Count;
            Assert.IsTrue(testRoot.AddFileToCWD(newFile, "/"));

            Assert.AreNotEqual(val, testRoot.files.Count);

        }

        [TestMethod]
        public void WhenAddFileThatAlreadyExistsReturnsFalse()
        {
            string newFile = "temp.txt";

            Assert.IsFalse(testRoot.AddFileToCWD(newFile, "/"));
        }

        [TestMethod]
        public void WhenAddFileThatAlreadyExistsReturnDoesNotAddItem()
        {
            string newFolder = "temp.txt";
            int val = testRoot.files.Count;
            testRoot.AddFileToCWD(newFolder, "/");
            Assert.AreEqual(val, testRoot.files.Count);
        }

        [TestMethod]
        public void WhenAddFileToRootListGrows()
        {
            string newFile = "something.txt";
            int val = testRoot.files.Count;
            testRoot.AddFileToCWD(newFile, "/");

            Assert.AreNotEqual(val, testRoot.files.Count);

        }

        [TestMethod]
        public void WhenAddFileToDirectoryListGrows()
        {
            string newFile = "something.txt";
            int val = testRoot.files.Count;
            testRoot.AddFileToCWD(newFile, "/firstFolder/");
            Folder something = testRoot.folders.Find(x => x.folderName == "firstFolder");
            Assert.AreNotEqual(val, something.files.Count);
        }

        [TestMethod]
        public void WhenAddFileToDirectoryWhereFileAlreadyExistsReturnFalse()
        {
            string newFile = "temp1.txt";
            int val = testRoot.files.Count;
            Assert.IsFalse(testRoot.AddFileToCWD(newFile, "/firstFolder/"));
        }

        [TestMethod]
        public void WhenAddFileToDirectoryWhereReturnTrue()
        {
            string newFile = "something";
            Assert.IsTrue(testRoot.AddFileToCWD(newFile, "/firstFolder/"));
        }



        //tests for folders
        [TestMethod]
        public void WhenAddFolderToRootReturnTrue()
        {
            string newFolder = "something";
            Assert.IsTrue(testRoot.AddFolderToCWD(newFolder, "/"));
        }

        [TestMethod]
        public void WhenAddFolderThatAlreadyExistsReturnsFalse()
        {
            string newFolder = "firstFolder";
            Assert.IsFalse(testRoot.AddFolderToCWD(newFolder, "/"));
        }

        [TestMethod]
        public void WhenAddFolderThatAlreadyExistsDoesNotAddItem()
        {
            string newFolder = "firstFolder";
            int val = testRoot.folders.Count;
            testRoot.AddFolderToCWD(newFolder, "/");
            Assert.AreEqual(val, testRoot.folders.Count);
        }

        [TestMethod]
        public void WhenAddFolderToRootListGrows()
        {
            string newFolder = "something";
            int val = testRoot.folders.Count;
            testRoot.AddFolderToCWD(newFolder, "/");

            Assert.AreNotEqual(val, testRoot.folders.Count);
        }

        [TestMethod]
        public void WhenAddFolderToDirectoryListGrows()
        {
            string newFolder = "something";
            int val = testRoot.folders.Find(x => x.folderName == "firstFolder").folders.Count;//count number of subfolders      
            testRoot.AddFolderToCWD(newFolder, "/firstFolder/");
            Folder subFolder = testRoot.folders.Find(x => x.folderName == "firstFolder");
            //val should be 1 and subfoldercount should be 2
            Assert.AreNotEqual(val, subFolder.folders.Count);
        }

        [TestMethod]
        public void WhenAddFolderToDirectoryWhereFolderAlreadyExistsReturnFalse()
        {
            string newFile = "firstFolder2";
            Assert.IsFalse(testRoot.AddFolderToCWD(newFile, "/firstFolder/"));
        }

        [TestMethod]
        public void WhenAddFolderToDirectoryWhereNotExistsReturnTrue()
        {
            string newFile = "something";
            Assert.IsTrue(testRoot.AddFileToCWD(newFile, "/firstFolder/"));
        }
    }
}
