using System.Collections.Generic;
using MyCode.Viewports.Data.Data.DashboardObjects;

namespace MyCode.Viewports.BlazorWasm.Models
{
    /// <summary>
    /// A model used to display a dashboard and it's tweets.
    /// </summary>
    public class DashboardComponentModel
    {        
        /// <summary>
        /// The instance of the dashboard.
        /// </summary>
        public Dashboard Dashboard { get; set; }

        /// <summary>
        /// A list of tweets associated with the dashboard.
        /// </summary>
        public List<Tweet> Tweets { get; set; }
    }
}
