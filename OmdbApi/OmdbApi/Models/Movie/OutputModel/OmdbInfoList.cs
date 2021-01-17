namespace OmdbApi.Models.Movie.OutputModel
{
    public class OmdbInfoList
    {
        public Search[] Search { get; set; }
        public int TotalResults { get; set; }
        public string Response { get; set; }
    }
}
