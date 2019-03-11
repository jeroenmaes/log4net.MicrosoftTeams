using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace log4net.MicrosoftTeams
{
    internal class MicrosoftTeamsMessageFact
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        public MicrosoftTeamsMessageFact(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
