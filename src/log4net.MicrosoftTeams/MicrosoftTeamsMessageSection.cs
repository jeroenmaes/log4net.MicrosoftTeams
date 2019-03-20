using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

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
