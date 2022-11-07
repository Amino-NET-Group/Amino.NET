using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Amino.Objects
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class MembershipInfo
    {
        public bool accountMembershipEnabled { get; }
        public bool hasAnyAppleSubscription { get; }
        public bool hasAnyAndroidSubscription { get; }
        public string json { get; }
        public _Membership Membership { get; }



        public MembershipInfo(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            accountMembershipEnabled = (bool)jsonObj["accountMembershipEnabled"];
            hasAnyAppleSubscription = (bool)jsonObj["hasAnyAppleSubscription"];
            hasAnyAndroidSubscription = (bool)jsonObj["hasAnyAndroidSubscriiption"];
            json = _json.ToString();
            if(jsonObj["membership"] != null) { Membership = new _Membership(_json); }
        }
    }

    public class _Membership
    {
        public int paymentType { get; }
        public string expiredDate { get; }
        public string renewedTime { get; }
        public string userId { get; }
        public string modifiedTime { get; }
        public bool isAutoRenew { get; }
        public int membershipStatus { get; }
        public _Membership(JObject _json)
        {
            dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(_json.ToString());
            paymentType = (int)jsonObj["membership"]["paymentType"];
            expiredDate = (string)jsonObj["membership"]["expiredTime"];
            renewedTime = (string)jsonObj["membership"]["renewedTime"];
            userId = (string)jsonObj["membership"]["uid"];
            modifiedTime = (string)jsonObj["membership"]["modifiedTime"];
            isAutoRenew = (bool)jsonObj["membership"]["isAutoRenew"];
            membershipStatus = (int)jsonObj["membership"]["membershipStatus"];
        }
    }
}
