﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Amino.Objects
{
    public class Membership
    {
        [JsonPropertyName("paymentType")] public int PaymentTime { get; set; }
        [JsonPropertyName("expiredDate")] public string ExpiredDate { get; set; }
        [JsonPropertyName("renewedTime")] public string RenewedTime { get; set; }
        [JsonPropertyName("uid")] public string ObjectId { get; set; }
        [JsonPropertyName("modifiedTime")] public string ModifiedTime { get; set; }
        [JsonPropertyName("isAutoRenew")] public bool IsAutoRenew { get; set; }
        [JsonPropertyName("membershipStatus")] public int MembershipStatus { get; set; }

    }
}
