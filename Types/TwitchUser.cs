
namespace TwitchLurkerV2.Types
{
    class TwitchUser
    {
        public string Username { get; set; }
        public string ID { get; set; }

        public TwitchUser(string _username = null,string _id= null)
        {
            if(!string.IsNullOrEmpty(_username)&&!string.IsNullOrEmpty(_id))
            {
                Username = _username;
                ID = _id;
            }           
        }
    }
}
