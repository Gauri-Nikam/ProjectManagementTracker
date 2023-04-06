namespace EventBus.Messages.Events
{
    public class TaskEvents: IntegrationBaseEvent
    {
        public int MemberId { get; set; }

        public string MemberName { get; set; }

        public string TaskName { get; set; }

        public string Deliverables { get; set; }

        public string TaskStartDate { get; set; }

        public string TaskEndDate { get; set; }

    }
}
