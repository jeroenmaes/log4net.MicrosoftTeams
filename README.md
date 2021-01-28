[![NuGet version (log4net.MicrosoftTeams)](https://img.shields.io/nuget/v/log4net.MicrosoftTeams.svg?style=flat)](https://www.nuget.org/packages/log4net.MicrosoftTeams/)
[![Build Status](https://jeroenmaes.visualstudio.com/Demo/_apis/build/status/JEMS.log4net.MicrosoftTeams?branchName=master)](https://jeroenmaes.visualstudio.com/Demo/_build/latest?definitionId=5&branchName=master)
# log4net.MicrosoftTeams
A log4net appender that writes to Microsoft Teams.

# Output
![Example log4net.MicrosoftTeams output](img/log4netMicrosoftTeamsWithTitleOutput.png)

# Usage
```xml
<appender name="myMicrosoftTeamsAppender" type="log4net.MicrosoftTeams.MicrosoftTeamsAppender, log4net.MicrosoftTeams">
  <WebhookUrl value="" /> <!-- Your MicrosoftTeams channel to post to -->
  <titleLayout> 
    <conversionPattern value="Title - %p: %date [%c]" />
  </titleLayout>
  <layout type="log4net.Layout.PatternLayout">
    <conversionPattern value="%-5level %logger - %message" />
  </layout>
</appender>
```
