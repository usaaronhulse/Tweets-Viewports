using Newtonsoft.Json;
using System.Collections.Generic;
using MyCode.Viewports.TwitterApi.Data.MediaObjects;
using MyCode.Viewports.TwitterApi.Data.UserObjects;

namespace MyCode.Viewports.TwitterApi.Data.BaseObjects
{
    /// <summary>
    /// The includes object that gets used when retrieving tweets from the Twitter API.
    /// </summary>
    public class Includes
    { 
        /// <summary>
        /// A list of all the users for the tweets that have been returned.
        /// </summary>
        [JsonProperty("users")]
        public List<User> Users { get; set; }

        /// <summary>
        /// A list of all the media for the tweets that have been returned.
        /// </summary>
        [JsonProperty("media")]
        public List<Media> Media { get; set; }
    }
}
