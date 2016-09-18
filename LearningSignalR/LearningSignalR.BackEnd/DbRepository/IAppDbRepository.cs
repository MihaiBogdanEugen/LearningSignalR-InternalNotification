using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LearningSignalR.BackEnd.ViewModels.Messages;
using LearningSignalR.Db;

namespace LearningSignalR.BackEnd.DbRepository
{
    public interface IAppDbRepository : IDisposable
    {
        Task<DbRepositoryResult<DetailsMessage>> DetailsMessage(string code, CancellationToken cancellationToken = default(CancellationToken));
        Task<DbRepositoryResult<List<ListMessage>>> ListMessage(long userId, CancellationToken cancellationToken = default(CancellationToken));
        Task<DbRepositoryResult<StatusMessage>> StatusMessage(long userId, CancellationToken cancellationToken = default(CancellationToken));
        Task<DbRepositoryResult> MarkAsRead(string code, long readByUserId, CancellationToken cancellationToken = default(CancellationToken));
    }
}