using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using NLog;

namespace LearningSignalR.Db
{
    public class LoggerDbCommandInterceptor : DbCommandInterceptor
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public override void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            this._stopwatch.Stop();
            var commandText = command.CommandText;
            var duration = this._stopwatch.Elapsed;

            if (interceptionContext.Exception == null)
            {
                var message = $"Non-query command [{commandText}] was executed in {duration}";
                LoggerDbCommandInterceptor.Logger.Trace(message);
            }
            else
            {
                var message = $"Error executing non-query command {commandText}";
                LoggerDbCommandInterceptor.Logger.Error(interceptionContext.Exception, message);
            }


            base.NonQueryExecuted(command, interceptionContext);
        }

        public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            base.NonQueryExecuting(command, interceptionContext);
            this._stopwatch.Restart();
        }

        public override void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            this._stopwatch.Stop();
            var commandText = command.CommandText;
            var duration = this._stopwatch.Elapsed;

            if (interceptionContext.Exception == null)
            {
                var message = $"Reader command [{commandText}] was executed in {duration}";
                LoggerDbCommandInterceptor.Logger.Trace(message);
            }
            else
            {
                var message = $"Error executing reader command {commandText}";
                LoggerDbCommandInterceptor.Logger.Error(interceptionContext.Exception, message);
            }

            base.ReaderExecuted(command, interceptionContext);
        }

        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            base.ReaderExecuting(command, interceptionContext);
            this._stopwatch.Restart();
        }

        public override void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            this._stopwatch.Stop();
            var commandText = command.CommandText;
            var duration = this._stopwatch.Elapsed;

            if (interceptionContext.Exception == null)
            {
                var message = $"Scalar command [{commandText}] was executed in {duration}";
                LoggerDbCommandInterceptor.Logger.Trace(message);
            }
            else
            {
                var message = $"Error executing scalar command {commandText}";
                LoggerDbCommandInterceptor.Logger.Error(interceptionContext.Exception, message);
            }

            base.ScalarExecuted(command, interceptionContext);
        }

        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            base.ScalarExecuting(command, interceptionContext);
            this._stopwatch.Restart();
        }
    }
}