using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
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
            var properties = new List<MicrosoftTeamsMessageFact>();
            
            properties.Add(new MicrosoftTeamsMessageFact("Process", value: _currentProcess.ProcessName));
            properties.Add(new MicrosoftTeamsMessageFact("Machine", value: Environment.MachineName));
            properties.Add(new MicrosoftTeamsMessageFact("Level", value: loggingEvent.Level.DisplayName));
            properties.Add(new MicrosoftTeamsMessageFact("Logger", value: loggingEvent.LoggerName));

            // Add exception fields if exception occurred
            var exception = loggingEvent.ExceptionObject;
            if (exception != null)
            {
                properties.Add(new MicrosoftTeamsMessageFact("Exception Type", value: exception.GetType().Name));
                properties.Add(new MicrosoftTeamsMessageFact("Exception Message", value: exception.Message));
            }
            
            var formattedMessage = (Layout != null ? Layout.FormatString(loggingEvent) : loggingEvent.RenderedMessage);

            teamsClient.PostMessageAsync(formattedMessage, properties);
        }
    }
}

