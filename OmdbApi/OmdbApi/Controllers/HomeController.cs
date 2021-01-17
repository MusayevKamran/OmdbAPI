using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OmdbApi.Interfaces;
using OmdbApi.Models.Enum;
using OmdbApi.Models.Movie.InputModel;
using OmdbApi.Models.Movie.OutputModel;
using OmdbApi.ViewModels;

namespace OmdbApi.Controllers
{
    public class HomeController : Controller
    {
        public readonly IOmdbApi _omdbApiService;
        public HomeController(IOmdbApi omdbApiService)
        {
            _omdbApiService = omdbApiService;
        }

        public async Task<IActionResult> Index(HomeIndexViewModel homeIndexViewModel)
        {
            if (homeIndexViewModel.SearchInputModel == null)
            {
                var defaultSearchModel = new SearchInputModel
                {
                    Title = "Marvel"
                };

                homeIndexViewModel.SearchInputModel = defaultSearchModel;
            }

            var sb = new StringBuilder();
            sb.Append($"&s={homeIndexViewModel.SearchInputModel.Title}");
            sb.Append($"&type={Category.Movie}");
            sb.Append($"&Page=1");
            sb.Append($"&y={DateTime.Now.AddYears(-10)}");  

            var model = await _omdbApiService.GetDataAsync(sb.ToString());

            var serializedData = JsonConvert.DeserializeObject<OmdbInfoList>(model);

            var returnModel = new HomeIndexViewModel()
            {
                OmdbInfoList = serializedData,
                SearchInputModel = homeIndexViewModel.SearchInputModel
            };

            return View(returnModel);
        }

        public async Task<IActionResult> Details(string id)
        {
            var model = await _omdbApiService.GetDataAsync($"&i={id}");

            var returnModel = JsonConvert.DeserializeObject<OmdbItem>(model);

            return View(returnModel);
        }
    }
}
