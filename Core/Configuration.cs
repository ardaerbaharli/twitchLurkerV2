using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TwitchLurkerV2.Core.Twitch;
using TwitchLurkerV2.Helpers;
using TwitchLurkerV2.Types;

namespace TwitchLurkerV2.Core
{
    public class Configuration
    {
        private static Lurker _lurker;
        public static List<string> EmoteList = new List<string>() { };
        public static List<string> BlacklistedChannelList = new List<string>() { };
        public static string MsgPath { get; set; }
        public static string CredentialsPath { get; set; }
        public static string EmotesPath { get; set; }
        public static string BlacklistChannelsPath { get; set; }

        public static void Configurate(Lurker lurker)
        {
            SetPaths();
            _lurker = lurker;

            bool isMessagingOn = GetMessagingStatus();
            SetMessagingStatus(isMessagingOn);

            List<string> emotes = GetEmotes();
            SetEmotes(emotes);

            List<string> blacklistChannels = GetBlacklistedChannels();
            SetBlacklistedChannels(blacklistChannels);

            var cred = GetCredentials();
            SetCredentials(cred);
        }
        public static void SetPaths()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string path = Path.Combine(appDataPath, @"Chastoca");
            path = Path.Combine(path, "LurkerV2");
            path = Path.Combine(path, "Config");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);


            MsgPath = Path.Combine(path, "Settings.txt");
            CredentialsPath = Path.Combine(path, "ConnectionCredentials.txt");
            EmotesPath = Path.Combine(path, "Emotes.txt");
            BlacklistChannelsPath = Path.Combine(path, "BlacklistedChannels.txt");
        }

        public static Credential GetCredentials()
        {
            Credential c = null;
            try
            {
                if (File.Exists(CredentialsPath))
                {
                    _lurker.Show();
                    // read the file               
                    string[] text = File.ReadAllLines(CredentialsPath);

                    c = new Credential();
                    c.LurkerName = text[0].Substring("username: ".Length);
                    c.LurkerToken = text[1].Substring("lurkerToken: ".Length);
                }
                else
                {
                    // call the input form
                    CredentialsInput ci = new CredentialsInput();
                    ci.FormClosed += _lurker.CredentialsInput_FormClosed;
                    ci.Show();
                }
            }
            catch (Exception ex)
            {
                LogHandler.CrashReport(ex);
                return null;
            }
            return c;
        }
        public static bool SetCredentials(Credential c)
        {
            try
            {
                if (c != null)
                {
                    Main m = new Main();
                    m.Connect(_lurker, c);
                }
                else
                    SetCredentials(GetCredentials());
            }
            catch (Exception ex)
            {
                LogHandler.CrashReport(ex);
                return false;
            }
            return true;
        }

        public static bool GetMessagingStatus()
        {
            bool status = true;

            try
            {
                string prefName = "SendMessages";
                if (File.Exists(MsgPath))
                {
                    // read the file               
                    var content = File.ReadAllLines(MsgPath).ToList();

                    foreach (var item in content)
                    {
                        if (item.Contains(prefName))
                        {
                            status = bool.Parse(item.Substring(prefName.Length + ":".Length));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHandler.CrashReport(ex);
                return status;
            }
            return status;
        }
        public static void SetMessagingStatus(bool state)
        {
            try
            {
                string prefName = "SendMessages";

                bool doesEmoteFileExists = File.Exists(MsgPath);
                if (doesEmoteFileExists)
                {
                    _lurker.checkMessages.Checked = state;
                    var content = $"{prefName}:{state}";
                    File.WriteAllText(MsgPath, content.ToString());
                }
            }
            catch (Exception ex)
            {
                LogHandler.CrashReport(ex);
            }

        }

        private static List<string> GetEmotes()
        {
            try
            {
                List<string> e = null;
                if (File.Exists(EmotesPath))
                {
                    e = File.ReadAllLines(EmotesPath).ToList();
                }
                else
                {
                    var content = new List<string>() {
                        "LUL", "Kappa", ":)", "PogChamp", "D:", "HeyGuys", "Wowee",
                        "HypeLol",  "ClappyHype", "PrideLionYay", "OhMyDog ","OpieOP", "TheRinger"
                    };

                    e = content;
                    File.WriteAllLines(EmotesPath, content.ToArray());
                }
                return e;
            }
            catch (Exception ex)
            {
                LogHandler.CrashReport(ex);
                return null;
            }
        }
        private static bool SetEmotes(List<string> emotes)
        {
            try
            {
                if (emotes != null)
                    EmoteList = emotes;
                else
                    SetEmotes(GetEmotes());
            }
            catch (Exception ex)
            {
                LogHandler.CrashReport(ex);
                return false;
            }
            return true;
        }

        private static List<string> GetBlacklistedChannels()
        {
            try
            {
                List<string> blacklistChannels = null;
                if (File.Exists(BlacklistChannelsPath))
                {
                    // read the file               
                    blacklistChannels = File.ReadAllLines(BlacklistChannelsPath).ToList();
                }
                else
                {
                    blacklistChannels = new List<string>() { "RocketLeague", "VALORANT_Esports_TR", "ROSHTEIN", "VonDice", "DeuceAce" };

                    File.WriteAllLines(BlacklistChannelsPath, blacklistChannels);
                }
                return blacklistChannels;
            }
            catch (Exception ex)
            {
                LogHandler.CrashReport(ex);
                return null;
            }
        }
        private static bool SetBlacklistedChannels(List<string> blacklistChannels)
        {
            try
            {
                if (blacklistChannels != null)
                    BlacklistedChannelList = blacklistChannels;
                else
                    SetBlacklistedChannels(GetBlacklistedChannels());
            }
            catch (Exception ex)
            {
                LogHandler.CrashReport(ex);
                return false;
            }
            return true;
        }
    }
}
