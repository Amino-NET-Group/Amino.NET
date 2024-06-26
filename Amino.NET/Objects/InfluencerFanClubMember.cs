﻿using System.Text.Json.Serialization;

namespace Amino.Objects
{
    public class InfluencerFanClubMember
    {
        [JsonPropertyName("fansStatus")] public int? FanStatus { get; set; }
        [JsonPropertyName("createdTime")] public string CreatedTime { get; set; }
        [JsonPropertyName("targetUserId")] public string TargetUserId { get; set; }
        [JsonPropertyName("expiredTime")] public string ExpiredTime { get; set; }
        [JsonPropertyName("targetUserProfile")] public GenericProfile TargetUserProfile { get; set; }
    }
}
