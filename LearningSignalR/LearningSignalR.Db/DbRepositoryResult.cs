using System;

namespace LearningSignalR.Db
{
    public class DbRepositoryResult<T> : DbRepositoryResult
    {
        public T Payload { get; set; }

        public new static DbRepositoryResult<T> Success(T payload)
        {
            return new DbRepositoryResult<T>
            {
                Message = string.Empty,
                Error = null,
                Status = DbRepositoryResultStatus.Success,
                Payload = payload
            };
        }

        public new static DbRepositoryResult<T> Failure(DbRepositoryResultStatus status = DbRepositoryResultStatus.Error, string message = null, Exception error = null)
        {
            var result = new DbRepositoryResult<T>
            {
                Status = status,
                Payload = default(T)
            };

            if (string.IsNullOrEmpty(message) == false)
                result.Message = message;

            if (error != null)
                result.Error = error;

            return result;
        }
    }

    public class DbRepositoryResult
    {
        private static readonly DbRepositoryResult SuccessfulResult;
        private static readonly DbRepositoryResult CreatedSuccessfulResult;
        private static readonly DbRepositoryResult UpdatedSuccessfulResult;
        private static readonly DbRepositoryResult DeletedSuccessfulResult;

        static DbRepositoryResult()
        {
            DbRepositoryResult.SuccessfulResult = new DbRepositoryResult
            {
                Message = string.Empty,
                Error = null,
                Status = DbRepositoryResultStatus.Success
            };

            DbRepositoryResult.CreatedSuccessfulResult = new DbRepositoryResult
            {
                Message = string.Empty,
                Error = null,
                Status = DbRepositoryResultStatus.Created
            };

            DbRepositoryResult.UpdatedSuccessfulResult = new DbRepositoryResult
            {
                Message = string.Empty,
                Error = null,
                Status = DbRepositoryResultStatus.Updated
            };

            DbRepositoryResult.DeletedSuccessfulResult = new DbRepositoryResult
            {
                Message = string.Empty,
                Error = null,
                Status = DbRepositoryResultStatus.Deleted
            };
        }

        public static DbRepositoryResult Success => DbRepositoryResult.SuccessfulResult;
        public static DbRepositoryResult CreatedSuccessful => DbRepositoryResult.CreatedSuccessfulResult;
        public static DbRepositoryResult UpdatedSuccessful => DbRepositoryResult.UpdatedSuccessfulResult;
        public static DbRepositoryResult DeletedSuccessful => DbRepositoryResult.DeletedSuccessfulResult;

        public static DbRepositoryResult Failure(DbRepositoryResultStatus status = DbRepositoryResultStatus.Error, string message = null, Exception error = null)
        {
            var result = new DbRepositoryResult
            {
                Status = status
            };

            if (string.IsNullOrEmpty(message) == false)
                result.Message = message;

            if (error != null)
                result.Error = error;

            return result;
        }

        public DbRepositoryResultStatus Status { get; set; }

        public string Message { get; set; }

        public Exception Error { get; set; }

        public bool IsSuccess => this.Status == DbRepositoryResultStatus.Success || this.Status == DbRepositoryResultStatus.Created || this.Status == DbRepositoryResultStatus.Updated || this.Status == DbRepositoryResultStatus.Deleted;

        public string ErrorMessage
        {
            get
            {
                if (string.IsNullOrEmpty(this.Message) == false)
                    return this.Message;

                if (this.Error != null && this.Message != null && string.IsNullOrEmpty(this.Error.Message) == false)
                    return this.Error.Message;

                return "Unkown error";
            }
        }
    }
}