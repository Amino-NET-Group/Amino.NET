using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class ChatMember
    {
        public string json { get; set; } // NEEDS TO BE ADDED AFTER
        [JsonPropertyName("status")]public int Status { get; set; }
        [JsonPropertyName("isNicknameVerified")]public bool IsNicknameVerified { get; set; }
        [JsonPropertyName("uid")]public string UserId { get; set; }
        [JsonPropertyName("level")]public int Level { get; set; }
        [JsonPropertyName("accountMembershipStatus")]public int AccountMembershipStatus { get; set; }
        [JsonPropertyName("membershipStatus")]public int MembershipStatus { get; set; }
        [JsonPropertyName("reputation")]public int Reputation { get; set; }
        [JsonPropertyName("role")]public int Role { get; set; }
        [JsonPropertyName("nickname")]public string Nickname { get; set; }
        [JsonPropertyName("icon")]public string IconUrl { get; set; }

        [JsonPropertyName("avatarFrame")]public GenericAvatarFrame AvatarFrame { get; set; }
    }
}
