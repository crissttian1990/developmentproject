namespace DevelopmentProject.Repository
{
    public interface IUtilRepository
    {
        Task<string> DownloadRenderedHtmlAsync(string url);
    }
}
