using Newtonsoft.Json;
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

        public Task create_community(string name, string tagline, byte[] icon, string themeColor, Types.Join_Types joinType = Types.Join_Types.Open, Types.Supported_Languages primaryLanguage = Types.Supported_Languages.english)
        {
            string _lang = "en";
            switch(primaryLanguage)
            {
                case Types.Supported_Languages.english:
                    _lang = "en";
                    break;
                case Types.Supported_Languages.spanish:
                    _lang = "es";
                    break;
                case Types.Supported_Languages.portuguese:
                    _lang = "pt";
                    break;
                case Types.Supported_Languages.arabic:
                    _lang = "ar";
                    break;
                case Types.Supported_Languages.russian:
                    _lang = "ru";
                    break;
                case Types.Supported_Languages.french:
                    _lang = "fr";
                    break;
                case Types.Supported_Languages.german:
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
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
            request.AddJsonBody(JsonConvert.SerializeObject(data));

            var response = RClient.ExecutePost(request);
            if(!response.IsSuccessStatusCode) { throw new Exception(response.Content); }
            if(Client.debug) { Trace.WriteLine(response.Content); }
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
                { "deviceId", this.Client.deviceID },
                { "timestamp", helpers.GetTimestamp() * 1000 }
            };
            RestRequest request = new RestRequest($"/g/s-x{CommunityId}/community/delete-request");
            request.AddJsonBody(JsonConvert.SerializeObject(data));
            request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
            var response = RClient.ExecutePost(request);
            if(!response.IsSuccessStatusCode) { throw new Exception(response.Content); }
            if(Client.debug) { Trace.WriteLine(response.Content); }
            return Task.CompletedTask;
        }

    }
}
