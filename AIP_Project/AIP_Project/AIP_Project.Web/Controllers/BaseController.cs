using System.Diagnostics;
using DataAccessLayer.Core.Interfaces.UoW;
using AIP_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AIP_Project.Web.Controllers
{

    public abstract class BaseController : Controller
    {
        protected readonly IUnitOfWork Uow;
        protected readonly ILogger Logger;
        protected BaseController(IUnitOfWork uow, ILoggerFactory loggerFactory)
        {
            Uow = uow;
            Logger = loggerFactory.CreateLogger<BaseController>();
        }

        // GET: /<controller>/
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        }
    }
}
