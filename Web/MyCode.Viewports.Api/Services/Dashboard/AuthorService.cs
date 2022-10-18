using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCode.Viewports.Api.Data;
using MyCode.Viewports.Data.Data.DashboardObjects;
using MyCode.Viewports.Shared.Logging.Extensions;

namespace MyCode.Viewports.Api.Services
{
    /// <summary>
    /// A service used for the <see cref="Author"/> entity, used to supply CRUD methods against <see cref="ViewportsDbContext"/>.
    /// </summary>
    public class AuthorService : BaseService<Author>, IAuthorService
    {
        /// <summary>
        /// Creates a new instance of <see cref="AuthorService"/>.
        /// </summary>
        /// <param name="ViewportsDbContext">An instance of <see cref="ViewportsDbContext"/>.</param>
        /// <param name="logger">An instance of <see cref="ILogger"/>, used to write logs.</param>
        public AuthorService(ViewportsDbContext ViewportsDbContext, ILogger<AuthorService> logger) : base(ViewportsDbContext, logger) { }

        /// <summary>
        /// Gets an author record from the <see cref="BlashDbContext"/> by passing in the Twitter API Author ID.
        /// </summary>
        /// <param name="twitterAuthorId">The author identifier from the Twitter API.</param>
        /// <returns>The author record returned from <see cref="ViewportsDbContext"/>, (or null if it cannot be found).</returns>
        public async Task<Author> GetByTwitterAuthorAsync(string twitterAuthorId)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("Method", "GetByTwitterAuthorAsync");
            parameters.Add("Twitter API Author Id", twitterAuthorId);

            try
            {
                // Get the record based on the Twitter API author identifier. Returns null if not found.
                return await _viewportsDbContext.AuthorEntities.FirstOrDefaultAsync(author => author.TwitterAuthorId == twitterAuthorId);
            }
            catch (Exception exception)
            {
                // Log any exceptions.
                _logger.LogWithParameters(LogLevel.Error, exception, "Unable to complete method due to an exception", parameters);
                throw;
            }
        }

        /// <summary>
        /// Delete any author records from <see cref="BlashDbContext"/> where there are no tweets associated with it.
        /// </summary>
        /// <returns>An instance of <see cref="Task"/>.</returns>
        public async Task DeleteMissingTweetsAsync()
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("Method", "DeleteMissingTweetsAsync");

            try
            {
                // Find any authors that don't have any tweets linked to them.
                var authorToDelete = await _viewportsDbContext.AuthorEntities.Where(author => !_viewportsDbContext.TweetEntities.Any(tweet => tweet.AuthorId == author.Id)).ToListAsync();

                // Mark all authors as deleted in the DbContext.
                authorToDelete.ForEach(dashboard =>
                {
                    _viewportsDbContext.Entry(dashboard).State = EntityState.Deleted;
                });
                await _viewportsDbContext.SaveChangesAsync(); // Save the changes.
            }
            catch (Exception exception)
            {
                _logger.LogWithParameters(LogLevel.Error, exception, "Unable to complete method due to an exception", parameters);
                throw;
            }
        }

    }
}
