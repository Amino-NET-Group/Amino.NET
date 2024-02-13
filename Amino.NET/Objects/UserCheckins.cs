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
    public class UserCheckins
    {

        public bool hasAnyCheckIn { get; } = false;
        public int consecutiveCheckInDays { get; } = 0;
        public int brokenStreaks { get; } = 0;
        public string json { get; }



        public UserCheckins(JObject json)
        {
            this.json = json.ToString();

            try { hasAnyCheckIn = (bool)json["hasAnyCheckIn"]; } catch { }
            try { consecutiveCheckInDays = (int)json["consecutiveCheckInDays"]; } catch { }
            try { brokenStreaks = (int)json["brokenStreaks"]; } catch { }
        }

    }
}
