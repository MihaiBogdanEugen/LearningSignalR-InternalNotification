using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LearningSignalR.Db;
using Microsoft.AspNet.SignalR;

namespace LearningSignalR.Infrastructure.Hubs
{
    public class JobHub : Hub
    {
        public async Task Subscribe(long companyId)
        {
            await this.Groups.Add(this.Context.ConnectionId, GetGroup(companyId));

            using (var dbContext = new AppDbContext())
            {
                var query = dbContext.Messages
                    .Select(x => new
                    {
                        x.CompanyId,
                        x.Id,
                        x.IsRead
                    })
                    .Where(x => x.CompanyId == companyId);

                var totalNoOfMessages = await query.LongCountAsync();
                var totalNoOfUnreadMessages = await query.LongCountAsync(x => x.IsRead == false);

                this.Clients.Client(this.Context.ConnectionId).update(companyId, totalNoOfMessages, totalNoOfUnreadMessages);
            }
        }

        public static string GetGroup(long companyId)
        {
            return "company:" + companyId;
        }
    }
}