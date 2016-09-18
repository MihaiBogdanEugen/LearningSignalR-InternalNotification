using System;
using System.ComponentModel.DataAnnotations;
using LearningSignalR.Db;

namespace LearningSignalR.BackEnd.ViewModels.Messages
{
    public class NewMessage
    {
        [Required]
        public long CompanyId { get; set; }

        [Required, StringLength(DbConstants.StringLength)]
        public string Subject { get; set; }

        [Required, StringLength(DbConstants.StringLength4096)]
        public string Body { get; set; }
        
        public long SentByUserId { get; set; }

        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}
