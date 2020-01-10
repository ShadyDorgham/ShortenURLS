using DeltaTre.Core.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DeltaTre.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServices _services;

        public IConfiguration Configuration { get; }

        public HomeController(
            IConfiguration configuration,
            IServices services)
        {
            _services = services;
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        
        [HttpGet("Home/NotFound/{url}")]
        public IActionResult NotFound(string url)
        {
            var numberOfMonthsValid = int.Parse(Configuration.GetSection("UrlValidMonths").Value);
            var responseResult = _services.ShortUrlServices.GetTheResponseUrl(url, numberOfMonthsValid);

            if (!string.IsNullOrWhiteSpace(responseResult.ErrorMessage))
                return BadRequest(responseResult.ErrorMessage);

            return Redirect(responseResult.ResponseUrl);
        }



        [HttpPost]
        public IActionResult SaveUrl(string originalUrl)
        {
            var saveResult = _services.ShortUrlServices.SaveUrl(originalUrl);

            if (saveResult != "")
                return BadRequest(saveResult);

            return Ok($"Successfully Shortened the url");


        }

        [HttpGet]
        public IActionResult GetValidUrls()
        {
            var numberOfMonthsValid = int.Parse(Configuration.GetSection("UrlValidMonths").Value);
            return Json(_services.ShortUrlServices.GetValidUrls(numberOfMonthsValid));
        }



       
    }
}
