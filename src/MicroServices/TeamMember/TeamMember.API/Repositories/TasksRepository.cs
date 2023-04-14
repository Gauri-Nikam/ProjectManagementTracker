using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamMember.API.Data;
using TeamMember.API.Entities;

namespace TeamMember.API.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly IDbContext _dbContext;

        public TasksRepository(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<string> AddTask(Tasks newTask)
        {
            await _dbContext.TaskDetails.InsertOneAsync(newTask);
            return newTask.Id;
        }

        public async Task<long> GetTaskCountById(int memberId)
        {
            return await _dbContext.TaskDetails.Find(x => x.MemberId == memberId).CountDocumentsAsync();
        }

        public async Task<IEnumerable<Tasks>> GetTasksById(int memberId, int count, int page)
        {
            var skipSize = (page - 1) * count;
            return await _dbContext.TaskDetails.Find(x => x.MemberId == memberId).SortBy(y => y.TaskName).Skip(skipSize).Limit(count).ToListAsync();

        }
    }
}
