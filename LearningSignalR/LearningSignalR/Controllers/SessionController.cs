using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LearningSignalR.BackEnd.ViewModels.Session;
using LearningSignalR.Identity;
using LearningSignalR.Identity.Managers;
using LearningSignalR.Infrastructure;
using Microsoft.AspNet.Identity.Owin;

namespace LearningSignalR.Controllers
{
    public class SessionController : Controller
    {
        private AppSignInManager _signInManager;

        public SessionController() { }

        public SessionController(AppSignInManager signInManager)
        {
            this.SignInManager = signInManager;
        }

        private AppSignInManager SignInManager
        {
            get
            {
                return this._signInManager ?? HttpContext.GetOwinContext().Get<AppSignInManager>();
            }
            set
            {
                this._signInManager = value;
            }
        }

        #region Login

        [AllowAnonymous, HttpGet]
        public ActionResult Login(string returnUrl = null)
        {
            if (this.Request.IsAuthenticated)
                return this.RedirectToAction("Index", "Home");

            return this.View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (this.ModelState.IsValid == false)
                return this.View(model);

            var result = await this.SignInManager.AdvancedPasswordSignInAsync(model.Email, model.Password, model.RememberMe,
                shouldLockout: ConfigurationService.GetApplicationSetting<bool>("UserLockoutEnabledByDefault"));

            switch (result)
            {
                case AdvancedSignInStatus.Success:
                    {
                        if (string.IsNullOrEmpty(model.ReturnUrl) || this.Url.IsLocalUrl(model.ReturnUrl) == false)
                            return this.RedirectToAction("Index", "Home");
                        return this.Redirect(model.ReturnUrl);
                    }
                case AdvancedSignInStatus.LockedOut:
                    return this.View("Lockout");
                case AdvancedSignInStatus.Disabled:
                    return this.View("Disabled");
                case AdvancedSignInStatus.RequiresVerification:
                case AdvancedSignInStatus.Failure:
                case AdvancedSignInStatus.Unknown:
                case AdvancedSignInStatus.EmailNotConfirmed:
                default:
                    this.ModelState.AddModelError("", "Invalid login attempt.");
                    return this.View(model);
            }
        }

        #endregion Login

        #region Logout

        [Authorize, HttpPost, ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            this.HttpContext.GetOwinContext().Authentication.AllSignOut();

            return this.Redirect("~/");
        }

        #endregion Logout

        protected override void Dispose(bool disposing)
        {
            if (
                //                disposing && 
                this._signInManager != null)
            {
                this._signInManager.Dispose();
                this._signInManager = null;
            }

            base.Dispose(disposing);
        }
    }
}