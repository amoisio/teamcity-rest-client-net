using System.Collections.Generic;
using TeamCityRestClientNet.Api;

namespace TeamCityRestClientNet.RestApi
{
    public class ArtifactFileListDto
    {
        public List<ArtifactFileDto> File { get; set; } = new List<ArtifactFileDto>();
    }

    public class ArtifactFileDto
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public long? Size { get; set; }
        public string ModificationTime { get; set; }
        public static readonly string FIELDS 
            = $"{nameof(FullName)},{nameof(Name)},${nameof(Size)},${nameof(ModificationTime)}";
    }

    public class BuildRunningInfoDto
    {
        public int PercentageComplete { get; set; } = 0;
        public long ElapsedSeconds { get; set; } = 0;
        public long EstimatedTotalSeconds { get; set; } = 0;
        public bool Outdated { get; set; } = false;
        public bool ProbablyHanging { get; set; } = false;
    }

    public class BuildProblemDto
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Identity { get; set; }
    }

    public class BuildProblemOccurrencesDto
    {
        public string NextHref { get; set; }
        public List<BuildProblemOccurrenceDto> ProblemOccurrence { get; set; } = new List<BuildProblemOccurrenceDto>();
    }

    public class BuildProblemOccurrenceDto
    {
        public string Details { get; set; }
        public string AdditionalData { get; set; }
        public BuildProblemDto Problem { get; set; }
        public BuildDto Build { get; set; }
    }

    public class TagDto
    {
        public string Name { get; set; }
    }

    public class TagsDto
    {
        public List<TagDto> Tag { get; set; } = new List<TagDto>();
    }

    public class TriggerBuildRequestDto
    {
        public string BranchName { get; set; }
        public bool? Personal { get; set; }
        public TriggeringOptionsDto TriggeringOptions { get; set; }
        public ParametersDto Properties { get; set; }
        public BuildTypeDto BuildType { get; set; }
        public CommentDto Comment { get; set; }
        //  TODO: lastChanges
        //    <lastChanges>
        //      <change id="modificationId"/>
        //    </lastChanges>
    }

    public class TriggeringOptionsDto
    {
        public bool? CleanSources { get; set; }
        public bool? RebuildAllDependencies { get; set; }
        public bool? QueueAtTop { get; set; }
    }

    public class CommentDto
    {
        public string Text { get; set; }
    }

    public class TriggerDto
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public ParametersDto Properties { get; set; } = new ParametersDto();
    }

    public class TriggersDto
    {
        public List<TriggerDto> Trigger { get; set; } = new List<TriggerDto>();
    }

    public class ArtifactDependencyDto : IdDto
    {
        public string Type { get; set; }
        public bool? Disabled { get; set; } = false;
        public bool? Inherited { get; set; } = false;
        public ParametersDto Properties { get; set; } = new ParametersDto();
        public BuildTypeDto SourceBuildType { get; set; } = new BuildTypeDto();
    }

    public class ArtifactDependenciesDto
    {
        public List<ArtifactDependencyDto> ArtifactDependency { get; set; } = new List<ArtifactDependencyDto>();
    }

    public class ParametersDto
    {
        public List<ParameterDto> Property { get; set; } = new List<ParameterDto>();

        public ParametersDto() { }
        public ParametersDto(List<ParameterDto> properties)
        {
            Property = properties;
        }
    }

    public class ParameterDto
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool? Own { get; set; }

        public ParameterDto(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
    }

    public class PinInfoDto
    {
        public UserDto User { get; set; }
        public string Timestamp { get; set; }
    }

    public class TriggeredDto
    {
        public UserDto User { get; set; }
        public BuildDto Build { get; set; }
    }

    public class BuildCommentDto
    {
        public UserDto User { get; set; }
        public string Timestamp { get; set; }
        public string Text { get; set; }
    }

    public class EnabledInfoCommentDto
    {
        public UserDto User { get; set; }
        public string Timestamp { get; set; }
        public string Text { get; set; }
    }

    public class EnabledInfoDto
    {
        public EnabledInfoCommentDto Comment { get; set; } = new EnabledInfoCommentDto();
    }

    public class AuthorizedInfoCommentDto
    {
        public UserDto User { get; set; }
        public string Timestamp { get; set; }
        public string Text { get; set; }
    }

    public class AuthorizedInfoDto
    {
        public AuthorizedInfoCommentDto Comment { get; set; } = new AuthorizedInfoCommentDto();
    }

    public class BuildCanceledDto
    {
        public UserDto User { get; set; }
        public string Timestamp { get; set; }
        public string Text { get; set; }
    }

    public class TriggeredBuildDto
    {
        public int? Id { get; set; }
        public string BuildTypeId { get; set; }
    }

    public class RevisionsDto
    {
        public List<RevisionDto> Revision { get; set; } = new List<RevisionDto>();
    }

    public class RevisionDto
    {
        public string Version { get; set; }
        public string VcsBranchName { get; set; }
        public VcsRootInstanceDto VcsRootInstance { get; set; }
    }

    public class NameValuePropertiesDto
    {
        public List<NameValuePropertyDto> Property { get; set; } = new List<NameValuePropertyDto>();
    }

    public class NameValuePropertyDto
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class BuildCancelRequestDto
    {
        public string Comment { get; set; } = "";
        public bool ReaddIntoQueue { get; set; } = false;
    }

    public class TestOccurrencesDto
    {
        public string NextHref { get; set; }
        public List<TestOccurrenceDto> TestOccurrence { get; set; } = new List<TestOccurrenceDto>();
    }

    public class TestDto
    {
        public string Id { get; set; }
    }

    public class TestOccurrenceDto
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public bool? Ignored { get; set; }
        public long? Duration { get; set; }
        public string IgnoreDetails { get; set; }
        public string Details { get; set; }
        public bool? CurrentlyMuted { get; set; }
        public bool? Muted { get; set; }
        public bool? NewFailure { get; set; }
        public BuildDto Build { get; set; }
        public TestDto Test { get; set; }
        public BuildDto NextFixed { get; set; }
        public BuildDto FirstFailed { get; set; }
        public const string FILTER = "testOccurrence(name,status,ignored,muted,currentlyMuted,newFailure,duration,ignoreDetails,details,firstFailed(id),nextFixed(id),build(id),test(id))";
    }

    public class InvestigationListDto
    {
        public List<InvestigationDto> Investigation { get; set; } = new List<InvestigationDto>();
    }

    public class InvestigationDto : IdDto
    {
        public InvestigationState? State { get; set; }
        public UserDto Assignee { get; set; }
        public AssignmentDto Assignment { get; set; }
        public InvestigationResolutionDto Resolution { get; set; }
        public InvestigationScopeDto Scope { get; set; }
        public InvestigationTargetDto Target { get; set; }
    }

    public class InvestigationResolutionDto
    {
        public string Type { get; set; }
    }

    public class AssignmentDto
    {
        public UserDto User { get; set; }
        public string Text { get; set; }
        public string Timestamp { get; set; }
    }

    public class InvestigationTargetDto
    {
        public TestUnderInvestigationListDto Tests { get; set; }
        public ProblemUnderInvestigationListDto Problems { get; set; }
        public bool? AnyProblem { get; set; }
    }

    public class TestUnderInvestigationListDto
    {
        public int? Count { get; set; }
        public List<TestDto> Test { get; set; } = new List<TestDto>();

    }

    public class ProblemUnderInvestigationListDto
    {
        public int? Count { get; set; }
        public List<BuildProblemDto> Problem { get; set; } = new List<BuildProblemDto>();
    }

    public class InvestigationScopeDto
    {
        public BuildTypesDto BuildTypes { get; set; }
        public ProjectDto Project { get; set; }
    }
}