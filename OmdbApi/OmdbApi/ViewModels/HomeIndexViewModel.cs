using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OmdbApi.Models.Movie.InputModel;
using OmdbApi.Models.Movie.OutputModel;

namespace OmdbApi.ViewModels
{
    public class HomeIndexViewModel
    {
        public SearchInputModel SearchInputModel { get; set; }
        public OmdbInfoList OmdbInfoList { get; set; }
    }
}
