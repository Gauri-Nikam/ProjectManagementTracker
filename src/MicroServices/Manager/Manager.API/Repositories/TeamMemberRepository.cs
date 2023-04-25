using Manager.API.Data;
using Manager.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.API.Repositories
{
    public class TeamMemberRepository : ITeamMemberRepository
    {
        private readonly IDbContext _dbContext;

        public TeamMemberRepository(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddMember(TeamMember teamMember)
        {
            var memberCount = _dbContext.TeamMembers.AsQueryable().Where(x => x.MemberId == teamMember.MemberId).Count();

            if(memberCount == 0)
            {
                await _dbContext.TeamMembers.InsertOneAsync(teamMember);
            }
            else 
            {
                throw new DuplicateWaitObjectException("The member with id " + teamMember.Id + " already exists");
            }
        }

        public async Task<TeamMember> GetMemberById(int memberId)
        {
            return await _dbContext.TeamMembers.Find(x => x.MemberId == memberId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TeamMember>> GetMemberDetails()
        {
            return await _dbContext.TeamMembers.Find(x => x.MemberId != null).ToListAsync();
        }

        public async Task UpdateAllocation(int memberId)
        {
            Console.WriteLine("member id =" + memberId);
            if (memberId != 0)
            {
                var member = _dbContext.TeamMembers.AsQueryable().Where(x => x.MemberId == memberId).FirstOrDefault();

                if (DateTime.Parse(member.ProjectEndDate).Date < DateTime.Now.Date)
                {
                    await _dbContext.TeamMembers.UpdateOneAsync(x => x.MemberId == memberId,
                        Builders<TeamMember>.Update.Set(y => y.AllocationPercentage, 0));
                }
                else if (DateTime.Parse(member.ProjectEndDate).Date >= DateTime.Now.Date)
                {
                    await _dbContext.TeamMembers.UpdateOneAsync(x => x.MemberId == memberId,
                        Builders<TeamMember>.Update.Set(y => y.AllocationPercentage, 100));
                }
            }

        }

        public bool ValidateTaskEndDate(int memberId, string taskEndDate)
        {
            var member = _dbContext.TeamMembers.AsQueryable().Where(x => x.MemberId == memberId).FirstOrDefault();

            if (DateTime.Parse(taskEndDate).Date > DateTime.Parse(member.ProjectEndDate).Date)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
