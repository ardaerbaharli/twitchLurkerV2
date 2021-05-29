using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitchLib.Api;
using TwitchLib.Api.Helix.Models.Users;
using TwitchLib.Client;
using TwitchLib.Client.Models;

namespace TwitchLurkerV2
{
    public partial class Lurker : Form
    {
        public Lurker()
        {
            InitializeComponent();
        }

        #region Move the window by form
        private bool mouseDown;
        private Point lastLocation;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        #endregion


        private int controlCount = 0;
        private int sentMessageCount = 0;
        private DateTime startTime;
        private int messageIntervalMinutes = 4;

        public static bool credentialsInputExitStatus = false;

        private List<string> emoteList = new List<string>() { };
        public static List<string> blacklistedChannelList = new List<string>() { };

        private List<string> joinedChannels = new List<string>();
        private List<Follow> followList = new List<Follow>();
        private List<string> subList = new List<string>();

        private void Lurker_Shown(object sender, EventArgs e)
        {
            this.Hide();
            Configurate();

            // design
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.TabStop = false;

            // Update online channels
            updateOnlineChannelsTimer.Interval = 1000 * 60 * messageIntervalMinutes;

            uptimeTimer.Interval = 1000 * 33;
            uptimeTimer.Start();
            startTime = DateTime.Now;

            // send data between forms
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        #region Configurations
        private void Configurate()
        {
            SetCredentials();
            SetMessagePreference();
            SetEmotes();
            SetBlacklistedChannels();
        }
        private bool SetMessagePreference()
        {
            try
            {
                string prefName = "SendMessages";

                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string path = Path.Combine(appDataPath, @"Chastoca");
                path = Path.Combine(path, "LurkerV2");
                path = Path.Combine(path, "Config");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = Path.Combine(path, "Settings.txt");
                bool doesEmoteFileExists = File.Exists(path);
                if (doesEmoteFileExists)
                {
                    // read the file               
                    var content = File.ReadAllLines(path).ToList();

                    foreach (var item in content)
                    {
                        if (item.Contains(prefName))
                        {
                            bool state = bool.Parse(item.Substring(prefName.Length + ":".Length));
                            checkMessages.Checked = state;
                        }
                    }
                }
                else
                {
                    bool state = true;
                    var content = $"{prefName}:{state}";

                    checkMessages.Checked = state;
                    File.WriteAllText(path, content.ToString());
                }
            }
            catch (Exception ex)
            {
                LogHandler.CrashReport(ex);
                return false;
            }
            return true;
        }
        private bool SetCredentials()
        {
            try
            {
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string path = Path.Combine(appDataPath, @"Chastoca");
                path = Path.Combine(path, "LurkerV2");
                path = Path.Combine(path, "Config");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = Path.Combine(path, "ConnectionCredentials.txt");
                bool doesCredentialsFileExists = File.Exists(path);
                if (doesCredentialsFileExists)
                {
                    this.Show();
                    // read the file               
                    string[] text = File.ReadAllLines(path);
                    lurkerName = text[0].Substring("username: ".Length);
                    lurkerToken = text[1].Substring("lurkerToken: ".Length);
                    Connect();
                }
                else
                {
                    // call the input form
                    CredentialsInput ci = new CredentialsInput();
                    ci.FormClosed += CredentialsInput_FormClosed;
                    ci.Show();
                }
            }
            catch (Exception ex)
            {
                LogHandler.CrashReport(ex);
                return false;
            }
            return true;
        }
        private void CredentialsInput_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (credentialsInputExitStatus == true)
                SetCredentials();
        }
        private bool SetEmotes()
        {
            try
            {
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string path = Path.Combine(appDataPath, @"Chastoca");
                path = Path.Combine(path, "LurkerV2");
                path = Path.Combine(path, "Config");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = Path.Combine(path, "Emotes.txt");
                bool doesEmoteFileExists = File.Exists(path);
                if (doesEmoteFileExists)
                {
                    // read the file               
                    emoteList = File.ReadAllLines(path).ToList();
                }
                else
                {
                    var content = new List<string>() {
                        "LUL", "Kappa", ":)", "PogChamp", "D:", "HeyGuys", "Wowee",
                        "HypeLol", "voyunHey", "voyunSip", "voyunLurk", "voyunMEGA",
                        "ClappyHype", "voyunPog", "PrideLionYay", "OhMyDog ","OpieOP",
                        "TheRinger"};

                    emoteList = content;
                    File.WriteAllLines(path, content.ToArray());
                }
            }
            catch (Exception ex)
            {
                LogHandler.CrashReport(ex);
                return false;
            }
            return true;
        }
        private bool SetBlacklistedChannels()
        {
            try
            {
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string path = Path.Combine(appDataPath, @"Chastoca");
                path = Path.Combine(path, "LurkerV2");
                path = Path.Combine(path, "Config");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = Path.Combine(path, "BlacklistedChannels.txt");
                bool doesEmoteFileExists = File.Exists(path);
                if (doesEmoteFileExists)
                {
                    // read the file               
                    blacklistedChannelList = File.ReadAllLines(path).ToList();
                }
                else
                {
                    var content = new List<string>() { "RocketLeague", "VALORANT_Esports_TR", "ROSHTEIN", "VonDice", "DeuceAce" };
                    blacklistedChannelList = content;
                    File.WriteAllLines(path, content);
                }
            }
            catch (Exception ex)
            {
                LogHandler.CrashReport(ex);
                return false;
            }
            return true;
        }

        #region Events
        private void CheckMessages_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string prefName = "SendMessages";

                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string path = Path.Combine(appDataPath, @"Chastoca");
                path = Path.Combine(path, "LurkerV2");
                path = Path.Combine(path, "Config");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = Path.Combine(path, "Settings.txt");
                bool doesEmoteFileExists = File.Exists(path);
                if (doesEmoteFileExists)
                {
                    List<string> content = File.ReadAllLines(path).ToList();

                    foreach (var item in content.ToList())
                    {
                        if (item.Contains(prefName))
                        {
                            bool state = bool.Parse(item.Substring(prefName.Length + ":".Length));
                            int index = content.FindLastIndex(x => x.Contains(prefName));
                            content[index] = $"{prefName}:{!state}";
                            File.WriteAllLines(path, content.ToArray());
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.CrashReport(ex);
            }
        }
        #endregion
        #endregion

        #region Twitch Side
        public static string lurkerName = "";
        public static string lurkerToken = "";
        private static string lurkerID = "";
        private readonly string clientId = "gp762nuuoqcoxypju8c569th9wz7q5";
        // private readonly ConnectionCredentials credentials = new ConnectionCredentials(username, lurkerToken);
        private TwitchClient client;
        private TwitchAPI api;
        private void Connect()
        {
            ConnectionCredentials credentials = new ConnectionCredentials(lurkerName, lurkerToken);
            client = new TwitchClient();
            client.Initialize(credentials);
            client.OnConnected += Client_OnConnected;
            client.OnMessageReceived += Client_OnMessageReceived;
            client.OnWhisperReceived += Client_OnWhisperReceived;
            client.OnWhisperSent += Client_OnWhisperSent;
            api = new TwitchAPI();
            api.Settings.ClientId = clientId;
            api.Settings.AccessToken = lurkerToken;

            client.Connect();
        }

        #region Events
        private async void Client_OnConnected(object sender, TwitchLib.Client.Events.OnConnectedArgs e)
        {
            lblConnected.Text = "Connected to " + lurkerName + ".";
            bool isJoined = await StartLurking();

            if (isJoined)
            {
                updateOnlineChannelsTimer.Enabled = true;
                updateOnlineChannelsTimer.Start();
            }
            else
            {
                Exception ex = new Exception("Client didn't join the channels.");
                LogHandler.CrashReport(ex);
                this.Close();
            }
        }
        private async void Client_OnMessageReceived(object sender, TwitchLib.Client.Events.OnMessageReceivedArgs e)
        {
            if (e.ChatMessage.Message.Contains(lurkerName))
            {
                try
                {
                    if (!string.IsNullOrEmpty(e.ChatMessage.Channel))
                    {
                        var channel = (await api.V5.Users.GetUserByNameAsync(e.ChatMessage.Channel)).Matches.FirstOrDefault();
                        if (channel != null)
                        {
                            string channelID = channel.Id;
                            string streamURL = "<streamURL>";
                            TimeSpan? uptimeTS = new TimeSpan();

                            if (await CriteriaControls.IsOnline(api, channelID))
                            {
                                uptimeTS = await api.V5.Streams.GetUptimeAsync(channelID);
                                var stream = await api.V5.Channels.GetChannelVideosAsync(channelID, 1, 0);
                                streamURL = stream.Videos.FirstOrDefault().Url;
                            }

                            string uptime = "<time>";
                            if (uptimeTS.HasValue)
                                uptime = $"{uptimeTS.Value.Hours}:{uptimeTS.Value.Minutes}:{uptimeTS.Value.Seconds}";

                            Random rand = new Random();
                            int streamID = rand.Next(10000000, 100000000);

                            Log streamIDLog = new Log();
                            streamIDLog.FolderDirectory = new string[] { "Mentions", "StreamIDs" };
                            streamIDLog.LogName = "StreamID";
                            streamIDLog.Message = $"{streamID} || Time: {uptime} >> Link: {streamURL}";
                            streamIDLog.TimeStamp = true;
                            LogHandler.Log(streamIDLog);

                            Log mentionLog = new Log();
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
                Log whisper = new Log();
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
                Log whisper = new Log();
                whisper.FolderDirectory = new string[] { "Whispers" };
                whisper.LogName = e.Receiver;
                whisper.TimeStamp = false;
                whisper.Message = $"{DateTime.Now} || {lurkerName} : {e.Message}";
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
                await RetrieveFollowList();
                // control if the channel is online and/or subscribed
                await JoinChannels();
                stayOnline.Enabled = true;
                stayOnline.Start();
            }
            catch (Exception ex)
            {
                LogHandler.CrashReport(ex);
                return await StartLurking();
            }
            return true;
        }
        private async Task<bool> RetrieveFollowList()
        {
            try
            {
                // reset the counter
                followList.Clear();

                // get the lurker account
                var lurker = (await api.V5.Users.GetUserByNameAsync(lurkerName)).Matches.First();
                lurkerID = lurker.Id;

                // request first 100 followers
                var requestedFollowers = await api.Helix.Users.GetUsersFollowsAsync(first: 100, fromId: lurkerID);
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
                    requestedFollowers = await api.Helix.Users.GetUsersFollowsAsync(after: pagination, first: requestingChannelCount, fromId: lurkerID);
                    // extract the follows from request
                    list = requestedFollowers.Follows;
                    // add follows to the list
                    followList.AddRange(list.ToList());
                }

                lblFollowedCount.Text = "Currently following " + followList.Count + " channels.";
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
                controlCount++;

                // if its 50th time calling this method, update follow list
                if (controlCount == 50)
                {
                    controlCount = 0;
                    await RetrieveFollowList();
                }

                if (client != null)
                    if (!client.IsConnected)
                        client.Connect();
                // check new subs and new online streams
                var joinedChannelsAPI = client.JoinedChannels;
                if (joinedChannels.Count != joinedChannels.Count)
                    await Restart();
                else
                    foreach (var c in followList.ToList())
                    {
                        string channelID = c.ToUserId;
                        string channelName = c.ToUserName;

                        bool isSubscribed = await CriteriaControls.IsSubscribed(api, channelID, lurkerID);
                        if (isSubscribed)
                            if (!subList.Contains(channelID))
                            {
                                // if it is not already in the list, add it to the list                    
                                subList.Add(channelID);
                                lblSubCount.Text = "Currently subscribed to " + subList.Count + " channels.";
                            }

                        bool isOnline = await CriteriaControls.IsOnline(api, channelID);
                        if (isOnline)
                        {
                            if (!joinedChannelsAPI.Any(p => p.Channel == channelName))
                            {
                                if (!joinedChannels.Contains(channelName))
                                {
                                    client.JoinChannel(channelName);
                                    joinedChannels.Add(channelName);
                                    lblLurkCount.Text = "Lurking " + joinedChannels.Count + " channels.";
                                }
                            }
                            else
                            {
                                if (!CriteriaControls.IsBlacklisted(channelName))
                                {
                                    bool isMoreThan2ThousandViewers = await CriteriaControls.DoesStreamHaveMoreThan2000Viewers(api, channelID);
                                    if (isMoreThan2ThousandViewers)
                                        SendRandomText(channelName);
                                }
                            }
                        }
                        else
                        {
                            if (joinedChannels.Contains(channelName))
                            {
                                client.LeaveChannel(channelName);
                                joinedChannels.Remove(channelName);
                                lblLurkCount.Text = "Lurking " + joinedChannels.Count + " channels.";
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
        private async Task Restart()
        {
            joinedChannels.Clear();
            lblLurkCount.Text = "Lurking " + joinedChannels.Count + " channels.";

            var joinedChannelsAPI = client.JoinedChannels;
            foreach (var c in joinedChannelsAPI.ToList())
            {
                client.LeaveChannel(c.Channel);
            }
            await JoinChannels();
        }
        private bool SendRandomText(string channelName)
        {
            try
            {
                if (checkMessages.Checked)
                {
                    if (client.JoinedChannels.Any(x => x.Channel == channelName))
                    {
                        Random rand = new Random();
                        int textIndex = rand.Next(0, emoteList.Count);
                        string message = emoteList[textIndex];
                        client.SendMessage(channelName, message);
                        sentMessageCount++;
                        lblMessageCount.Text = "Sent " + sentMessageCount + " messages.";
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

        #region Events
        private void StayOnline_Tick(object sender, EventArgs e)
        {
            if (joinedChannels.Count > 0)
                foreach (var c in joinedChannels.ToList())
                {
                    client.JoinChannel(c);
                }
        }
        private async void UpdateOnlineChannelsTimer_Tick(object sender, EventArgs e)
        {
            await JoinChannels();
        }
        #endregion
        #endregion
        #endregion

        #region Form Events
        private void Uptime_Tick(object sender, EventArgs e)
        {
            TimeSpan uptime = DateTime.Now - startTime;
            lblUptime.Text = string.Format("Uptime: {0} days, {1} hours, {2} minutes.", uptime.Days, uptime.Hours, uptime.Minutes);
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}