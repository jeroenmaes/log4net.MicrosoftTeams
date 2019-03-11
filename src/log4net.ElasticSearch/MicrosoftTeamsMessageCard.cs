﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace log4net.MicrosoftTeams
{
    internal class MicrosoftTeamsMessageCard
    {
        [JsonProperty("@type")]
        public string Type { get; } = "MessageCard";

        [JsonProperty("@context")]
        public string Context { get; } = "http://schema.org/extensions";

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("themeColor")]
        public string Color { get; set; }

        [JsonProperty("sections")]
        public IList<MicrosoftTeamsMessageSection> Sections { get; set; }
    }
}
