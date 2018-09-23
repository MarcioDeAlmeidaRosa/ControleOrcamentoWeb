using RestSharp;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using ControleOrcamentoWeb.Models;
using System.Collections.Generic;

namespace ControleOrcamentoWeb.Controllers
{
    public class UserManageController : BaseController
    {
        private ListaTimeZone[] RecuperarListaTimeZone()
        {
            RestClient client = new RestClient(string.Format("{0}timezone", URL_API_SERVICE));
            RestRequest request = new RestRequest(Method.GET);
            var dado = client.Execute<List<ListaTimeZone>>(request);
            return dado.Data.ToArray();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.ListaTimeZone = RecuperarListaTimeZone();
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterBindingModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    RestClient client = new RestClient(string.Format("{0}auth/registrar", URL_API_SERVICE));
                    RestRequest request = new RestRequest(Method.POST);
                    var json = JsonConvert.SerializeObject(model);
                    request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
                    var retorno = client.Execute<object>(request);
                    if (retorno.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    if (!string.IsNullOrWhiteSpace(retorno.Content))
                    {
                        var result = Newtonsoft.Json.Linq.JObject.Parse(retorno.Content).ToObject<MessaResponseAPI>();
                        ModelState.AddModelError("", result.Message);
                    }
                    else
                    {
                        ModelState.AddModelError("", SEM_COMUNICACAO_COM_SERVICO);
                    }
                }
                catch (System.Exception)
                {

                    ModelState.AddModelError("", ERRO_COMUNICAR_SERVICO);
                }
            }
            ViewBag.ListaTimeZone = RecuperarListaTimeZone();
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
                try
                {
                    RestClient client = new RestClient(string.Format("{0}auth/login", URL_API_SERVICE));
                    RestRequest request = new RestRequest(Method.POST);
                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    request.AddParameter("application/x-www-form-urlencoded", $"grant_type=password&Username={model.Email}&Password={model.Senha}", ParameterType.RequestBody);
                    var retorno = client.Execute<object>(request);
                    if (retorno.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return RedirectToLocal(returnUrl);
                    }
                    else if (!string.IsNullOrWhiteSpace(retorno.Content))
                    {
                        var result = Newtonsoft.Json.Linq.JObject.Parse(retorno.Content).ToObject<MessaResponseAPI>();
                        //TODO: VERIFICAR
                        //if (result.Error.Equals("1"))
                        //    return View("Lockout");
                        //else if (result.Error.Equals("2"))
                        //    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                        ModelState.AddModelError("", result.Error_Description);
                    }
                    else
                    {
                        ModelState.AddModelError("", SEM_COMUNICACAO_COM_SERVICO);
                    }
                }
                catch (System.Exception)
                {
                    ModelState.AddModelError("", ERRO_COMUNICAR_SERVICO);
                }
            }
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
    }
}