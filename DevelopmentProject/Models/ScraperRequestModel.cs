namespace DevelopmentProject.Models
{
    public class ScraperRequestModel
    {
        public string UrlToSearch { get; set; }

        public string Keywords { get; set; }

        public int NumberOfResults { get; set; } = 100;
    }
}
