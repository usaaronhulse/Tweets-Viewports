using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCode.Viewports.Api.Background.Tasks.BaseObjects;

namespace MyCode.Viewports.Api.Background.Jobs
{
    /// <summary>
    /// A service that runs background jobs.
    /// </summary>
    public interface ITwitterIntegrationJobService
    {
        /// <summary>
        /// Method to run a job.
        /// </summary>
        /// <param name="twitterIntegrationJob">An instance of the job that will be ran.</param>
        /// <returns>An instance of <see cref="Task"/>.</returns>
        Task RunJobAsync(TwitterIntegrationJob twitterIntegrationJob);
    }
}
