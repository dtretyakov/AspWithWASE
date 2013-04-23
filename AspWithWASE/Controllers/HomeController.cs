using System.Web.Mvc;

namespace AspWithAzureExtensions.Controllers
{
    /// <summary>
    /// Home controller.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Index page.
        /// </summary>
        /// <returns>Result.</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Comments page.
        /// </summary>
        /// <returns>Result.</returns>
        public ActionResult Comments()
        {
            return View();
        }
    }
}