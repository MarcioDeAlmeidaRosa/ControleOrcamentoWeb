using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ControleOrcamentoWeb.Startup))]
namespace ControleOrcamentoWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
