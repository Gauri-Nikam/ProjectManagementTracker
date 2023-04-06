using System.Collections.Generic;
using System.Threading.Tasks;
using TeamMember.API.Entities;

namespace TeamMember.API.Repositories
{
    public interface ITasksRepository
    {
        Task<IEnumerable<Tasks>> GetTasksById(int memberId, int count, int page);

        Task<long> GetTaskCountById(int memberId);

        Task<string> AddTask(Tasks newTask);

    }
}
