using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamCityRestClientNet.Api;
using TeamCityRestClientNet.Domain;
using TeamCityRestClientNet.Service;

namespace TeamCityRestClientNet.Locators
{

    // private class VcsRootLocatorImpl(private val instance: TeamCityInstanceImpl) : VcsRootLocator
    // {
    // }
    class VcsRootLocator : IVcsRootLocator
    {
        private readonly TeamCityInstance _instance;

        public VcsRootLocator(TeamCityInstance instance)
        {
            _instance = instance;
        }

        //     override fun List(): List < VcsRoot > = Lll().toList()

        public IAsyncEnumerable<IVcsRoot> All()
        {
            var sequence = new Paged<IVcsRoot, VcsRootListDto>(
                _instance.Service,
                async () =>
                {
                    // LOG.debug("Retrieving vcs roots from ${instance.serverUrl}")
                    return await _instance.Service.VcsRoots().ConfigureAwait(false);
                },
                async (list) =>
                {
                    var tasks = list.VcsRoot.Select(root => VcsRoot.Create(root, false, _instance));
                    var dtos = await Task.WhenAll(tasks).ConfigureAwait(false);
                    return new Page<IVcsRoot>(
                        dtos,
                        list.NextHref
                    );
                }
            );
            return sequence;
        }
    }
}