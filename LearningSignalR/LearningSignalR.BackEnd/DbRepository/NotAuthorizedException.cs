using System;
using System.Runtime.Serialization;

namespace LearningSignalR.BackEnd.DbRepository
{
    [Serializable]
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException() : base() { }

        public NotAuthorizedException(string errorMessage) : base(errorMessage) { }

        public NotAuthorizedException(string errorMessage, Exception innerException) : base(errorMessage, innerException) { }

        public NotAuthorizedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}