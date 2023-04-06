using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TeamMember.API.Feature;

namespace TeamMember.API.EventBusConsumer
{
    public class TasksConsumer : IConsumer<TaskEvents>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<TasksConsumer> _logger;

        public TasksConsumer(IMediator mediator, IMapper mapper, ILogger<TasksConsumer> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<TaskEvents> context)
        {
            var command = _mapper.Map<TaskCreation>(context.Message);
            var result = await _mediator.Send(command);

            Console.WriteLine(result);
        }
    }
}
