using Amino.Objects;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Amino
{
    public class ACMClient
    {

        public Amino.Client Client { get; }
        public int CommunityId { get; }

        private RestClient RClient { get; }

        public ACMClient(Amino.Client client, string communityId)
        {
            this.Client = client;
            this.CommunityId = Convert.ToInt32(communityId);
            RClient = new RestClient(helpers.BaseUrl);
            RClient.AddDefaultHeaders(Client.headers);
        }
        public ACMClient(Amino.Client client, int communityId)
        {
            this.Client = client;
            this.CommunityId = communityId;
            RClient = new RestClient(helpers.BaseUrl);
            RClient.AddDefaultHeaders(Client.headers);
        }

        public ACMClient(Amino.SubClient client)
        {
            this.Client = client.GetClient();
            this.CommunityId = Convert.ToInt32(client.GetCurrentCommunityId());
            RClient = new RestClient(helpers.BaseUrl);
            RClient.AddDefaultHeaders(Client.headers);
        }

        public Task create_community(string name, string tagline, byte[] icon, string themeColor, Types.Join_Types joinType = Types.Join_Types.Open, Types.Supported_Languages primaryLanguage = Types.Supported_Languages.English)
        {
            string _lang = "en";
            switch (primaryLanguage)
            {
                case Types.Supported_Languages.English:
                    _lang = "en";
                    break;
                case Types.Supported_Languages.Spanish:
                    _lang = "es";
                    break;
                case Types.Supported_Languages.Portuguese:
                    _lang = "pt";
                    break;
                case Types.Supported_Languages.Arabic:
                    _lang = "ar";
                    break;
                case Types.Supported_Languages.Russian:
                    _lang = "ru";
                    break;
                case Types.Supported_Languages.French:
                    _lang = "fr";
                    break;
                case Types.Supported_Languages.German:
                    _lang = "de";
                    break;
            }
            JObject data = new JObject()
            {
                { "icon", new JObject() {
                    { "height", 512.0 },
                    { "path", this.Client.upload_media(icon, Types.upload_File_Types.Image) },
                    { "width", 512.0 },
                    { "x", 0.0 },
                    { "y", 0.0 },
                    { "imageMatrix", new JArray() { 1.6875, 0.0, 108.0, 0.0, 1.6875, 497.0, 0.0, 0.0, 1.0} }
                } },
                { "joinType", (int)joinType },
                { "name", name },
                { "primaryLanguage", _lang },
                { "tagline", tagline },
                { "templateId", 9 },
                { "themeColor", themeColor },
                { "timestamp", helpers.GetTimestamp() * 1000 }
            };
            RestRequest request = new RestRequest("/g/s/community");
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonSerializer.Serialize(data)));
            request.AddJsonBody(JsonSerializer.Serialize(data));

            var response = RClient.ExecutePost(request);
            if (!response.IsSuccessStatusCode) { throw new Exception(response.Content); }
            if (Client.Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;

        }

        public Task delete_community(string email, string password, string verificationCode)
        {
            JObject data = new JObject()
            {
                { "secret", $"0 {password}" },
                { "validationContext", new JObject() {
                    { "data", new JObject() { "code", verificationCode } }
                }},
                { "type", 1 },
                { "identity", email },
                { "deviceId", this.Client.DeviceId },
                { "timestamp", helpers.GetTimestamp() * 1000 }
            };
            RestRequest request = new RestRequest($"/g/s-x{CommunityId}/community/delete-request");
            request.AddJsonBody(JsonSerializer.Serialize(data));
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonSerializer.Serialize(data)));
            var response = RClient.ExecutePost(request);
            if (!response.IsSuccessStatusCode) { throw new Exception(response.Content); }
            if (Client.Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        public List<Community> list_communities(int start = 0, int size = 25)
        {
            RestRequest request = new RestRequest($"/g/s/community/managed?start={start}&size={size}");
            var response = RClient.ExecuteGet(request);
            if (!response.IsSuccessStatusCode) { throw new Exception(response.Content); }
            if (Client.Debug) { Trace.WriteLine(response.Content); }
            return System.Text.Json.JsonSerializer.Deserialize<List<Community>>(JsonDocument.Parse(response.Content).RootElement.GetProperty("communityList").GetRawText());
        }

        public List<BlogCategory> get_blog_categories(int start = 0, int size = 25)
        {
            RestRequest request = new RestRequest($"/x{CommunityId}/s/blog-category?start={start}&size={size}");
            var response = RClient.ExecuteGet(request);
            if (!response.IsSuccessStatusCode) { throw new Exception(response.Content); }
            if (Client.Debug) { Trace.WriteLine(response.Content); }
            return System.Text.Json.JsonSerializer.Deserialize<List<BlogCategory>>(JsonDocument.Parse(response.Content).RootElement.GetProperty("blogCategoryList").GetRawText());
        }


        public Task change_sidebar_color(string color)
        {
            RestRequest request = new RestRequest($"/x{CommunityId}/s/community/configuration");
            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                { "path", "appearance.leftSidePanel.style.iconColor" },
                { "value", color.Length == 7 ? color : "#000000" },
                { "timestamp", helpers.GetTimestamp() * 1000 }
            };
            request.AddHeader("NDC-MSG-SIG", System.Text.Json.JsonSerializer.Serialize(data));
            request.AddJsonBody(JsonSerializer.Serialize(data));
            var response = RClient.ExecutePost(request);
            if (!response.IsSuccessStatusCode) { throw new Exception(response.Content); }
            if (Client.Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        public AdvancedCommunityInfo get_community_info()
        {
            RestRequest request = new RestRequest($"/g/s-x{CommunityId}/community/info?withTopicList=1&withInfluencerList=1&influencerListOrderStrategy=fansCount");
            var response = RClient.ExecuteGet(request);
            if (!response.IsSuccessStatusCode) { throw new Exception(response.Content); }
            if (Client.Debug) { Trace.WriteLine(response.Content); }
            return JsonSerializer.Deserialize<AdvancedCommunityInfo>(JsonDocument.Parse(response.Content).RootElement.GetProperty("community").GetRawText());
        }

        public Task promote(string userId, Types.RoleTypes roleType)
        {
            string _role = "";
            switch (roleType)
            {
                case Types.RoleTypes.Agent:
                    _role = "transfer-agent";
                    break;
                case Types.RoleTypes.Leader:
                    _role = "leader";
                    break;
                case Types.RoleTypes.Curator:
                    _role = "curator";
                    break;
            }
            RestRequest request = new RestRequest($"/x{CommunityId}/s/user-profile/{userId}/{_role}");
            var response = RClient.ExecutePost(request);
            if (!response.IsSuccessStatusCode) { throw new Exception(response.Content); }
            if (Client.Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        public List<JoinRequest> get_join_request(int start = 0, int size = 25)
        {
            RestRequest request = new RestRequest($"/x{CommunityId}/s/community/membership-request?status=pending?start={start}&size={size}");
            var response = RClient.ExecuteGet(request);
            if (!response.IsSuccessStatusCode) { throw new Exception(response.Content); }
            if (Client.Debug) { Trace.WriteLine(response.Content); }
            return JsonSerializer.Deserialize<List<JoinRequest>>(JsonDocument.Parse(response.Content).RootElement.GetRawText());
        }

        public Task accept_join_request(string userId)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            RestRequest request = new RestRequest($"/x{CommunityId}/s/community/membership-request/{userId}/accept");
            request.AddJsonBody(JsonSerializer.Serialize(data));
            request.AddHeader("NDC-MSG-SIG", JsonSerializer.Serialize(data));
            var response = RClient.ExecutePost(request);
            if (!response.IsSuccessStatusCode) { throw new Exception(response.Content); }
            if (Client.Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }
        public Task reject_join_request(string userId)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            RestRequest request = new RestRequest($"/x{CommunityId}/s/community/membership-request/{userId}/reject");
            request.AddJsonBody(JsonSerializer.Serialize(data));
            request.AddHeader("NDC-MSG-SIG", JsonSerializer.Serialize(data));
            var response = RClient.ExecutePost(request);
            if (!response.IsSuccessStatusCode) { throw new Exception(response.Content); }
            if (Client.Debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

        public CommunityStats get_community_stats()
        {
            RestRequest request = new RestRequest($"/x{CommunityId}/s/community/stats");
            var response = RClient.ExecutePost(request);
            if (!response.IsSuccessStatusCode) { throw new Exception(response.Content); }
            if (Client.Debug) { Trace.WriteLine(response.Content); }
            return JsonSerializer.Deserialize<CommunityStats>(JsonDocument.Parse(response.Content).RootElement.GetProperty("communityStats").GetRawText());
        }
    } 
}
