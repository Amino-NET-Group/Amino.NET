using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amino.Objects
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ChatMember
    {
        public int status { get; private set; }
        public bool isNicknameVerified { get; private set; }
        public string userId { get; private set; }
        public int level { get; private set; }
        public int accountMembershipStatus { get; private set; }
        public int membershipStatus { get; private set; }
        public int reputation { get; private set; }
        public int role { get; private set; }
        public string nickname { get; private set; }
        public string iconUrl { get; private set; }
        public string json { get; private set; }
        public _AvatarFrame AvatarFrame { get; }

        public ChatMember(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            status = (int)jsonObj["status"];
            isNicknameVerified = (bool)jsonObj["isNicknameVerified"];
            userId = (string)jsonObj["uid"];
            level = (int)jsonObj["level"];
            accountMembershipStatus = (int)jsonObj["accountMembershipStatus"];
            membershipStatus = (int)jsonObj["membershipStatus"];
            reputation = (int)jsonObj["reputation"];
            role = (int)jsonObj["role"];
            nickname = (string)jsonObj["nickname"];
            iconUrl = (string)jsonObj["icon"];
            json = _json.ToString();
            if (jsonObj["avatarFrame"] != null) { AvatarFrame = new _AvatarFrame(_json); }
        }


        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public class _AvatarFrame
        {
            public int status { get; private set; }
            public int ownershipStatus { get; private set; }
            public int verion { get; private set; }
            public string resourceUrl { get; private set; }
            public string name { get; private set; }
            public string iconUrl { get; private set; }
            public int frameType { get; private set; }
            public string frameId { get; private set; }

            public _AvatarFrame(JObject _json)
            {
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
                status = (int)jsonObj["avatarFrame"]["status"];
                ownershipStatus = (int)jsonObj["avatarFrame"]["ownershipStatus"];
                verion = (int)jsonObj["avatarFrame"]["version"];
                resourceUrl = (string)jsonObj["avatarFrame"]["resourceUrl"];
                name = (string)jsonObj["avatarFrame"]["name"];
                iconUrl = (string)jsonObj["avatarFrame"]["icon"];
                frameType = (int)jsonObj["avatarFrame"]["frameType"];
                frameId = (string)jsonObj["avatarFrame"]["frameId"];
            }
        }
    }
}
