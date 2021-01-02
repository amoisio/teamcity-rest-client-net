using System.Collections.Generic;
using Newtonsoft.Json;

namespace TeamCityRestClientNet.RestApi
{
    public class ChangesDto : ListDto<ChangeDto>
    {
        [JsonProperty(PropertyName = "change")]
        public override List<ChangeDto> Items { get; set; }
    }

    public class ChangeDto : IdDto
    {
        public string Version { get; set; }
        public UserDto User { get; set; }
        public string Date { get; set; }
        public string Comment { get; set; }
        public string Username { get; set; }
        public VcsRootInstanceDto VcsRootInstance { get; set; }
    }
}