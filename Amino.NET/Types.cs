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
            Coins,
            Membeership
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
            Blog_Category__Item_Tag,
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
            General,
            Strike,
            Voice,
            Sticker,
            Video,
            Share_EXURL,
            Share_User,
            Call_Not_Answered,
            Call_Cancelled,
            Call_Declined,
            Video_Call_Not_Answered,
            Video_Call_Cancelled,
            Video_Call_Declined,
            Deleted,
            Member_Join,
            Member_Quit,
            Private_Chat_Init,
            Background_Change,
            Title_Change,
            Icon_Change,
            Start_Voice_Chat,
            Start_Video_Chat,
            Start_Avatar_Chat,
            End_Voice_Chat,
            End_Video_Chat,
            End_Avatar_Chat,
            Content_Change,
            Start_Screening_Room,
            End_Screening_Room,
            Organizer_Transferred,
            Force_Removed_From_Chat,
            Chat_Removed,
            Deleted_By_Admin,
            Send_Coins,
            Pin_Announcement,
            VV_Chat_Permission_Open_To_Everyone,
            VV_Chat_Permission_Invited,
            VV_Chat_Permission_Invite_Only,
            Enable_View_Only,
            Disable_View_Only,
            Unpin_Announcement,
            Enable_Tip_Permission,
            Disable_Tip_Permission,
            Timestamp,
            Welcome_Message,
            Invite_Message
        }
    }
}
