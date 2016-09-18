namespace LearningSignalR.Db
{
    public enum DbRepositoryResultStatus
    {
        Success,
        Error,
        NotFound,
        Created,
        Updated,
        Deleted,
        NotAuthorized,
        InvalidToken,
        TokenExpired,
        Unknown,
    }
}