using System.Collections.Generic;
using System.Threading.Tasks;
using MyCode.Viewports.Api.Services;
using MyCode.Viewports.Data.Data.DashboardObjects;
using MyCode.Viewports.Data.Results;

namespace MyCode.Viewports.Api.Extensions
{
    /// <summary>
    /// A list of API extensions.
    /// </summary>
    public static class ApiExtensions
    {
        /// <summary>
        /// Get a list of dashboards, and the tweets that belong to that dashboard.
        /// </summary>
        /// <param name="dashboards">A list of dashboards.</param>
        /// <param name="tweetService">An instance of <see cref="ITweetService"/>.</param>
        /// <returns></returns>
        public static async Task<List<DashboardAndTweetsResult>> GetDashboardAndTweetsAsync(List<Dashboard> dashboards, ITweetService tweetService)
        {
            List<DashboardAndTweetsResult> dashboardAndTweets = new List<DashboardAndTweetsResult>();

            // Go through each dashboard & get the tweets.
            foreach (var dashboard in dashboards)
            {               
                // Find the tweets for the dashboard.
                var tweets = await tweetService.GetByDashboardAsync(dashboard.Id);

                // Add them to the response.
                dashboardAndTweets.Add(new DashboardAndTweetsResult(dashboard, tweets));
            }

            return dashboardAndTweets;
        }
    }
}
