﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Amino
{
    public class Client
    {
        public string deviceID { get; } 
        public string sessionID { get; }
        public string secret { get; }
        public string userID { get; }
        public string json { get; }

        //public bool renew_device { get; }
        //public string signiture { get; }

        public IDictionary<string, string> headers = new Dictionary<string, string>();

        //Handles the header stuff
        private Task headerBuilder()
        {
            headers.Clear();
            headers.Add("NDCDEVICEID", deviceID);
            headers.Add("Accept-Language", "en-US");
            headers.Add("Content-Type", "application/json; charset=utf-8");
            headers.Add("Host", "service.narvii.com");
            headers.Add("Accept-Encoding", "gzip");
            headers.Add("Connection", "Keep-Alive");
            if(sessionID != null) { headers.Add("NDCAUTH", $"sid={sessionID}"); }

            return Task.CompletedTask;
        }

        public Client(string _deviceID = null)
        {
            if(_deviceID == null) { deviceID = helpers.generate_device_id(); } else { deviceID = _deviceID; }
            headerBuilder();
        }

        public Task request_verify_code(string email, bool resetPassword = false)
        {
           
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/auth/request-security-validation", Method.Post);
                if(!resetPassword) { request.AddJsonBody(new { identity = email, type = 1, deviceID = deviceID}); } else { request.AddJsonBody(new { identity = email, type = 1, deviceID = deviceID, level = 2, purpose = "reset-password"}); }
                request.AddHeaders(headers);
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                return Task.CompletedTask;

            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task Login(string _email, string _password, string _secret = null)
        {
            try
            {
                string secret;
                if (_secret == null) { secret = $"0 {_password}"; } else { secret = _secret; }
                var data = new { email = _email, v = 2, secret = secret, deviceID = deviceID, clientType = 100, action = "normal", timestamp = helpers.GetTimestamp() * 1000};

                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/auth/login", Method.Post);
                request.AddHeaders(headers);
                request.AddJsonBody(data);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonSerializer.Serialize(data)));
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }

                return Task.CompletedTask;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task Register(string _name, string _email, string _password, string _verificationCode, string _deviceID = null)
        {
            try
            {
                if (_deviceID == null) { if (deviceID != null) { _deviceID = deviceID; } else { _deviceID = helpers.generate_device_id(); } }
                var data = new
                {
                    secret = $"0 {_password}",
                    deviceID = _deviceID,
                    email = _email,
                    clientType = 100,
                    nickname = _name,
                    latitude = 0,
                    longtitude = 0,
                    address = String.Empty,
                    clientCallbackURL = "narviiapp://relogin",
                    validationContext = new
                    {
                        data = new { code = _verificationCode },
                        type = 1,
                        identity = _email
                    },
                    type = 1,
                    identity = _email,
                    timestamp = helpers.GetTimestamp() * 1000
                };
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/auth/register");
                request.AddHeaders(headers);
                request.AddJsonBody(data);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonSerializer.Serialize(data)));
                var response = client.ExecutePost(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                
                return Task.CompletedTask;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task Restore_account(string _email, string _password, string _deviceID = null)
        {
            try
            {
                if (_deviceID == null) { if (deviceID != null) { _deviceID = deviceID; } else { _deviceID = helpers.generate_device_id(); } }
                var data = new { secret = $"0 {_password}", deviceID = _deviceID, email = _email, timestamp = helpers.GetTimestamp() * 1000 };
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/account/delete-request/cancel");
                request.AddHeaders(headers);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonSerializer.Serialize(data)));
                var response = client.ExecutePost(request);
                if ((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e) { throw new Exception(e.Message); }
        }
        public Task Delete_account(string _password)
        {
            var data = new { deviceID = deviceID, secret = $"0 {_password}" };
            try
            {
                RestClient client = new RestClient(helpers.BaseUrl);
                RestRequest request = new RestRequest("/g/s/account/delete-request");
                request.AddHeaders(headers);
                request.AddHeader("NDC-MSG-SIG", helpers.generate_signiture(JsonSerializer.Serialize(data)));
                var response = client.ExecutePost(request);
                if((int)response.StatusCode != 200) { throw new Exception(response.Content); }
                return Task.CompletedTask;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
