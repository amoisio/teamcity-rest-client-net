using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamCityRestClientNet.Api;
using TeamCityRestClientNet.Service;

namespace TeamCityRestClientNet.Implementations 
{
    class BuildAgentPool : Base<BuildAgentPoolDto>, IBuildAgentPool
    {
        public BuildAgentPool(BuildAgentPoolDto dto, bool isFullDto, TeamCityInstance instance)
            : base(dto, isFullDto, instance)
        {
            
        }

        public BuildAgentPoolId Id => new BuildAgentPoolId(IdString);

        public string Name => NotNull(dto => dto.Name);

        public List<IProject> Projects 
            => this.FullDto.Projects?.Project
                ?.Select(project => new Project(project, false, Instance))
                .ToList<IProject>() ?? new List<IProject>();

        public List<IBuildAgent> Agents 
            => this.FullDto.Agents?.Agent
                ?.Select(agent => new BuildAgent(agent, false, Instance))
                .ToList<IBuildAgent>() ?? new List<IBuildAgent>();

        public override string ToString()
            => $"BuildAgentPool(id={Id}, name={Name})";

        protected override async Task<BuildAgentPoolDto> FetchFullDto()
            => await Service.AgentPools($"id:{IdString}");
    }
}