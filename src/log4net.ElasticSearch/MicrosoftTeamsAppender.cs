using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using log4net.Appender;

namespace log4net.MicrosoftTeams
{
    public class MicrosoftTeamsAppender : AppenderSkeleton
    {
        private readonly Process _currentProcess = Process.GetCurrentProcess();

        public string WebhookUrl { get; set; }

        protected override void Append(log4net.Core.LoggingEvent loggingEvent)
        {
            var teamsClient = new MicrosoftTeamsClient(WebhookUrl.Expand());
            var facts = new List<MicrosoftTeamsMessageFact>();

            facts.Add(new MicrosoftTeamsMessageFact { Name = "Process", Value = _currentProcess.ProcessName });
            facts.Add(new MicrosoftTeamsMessageFact { Name = "Machine", Value = Environment.MachineName });
            facts.Add(new MicrosoftTeamsMessageFact { Name = "Level", Value = loggingEvent.Level.DisplayName });
            facts.Add(new MicrosoftTeamsMessageFact { Name = "Logger", Value = loggingEvent.LoggerName });

            // Add exception fields if exception occurred
            var exception = loggingEvent.ExceptionObject;
            if (exception != null)
            {
                facts.Add(new MicrosoftTeamsMessageFact { Name = "Exception Type", Value = exception.GetType().Name });
                facts.Add(new MicrosoftTeamsMessageFact { Name = "Exception Message", Value = exception.Message });
            }

            var formattedMessage = (Layout != null ? Layout.FormatString(loggingEvent) : loggingEvent.RenderedMessage);

            teamsClient.PostMessageAsync(formattedMessage, facts);
        }
    }
}

