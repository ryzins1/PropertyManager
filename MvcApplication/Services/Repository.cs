using System.Configuration;
using MongoDB.Driver;
using MvcApplication.Models;

namespace MvcApplication.Services
{
    public class Repository
    {
	    private readonly MongoDatabase _mongoDb;

        public Repository()
        {
		    var connectionString = ConfigurationManager.ConnectionStrings["PropertyManager"].ConnectionString;
		    var client = new MongoClient(connectionString);
		    var server = client.GetServer();
		    _mongoDb = server.GetDatabase("PropertyManager");
        }

        public MongoCollection<Company> Companies
        {
            get { return _mongoDb.GetCollection<Company>(typeof(Company).Name); }
        }

        public MongoCollection<Building> Buildings
        {
            get { return _mongoDb.GetCollection<Building>(typeof(Building).Name); }
        }

        public MongoCollection<Lease> Leases
        {
            get { return _mongoDb.GetCollection<Lease>(typeof(Lease).Name); }
        }

        public MongoCollection<Unit> Units
        {
            get { return _mongoDb.GetCollection<Unit>(typeof(Unit).Name); }
        }
    }
}