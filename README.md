# Sprint Planning Ordering Tool

Das Tool sortiert teilnehmende Mitglieder nach folgendem Algorithmus:

1. Mitglieder sortieren nach StoryPoints aufsteigend
2. Mitglieder sortieren nach Lines of code (added + deleted) 
3. Für jedes Mitglied wird: indexInStoryPointListe(user) + 2 * indexInLinesOfCodeListe(user) gerechnet, damit lines of code stärker mit einfließen
4. Daraus wird eine Liste aufsteigend sortiert
5. fertig

# Config
```json
{
  "AtlassianUsername": "", //Your username
  "AtlassianPassword": "", //Your password
  "JiraBaseUrl": "", //https://jira.de/
  "BitbucketBaseUrl": "", //https://git.de/
  "BitbucketProject": "",
  "BitbucketRepo": "",  
  "CalculateCodeMetricsSince": "1997-07-16T19:20:30+01:00", //Timestamp since when the added and deleted code should be measured
  "ParticipatingJiraUsers": [
    {
      "Username": "", //git username
      "Name": "", //optional, name des users
      "Email": [
          //ALL emails of the user
      ]
    }
  ]
}

```

# Info!
.NET 7 muss installiert werden um das Release auszuführen
Win 64x (https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-7.0.102-windows-x64-installer)
Linux (https://learn.microsoft.com/en-gb/dotnet/core/install/linux?WT.mc_id=dotnet-35129-website)
