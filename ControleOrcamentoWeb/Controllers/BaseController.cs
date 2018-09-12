using System.Web.Mvc;

namespace ControleOrcamentoWeb.Controllers
{
    public class BaseController : Controller
    {
        protected string URL_API_SERVICE
        {
            get
            {
                return System.Configuration.ConfigurationSettings.AppSettings["URL_API_CONTROLE_ORCAMENTO"];
            }
        }
    }
}