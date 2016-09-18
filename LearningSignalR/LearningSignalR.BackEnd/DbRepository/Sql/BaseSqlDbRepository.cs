using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using LearningSignalR.Db;

namespace LearningSignalR.BackEnd.DbRepository.Sql
{
    public class BaseSqlDbRepository : IDisposable
    {
        private readonly string _connectionString;

        protected BaseSqlDbRepository(DbRepositoryArgs args)
        {
            if (string.IsNullOrEmpty(args?.ConnectionString))
                throw new ArgumentNullException(nameof(args));

            this._connectionString = args.ConnectionString;
        }

        protected async Task<T> SafeExecuteAsync<T>(Func<IDbConnection, Task<T>> getDataAsync)
        {
            try
            {
                using (var dbConnection = new SqlConnection(this._connectionString))
                {
                    await dbConnection.OpenAsync();
                    return await getDataAsync(dbConnection);
                }
            }
            catch (TimeoutException error)
            {
                var errorMessage = $"{this.GetType().FullName}.WithConnection() experienced a timeout";
                throw new Exception(errorMessage, error);
            }
            catch (SqlException error)
            {
                var errorMessage = $"{this.GetType().FullName}.WithConnection() experienced an sql error (not a timeout)";

                throw new Exception(errorMessage, error);
            }
        }

        protected async Task<T> SafeExecuteAsync<T>(Func<IDbConnection, IDbTransaction, Task<T>> getDataAsync)
        {
            try
            {
                using (var dbConnection = new SqlConnection(this._connectionString))
                {
                    await dbConnection.OpenAsync();
                    using (var dbTransaction = dbConnection.BeginTransaction())
                    {
                        try
                        {
                            var result = await getDataAsync(dbConnection, dbTransaction);
                            dbTransaction.Commit();
                            return result;
                        }
                        catch
                        {
                            dbTransaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (TimeoutException error)
            {
                var errorMessage = $"{this.GetType().FullName}.SafeExecuteAsync() experienced a timeout";
                throw new Exception(errorMessage, error);
            }
            catch (SqlException error)
            {
                var errorMessage = $"{this.GetType().FullName}.SafeExecuteAsync() experienced an error (not a timeout)";

                throw new Exception(errorMessage, error);
            }
        }

        protected T SafeExecute<T>(Func<IDbConnection, T> getData)
        {
            try
            {
                using (var dbConnection = new SqlConnection(this._connectionString))
                {
                    dbConnection.Open();
                    return getData(dbConnection);
                }
            }
            catch (TimeoutException error)
            {
                var errorMessage = $"{this.GetType().FullName}.WithConnection() experienced a timeout";
                throw new Exception(errorMessage, error);
            }
            catch (SqlException error)
            {
                var errorMessage = $"{this.GetType().FullName}.WithConnection() experienced an error (not a timeout)";

                throw new Exception(errorMessage, error);
            }
            catch (Exception error)
            {
                var errorMessage = $"{this.GetType().FullName}.WithConnection() experienced an error";

                throw new Exception(errorMessage, error);
            }
        }

        protected T SafeExecute<T>(Func<IDbConnection, IDbTransaction, T> getData)
        {
            try
            {
                using (var dbConnection = new SqlConnection(this._connectionString))
                {
                    dbConnection.Open();
                    using (var dbTransaction = dbConnection.BeginTransaction())
                    {
                        try
                        {
                            var result = getData(dbConnection, dbTransaction);
                            dbTransaction.Commit();
                            return result;
                        }
                        catch
                        {
                            dbTransaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (TimeoutException error)
            {
                var errorMessage = $"{this.GetType().FullName}.WithConnection() experienced a timeout";
                throw new Exception(errorMessage, error);
            }
            catch (SqlException error)
            {
                var errorMessage = $"{this.GetType().FullName}.WithConnection() experienced an error (not a timeout)";

                throw new Exception(errorMessage, error);
            }
        }

        ~BaseSqlDbRepository()
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
            if (disposing)
            {
                //do nothing
            }
            // free native resources if there are any.
        }
    }
}
