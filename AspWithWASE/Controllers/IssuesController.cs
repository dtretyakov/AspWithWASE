using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using AspWithAzureExtensions.Models;
using WindowsAzure.Table;
using WindowsAzure.Table.Extensions;

namespace AspWithAzureExtensions.Controllers
{
    /// <summary>
    ///     Issues controller.
    /// </summary>
    public class IssuesController : ApiController
    {
        private readonly ITableSet<Issue> _issues;

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="issues">Issues table context.</param>
        public IssuesController(ITableSet<Issue> issues)
        {
            _issues = issues;
        }

        // GET api/issues

        /// <summary>
        ///     Retrieves list of issues.
        /// </summary>
        /// <returns>List of issues.</returns>
        [Queryable]
        public IQueryable<Issue> Get()
        {
            return _issues;
        }

        // GET api/issues/5

        /// <summary>
        ///     Retrieves issue by identifier.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <returns>Issue.</returns>
        public async Task<Issue> Get(string id)
        {
            Issue issue = await _issues.SingleOrDefaultAsync(p => p.Id == id);
            if (issue == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return issue;
        }

        // POST api/issues

        /// <summary>
        ///     Adds a new issue.
        /// </summary>
        /// <param name="value">Issue.</param>
        /// <returns>Issue.</returns>
        public Issue Post(Issue value)
        {
            value.Created = DateTime.UtcNow;
            value.Modified = DateTime.UtcNow;
            value.Id = DateTime.UtcNow.Ticks.ToString(CultureInfo.InvariantCulture);

            return _issues.Add(value);
        }

        // PUT api/issues/5

        /// <summary>
        ///     Updates an issue.
        /// </summary>
        /// <param name="value">Issue.</param>
        /// <returns>Issue.</returns>
        public async Task<Issue> Put(Issue value)
        {
            Issue issue = await _issues.SingleOrDefaultAsync(p => p.Id == value.Id);
            if (issue == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return await _issues.UpdateAsync(value);
        }

        // DELETE api/issues/5

        /// <summary>
        ///     Removes an issue by identifier.
        /// </summary>
        /// <param name="id">Identifier.</param>
        public async Task Delete(string id)
        {
            Issue issue = await _issues.SingleOrDefaultAsync(p => p.Id == id);
            if (issue == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            await _issues.RemoveAsync(issue);
        }
    }
}