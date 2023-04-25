using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TeamMember.API.Entities;
using TeamMember.API.Repositories;

namespace TeamMember.API.Controllers
{
    [ApiController]
    [Route("projectmgmt/api/v1/[controller]/[action]")]
    public class MemberController: ControllerBase
    {
        private readonly ITasksRepository _repository;
        private readonly ILogger<MemberController> _logger;

        public MemberController(ITasksRepository repository, ILogger<MemberController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        /// <summary>
        /// Get team member's task by member id
        /// </summary>
        /// <param name="memberId"></param>
        /// <param name="count"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "TeamMember")]
        [Route("{memberId}/{count}/{page}")]
        [ProducesResponseType(typeof(IEnumerable<Tasks>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Tasks>> GetTasksById(int memberId, int count, int page)
        {
            var tasks = await _repository.GetTasksById(memberId, count, page);
            if (tasks == null)
            {
                return null;
            }

            return Ok(tasks);
        }

        /// <summary>
        /// Get team member's task count
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "TeamMember")]
        [Route("{memberId}")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<long>> GetTaskCountById(int memberId)
        {
            var taskCount = await _repository.GetTaskCountById(memberId);
            return Ok(taskCount);
        }
    }
}
