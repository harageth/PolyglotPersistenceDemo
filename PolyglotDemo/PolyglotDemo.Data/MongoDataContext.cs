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

    internal class DonationView
    {
        public string _id { get; set; }
        public Folder value { get; set; }
    }

    public class MongoDataContext
    {
        protected virtual string ConnectionString
        {
            get
            {
                return "mongodb://localhost";
            }
        }

        protected virtual MongoDatabase GetDatabase()
        {
            const string connectionString = "mongodb://localhost";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            return server.GetDatabase("Polyglot");
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

        public virtual void UpdateFileStructure(RootDirectory fileStructureUpdate)
        {
            //needs to be implemented
        }

    }
}
