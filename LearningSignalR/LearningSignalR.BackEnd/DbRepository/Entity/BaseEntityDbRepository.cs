using System;
using System.Data.Entity;
using System.Threading.Tasks;
using LearningSignalR.Db;

namespace LearningSignalR.BackEnd.DbRepository.Entity
{
    public abstract class BaseEntityDbRepository : IDisposable
    {
        private AppDbContext _dbContext;

        protected BaseEntityDbRepository(DbRepositoryArgs args)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));

            this._dbContext = new AppDbContext(args.ConnectionName);
        }

        ~BaseEntityDbRepository()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && this._dbContext != null)
            {
                this._dbContext.Dispose();
                this._dbContext = null;
            }
        }

        protected async Task<DbRepositoryResult<T>> SafeExecuteAsync<T>(Func<AppDbContext, Task<T>> getDataAsync)
        {
            try
            {
                var result = await getDataAsync(this._dbContext);

                return result == null
                    ? DbRepositoryResult<T>.Failure(DbRepositoryResultStatus.NotFound)
                    : DbRepositoryResult<T>.Success(result);
            }
            catch (Exception error)
            {
                return DbRepositoryResult<T>.Failure(DbRepositoryResultStatus.Error, error.Message, error);
            }
        }

        protected async Task<DbRepositoryResult> SafeExecuteAsync(Func<AppDbContext, Task<DbRepositoryResult>> func)
        {
            try
            {
                return await func(this._dbContext);
            }
            catch (NotAuthorizedException authError)
            {
                return DbRepositoryResult.Failure(DbRepositoryResultStatus.NotAuthorized, authError.Message, authError);
            }
            catch (Exception error)
            {
                return DbRepositoryResult.Failure(DbRepositoryResultStatus.Error, error.Message, error);
            }
        }

        protected async Task<DbRepositoryResult> DeleteAsync<T>(int id) where T : BaseModel
        {
            return await this.SafeExecuteAsync(async dbContext =>
            {
                var entity = await dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
                if (entity == null)
                    return DbRepositoryResult.Failure(DbRepositoryResultStatus.NotFound);

                dbContext.Entry(entity).State = EntityState.Deleted;
                await dbContext.SaveChangesAsync();

                return DbRepositoryResult.DeletedSuccessful;
            });
        }
    }
}