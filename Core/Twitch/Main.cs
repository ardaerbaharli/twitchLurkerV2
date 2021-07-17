using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitchLib.Api;
using TwitchLib.Api.Helix.Models.Users;
using TwitchLib.Client;
using TwitchLib.Client.Models;
using TwitchLurkerV2.Helpers;
using TwitchLurkerV2.Types;

namespace TwitchLurkerV2.Core.Twitch
{
    public class Main
    {
        public static Main instance { get; set; }
        public static TwitchClient Client { get; set; }
        public static TwitchAPI Api { get; set; }

        private Lurker _lurker { get; set; }
        private Credential _credential { get; set; }

        private int JoinChannelsCount { get; set; }
        private int SentMessageCount { get; set; }

        public static List<string> joinedChannels { get; set; }
        private static List<Follow> followList { get; set; }
        private static List<string> subList { get; set; }

        public void Connect(Lurker lurker, Credential c)
        {
            instance = this;

            followList = new List<Follow>();
            joinedChannels = new List<string>();
            subList = new List<string>();

            _lurker = lurker;
            _credential = c;

            _credential.ClientID = "gp762nuuoqcoxypju8c569th9wz7q5";

            JoinChannelsCount = 0;
            SentMessageCount = 0;

            var credentials = new ConnectionCredentials(_credential.LurkerName, _credential.LurkerName);
            Client = new TwitchClient();
            Client.Initialize(credentials);

            Client.OnConnected += Client_OnConnected;
            Client.OnMessageReceived += Client_OnMessageReceived;
            Client.OnWhisperReceived += Client_OnWhisperReceived;
            Client.OnWhisperSent += Client_OnWhisperSent;

            Api = new TwitchAPI();
            Api.Settings.ClientId = _credential.ClientID;
            Api.Settings.AccessToken = _credential.LurkerToken;

            Client.Connect();
        }

        #region Events
        private async void Client_OnConnected(object sender, TwitchLib.Client.Events.OnConnectedArgs e)
        {
            _lurker.lblConnected.Text = "Connected to " + _credential.LurkerName + ".";
            bool isJoined = await StartLurking();

            if (isJoined)
            {
                _lurker.updateOnlineChannelsTimer.Enabled = true;
                _lurker.updateOnlineChannelsTimer.Start();
            }
            else
            {
                Exception ex = new Exception("Client didn't join any channel.");
                LogHandler.CrashReport(ex);
                _lurker.Close();
            }
        }
        private async void Client_OnMessageReceived(object sender, TwitchLib.Client.Events.OnMessageReceivedArgs e)
        {
            if (e.ChatMessage.Message.Contains(_credential.LurkerName))
            {
                try
                {
                    if (!string.IsNullOrEmpty(e.ChatMessage.Channel))
                    {
                        var channel = (await Api.V5.Users.GetUserByNameAsync(e.ChatMessage.Channel)).Matches.FirstOrDefault();
                        if (channel != null)
                        {
                            string channelID = channel.Id;
                            string streamURL = "<streamURL>";
                            TimeSpan? uptimeTS = new TimeSpan();

                            if (await CriteriaControls.IsOnline(Api, channelID))
                            {
                                uptimeTS = await Api.V5.Streams.GetUptimeAsync(channelID);
                                var stream = await Api.V5.Channels.GetChannelVideosAsync(channelID, 1, 0);
                                streamURL = stream.Videos.FirstOrDefault().Url;
                            }

                            string uptime = "<time>";
                            if (uptimeTS.HasValue)
                                uptime = $"{uptimeTS.Value.Hours}:{uptimeTS.Value.Minutes}:{uptimeTS.Value.Seconds}";

                            var rand = new Random();
                            int streamID = rand.Next(10000000, 100000000);

                            var streamIDLog = new Log();
                            streamIDLog.FolderDirectory = new string[] { "Mentions", "StreamIDs" };
                            streamIDLog.LogName = "StreamID";
                            streamIDLog.Message = $"{streamID} || Time: {uptime} >> Link: {streamURL}";
                            streamIDLog.TimeStamp = true;
                            LogHandler.Log(streamIDLog);

                            var mentionLog = new Log();
                            mentionLog.FolderDirectory = new string[] { "Mentions", "ChatLogs" };
                            mentionLog.LogName = "ChatLog";
                            mentionLog.Message = $"{e.ChatMessage.Channel} || Stream ID : {streamID} || {e.ChatMessage.Username} : {e.ChatMessage.Message} ";
                            mentionLog.TimeStamp = true;
                            LogHandler.Log(mentionLog);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHandler.CrashReport(ex);
                }
            }
        }

        private void Client_OnWhisperReceived(object sender, TwitchLib.Client.Events.OnWhisperReceivedArgs e)
        {
            try
            {
                string username = e.WhisperMessage.Username;
                string message = e.WhisperMessage.Message;

                var whisper = new Log();
                whisper.FolderDirectory = new string[] { "Whispers" };
                whisper.LogName = e.WhisperMessage.Username;
                whisper.TimeStamp = false;
                whisper.Message = $"{DateTime.Now} || {username} : {message}";
                LogHandler.Log(whisper);
            }
            catch (Exception ex)
            {
                LogHandler.CrashReport(ex);
            }

        }
        private void Client_OnWhisperSent(object sender, TwitchLib.Client.Events.OnWhisperSentArgs e)
        {
            try
            {
                var whisper = new Log();
                whisper.FolderDirectory = new string[] { "Whispers" };
                whisper.LogName = e.Receiver;
                whisper.TimeStamp = false;
                whisper.Message = $"{DateTime.Now} || {_credential.LurkerName} : {e.Message}";
                LogHandler.Log(whisper);
            }
            catch (Exception ex)
            {
                LogHandler.CrashReport(ex);
            }
        }
        #endregion

        #region Functions
        private async Task<bool> StartLurking()
        {
            try
            {
                // set the follower list
                await SetFollowList();
                // control if the channel is online and/or subscribed
                await JoinChannels();
                _lurker.stayOnline.Enabled = true;
                _lurker.stayOnline.Start();
            }
            catch (Exception ex)
            {
                LogHandler.CrashReport(ex);
                return await StartLurking();
            }
            return true;
        }
        private async Task<bool> SetFollowList()
        {
            try
            {
                // reset the counter
                followList.Clear();

                // get the lurker account
                var lurker = (await Api.V5.Users.GetUserByNameAsync(_credential.LurkerName)).Matches.First();
                _credential.LurkerID = lurker.Id;

                // request first 100 followers
                var requestedFollowers = await Api.Helix.Users.GetUsersFollowsAsync(first: 100, fromId: _credential.LurkerID);
                // extract the follows from request
                var list = requestedFollowers.Follows;
                // add follows to the list
                followList.AddRange(list.ToList());
                // total following channel count
                long totalFollows = requestedFollowers.TotalFollows;

                totalFollows -= 100;
                while (totalFollows > 0)
                {
                    // request firts requestingChannelCount follows
                    int requestingChannelCount = Convert.ToInt32(Math.Min(totalFollows, 100));

                    totalFollows -= requestingChannelCount;
                    // pagination for offset
                    string pagination = requestedFollowers.Pagination.Cursor;
                    // request the follows
                    requestedFollowers = await Api.Helix.Users.GetUsersFollowsAsync(after: pagination, first: requestingChannelCount, fromId: _credential.LurkerID);
                    // extract the follows from request
                    list = requestedFollowers.Follows;
                    // add follows to the list
                    followList.AddRange(list.ToList());
                }

                _lurker.lblFollowedCount.Text = "Currently following " + followList.Count + " channels.";
            }
            catch (Exception ex)
            {
                LogHandler.CrashReport(ex);
                return false;
            }
            return true;
        }
        public async Task<bool> JoinChannels()
        {
            try
            {
                JoinChannelsCount++;

                // if its 50th time calling this method, update follow list
                if (JoinChannelsCount == 50)
                {
                    JoinChannelsCount = 0;
                    await SetFollowList();
                }

                if (Client != null)
                    if (!Client.IsConnected)
                        Client.Connect();

                // check new subs and new online streams
                var joinedChannelsAPI = Client.JoinedChannels;
                if (joinedChannels.Count != joinedChannels.Count)
                    await RestartBot();
                else
                    foreach (var c in followList.ToList())
                    {
                        string channelID = c.ToUserId;
                        string channelName = c.ToUserName;

                        bool isSubscribed = await CriteriaControls.IsSubscribed(Api, channelID, _credential.LurkerID);
                        if (isSubscribed)
                            if (!subList.Contains(channelID))
                            {
                                // if it is not already in the list, add it to the list                    
                                subList.Add(channelID);
                                _lurker.lblSubCount.Text = "Currently subscribed to " + subList.Count + " channels.";
                            }

                        bool isOnline = await CriteriaControls.IsOnline(Api, channelID);
                        if (isOnline)
                        {
                            if (!joinedChannelsAPI.Any(p => p.Channel == channelName))
                            {
                                if (!joinedChannels.Contains(channelName))
                                {
                                    Client.JoinChannel(channelName);
                                    joinedChannels.Add(channelName);
                                    _lurker.lblLurkCount.Text = "Lurking " + joinedChannels.Count + " channels.";
                                }
                            }
                            else
                            {
                                if (!CriteriaControls.IsBlacklisted(channelName))
                                {
                                    bool isMoreThan2ThousandViewers = await CriteriaControls.DoesStreamHaveMoreThan2000Viewers(Api, channelID);
                                    if (isMoreThan2ThousandViewers)
                                        SendRandomText(channelName);
                                }
                            }
                        }
                        else
                        {
                            if (joinedChannels.Contains(channelName))
                            {
                                Client.LeaveChannel(channelName);
                                joinedChannels.Remove(channelName);
                                _lurker.lblLurkCount.Text = "Lurking " + joinedChannels.Count + " channels.";
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                LogHandler.CrashReport(ex);
                return false;
            }
            return true;
        }
        private async Task RestartBot()
        {
            joinedChannels.Clear();
            _lurker.lblLurkCount.Text = "Lurking " + joinedChannels.Count + " channels.";

            var joinedChannelsAPI = Client.JoinedChannels;
            foreach (var c in joinedChannelsAPI.ToList())
            {
                Client.LeaveChannel(c.Channel);
            }
            await JoinChannels();
        }
        private bool SendRandomText(string channelName)
        {
            try
            {
                if (_lurker.checkMessages.Checked)
                {
                    if (Client.JoinedChannels.Any(x => x.Channel == channelName))
                    {
                        Random rand = new Random();
                        int textIndex = rand.Next(0, Configuration.EmoteList.Count);
                        string message = Configuration.EmoteList[textIndex];
                        Client.SendMessage(channelName, message);
                        SentMessageCount++;
                        _lurker.lblMessageCount.Text = "Sent " + SentMessageCount + " messages.";
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.CrashReport(ex);
            }
            return false;
        }

        #endregion
    }
}
