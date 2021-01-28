using Newtonsoft.Json;
using System.Collections.Generic;

namespace log4net.MicrosoftTeams
{
    internal class MicrosoftTeamsMessageSection
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("facts")]
        public IList<MicrosoftTeamsMessageFact> Facts { get; set; }
    }
}
