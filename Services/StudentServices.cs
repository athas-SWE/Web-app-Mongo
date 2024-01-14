using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Web_App_Mongo.Data;
using Web_App_Mongo.Models;
using Web_App_Mongo.Services;


namespace Web_App_Mongo.Services
{
    public class StudentServices
    {
        private readonly IMongoCollection<Student> studentCollection;

        public StudentServices(IOptions<DatabaseSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.Connection);
            var mongoDb = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _studentCollection = mongoDb.GetCollection<Student>(settings.Value.DatabaseName);
        }

        public async Task<List<Student>> GetAsync() => await _studentCollection.Find(_ => true).ToListAsync();

        public async Task<Student> GetAsync(string id) =>
            await _studentCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    }
}
