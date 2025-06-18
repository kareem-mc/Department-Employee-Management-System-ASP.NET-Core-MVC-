using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web_mvd.ViweModels;

namespace Web_mvd.Controllers
{
    public class HomeController : Controller
    {


        //public ViewResult showview()
        //{
        //    ViewResult result = new ViewResult();
        //    result.ViewName = "view 1";
        //    return result;
        //}

        //public IActionResult () 
        //{
       
        //}



        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       


    }
}
