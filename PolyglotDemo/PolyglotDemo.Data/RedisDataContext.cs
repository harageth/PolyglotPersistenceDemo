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
        
        protected RedisClient GetDatabase()
        {
            return new RedisClient("localhost");
        }

        public virtual string InsertFile(string filePath, string uploadFilePath)
        {
            RedisClient dataContext = GetDatabase();
            string val = GetFileContents(uploadFilePath);
            dataContext.Add<string>(filePath, val);
            return val;
        }

        public virtual void InsertFile(string filePath, byte[] uploadFilePath)
        {
            RedisClient dataContext = GetDatabase();
            dataContext.Add<byte[]>(filePath, uploadFilePath);
        }

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

        public virtual byte[] ReadFile(string filePath)
        {
            RedisClient dataContext = GetDatabase();
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
            RedisClient dataContext = GetDatabase();

            dataContext.Remove(filePath);
        }
    }
}
