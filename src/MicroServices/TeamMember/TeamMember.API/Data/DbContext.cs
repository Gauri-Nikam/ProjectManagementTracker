using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using TeamMember.API.Entities;

namespace TeamMember.API.Data
{
    public class DbContext: IDbContext
    {
        public DbContext(IConfiguration configuration)
        {
            try
            {
                var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
                TaskDetails = database.GetCollection<Tasks>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public IMongoCollection<Tasks> TaskDetails { get; }
    }
}
