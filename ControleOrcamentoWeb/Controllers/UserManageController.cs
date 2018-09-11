using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using ControleOrcamentoWeb.Models;
using Microsoft.AspNet.Identity.Owin;
using RestSharp;
using Newtonsoft.Json;

namespace ControleOrcamentoWeb.Controllers
{
    public class UserManageController : Controller
    {
        //public UserManager<IdentityUser> UserManager { get; private set; }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        //public UserManageController() : this(Startup.UserManagerFactory(), Startup.OAuthOptions.AccessTokenFormat) { }
        //public UserManageController(UserManager<IdentityUser> userManager, ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        //{
        //    UserManager = userManager;
        //    AccessTokenFormat = accessTokenFormat;
        //}

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterBindingModel model)
        {
            if (ModelState.IsValid)
            {
                RestClient client = new RestClient("http://localhost:60004/api/auth/registrar");
                RestRequest request = new RestRequest(Method.POST);
                var json = JsonConvert.SerializeObject(model);
                request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
                var retorno = client.Execute<object>(request);
                if (retorno.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index", "Home");
                }
                var result = Newtonsoft.Json.Linq.JObject.Parse(retorno.Content).ToObject<MessaResponseAPI>();
                ModelState.AddModelError("", result.Message);
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //var user = await UserManager.FindAsync(model.UserName, model.Senha);
                //if (user != null)
                //{
                //    //await SignInAsync(user, model.RememberMe);
                //    return RedirectToLocal(returnUrl);
                //}
                //else
                //{
                //    ModelState.AddModelError("", "Invalid username or password.");
                //}
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}