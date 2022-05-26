using Microsoft.AspNetCore.Mvc;

namespace Book2App.Controllers
{
    public class TestController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TestMethod(string data)
        {
            return View("TestMethod",data);
        }
    }
}
