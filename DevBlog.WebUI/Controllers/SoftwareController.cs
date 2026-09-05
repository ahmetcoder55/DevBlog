using Microsoft.AspNetCore.Mvc;

namespace DevBlog.WebUI.Controllers
{
    [Route("[controller]")]
    public class SoftwareController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("kurucuk-os",Name ="kurucukos")]
        public IActionResult KurucukOS()
        {
            return View();
        }


        [HttpGet("dev-pomodoro-app",Name ="devpomodoroapp")]
        public IActionResult DevPomodoro()
        {
            return View();
        }
    }
}
