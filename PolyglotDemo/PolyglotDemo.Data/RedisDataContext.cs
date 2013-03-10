using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;
using ServiceStack.Text;

namespace PolyglotDemo.Data
{
    public class RedisDataContext
    {
        RedisClient dataContext;

        public RedisDataContext()
        {
            dataContext = new RedisClient("localhost");
        }

        
        ~RedisDataContext()
        {
            this.dataContext.Quit();
        }

        public virtual string InsertFile(string filePath, string uploadFilePath)
        {
            string val = GetFileContents(uploadFilePath);
            dataContext.Add<string>(filePath, val);
            return val;
        }

        /// <summary>
        /// Helper function for InsertFile(string filePath, string uploadFilePath)
        /// </summary>
        /// <param name="uploadFilePath"></param>
        /// <returns></returns>
        private string GetFileContents(string uploadFilePath)
        {
            string[] fileContents = File.ReadAllLines(uploadFilePath);
            string returnValue = "";
            foreach (string val in fileContents)
            {
                returnValue = returnValue + val;
            }
            return returnValue;
        }
        
        /// <summary>
        /// Adds a new key/value pair to redis with the key being the virtual file path and the data being a byte array
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="uploadFilePath"></param>
        public virtual void InsertFile(string filePath, byte[] uploadFilePath)
        {
            dataContext.Add<byte[]>(filePath, uploadFilePath);
            //dataContext.Save(); //will save the dataset to disk.. which I haven't configured permissions to do just yet
        }

        

        public virtual byte[] ReadFile(string filePath)
        {
            try
            {
                return dataContext.Get(filePath);
            }
            catch (System.NullReferenceException)
            {
                return null;
            }
        }

        public virtual void DeleteFile(string filePath)
        {
            dataContext.Remove(filePath);
        }
    }
}
