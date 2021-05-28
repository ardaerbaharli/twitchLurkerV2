# twitchLurkerV2

Download from <a href="https://github.com/ardaerbaharli/twitchLurkerV2/releases/tag/twitchLurkerV2.1"> here</a>

### What is Twitch Lurker?

**Twitch Lurker** is a bot that joins to chat room of the Twitch channels you follow.
It acts as a chatter and lets you send messages to as many channels as you wish at the same time without you needing to open up Twitch.
You can also just join the chat to benefit from subscription gifts and drops, you do not have to send any messages.

### How To Edit Tiwtch Lurker?

From <a href="https://github.com/ardaerbaharli/twitchLurkerV2/blob/main/CriteriaControls.cs"> here</a> (file name on your local repo would be CriteriaControl.cs), you can edit a list of things.

Bot will come with default settings as below:

-Bot sends text messages to channels only if the channel has more than 2000 viewers.

>In order to change that, you can change the value of **if (channel.Stream.Viewers > 2000)** to any value you want it to be.

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
                        else
                            return false;
                    }
                    return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                LogHandler.CrashReport(ex);
                return false;
            }
        }