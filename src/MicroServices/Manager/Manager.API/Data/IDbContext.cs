using Manager.API.Entities;
using MongoDB.Driver;

namespace Manager.API.Data
{
    public interface IDbContext
    {
        IMongoCollection<TeamMember> TeamMembers { get; }
    }
}
