namespace LearningSignalR.BackEnd.ViewModels.Messages
{
    public class StatusMessage
    {
        public long CompanyId { get; set; }

        public long TotalNoOfMessages { get; set; }

        public long TotalNoOfUnreadMessages { get; set; }
    }
}