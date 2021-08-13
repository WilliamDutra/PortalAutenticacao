using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PortalAutenticacao.UI.Controllers
{
    public class BaseController : Controller
    {

        public void ExibirMensagem(string Mensagem)
        {
            TempData["Mensagem"] = Mensagem;
        }
    }
}
