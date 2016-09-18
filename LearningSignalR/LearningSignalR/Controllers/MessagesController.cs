using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using LearningSignalR.BackEnd.DbRepository.Entity;
using LearningSignalR.BackEnd.ViewModels.Messages;
using LearningSignalR.Infrastructure;
using LearningSignalR.Infrastructure.Hubs;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;

namespace LearningSignalR.Controllers
{
    public class MessagesController : BaseDbRepositoryController
    {
        private readonly IHubContext _hubContext;

        public MessagesController() : base()
        {
            this._hubContext = GlobalHost.ConnectionManager.GetHubContext<JobHub>();
        }

        public MessagesController(IHubContext hubContext, AppEntityDbRepository dbRepository) : base(dbRepository)
        {
            this._hubContext = hubContext;
        }

        [System.Web.Mvc.Authorize, HttpGet]
        public async Task<ActionResult> New()
        {
            var selectListResult = await this.DbRepository.GetCompaniesSelectList();
            if (selectListResult.IsSuccess)
                this.ViewBag.Companies = selectListResult.Payload;
            
            return View(new NewMessage());
        }

        [System.Web.Mvc.Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> New(NewMessage message)
        {
            if (this.ModelState.IsValid)
            {
                message.SentAt = DateTime.UtcNow;
                message.SentByUserId = this.User.Identity.GetUserId<long>();

                var result = await this.DbRepository.NewMessage(message);
                if (result.IsSuccess)
                {
                    var statusResult = await this.DbRepository.StatusMessage(this.User.Identity.GetUserId<long>());
                    if (statusResult.IsSuccess)
                    {
                        var status = statusResult.Payload;   
                        this._hubContext.Clients.Group(JobHub.GetGroup(status.CompanyId)).update(status.CompanyId, status.TotalNoOfMessages, status.TotalNoOfUnreadMessages);
                    }
                    
                    return this.RedirectToAction("Status");
                }
            }

            var selectListResult = await this.DbRepository.GetCompaniesSelectList(message.CompanyId);
            if (selectListResult.IsSuccess)
                this.ViewBag.Companies = selectListResult.Payload;

            return this.View(message);
        }

        [System.Web.Mvc.Authorize, HttpGet]
        public async Task<ActionResult> List()
        {
            var userId = this.User.Identity.GetUserId<long>();
            var result = await this.DbRepository.ListMessage(userId);
            if (result.IsSuccess)
                return this.View(result.Payload);

            return this.View("Error", new HandleErrorInfo(result.Error, "Messages", "Status"));
        }

        [System.Web.Mvc.Authorize, HttpGet]
        public async Task<ActionResult> Status()
        {
            var result = await this.DbRepository.StatusMessage(this.User.Identity.GetUserId<long>());
            if (result.IsSuccess)
                return this.View(result.Payload);

            return this.View("Error", new HandleErrorInfo(result.Error, "Messages", "Status"));
        }

        [System.Web.Mvc.Authorize, HttpGet]
        public async Task<ActionResult> Details(string code)
        {
            if (string.IsNullOrEmpty(code))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            await this.DbRepository.MarkAsRead(code, this.User.Identity.GetUserId<long>());

            var result = await this.DbRepository.DetailsMessage(code);
            if (result.IsSuccess == false || result.Payload == null)
                return HttpNotFound();

            var statusResult = await this.DbRepository.StatusMessage(this.User.Identity.GetUserId<long>());
            if (statusResult.IsSuccess)
            {
                var status = statusResult.Payload;
                this._hubContext.Clients.Group(JobHub.GetGroup(status.CompanyId)).update(status.CompanyId, status.TotalNoOfMessages, status.TotalNoOfUnreadMessages);
            }

            return this.View(result.Payload);
        }
    }
}