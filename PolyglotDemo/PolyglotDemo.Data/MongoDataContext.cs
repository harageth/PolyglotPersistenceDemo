using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PolyglotDemo.Model;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace PolyglotDemo.Data
{
    public class MongoDataContext
    {
        //connection string format - mongodb://[username:password@]hostname[:port][/[database][?options]]
        protected virtual string ConnectionString
        {
            get
            {
                //return "mongodb://127.4.112.1:27017";
                return "mongodb://localhost:27017";
            }
        }

        ~MongoDataContext()
        {
            
        }

        protected virtual MongoDatabase GetDatabase()
        {
            //const string connectionString = "mongodb://admin:Y7UItTVlGTrw@127.4.112.1:27017/redis";
            const string connectionString = "mongodb://localhost:27017";
            //const string connectionString = "mongodb://admin:Y7UItTVlGTrw@redis-filecloud.rhcloud.com/redis";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            return server.GetDatabase("Polyglot");
            //return server.GetDatabase("redis");
        }

        private MongoCollection<TDataType> GetCollection<TDataType>()
        {
            var database = GetDatabase();
            return database.GetCollection<TDataType>(typeof(TDataType).Name.ToLower());
        }

        public virtual IEnumerable<RootDirectory> GetFileStructure(string username = null)
        {
            var query = string.IsNullOrEmpty(username) ? new QueryDocument() : Query<RootDirectory>.EQ(x => x.un, username);
            return GetCollection<RootDirectory>().Find(query);
        }

        public virtual void AppendFile(string fileName, string username)
        {
            var collection = GetCollection<RootDirectory>();
            collection.Update(Query<RootDirectory>.EQ(x => x.un, username),
                Update<RootDirectory>.Push(x => x.files, fileName));
        }

        public virtual void UpdateFileStructure(Folder fileStructureUpdate, string username)
        {
            //needs to be implemented

            var collection = GetCollection<RootDirectory>();
            collection.Update(Query<RootDirectory>.EQ(x => x.un, username), 
                Update<RootDirectory>.Push(x => x.folders, fileStructureUpdate));
        }

        public virtual void UpdateFileStructure(RootDirectory updateFileStructure)
        {
            //needs to be implemented

            var collection = GetCollection<RootDirectory>();
            collection.Save(updateFileStructure);
        }
    }
}
