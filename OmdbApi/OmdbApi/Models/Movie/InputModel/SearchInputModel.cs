using System;
using System.ComponentModel.DataAnnotations;
using OmdbApi.Models.Enum;

namespace OmdbApi.Models.Movie.InputModel
{
    public class SearchInputModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "Min value is 3")]
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Page { get; set; }
        public Category Type { get; set; }
    }
}
