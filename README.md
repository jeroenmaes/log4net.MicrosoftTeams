# log4net.MicrosoftTeams
A log4net appender that writes to Microsoft Teams.

# Output
![Example log4net.MicrosoftTeams output](img/log4netMicrosoftTeamsOutput.png)

# Usage
```xml
<appender name="myMicrosoftTeamsAppender" type="log4net.MicrosoftTeams.MicrosoftTeamsAppender, log4net.MicrosoftTeams">
  <WebhookUrl value="" /> <!-- Your MicrosoftTeams channel to post to -->
  <layout type="log4net.Layout.PatternLayout">
    <conversionPattern value="%-5level %logger - %message" />
  </layout>
</appender>
```
