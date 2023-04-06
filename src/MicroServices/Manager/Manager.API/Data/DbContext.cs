using Manager.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;

namespace Manager.API.Data
{
    public class DbContext : IDbContext
    {
        public DbContext(IConfiguration configuration)
        {
            try
            {
                var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
                TeamMembers = database.GetCollection<TeamMember>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public IMongoCollection<TeamMember> TeamMembers { get; }
    }
}
