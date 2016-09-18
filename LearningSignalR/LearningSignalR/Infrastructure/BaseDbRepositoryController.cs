using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LearningSignalR.BackEnd.DbRepository.Entity;
using LearningSignalR.Db;
using Microsoft.AspNet.Identity.Owin;

namespace LearningSignalR.Infrastructure
{
    public abstract class BaseDbRepositoryController : Controller
    {
        #region Properties

        private AppEntityDbRepository _dbRepository;
        protected AppEntityDbRepository DbRepository
        {
            get
            {
                return this._dbRepository ?? this.HttpContext.GetOwinContext().Get<AppEntityDbRepository>();
            }
            set
            {
                this._dbRepository = value;
            }
        }

        #endregion Properties

        #region Constructors

        protected BaseDbRepositoryController()
        {
        }

        protected BaseDbRepositoryController(AppEntityDbRepository dbRepository)
        {
            this.DbRepository = dbRepository;
        }

        #endregion Constructors

        #region Overriden Members

        [NonAction]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._dbRepository != null)
                {
                    this._dbRepository.Dispose();
                    this._dbRepository = null;
                }
            }

            base.Dispose(disposing);
        }

        #endregion Overriden Members

        protected static ActionResult ReportError(DbRepositoryResult result, string returnUrl = null)
        {
            return BaseDbRepositoryController.ReportError(result.ErrorMessage, result.Error, returnUrl);
        }

        protected static ActionResult ReportError<T>(DbRepositoryResult<T> result, string returnUrl = null)
        {
            return BaseDbRepositoryController.ReportError(result.ErrorMessage, result.Error, returnUrl);
        }

        protected static ActionResult ReportError(string errorMessage, Exception error, string returnUrl = null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, errorMessage);
        }
    }
}