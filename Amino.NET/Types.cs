namespace Amino
{
    public class Types
    {
        public enum account_gender
        {
            Male,
            Female,
            Non_Binary
        }
        public enum Call_Types
        {
            None,
            Video,
            Avatar,
            Screen_Room
        }
        public enum Call_Permission_Types
        {
            Open_To_Everyone,
            Join_Request,
            Invite_Only
        }
        public enum Flag_Types
        {
            Aggression,
            Spam,
            Off_Topic,
            Violence,
            Intolerance,
            Suicide,
            Trolling,
            Pronography
        }
        public enum File_Types
        {
            Audio,
            Image,
            Stream
        }
        public enum Sorting_Types
        {
            Newest,
            Oldest,
            Top
        }
        public enum Repair_Types
        {
            Coins,
            Membeership
        }
        public enum Activity_Status_Types
        {
            On,
            Off
        }
        public enum Chat_Publish_Types
        {
            Is_Global,
            Off,
            On,
        }
        public enum Source_Types
        {
            User_Profile,
            Detail_Post,
            Global_Compose
        }
        public enum User_Types
        {
            Recent,
            Banned,
            Featured,
            Leaders,
            Curators
        }
        public enum Leaderboard_Types
        {
            Day,
            Week,
            Reputation,
            Check_In,
            Quiz
        }
        public enum Featured_Types
        {
            Unfreature,
            User,
            Blog,
            Wiki,
            Chat
        }
    }
}
