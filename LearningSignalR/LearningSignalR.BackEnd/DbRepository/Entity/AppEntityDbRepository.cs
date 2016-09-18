using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using LearningSignalR.BackEnd.ViewModels.Messages;
using LearningSignalR.Db;
using LearningSignalR.Db.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace LearningSignalR.BackEnd.DbRepository.Entity
{
    public class AppEntityDbRepository : BaseEntityDbRepository, IAppDbRepository
    {
        public AppEntityDbRepository(DbRepositoryArgs args) : base(args) { }

        public static AppEntityDbRepository Create(IdentityFactoryOptions<AppEntityDbRepository> options, IOwinContext context)
        {
            return new AppEntityDbRepository(context.Get<DbRepositoryArgs>());
        }

        public async Task<DbRepositoryResult> NewMessage(NewMessage message, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.SafeExecuteAsync(async dbContext =>
            {
                dbContext.Messages.Add(new Message
                {
                    Body = message.Body,
                    CompanyId = message.CompanyId,
                    IsRead = false,
                    ReadByUserId = null,
                    ReatAt = null,
                    SentAt = message.SentAt,
                    SentByUserId = message.SentByUserId,
                    Subject = message.Subject,
                });
                await dbContext.SaveChangesAsync(cancellationToken);
                return DbRepositoryResult.CreatedSuccessful;
            });
        }

        public async Task<DbRepositoryResult<DetailsMessage>> DetailsMessage(string code, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.SafeExecuteAsync(async dbContext =>
            {
                return await dbContext.Messages
                    .Include(x => x.Company)
                    .Include(x => x.SentByUser)
                    .Select(x => new DetailsMessage
                    {
                        Id = x.Id,
                        SentAt = x.SentAt,
                        Subject = x.Subject,
                        Body = x.Body,
                        IsRead = x.IsRead,
                        Code = x.Code,
                        CompanyName = x.Company.Name,
                        SentByUserName = x.SentByUser.FirstName + " " + x.SentByUser.LastName
                    })
                    .FirstOrDefaultAsync(x => x.Code == code, cancellationToken);
            });
        }

        public async Task<DbRepositoryResult<List<ListMessage>>> ListMessage(long userId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.SafeExecuteAsync(async dbContext =>
            {
                var temp = await dbContext.Users
                    .Select(x => new
                    {
                        x.CompanyId,
                        x.Id
                    })
                    .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

                var query = dbContext.Messages
                    .Include(x => x.Company)
                    .Include(x => x.SentByUser)
                    .Select(x => new ListMessage
                    {
                        CompanyId = x.CompanyId,
                        Id = x.Id,
                        SentAt = x.SentAt,
                        Subject = x.Subject,
                        IsRead = x.IsRead,
                        Code = x.Code,
                        CompanyName = x.Company.Name,
                        SentByUserName = x.SentByUser.FirstName + " " + x.SentByUser.LastName
                    });

                if (temp == null)
                    return await query
                        .OrderByDescending(x => x.SentAt)
                        .ToListAsync(cancellationToken);

                return await query
                    .Where(x => x.CompanyId == temp.CompanyId)
                    .OrderByDescending(x => x.SentAt)
                    .ToListAsync(cancellationToken);
            });
        }

        public async Task<DbRepositoryResult<List<SelectListItem>>> GetCompaniesSelectList(long? selectedId = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.SafeExecuteAsync(async dbContext =>
            {
                return await dbContext.Companies
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString(),
                        Selected = selectedId.HasValue && selectedId.Value == x.Id
                    })
                    .ToListAsync(cancellationToken);
            });
        }

        public async Task<DbRepositoryResult<StatusMessage>> StatusMessage(long userId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.SafeExecuteAsync(async dbContext =>
            {
                var temp = await dbContext.Users
                    .Select(x => new
                    {
                        x.CompanyId,
                        x.Id
                    })
                    .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
                if (temp == null)
                    return null;

                var companyId = temp.CompanyId;

                var query = dbContext.Messages
                    .Select(x => new
                    {
                        x.CompanyId,
                        x.Id,
                        x.IsRead
                    })
                    .Where(x => x.CompanyId == companyId);

                var totalNoOfMessages = await query.LongCountAsync(cancellationToken);
                var totalNoOfUnreadMessages = await query.LongCountAsync(x => x.IsRead == false, cancellationToken);

                return new StatusMessage
                {
                    CompanyId = companyId,
                    TotalNoOfMessages = totalNoOfMessages,
                    TotalNoOfUnreadMessages = totalNoOfUnreadMessages
                };
            });
        }

        public async Task<DbRepositoryResult> MarkAsRead(string code, long readByUserId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.SafeExecuteAsync(async dbContext =>
            {
                var message = await dbContext.Messages.FirstOrDefaultAsync(x => x.Code == code, cancellationToken);
                if (message == null)
                    return new DbRepositoryResult
                    {
                        Status = DbRepositoryResultStatus.NotFound
                    };

                message.IsRead = true;
                message.ReadByUserId = readByUserId;

                dbContext.Entry(message).State = EntityState.Modified;
                await dbContext.SaveChangesAsync(cancellationToken);

                return DbRepositoryResult.UpdatedSuccessful; 
            });
        }
    }
}