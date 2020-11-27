using System;
using System.Collections.Generic;
using System.Linq;
using TeamCityRestClientNet.Api;
using TeamCityRestClientNet.Domain;
using TeamCityRestClientNet.Extensions;
using TeamCityRestClientNet.Service;
using TeamCityRestClientNet.Tools;

namespace TeamCityRestClientNet.Locators
{
    class TestRunsLocator : Locator, ITestRunsLocator
    {
        private int? _limitResults;
        private int? _pageSize;
        private BuildId? _buildId;
        private TestId? _testId;
        private ProjectId? _affectedProjectId;
        private TestStatus? _testStatus;
        private bool _expandMultipleInvocations = false;

        public TestRunsLocator(TeamCityInstance instance) : base(instance) { }

        public async IAsyncEnumerable<ITestRun> All()
        {
            var statusLocator = _testStatus switch
            {
                TestStatus.FAILED => "status:FAILURE",
                TestStatus.SUCCESSFUL => "status:SUCCESS",
                TestStatus.IGNORED => "ignored:true",
                _ => throw new Exception($"Unsupported filter by test status {_testStatus}")
            };
            var count = Utilities.SelectRestApiCountForPagedRequests(_limitResults, _pageSize);
            var parameters = Utilities.ListOfNotNull(
                count?.Let(val => $"count:{val}"),
                _affectedProjectId?.Let(val => $"affectedProject:{val}"),
                _buildId?.Let(val => $"build:{val}"),
                _testId?.Let(val => $"test:{val}"),
                _expandMultipleInvocations.Let(val => $"expandInvocations:{val}"),
                statusLocator
            );

            if (parameters.IsEmpty()) {
                throw new ArgumentException("At least one parameter should be specified");

            }

            var sequence = new Paged<ITestRun, TestOccurrencesDto>(
                Instance,
                async () => 
                {
                    var testOccurrencesLocator = String.Join(",", parameters);
                // LOG.debug("Retrieving test occurrences from ${instance.serverUrl} using query '$testOccurrencesLocator'")
                    return await Service
                        .TestOccurrences(testOccurrencesLocator, TestOccurrenceDto.FILTER)
                        .ConfigureAwait(false);
                },
                async (list) =>
                {
                    var tasks = list.TestOccurrence.Select(test => new TestRu)
                   Page(
                           data = testOccurrencesBean.testOccurrence.map { TestRunImpl(it) },
                                nextHref = testOccurrencesBean.nextHref
                        )
                    }

                }
            );
            //     val sequence = LazyPaging(instance, {
            //         }) {

            //     val limitResults1 = limitResults
            //         return if (limitResults1 != null) sequence.take(limitResults1) else sequence
            //     }
            // }
        }

        public ITestRunsLocator ExpandMultipleInvocations()
        {
            this._expandMultipleInvocations = true;
            return this;
        }

        public ITestRunsLocator ForBuild(BuildId buildId)
        {
            this._buildId = buildId;
            return this;
        }

        public ITestRunsLocator ForProject(ProjectId projectId)
        {
            this._affectedProjectId = projectId;
            return this;
        }

        public ITestRunsLocator ForTest(TestId testId)
        {
            this._testId = testId;
            return this;
        }

        public ITestRunsLocator LimitResults(int count)
        {
            this._limitResults = count;
            return this;
        }

        public ITestRunsLocator PageSize(int pageSize)
        {
            this._pageSize = pageSize;
            return this;
        }

        public ITestRunsLocator WithStatus(TestStatus testStatus)
        {
            this._testStatus = testStatus;
            return this;
        }
    }
}