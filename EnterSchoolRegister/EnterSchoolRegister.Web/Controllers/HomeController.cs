using DataAccessLayer.Core.Interfaces.UoW;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EnterSchoolRegister.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IUnitOfWork uow, ILoggerFactory loggerFactory) : base(uow, loggerFactory)
        {
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Description of the application";
            ViewData["Contacts"] = "Contacs";

            return View();
        }
    }
}
