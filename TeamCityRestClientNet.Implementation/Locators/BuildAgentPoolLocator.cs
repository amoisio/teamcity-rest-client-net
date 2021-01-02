using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamCityRestClientNet.Api;
using TeamCityRestClientNet.Domain;

namespace TeamCityRestClientNet.Locators
{
    class BuildAgentPoolLocator : Locator, IBuildAgentPoolLocator
    {
        public BuildAgentPoolLocator(TeamCityServer instance) : base(instance) { }

        /// <summary>
        /// Retrieve a build agent pool from TeamCity by its id.
        /// </summary>
        /// <param name="id">Id of the build agent pool to retrieve.</param>
        /// <returns>Matching build agent. Throws a Refit.ApiException if build agent not found.</returns>
        public async Task<IBuildAgentPool> BuildAgentPool(BuildAgentPoolId id)
            => await Domain.BuildAgentPool.Create(id.stringId, Instance).ConfigureAwait(false);

        public async Task<IEnumerable<IBuildAgentPool>> All()
        {
            var pools = await Service.AgentPools().ConfigureAwait(false);
            var tasks = pools.Items.Select(pool => Domain.BuildAgentPool.Create(pool.Id, Instance));
            return await Task.WhenAll(tasks).ConfigureAwait(false);
        }
    }
}