using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LearningSignalR.BackEnd.ViewModels.Messages;
using LearningSignalR.Db;

namespace LearningSignalR.BackEnd.DbRepository.Sql
{
    public class AppSqlDbRepository : BaseSqlDbRepository, IAppDbRepository
    {
        public AppSqlDbRepository(DbRepositoryArgs settings) : base(settings) { }

        public Task<DbRepositoryResult<DetailsMessage>> DetailsMessage(string code, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<DbRepositoryResult<List<ListMessage>>> ListMessage(long userId, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<DbRepositoryResult<StatusMessage>> StatusMessage(long userId, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<DbRepositoryResult> MarkAsRead(string code, long readByUserId, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}
