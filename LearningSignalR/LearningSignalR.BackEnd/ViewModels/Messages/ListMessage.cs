using System;

namespace LearningSignalR.BackEnd.ViewModels.Messages
{
    public class ListMessage
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public long CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string Subject { get; set; }
        
        public string SentByUserName { get; set; }

        public DateTime SentAt { get; set; }

        public bool IsRead { get; set; }
    }
}