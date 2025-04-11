namespace DevelopmentProject.Models
{
    public class ScraperResponseModel
    {
        public string UrlToSearch { get; set; }

        public string Keywords { get; set; }

        public int Coincidences { get; set; }

        public int NumberOfResults { get; set; } = 100;
    }
}
