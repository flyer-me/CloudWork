using System.Diagnostics;
using CloudWork.Filter;
using CloudWork.Model;
using Microsoft.AspNetCore.Mvc;

namespace CloudWork.Controllers
{
    [TimerFilter]  // �˴�ActionFilter�Ѿ�ȫ��ע�ᣬ����ʡ��
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public string Login()
        {
            return "This is the Login Page";
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
