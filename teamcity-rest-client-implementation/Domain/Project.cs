using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Threading.Tasks;
using TeamCityRestClientNet.Api;
using TeamCityRestClientNet.Extensions;
using TeamCityRestClientNet.Service;
using System.Xml.Linq;
using Nito.AsyncEx;

namespace TeamCityRestClientNet.Domain
{
    class Project : Base<ProjectDto>, IProject
    {
        private Project(ProjectDto dto, TeamCityInstance instance)
            : base(dto, instance)
        {
            this.ChildProjects = new AsyncLazy<List<IProject>>(async () 
                => {
                    var tasks = dto.Projects.Project
                        .Select(proj => Project.Create(proj, false, instance));
                    var projects = await Task.WhenAll(tasks).ConfigureAwait(false);
                    return projects.ToList();
                });

            this.BuildConfigurations = new AsyncLazy<List<IBuildConfiguration>>(async ()
                => {
                    var tasks = dto.BuildTypes.BuildType
                        .Select(type => BuildConfiguration.Create(type.Id, instance));
                    var configs = await Task.WhenAll(tasks).ConfigureAwait(false);
                    return configs.ToList();                    
                });

        }

        public static async Task<IProject> Create(ProjectDto dto, bool isFullDto, TeamCityInstance instance)
        {
            var fullDto = isFullDto
                ? dto
                : await instance.Service.Project(dto.Id).ConfigureAwait(false);
            return new Project(fullDto, instance);
        }

        public ProjectId Id => new ProjectId(IdString);
        public string Name => Dto.Name.SelfOrNullRef();
        public bool Archived => Dto.Archived ?? false;
        public ProjectId? ParentProjectId
            => Dto.ParentProjectId.Let(id => new ProjectId(Dto.ParentProjectId));
        public AsyncLazy<List<IProject>> ChildProjects { get; }
        public AsyncLazy<List<IBuildConfiguration>> BuildConfigurations { get; }
        public List<IParameter> Parameters
            => Dto.Parameters
                ?.Property
                .Select(prop => new Parameter(prop))
                .ToList<IParameter>();

        public async Task<IBuildConfiguration> CreateBuildConfiguration(string buildConfigurationDescriptionXml)
        {
            var dto = await Service.CreateBuildType(buildConfigurationDescriptionXml).ConfigureAwait(false);
            return await BuildConfiguration.Create(dto.Id, Instance).ConfigureAwait(false);
        }

        public async Task<IProject> CreateProject(ProjectId id, string name)
        {
            var xml = new XElement("newProjectDescription",
                new XAttribute("name", name),
                new XAttribute("id", Id.stringId),
                new XElement("parentProject",
                    new XAttribute("locator", $"id:{Id.stringId}")
                )
            );
            var projectDto = await Service.CreateProject(xml.ToString()).ConfigureAwait(false);
            return new Project(projectDto, Instance);
        }

        public async Task<IVcsRoot> CreateVcsRoot(VcsRootId id, string name, VcsRootType type, IDictionary<string, string> properties)
        {
            var propElement = new XElement("properties");
            foreach (var prop in properties.OrderBy(prop => prop.Key))
            {
                propElement.Add(new XElement("property",
                    new XAttribute("name", prop.Key),
                    new XAttribute("value", prop.Value)));
            }

            var xml = new XElement("vcs-root",
                new XAttribute("name", name),
                new XAttribute("id", Id.stringId),
                new XAttribute("vcsName", type.stringType),
                new XElement("project",
                    new XAttribute("id", IdString)
                ),
                propElement);

            var vcsRootDto = await Service.CreateVcsRoot(xml.ToString()).ConfigureAwait(false);
            return await VcsRoot.Create(vcsRootDto, true, Instance).ConfigureAwait(false);
        }

        public string GetHomeUrl(string branch = null)
            => Instance.GetUserUrlPage(
                "project.html",
                projectId: Id,
                branch: branch);

        public string GetTestHomeUrl(TestId testId)
            => Instance.GetUserUrlPage(
                "project.html",
                projectId: Id,
                testNameId: testId,
                tab: "testDetails");

        public async Task SetParameter(string name, string value)
        {
            //         LOG.info("Setting parameter $name=$value in ProjectId=$idString")
            await Service.SetProjectParameter(Id.stringId, name, value).ConfigureAwait(false);
        }

        public override string ToString()
            => $"Project(id={IdString},name={Name})";
    }
}