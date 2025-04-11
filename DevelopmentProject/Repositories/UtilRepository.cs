using PuppeteerSharp;

namespace DevelopmentProject.Repository
{
    public class UtilRepository : IUtilRepository
    {
        // Renders google html bypassing their anti-bot policies
        public async Task<string> DownloadRenderedHtmlAsync(string url)
        {
            // Download Chromium if it's not already - Puppeteer uses Chromium
            await new BrowserFetcher().DownloadAsync();

            // Launch headless browser
            using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                Args = new[] { "--no-sandbox",
                                "--disable-setuid-sandbox",
                                "--disable-blink-features=AutomationControlled", // hide puppeteer
                                "--disable-dev-shm-usage" }
            });

            // Open a new page
            using var page = await browser.NewPageAsync();

            // Set a realistic set of options - in order to bypassing bots
            await page.SetUserAgentAsync("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 " +
                                         "(KHTML, like Gecko) Chrome/134.0.0.0 Safari/537.36");
            await page.SetViewportAsync(new ViewPortOptions
            {
                Width = 1920,
                Height = 1040
            });
            await page.SetExtraHttpHeadersAsync(new System.Collections.Generic.Dictionary<string, string>
            {
                ["Accept-Language"] = "en-US,en;q=0.9"
            });
            await page.EvaluateFunctionOnNewDocumentAsync(@"() => {
                                                                    Object.defineProperty(navigator, 'webdriver', {
                                                                        get: () => false
                                                                    });
                                                                }");

            // Navigate and wait for the network to be idle (JS finished)
            await page.GoToAsync(url, WaitUntilNavigation.Networkidle2);

            // Wait for results to load
            page.DefaultNavigationTimeout = 3000;

            // Get the full rendered HTML
            string content = await page.GetContentAsync();
            return content;
        }
    }
}
