using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace PolyglotDemo.Model.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void init()
        {
            //RootDirectory testRoot = new RootDirectory() { folders = new List<Folder>() {  }}
        }

        [TestMethod]
        public void OnSplitArrayIsOrganizedAsExpected()
        {
            string virtualPath = "/folder1/folder2";
            string[] splitPath = virtualPath.Split('/');

            Assert.AreEqual("", splitPath[0]);

        }

        [TestMethod]
        public void WhenAddFileToRootListGrows()
        {
            
        }
    }
}
