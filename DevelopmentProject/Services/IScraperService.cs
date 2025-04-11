using DevelopmentProject.Models;

namespace DevelopmentProject.Services
{
    public interface IScraperService
    {
        Task<ScraperResponseModel> NumberOfResultsOnGoogleAsync(ScraperRequestModel request);
    }
}
