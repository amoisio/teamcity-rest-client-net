using System;
using TeamCityRestClientNet.Api;
using Xunit;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace TeamCityRestClientNet.Tests
{
    [Collection("TeamCity Collection")]
    public class ProjectTests : TestsBase
    {
        public ProjectTests(TeamCityFixture teamCityFixture) : base(teamCityFixture) { }

        [Fact]
        public async Task RootProject_query_returns_the_root_project()
        {
            var rootProject = await _teamCity.RootProject();

            Assert.False(rootProject.Archived);
            Assert.Null(rootProject.ParentProjectId.Value.stringId);
            Assert.Equal("<Root project>", rootProject.Name);
            Assert.Empty(await rootProject.BuildConfigurations);
        }

        [Fact]
        public async Task Projects_can_be_accessed_via_the_root_project()
        {
            var rootProject = await _teamCity.RootProject();

            var childProjects = await rootProject.ChildProjects;

            Assert.Contains(childProjects, p => p.Name == "TeamCity CLI .NET");
            Assert.Contains(childProjects, p => p.Id.stringId == "TeamCityCliNet");
            Assert.Contains(childProjects, p => p.Name == "TeamCity Rest Client .NET");
            Assert.Contains(childProjects, p => p.Id.stringId == "TeamCityRestClientNet");
        }

        [Fact]
        public async Task Project_query_returns_the_matching_project()
        {
            var rootProject = await _teamCity.RootProject();

            var project = await _teamCity.Project(new ProjectId("TeamCityRestClientNet"));
            var buildConfigurations = await project.BuildConfigurations;

            Assert.Equal("TeamCity Rest Client .NET", project.Name);
            Assert.Equal(rootProject.Id, project.ParentProjectId);
            Assert.False(project.Archived);
            Assert.Empty(await project.ChildProjects);
            Assert.NotEmpty(buildConfigurations);
            Assert.Contains(buildConfigurations, p => p.Name == "Rest Client");
        }

        [Fact]
        public async Task Project_can_create_new_projects()
        {
            var project = await _teamCity.RootProject();
            
            var projectId = $"Project_{Guid.NewGuid().ToString().Replace('-', '_')}";
            await project.CreateProject(new ProjectId(projectId), projectId);

            var newProject = await _teamCity.Project(new ProjectId(projectId));

            Assert.NotNull(newProject);
            Assert.Equal(projectId, newProject.Name);
        }

        [Fact]
        public async Task Created_project_is_returned_by_the_create_call()
        {
            var project = await _teamCity.RootProject();

            var projectId = $"Project_{Guid.NewGuid().ToString().Replace('-', '_')}";
            var newProject = await project.CreateProject(new ProjectId(projectId), projectId);

            Assert.NotNull(newProject);
            Assert.Equal(projectId, newProject.Name);
        }

        [Fact]
        public async Task Project_can_create_new_vcs_roots()
        {
            var project = await _teamCity.RootProject();

            var vcsId = Guid.NewGuid().ToString().Replace('-', '_');
            await project.CreateVcsRoot(
                new VcsRootId(vcsId), 
                $"Vcs-{vcsId}",
                VcsRootType.GIT,
                new Dictionary<string, string>());

            var vcs = await _teamCity.VcsRoot(new VcsRootId(vcsId));
            Assert.NotNull(vcs);
            Assert.Equal($"Vcs-{vcsId}", vcs.Name);
        }
    }
}