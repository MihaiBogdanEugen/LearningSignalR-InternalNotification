using System;

namespace LearningSignalR.Db
{
    public class DbRepositoryArgs : IDisposable
    {
        public string ConnectionName { get; set; }
        public string ConnectionString { get; set; }

        public string ConnectionNameOrConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(this.ConnectionName) == false)
                    return this.ConnectionName;

                if (string.IsNullOrEmpty(this.ConnectionString) == false)
                    return this.ConnectionString;

                throw new ArgumentNullException();
            }
        }

        public void Dispose()
        {
            // do nothing
        }
    }
}