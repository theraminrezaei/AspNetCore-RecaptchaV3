using GoogleReCaptcha.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GoogleReCaptcha.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRecaptchaService _recaptchaService;

        private readonly RecaptchaOptions _options;

        public HomeController(IRecaptchaService recaptchaService, IOptions<RecaptchaOptions> options)
        {
            _recaptchaService = recaptchaService;
            _options = options.Value;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["SiteKey"] = _options.SiteKey;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string name, string gRecaptchaResponse)
        {
            ViewData["SiteKey"] = _options.SiteKey;

            var isHuman = await _recaptchaService.VerifyTokenAsync(gRecaptchaResponse);
            if (!isHuman)
            {
                ModelState.AddModelError(string.Empty, "reCAPTCHA verification failed or score too low.");
                return View();
            }

            ViewBag.Message = $"Hello {name}, verification successful!";
            return View();
        }
    }
}