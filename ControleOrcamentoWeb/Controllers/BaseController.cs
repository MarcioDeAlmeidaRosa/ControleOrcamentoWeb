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

        protected string ERRO_COMUNICAR_SERVICO
        {
            get
            {
                return "Ocorreu um erro ao comunicar com o serviço, tente novamente mais tarde. Obrigado.";
            }
        }

        protected string SEM_COMUNICACAO_COM_SERVICO
        {
            get
            {
                return "Sem comunição com o serviço, tente novamente mais tarde. Obrigado.";
            }
        }
    }
}