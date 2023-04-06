using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using TeamMember.API.Entities;
using TeamMember.API.Repositories;

namespace TeamMember.API.Feature
{
    public class TaskCreationHandler : IRequestHandler<TaskCreation, string>
    {
        private readonly ITasksRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<TaskCreationHandler> _logger;

        public TaskCreationHandler(ITasksRepository repository, IMapper mapper, ILogger<TaskCreationHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> Handle(TaskCreation request, CancellationToken cancellationToken)
        {
            var newTask = _mapper.Map<Tasks>(request);
            var createdTaskId = await _repository.AddTask(newTask);
            return createdTaskId;
        }
    }
}
