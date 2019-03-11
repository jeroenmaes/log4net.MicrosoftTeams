using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json;

namespace log4net.MicrosoftTeams
{
    class MicrosoftTeamsClient
    {
        private readonly Uri _uri;
        private static readonly HttpClient Client = new HttpClient();

        public MicrosoftTeamsClient(string url)
        {
            _uri = new Uri(url);
        }

        public void PostMessageAsync(string text, List<MicrosoftTeamsMessageFact> attachments = null)
        {
            var message = CreateMessage(text, attachments);
            var json = JsonConvert.SerializeObject(message);

            var result = Client.PostAsync(_uri, new StringContent(json, Encoding.UTF8, "application/json")).Result;
        }

        private MicrosoftTeamsMessageCard CreateMessage(string text, List<MicrosoftTeamsMessageFact> attachments = null)
        {
            var request = new MicrosoftTeamsMessageCard
            {
                Title = text,
                Text = text,
                Color = GetAttachmentColor(),
                Sections = new[]
                {
                    new MicrosoftTeamsMessageSection
                    {
                        Title = "Properties",
                        Facts = attachments
                    }
                }
            };

            return request;
        }

        private static string GetAttachmentColor()
        {
            return "777777";
        }

        private static string JsonSerializeObject(object obj)
        {
            var serializer = new DataContractJsonSerializer(obj.GetType());
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, obj);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
    }
}
