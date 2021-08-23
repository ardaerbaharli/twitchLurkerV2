using System;
using System.Threading.Tasks;
using TwitchLib.Api;
using TwitchLurkerV2.Core;

namespace TwitchLurkerV2.Helpers
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
                    if (channel.Stream != null)
                    {
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
            return Configuration.BlacklistedChannelList.Contains(channelName.ToLower());
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
        public static async Task<bool> IsFollowing(TwitchAPI api, string channelID, string lurkerID)
        {
            // if the lurker is not following the channel, it will throw an error
            // with that error, we return false 

            try
            {
                var ch = await api.V5.Users.CheckUserFollowsByChannelAsync(lurkerID, channelID);
                return true;
            }
            catch (Exception ex)
            {
                LogHandler.CrashReport(ex);
                return false;
            }
        }
    }
}
