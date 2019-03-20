using System;
using System.Collections.Generic;
using System.Diagnostics;
using log4net.Appender;
using log4net.Core;

namespace log4net.MicrosoftTeams
{
    public class MicrosoftTeamsAppender : AppenderSkeleton
    {
        private readonly Process _currentProcess = Process.GetCurrentProcess();

        private string WebhookUrl { get; set; }

        private MicrosoftTeamsClient _teamsClient;

        internal MicrosoftTeamsClient TeamsClient
        {
            get { return this._teamsClient; }
        }

        public override void ActivateOptions()
        {
            base.ActivateOptions();

            if (string.IsNullOrEmpty(WebhookUrl))
            {
                throw new ArgumentException("WebhookUrl not set!");
            }

            this._teamsClient = new MicrosoftTeamsClient(WebhookUrl.Expand());
        }

        protected override void Append(log4net.Core.LoggingEvent loggingEvent)
        {
            var facts = new Dictionary<string, string>();

            facts.Add("Process", _currentProcess.ProcessName );
            facts.Add("Machine", Environment.MachineName );
            facts.Add("Level", loggingEvent.Level.DisplayName );
            facts.Add("Logger", loggingEvent.LoggerName );

            // Add exception fields if exception occurred
            var exception = loggingEvent.ExceptionObject;
            if (exception != null)
            {
                facts.Add("Exception Type", exception.GetType().Name);
                facts.Add("Exception Message", exception.Message);
            }

            var formattedMessage = (Layout != null ? Layout.FormatString(loggingEvent) : loggingEvent.RenderedMessage);

            try
            {
                TeamsClient.PostMessageAsync(formattedMessage, facts);
            }
            catch (Exception ex)
            {
                throw new LogException(ex.Message, ex);
            }
        }
    }
}

