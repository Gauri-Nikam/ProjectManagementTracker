using MongoDB.Driver;
using TeamMember.API.Entities;

namespace TeamMember.API.Data
{
    public interface IDbContext
    {
        IMongoCollection<Tasks> TaskDetails { get; }
    }
}
