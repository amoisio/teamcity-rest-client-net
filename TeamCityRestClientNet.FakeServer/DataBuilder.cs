using System;
using System.Collections.Generic;
using TeamCityRestClientNet.Api;
using TeamCityRestClientNet.RestApi;

namespace TeamCityRestClientNet.FakeServer
{
    class DataBuilder
    {
        #region BuildAgents

        public readonly BuildAgentDto Agent1 = new BuildAgentDto
        {
            Id = "1",
            Name = "ip_172.17.0.3",
            Connected = true,
            Enabled = true,
            Authorized = true,
            Uptodate = true,
            Ip = "172.17.0.3",
            Properties = new ParametersDto
            {
                Property = new List<ParameterDto>
                {
                    new ParameterDto("env._", "/opt/buildagent/bin/agent.sh"),
                    new ParameterDto("env.ASPNETCORE_URLS", "http://+:80"),
                    new ParameterDto("env.CONFIG_FILE", "/data/teamcity_agent/conf/buildAgent.properties"),
                    new ParameterDto("env.DEBIAN_FRONTEND", "noninteractive"),
                    new ParameterDto("env.DOTNET_CLI_TELEMETRY_OPTOUT", "true"),
                    new ParameterDto("env.DOTNET_RUNNING_IN_CONTAINER", "true"),
                    new ParameterDto("env.DOTNET_SDK_VERSION", "3.1.403"),
                    new ParameterDto("env.DOTNET_SKIP_FIRST_TIME_EXPERIENCE", "true"),
                    new ParameterDto("env.DOTNET_USE_POLLING_FILE_WATCHER", "true"),
                    new ParameterDto("env.GIT_SSH_VARIANT", "ssh")
                }
            }
        };

        #endregion

        #region BuildAgentPools

        public readonly BuildAgentPoolDto DefaultPool = new BuildAgentPoolDto
        {
            Id = "0",
            Name = "Default"
        };

        #endregion

        #region Users
        private readonly UserDto UserJohnDoe = new UserDto
        {
            Id = "1",
            Name = "John Doe",
            Username = "jodoe",
            Email = "john.doe@mailinator.com"
        };

        private readonly UserDto UserJaneDoe = new UserDto
        {
            Id = "2",
            Name = "Jane Doe",
            Username = "jadoe",
            Email = "jane.doe@mailinator.com"
        };

        private readonly UserDto UserDunkinDonuts = new UserDto
        {
            Id = "3",
            Name = "Dunkin' Donuts",
            Username = "dunkin",
            Email = "dunkin@mailinator.com"
        };

        private readonly UserDto UserMacCheese = new UserDto
        {
            Id = "4",
            Name = "Mac Cheese",
            Username = "maccheese",
            Email = "maccheese@mailinator.com"
        };
        #endregion

        #region VcsRoots
        private readonly VcsRootDto VcsRestClientGit = new VcsRootDto
        {
            Id = "TeamCityRestClientNet_Bitbucket",
            Name = "Bitbucket",
            Properties = new NameValuePropertiesDto
            {
                Property = new List<NameValuePropertyDto>
                {
                    new NameValuePropertyDto { Name = "agentCleanFilesPolicy", Value = "ALL_UNTRACKED" },
                    new NameValuePropertyDto { Name = "agentCleanPolicy", Value = "ON_BRANCH_CHANGE" },
                    new NameValuePropertyDto { Name = "authMethod", Value = "PASSWORD" },
                    new NameValuePropertyDto { Name = "branch", Value = "refs/heads/master" },
                    new NameValuePropertyDto { Name = "ignoreKnownHosts", Value = "true" },
                    new NameValuePropertyDto { Name = "secure:password" },
                    new NameValuePropertyDto { Name = "submoduleCheckout", Value = "CHECKOUT" },
                    new NameValuePropertyDto { Name = "teamcity:branchSpec", Value="+:*" },
                    new NameValuePropertyDto { Name = "url", Value = "https://noexist@bitbucket.org/joedoe/teamcityrestclientnet.git" },
                    new NameValuePropertyDto { Name = "useAlternates", Value = "true" },
                    new NameValuePropertyDto { Name = "usernameStyle", Value = "USERID" }
                }
            }
        };

        private readonly VcsRootDto Vcs1 = new VcsRootDto
        {
            Id = "Vcs_af57aa45_ddd0_4e39_8163_b685be56e269",
            Name = "Vcs_af57aa45_ddd0_4e39_8163_b685be56e269"
        };

        private readonly VcsRootDto Vcs2 = new VcsRootDto
        {
            Id = "Vcs_b283d84e_6dc1_4fa8_87cf_1fecf65aada6",
            Name = "Vcs_b283d84e_6dc1_4fa8_87cf_1fecf65aada6"
        };

        private readonly VcsRootDto Vcs3 = new VcsRootDto
        {
            Id = "Vcs_ExtraOne",
            Name = "Vcs_ExtraOne"
        };

        private readonly VcsRootDto Vcs4 = new VcsRootDto
        {
            Id = "Vcs_AnotherOne",
            Name = "Vcs_AnotherOne"
        };
        #endregion

        #region BuildTypes
        private readonly BuildTypeDto BuildTypeRestClient = new BuildTypeDto
        {
            Id = "TeamCityRestClientNet_RestClient",
            Name = "Rest Client",
            ProjectId = "TeamCityRestClientNet",
            Settings = new BuildTypeSettingsDto
            {
                Property = new List<NameValuePropertyDto>
                {
                    new NameValuePropertyDto { Name = "artifactRules", Value = "artifacts" },
                    new NameValuePropertyDto { Name = "buildNumberCounter", Value = "138" },
                    new NameValuePropertyDto { Name = "cleanBuild", Value = "true" },
                    new NameValuePropertyDto { Name = "publishArtifactCondition", Value = "SUCCESSFUL" }
                }
            }
        };

        private readonly BuildTypeDto BuildTypeTeamCityCli = new BuildTypeDto
        {
            Id = "TeamCityCliNet_Cli",
            Name = "CLI",
            ProjectId = "TeamCityCliNet",
            Settings = new BuildTypeSettingsDto
            {
                Property = new List<NameValuePropertyDto>
                {
                    new NameValuePropertyDto { Name = "buildNumberCounter", Value = "1" }
                }
            }
        };
        #endregion

        #region Projects
        private readonly ProjectDto RootProject = new ProjectDto
        {
            Id = "_Root",
            Name = "<Root project>"
        };

        private readonly ProjectDto RestClientProject = new ProjectDto
        {
            Id = "TeamCityRestClientNet",
            ParentProjectId = "_Root",
            Name = "TeamCity Rest Client .NET",
            Parameters = new ParametersDto
            {
                Property = new List<ParameterDto>
                {
                    new ParameterDto("configuration_parameter", "6692e7bf_c9a4_4941_9e89_5dde9417f05f"),
                }
            }
        };

        private readonly ProjectDto TeamCityCliProject = new ProjectDto
        {
            Id = "TeamCityCliNet",
            ParentProjectId = "_Root",
            Name = "TeamCity CLI .NET"
        };

        private readonly ProjectDto Project1 = new ProjectDto
        {
            Id = "Project_e8fbb7af_1267_4df8_865f_7be55fdd54c4",
            ParentProjectId = "_Root",
            Name = "Project_e8fbb7af_1267_4df8_865f_7be55fdd54c4"
        };

        private readonly ProjectDto Project2 = new ProjectDto
        {
            Id = "Project_1cd586a8_d65c_44b1_b60e_e63f8b471819",
            ParentProjectId = "_Root",
            Name = "Project_1cd586a8_d65c_44b1_b60e_e63f8b471819"
        };

        private readonly ProjectDto Project3 = new ProjectDto
        {
            Id = "Project_3a1ac261_96d4_45b0_ac3d_7245718a3928",
            ParentProjectId = "_Root",
            Name = "Project_3a1ac261_96d4_45b0_ac3d_7245718a3928"
        };
        #endregion
        
        
        public DataBuilder()
        {
            Users = new UserRepository();
            VcsRoots = new VcsRootRepository();
            BuildTypes = new BuildTypeRepository();
            Projects = new ProjectRepository();
        }

        public void Load()
        {
            var authComment = Agent1.AuthorizedInfo.Comment;
            authComment.Timestamp = DateTime.UtcNow.AddDays(-14).ToString(Constants.TEAMCITY_DATETIME_FORMAT);
            authComment.Text = "Authorized";
            authComment.User = UserJohnDoe;
            var enabComment = Agent1.EnabledInfo.Comment;
            enabComment.Timestamp = DateTime.UtcNow.AddDays(-13).ToString(Constants.TEAMCITY_DATETIME_FORMAT);
            enabComment.Text = "Enabled";
            enabComment.User = UserJohnDoe;
            Agent1.Pool = DefaultPool;
            BuildAgents.Add(Agent1);

            DefaultPool.Agents.Agent.Add(Agent1);
            DefaultPool.Projects.Project.Add(RestClientProject);
            DefaultPool.Projects.Project.Add(TeamCityCliProject);
            DefaultPool.Projects.Project.Add(Project1);
            DefaultPool.Projects.Project.Add(Project2);
            DefaultPool.Projects.Project.Add(Project3);
            BuildAgentPools.Add(DefaultPool);

            BuildTypes.Add(BuildTypeRestClient);
            BuildTypes.Add(BuildTypeTeamCityCli);

            RootProject.Projects.Project.Add(RestClientProject);
            RootProject.Projects.Project.Add(TeamCityCliProject);
            RootProject.Projects.Project.Add(Project1);
            RootProject.Projects.Project.Add(Project2);
            RootProject.Projects.Project.Add(Project3);
            Projects.Add(RootProject);
            RestClientProject.BuildTypes.BuildType.Add(BuildTypeRestClient);
            Projects.Add(RestClientProject);
            TeamCityCliProject.BuildTypes.BuildType.Add(BuildTypeTeamCityCli);
            Projects.Add(TeamCityCliProject);
            Projects.Add(Project1);
            Projects.Add(Project2);
            Projects.Add(Project3);

            Users.Add(UserJohnDoe);
            Users.Add(UserJaneDoe);
            Users.Add(UserDunkinDonuts);
            Users.Add(UserMacCheese);

            VcsRoots.Add(VcsRestClientGit);
            VcsRoots.Add(Vcs1);
            VcsRoots.Add(Vcs2);
            VcsRoots.Add(Vcs3);
            VcsRoots.Add(Vcs4);
        }

        public BuildAgentRepository BuildAgents { get; private set; }
        public BuildAgentPoolRepository BuildAgentPools { get; private set; }
        public BuildTypeRepository BuildTypes { get; private set; }
        public ChangeRepository Change { get; private set; }
        public UserRepository Users { get; private set; }
        public VcsRootRepository VcsRoots { get; private set; }
        public ProjectRepository Projects { get; private set; }

    }
}