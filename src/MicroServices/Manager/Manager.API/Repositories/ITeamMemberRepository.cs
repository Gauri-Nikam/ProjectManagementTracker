using Manager.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manager.API.Repositories
{
    public interface ITeamMemberRepository
    {
        Task<IEnumerable<TeamMember>> GetMemberDetails();

        Task<TeamMember> GetMemberById(int memberId);

        Task AddMember(TeamMember teamMember);

        Task UpdateAllocation(int memberId);

        bool ValidateTaskEndDate(int memberId, string taskEndDate);
    }
}
