using DevelopmentProject.Models;
using DevelopmentProject.Repository;
using PuppeteerSharp;
using System.Text.RegularExpressions;

namespace DevelopmentProject.Services
{
    public class ScraperService : IScraperService
    {
        private IUtilRepository _utilRepository;

        public ScraperService(IUtilRepository utilRepository) 
        { 
            _utilRepository = utilRepository;
        }

        public async Task<ScraperResponseModel> NumberOfResultsOnGoogleAsync(ScraperRequestModel request)
        {
            // Building the url to scrape
            string urlToScrape = $"https://www.google.co.uk/search?num={request.NumberOfResults}&q={request.Keywords.Replace(" ", "+")}";

            // Downloading source code using Puppeteer to skip antibot restrictions from Google and render JS content as well
            string sourceCode = await _utilRepository.DownloadRenderedHtmlAsync(urlToScrape);
            return NumberOfResultsOnGoogle(sourceCode, request.UrlToSearch, request.Keywords, request.NumberOfResults);
        }

        public ScraperResponseModel NumberOfResultsOnGoogle(string sourceCode, string urlSearch, string keywords, int numberOfResults = 100)
        {
            // Declaring this regular expression in order to get the number of times that url was listed. By filtering using
            // this cite tag (with the classes "qLRx3b tjvcx GvPZzd cHaqb") it allow us to check if the url requested is listed. Every url is listed twice, so we will have
            // to divide by 2 after the count
            string pattern = @"<cite[^>]*class=""qLRx3b tjvcx GvPZzd cHaqb""[^>]*>(https:\/\/[^<\s]+)";

            // Looking for matches and adding the urls to an string list and dividing the results by 2
            var matches = Regex.Matches(sourceCode, pattern);
            List<string> urls = new();
            urls = matches.Where(r => r.Success).Select(x => x.Groups[1].Value).Where(x => x.Contains(urlSearch)).ToList();

            // Returning the result if there are matches
            var result = new ScraperResponseModel() { UrlToSearch = urlSearch, Keywords = keywords, Coincidences = 0, NumberOfResults = numberOfResults };
            if (urls.Any())
            {
                result.Coincidences = urls.Count() / 2;
            }

            // Returning Result
            return result;
        }
    }
}
