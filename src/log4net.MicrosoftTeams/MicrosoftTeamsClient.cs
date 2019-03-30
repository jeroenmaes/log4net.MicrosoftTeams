using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace log4net.MicrosoftTeams
{
    class MicrosoftTeamsClient : IDisposable
    {
        private readonly Uri _uri;
        private readonly HttpClient _client = new HttpClient();

        public MicrosoftTeamsClient(string url)
        {
            _uri = new Uri(url);
        }

        public void PostMessage(string formattedMessage, Dictionary<string, string> facts)
        {
            var message = CreateMessageCard(formattedMessage, facts);
            var json = JsonConvert.SerializeObject(message);

            var response = _client.PostAsync(_uri, new StringContent(json, Encoding.UTF8, "application/json")).Result;
            response.EnsureSuccessStatusCode();
        }

        public void Dispose()
        {
            _client?.Dispose();
        }

        private MicrosoftTeamsMessageCard CreateMessageCard(string text, Dictionary<string, string> facts)
        {
            var request = new MicrosoftTeamsMessageCard
            {
                Title = text,
                Text = text,
                Color = GetAttachmentColor(facts),
                Sections = new[]
                {
                    new MicrosoftTeamsMessageSection
                    {
                        Title = "Properties",
                        Facts = facts.Where(x => !x.Key.StartsWith("Exception")).Select(x => new MicrosoftTeamsMessageFact{ Name = x.Key, Value = x.Value}).ToArray()
                    },
                    new MicrosoftTeamsMessageSection
                    {
                        Title = "Exception",
                        Facts = facts.Where(x => x.Key.StartsWith("Exception")).Select(x => new MicrosoftTeamsMessageFact{ Name = x.Key, Value = x.Value}).ToArray()
                    }
                }
            };

            return request;
        }

        private static string GetAttachmentColor(Dictionary<string, string> facts)
        {
            var level = facts.FirstOrDefault(x => x.Key.StartsWith("Level"));

            return GetAttachmentColor(level.Value);
        }

        private static string GetAttachmentColor(string level)
        {
            switch (level)
            {
                case "INFO":
                    return "5bc0de";

                case "WARN":
                    return "f0ad4e";

                case "ERROR":
                case "FATAL":
                    return "d9534f";

                default:
                    return "777777";
            }
        }
    }
}
