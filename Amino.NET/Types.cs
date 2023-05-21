namespace Amino
{
    public class Types
    {
        /// <summary>
        /// Account gender properties
        /// </summary>
        public enum account_gender
        {
            Male,
            Female,
            Non_Binary
        }

        /// <summary>
        /// Types of different Amino calls
        /// </summary>
        public enum Call_Types
        {
            None,
            Voice,
            Video,
            Avatar,
            Screen_Room
        }

        /// <summary>
        /// Types for call permissions
        /// </summary>
        public enum Call_Permission_Types
        {
            Open_To_Everyone,
            Join_Request,
            Invite_Only
        }

        /// <summary>
        /// Types to flag stuff
        /// </summary>
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

        /// <summary>
        /// Aminos supported file types
        /// </summary>
        public enum File_Types
        {
            Audio,
            Image,
            Stream
        }
        /// <summary>
        /// Supported File types for raw uploads
        /// </summary>
        public enum upload_File_Types
        {
            Audio, 
            Image
        }
        /// <summary>
        /// Sorting types for stuff
        /// </summary>
        public enum Sorting_Types
        {
            Newest,
            Oldest,
            Top
        }

        /// <summary>
        /// Repair types for Amino+
        /// </summary>
        public enum Repair_Types
        {
            Coins = 1,
            Membeership = 2
        }

        public enum Send_Coin_Targets
        {
            Chat,
            Blog,
            Wiki
        }

        /// <summary>
        /// Activity status types.
        /// </summary>
        public enum Activity_Status_Types
        {
            On,
            Off
        }

        /// <summary>
        /// Types for chat settings
        /// </summary>
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
        public enum Object_Types
        {
            User,
            Blog,
            Item,
            Comment,
            Blog_Category,
            Blog_Category_Item_Tag,
            Featured_Item,
            Chat_Message,
            Reputationlog_Item,
            Poll_Option,
            Chat_Thread,
            Community,
            Image,
            Music,
            Video,
            YouTube,
            Shared_Folder,
            Shared_File,
            Voice,
            Moderation_Task,
            Screenshot,
            Sticker,
            Sticker_Collection,
            Prop,
            Chat_Bubble,
            Video_Filter,
            Order,
            Share_Request,
            VV_Chat,
            P2A,
            Amino_Video
        }
        public enum Message_Types
        {
            General = 0,
            Strike = 1,
            Voice = 2,
            Sticker = 3,
            Video = 4,
            Share_EXURL = 50,
            Share_User = 51,
            Call_Not_Answered = 55,
            Call_Cancelled = 53,
            Call_Declined = 54,
            Video_Call_Not_Answered = 55,
            Video_Call_Cancelled = 56,
            Video_Call_Declined = 57,
            Deleted = 100,
            Member_Join = 101,
            Member_Quit = 102,
            Private_Chat_Init = 103,
            Background_Change = 104,
            Title_Change = 105,
            Icon_Change = 106,
            Start_Voice_Chat = 107,
            Start_Video_Chat = 108,
            Start_Avatar_Chat = 109,
            End_Voice_Chat = 110,
            End_Video_Chat = 111,
            End_Avatar_Chat = 112,
            Content_Change = 113,
            Start_Screening_Room = 114,
            End_Screening_Room = 115,
            Organizer_Transferred = 116,
            Force_Removed_From_Chat = 117,
            Chat_Removed = 118,
            Deleted_By_Admin = 119,
            Send_Coins = 120,
            Pin_Announcement = 121,
            VV_Chat_Permission_Open_To_Everyone = 122,
            VV_Chat_Permission_Invited = 123,
            VV_Chat_Permission_Invite_Only = 124,
            Enable_View_Only = 125,
            Disable_View_Only = 126,
            Unpin_Announcement = 127,
            Enable_Tip_Permission = 128,
            Disable_Tip_Permission = 129,
            Timestamp = 65281,
            Welcome_Message = 65282,
            Invite_Message = 65283
        }
        public enum Post_Types
        {
            Wiki,
            Blog,
            Quiz,
            Shared_File
        }
        public enum Flag_Targets
        {
            User,
            Blog,
            Wiki
        }

        public enum Comment_Types
        {
            User,
            Blog,
            Wiki,
            Reply
        }

        public enum Supported_Languages
        {
            english,
            spanish,
            portuguese,
            arabic,
            russian,
            french,
            german
        }

        public enum Wallet_Config_Levels
        {
            lvl_1,
            lvl_2
        }

        public enum Repost_Types
        {
            Blog = 1,
            Wiki = 2
        }
    }
}
