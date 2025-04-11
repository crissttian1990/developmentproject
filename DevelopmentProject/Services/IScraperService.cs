using DevelopmentProject.Models;

namespace DevelopmentProject.Services
{
    public interface IScraperService
    {
        Task<ScraperResponseModel> NumberOfResultsOnGoogleAsync(ScraperRequestModel request);

        ScraperResponseModel NumberOfResultsOnGoogle(string sourceCode, string urlSearch, string keywords, int numberOfResults = 100);
    }
}
