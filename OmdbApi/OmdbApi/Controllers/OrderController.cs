using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using OmdbApi.Helpers;
using OmdbApi.Interfaces;
using OmdbApi.Models.Movie.OutputModel;

namespace OmdbApi.Controllers
{
    public class OrderController : Controller
    {
        private readonly IEmail _emailService;
        public readonly IOmdbApi _omdbApiService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public OrderController(
            IEmail emailService,
            IOmdbApi omdbApiService,
            IWebHostEnvironment hostingEnvironment)
        {
            _emailService = emailService;
            _omdbApiService = omdbApiService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        public async Task<IActionResult> Send(string email, string imdbID)
        {
            var body = "";

            var model = await _omdbApiService.GetDataAsync($"&i={imdbID}");
            var serializedData = JsonConvert.DeserializeObject<OmdbItem>(model);

            var emailTemplate = _hostingEnvironment.ContentRootPath + @"\Views\Order\EmailTemplate.cshtml";
            using (var reader = new StreamReader(emailTemplate))
            {
                var template = await reader.ReadToEndAsync();
                body = ReflectionHelper.ReflectionReplace(template, serializedData);
            }

            _emailService.SendEmail(email, "You ordered following movie", body);

            return RedirectToAction("Index", "Order");
        }
    }
}
