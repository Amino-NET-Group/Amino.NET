using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amino
{
    public class SubClient
    {

        private Amino.Client client;
        public bool debug { get; set; }
        private string communityId;



        //headers.
        private IDictionary<string, string> headers = new Dictionary<string, string>();

        //Handles the header stuff
        private Task headerBuilder()
        {
            headers.Clear();
            headers.Add("NDCDEVICEID", client.deviceID);
            headers.Add("Accept-Language", "en-US");
            headers.Add("Content-Type", "application/json; charset=utf-8");
            headers.Add("Host", "service.aminoapps.com");
            headers.Add("Accept-Encoding", "gzip");
            headers.Add("Connection", "Keep-Alive");
            headers.Add("User-Agent", "Apple iPhone13,4 iOS v15.6.1 Main/3.12.9");
            if (client.sessionID != null) { headers.Add("NDCAUTH", $"sid={client.sessionID}"); }
            return Task.CompletedTask;
        }


        public SubClient(Amino.Client _client, string _communityId)
        {
            if (_client.sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            client = _client;
            debug = client.debug;
            communityId = _communityId;
            headerBuilder();
        }

        public SubClient(Amino.Client _client, int _communityId)
        {
            if (_client.sessionID == null) { throw new Exception("ErrorCode: 0: Client not logged in"); }
            client = _client;
            debug = client.debug;
            communityId = _communityId.ToString();
            headerBuilder();
        }


        
        public List<Amino.Objects.InviteCode> get_invite_codes(string status = "normal", int start = 0, int size = 25)
        {
            try
            {
                List<Amino.Objects.InviteCode> inviteCodeList = new List<Objects.InviteCode>();
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s-x{communityId}/community/invitation?status={status}&start={start}&size={size}");
                request.AddHeaders(headers);
                var response = client.ExecuteGet(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if (debug) { Trace.WriteLine(response.Content); }
                dynamic jsonObj = (JObject)JsonConvert.DeserializeObject(response.Content);
                JArray inviteCodesJson = jsonObj["communityInvitationList"];
                foreach(JObject code in inviteCodesJson)
                {
                    Amino.Objects.InviteCode invCode = new Objects.InviteCode(code);
                    inviteCodeList.Add(invCode);
                }
                Console.WriteLine(response.Content);
                return inviteCodeList;

            } catch(Exception e) { throw new Exception(e.Message); }

        } 

        public Task generate_invite_code(int duration = 0, bool force = true)
        {
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s-x{communityId}/community/invitation");
                request.AddHeaders(headers);
                JObject data = new JObject();
                data.Add("duration", duration);
                data.Add("force", force);
                data.Add("timestamp", helpers.GetTimestamp() * 1000);
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
                var response = client.ExecuteGet(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task delete_invite_code(string inviteId)
        {
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/g/s-x{communityId}/community/invitation/{inviteId}");
                request.AddHeaders(headers);
                var response = client.Delete(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
        }

        public Task post_blog(string title, string content)
        {
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/blog");
                request.AddHeaders(headers);
                JObject data = new JObject();
                data.Add("address", null);
                data.Add("title", title);
                data.Add("content", content);
                data.Add("mediaList", new JArray());
                data.Add("extensions", new JObject());
                data.Add("latitude", 0);
                data.Add("longitude", 0);
                data.Add("eventSource", "GlobalComposeMenu");
                data.Add("timestamp", helpers.GetTimestamp() * 1000);
                request.AddJsonBody(JsonConvert.SerializeObject(data));
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }

                return Task.CompletedTask;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task post_wiki(string title, string content, byte[] icon = null)
        {
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest($"/x{communityId}/s/item");
                request.AddHeaders(headers);
                JObject data = new JObject();
                data.Add("label", title);
                data.Add("content", content);
                data.Add("eventSource", "GlobalComposeMenu");
                data.Add("mediaList", new JArray());
                data.Add("extensions", new JObject());
                data.Add("timestamp", helpers.GetTimestamp() * 1000);
                if(icon != null) { data.Add("icon", this.client.upload_media(icon, Types.upload_File_Types.Image)); }
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonConvert.SerializeObject(data)));
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                if(debug) { Trace.WriteLine(response.Content); }
                return Task.CompletedTask;

            } catch(Exception e) { throw new Exception(e.Message); }
        }

        //public Task edit_blog(string blogId, string title, )


    }
}
