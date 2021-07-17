# twitchLurkerV2

# Download **Twitch Lurker** from <a href="https://github.com/ardaerbaharli/twitchLurkerV2/releases/"> here</a>

### What is Twitch Lurker?

**Twitch Lurker** is a bot that joins to chat room of the Twitch channels you follow.
It acts as a chatter and sends messages to as many channels as you wish at the same time without you needing to open up Twitch.
It also lets you benefit from subscription gifts and drops as long as you leave it on.
<br>
### How To Connect Your Twitch Account?

From <a href="https://twitchtokengenerator.com" target="_blank">here</a>, you can generate a Twitch token to use in **Twitch Lurker**. Give permission to everything while generating your token and inject it to **Twitch Lurker** when prompted.
<br>

### How To Edit Twitch Lurker?

From <a href="https://github.com/ardaerbaharli/twitchLurkerV2/blob/main/CriteriaControls.cs" target="_blank"> here</a> (file name on your local repo would be CriteriaControl.cs), you can edit viewer count and which files to use for criteria control.

## IMPORTANT NOTE: It is not suggested that you change the default values in *CriteriaControl.cs* but we all make mistakes in the heat of passion.

<br>

-Bot sends text messages to channels only if the channel has more than 2000 viewers.

>In order to change that, you can change the value of **if (channel.Stream.Viewers > 2000)** to any value you want it to be.

```
public static async Task<bool> DoesStreamHaveMoreThan2000Viewers(TwitchAPI api, string channelID)
        {
            try
            {
                var channel = await api.V5.Streams.GetStreamByUserAsync(channelID);
                if (channel != null)
                {
                    if (channel.Stream != null) { 
                        if (channel.Stream.Viewers > 2000)
                            return true;
```
<br>

-Bot has default blacklisted channels. You can delete the current ones or add more channels.

>In order to add more channels, go to Windows search and type in *%appdata%*. From there, go to *Chastoca>LurkerV2>Config>BlacklistedChannels* and type in the channel ID you want to blacklist.

<br>

-Bot only sends emotes as messages and has some default emotes. You can delete the default ones or add more to them.

>In order to edit emotes, go to Windows search and type in *%appdata%*. From there, go to *Chastoca>LurkerV2>Config>Emotes* and edit emotes in the text file as you please.

<br>

-You can change your connection credentials if you wish to change the account connected to bot.

>In order to change connection credentials, go to Windows search and type in *%appdata%*. From there, go to *Chastoca>LurkerV2>Config>ConnectionCredentials* and change login details.

<br><br>

### How to check log details?

First, go to Windows search and type in *%appdata%*, then navigate to **Chastoca>LurkerV2**. From here:

<br>

> - You can check crash reports from **CrashReports**
> - You can see if anyone mentioned you in the chat from **Mentions**.
> In mentions you have two files:
> - From ChatLogs, you can see the channel name, link ID, person who sent the message and the message itself.
> - From StreamIDs, you can see when the message was sent and link of the clip from the time message was sent.

<br>

## BE RESPONSIBLE AND DO NOT USE THIS BOT FOR MALICIOUS INTENT. HAVE FUN!
