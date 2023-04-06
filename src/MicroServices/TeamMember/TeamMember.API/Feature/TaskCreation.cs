using MediatR;

namespace TeamMember.API.Feature
{
    public class TaskCreation: IRequest<string>
    {
        public int MemberId { get; set; }

        public string MemberName { get; set; }

        public string TaskName { get; set; }

        public string Deliverables { get; set; }

        public string TaskStartDate { get; set; }

        public string TaskEndDate { get; set; }
    }
}
