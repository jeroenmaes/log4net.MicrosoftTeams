using System;
using System.Collections.Generic;
using System.Diagnostics;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;

namespace log4net.MicrosoftTeams
{
    public class MicrosoftTeamsAppender : AppenderSkeleton
    {
        private readonly Process _currentProcess = Process.GetCurrentProcess();
        
        public PatternLayout TitleLayout { get; set; }
        
        public string WebhookUrl { get; set; }
        
        private MicrosoftTeamsClient TeamsClient { get; set; }

        public override void ActivateOptions()
        {
            base.ActivateOptions();

            if (string.IsNullOrEmpty(WebhookUrl))
            {
                throw new ArgumentException("WebhookUrl not set!");
            }

            this.TeamsClient = new MicrosoftTeamsClient(WebhookUrl.Expand());
        }

        protected override void Append(LoggingEvent loggingEvent)
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
            var title = (TitleLayout != null ?  TitleLayout.FormatString(loggingEvent) : formattedMessage);

            try
            {
                TeamsClient.PostMessage(title, formattedMessage, facts);
            }
            catch (Exception ex)
            {
                throw new LogException(ex.Message, ex);
            }
        }
    }
}

