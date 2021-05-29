# twitchLurkerV2

Download from <a href="https://github.com/ardaerbaharli/twitchLurkerV2/releases/"> here</a>

### What is Twitch Lurker?

**Twitch Lurker** is a bot that joins to chat room of the Twitch channels you follow.
It acts as a chatter and sends messages to as many channels as you wish at the same time without you needing to open up Twitch.
It also lets you benefit from subscription gifts and drops as long as you leave it on.

<br><br>

### How To Edit Tiwtch Lurker?

From <a href="https://github.com/ardaerbaharli/twitchLurkerV2/blob/main/CriteriaControls.cs"> here</a> (file name on your local repo would be CriteriaControl.cs), you can edit a list of things.

##IMPORTANT NOTE: It is not suggested that you change the default values but we all make mistakes in the heat of passion.

<br><br>

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
<br><br>

-Bot has default blacklisted channels. It is not suggested that you delete default channels but you can add more.

>In order to add more channels, go to windows search and type in *%appdata%*. From there, go to *Chastoca>LurkerV2>Config>BlacklistedChannels* and type in the channel ID you want to blacklist.