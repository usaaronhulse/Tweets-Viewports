﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyCode.Viewports.TwitterApi.Configuration;
using MyCode.Viewports.TwitterApi.Services;
using MyCode.Viewports.TwitterApi.Services.AuthenticateObjects;
using MyCode.Viewports.TwitterApi.Services.RuleObjects;
using MyCode.Viewports.TwitterApi.Services.TweetObjects;

namespace MyCode.Viewports.TwitterApi.Extensions
{
    /// <summary>
    /// A list of Twitter API extensions.
    /// </summary>
    public static class TwitterApiExtensions
    {
        /// <summary>
        /// A list of services to add to the <see cref="IServiceCollection" /> when configuration the application.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="twitterApiConfigurationSection"></param>
        /// <returns></returns>
        public static IServiceCollection AddTwitterApiServices(this IServiceCollection services, IConfigurationSection twitterApiConfigurationSection)
        {
            services.Configure<TwitterApiConfiguration>(twitterApiConfigurationSection);
            services.AddSingleton<ITwitterApiAuthenticateService, TwitterApiAuthenticateService>();
            services.AddSingleton<ITwitterApiRuleService, TwitterApiRuleService>();
            services.AddSingleton<ITwitterApiTweetService, TwitterApiTweetService>();

            return services;
        }
    }
}
