﻿using Newtonsoft.Json;
using System.Collections.Generic;
using MyCode.Viewports.TwitterApi.Data.BaseObjects;
using MyCode.Viewports.TwitterApi.Data.TweetObjects;

namespace MyCode.Viewports.TwitterApi.Models
{
    /// <summary>
    /// The stored object when using the Twitter API to search for tweets.
    /// </summary>
    public class TweetResult
    {
        /// <summary>
        /// The tweet returned.
        /// </summary>
        [JsonProperty("data")]
        public List<Tweet> Tweets { get; set; }

        /// <summary>
        /// A list of user's and media associated with all the tweets returned. 
        /// </summary>
        [JsonProperty("includes")]
        public Includes Includes { get; set; }

        /// <summary>
        /// Meta information, such as the number of tweets returned.
        /// </summary>
        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }
}
