using AutoMapper;
using EventBus.Messages.Events;
using Manager.API.Entities;
using Manager.API.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;

namespace Manager.API.Controllers
{
    [ApiController]
    [Route("projectmanagement/api/v1/[controller]/[action]" )]
    public class ManagerController: ControllerBase
    {
        private readonly ITeamMemberRepository _repository;
        private readonly ILogger<ManagerController> _logger;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndPoint;

        public ManagerController(ITeamMemberRepository repository, ILogger<ManagerController> logger, IMapper mapper,IPublishEndpoint publishEndpoint)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _publishEndPoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        /// <summary>
        /// Get all membes details 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TeamMember>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<TeamMember>>> GetMemberDetails()
        {
            try
            {
                var teamMembers = await _repository.GetMemberDetails();
                return Ok(teamMembers);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        /// <summary>
        /// Get member details by id
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("memberId")]
        [ProducesResponseType(typeof(TeamMember), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<TeamMember>> GetMemberById(int memberId)
        {
            var teamMember = await _repository.GetMemberById(memberId);
            if(teamMember == null)
            {
                return null;
            }

            return Ok(teamMember);
        }

        /// <summary>
        /// Add one member at a time
        /// </summary>
        /// <param name="teamMember"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task AddMember([FromBody] TeamMember teamMember)
        {
            await _repository.AddMember(teamMember);
        }

        /// <summary>
        /// Updates allocation percentage for team member
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("memberId")]
        public async Task UpdateAllocation(int memberId)
        {
            await _repository.UpdateAllocation(memberId);
        }

        /// <summary>
        /// Add tasks for team member
        /// </summary>
        /// <param name="assignedTask"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddTask([FromBody] Tasks assignedTask)
        {
            try
            {
                if (_repository.ValidateTaskEndDate(assignedTask.MemberId, assignedTask.TaskEndDate))
                {
                    var eventMessage = _mapper.Map<TaskEvents>(assignedTask);
                    await _publishEndPoint.Publish(eventMessage);
                    return Accepted();
                }
                else
                {
                    throw new DataException("Task date cannot be greater than end date");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                Console.WriteLine(ex);
                return null;
            }
        }

    }
}
