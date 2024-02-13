using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class UserAchievements
    {
        public int numberOfMembersCount { get; } = 0;
        public int numberOfPostsCreated { get; } = 0;
        public string json { get; }

        public UserAchievements(JObject json)
        {
            this.json = json.ToString();

            try { numberOfMembersCount = (int)json["numberOfMembersCount"]; } catch { }
            try { numberOfPostsCreated = (int)json["numberOfPostsCreated"]; } catch { }
        }
    }
}
