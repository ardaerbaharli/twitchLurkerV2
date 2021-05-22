using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api;

namespace TwitchLurkerV2
{
    class CriteriaControls
    {
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
        public static bool IsBlacklisted(string channelName)
        {
            
            return Lurker.blacklistedChannelList.Contains(channelName);
        }

        public static async Task<bool> IsOnline(TwitchAPI api, string channelID)
        {
            bool isOnline;
            try
            {
                isOnline = await api.V5.Streams.BroadcasterOnlineAsync(channelID);
            }
            catch (Exception ex)
            {
                LogHandler.CrashReport(ex);
                return false;
            }
            return isOnline;
        }
        public static async Task<bool> IsSubscribed(TwitchAPI api, string channelID, string lurkerID)
        {
            // if the lurker is not subscribed to the channel, it will throw an error
            // with that error, we return false 

            try
            {
                var ch = await api.V5.Users.CheckUserSubscriptionByChannelAsync(lurkerID, channelID);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
