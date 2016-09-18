using System;
using System.Data.Entity;
using LearningSignalR.Db.Models.Identity;

namespace LearningSignalR.Db.Models
{
    public class Message : BaseIdModel<long>
    {
        public long CompanyId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public long SentByUserId { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;
        public DateTime? ReatAt { get; set; } = null;
        public long? ReadByUserId { get; set; }
        public string Code { get; set; } = Guid.NewGuid().ToString("N");

        public virtual Company Company { get; set; }
        public virtual User SentByUser { get; set; }
        public virtual User ReadByUser { get; set; }

        internal static void AddModelDefinition(DbModelBuilder modelBuilder, string tableName = "Messages", string schemaName = "dbo")
        {
            var model = BaseIdModel<long>.GetModelDefinition<Message>(modelBuilder, tableName, schemaName);

            model.Column(x => x.CompanyId, "CompanyId", 1);
            model.StringColumn(x => x.Subject, "Subject", 2);
            model.StringColumn(x => x.Body, "Body", 3, isRequired: false, stringMaxLength: DbConstants.StringLength4096);
            model.Column(x => x.SentByUserId, "SentByUserId", 4);
            model.DateTimeColumn(x => x.SentAt, "SentAt", 5);
            model.Column(x => x.IsRead, "IsRead", 6);
            model.DateTimeColumn(x => x.ReatAt, "ReatAt", 7);
            model.Column(x => x.ReadByUserId, "ReadByUserId", 8);
            model.StringColumn(x => x.Code, "Code", 9);
        }
    }
}