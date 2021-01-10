using System;
using System.Linq;
using TeamCityRestClientNet.Api;
using TeamCityRestClientNet.RestApi;

namespace TeamCityRestClientNet.FakeServer
{
    class InvestigationRepository : BaseRepository<InvestigationDto, InvestigationListDto>
    {
        public InvestigationRepository()
        {
            _itemsById.Add("1", new InvestigationDto
            {
                Id = "1",
                Assignee = UserRepository.UserJaneDoe,
                Assignment = new AssignmentDto
                {
                    Text = "Assignment",
                    Timestamp = DateTime.UtcNow.ToString(Constants.TEAMCITY_DATETIME_FORMAT),
                    User = UserRepository.UserJaneDoe
                },
                Resolution = new InvestigationResolutionDto
                {
                    Type = "test"
                },
                Scope = new InvestigationScopeDto
                {
                    BuildTypes = new BuildTypeListDto(),
                    Project = new ProjectDto()
                },
                State = InvestigationState.FIXED,
                Target = new InvestigationTargetDto
                {
                    AnyProblem = false,
                    Problems = new ProblemUnderInvestigationListDto(),
                    Tests = new TestUnderInvestigationListDto()
                }
            });
        }


    }
}