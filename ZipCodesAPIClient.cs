using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace APIVerve
{
    public class ZipCodesAPIClient
    {
        private readonly string _apiEndpoint = "https://api.apiverve.com/v1/zipcodes";
        private readonly string _method = "GET";

        private string _apiKey { get; set; }
        private bool _isSecure { get; set; }
        private bool _isDebug { get; set; }

        public ZipCodesAPIClient(string apiKey, bool isSecure = true)
        {
            _apiKey = apiKey;
            _isSecure = isSecure;
        }

        public ZipCodesAPIClient(string apiKey, bool isSecure = true, bool isDebug = false)
        {
            _apiKey = apiKey;
            _isSecure = isSecure;
            _isDebug = isDebug;
        }

        public string GetApiKey() => _apiKey;
        public bool GetIsSecure() => _isSecure;
        public bool GetIsDebug() => _isDebug;

        public void SetApiKey(string apiKey) => _apiKey = apiKey;
        public void SetIsSecure(bool isSecure) => _isSecure = isSecure;
        public void SetIsDebug(bool isDebug) => _isDebug = isDebug;
        public string GetApiEndpoint() => _apiEndpoint;

        public ResponseObj Execute(zipcodesQueryOptions options)
        {
            try
            {
                if (_isDebug)
                {
                    Console.WriteLine("Executing API request...");
                }

                var url = constructURL(options);

                if (_isDebug)
                {
                    Console.WriteLine("URL: " + url);
                }

                var request = WebRequest.Create(url);
                request.Headers["x-api-key"] = _apiKey;
                request.Method = _method;

                if (_method == "POST")
                {
                    var body = JsonConvert.SerializeObject(options);
                    var data = Encoding.UTF8.GetBytes(body);

                    request.ContentType = "application/json";
                    request.ContentLength = data.Length;

                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }

                string responseString;
                using (var response = request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseString = reader.ReadToEnd();
                    }
                }

                if (_isDebug)
                {
                    Console.WriteLine("Response: " + responseString);
                }

                if (string.IsNullOrEmpty(responseString))
                {
                    throw new Exception("No response from the server");
                }

                var responseObj = JsonConvert.DeserializeObject<ResponseObj>(responseString);
                return responseObj;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        private string constructURL(zipcodesQueryOptions options)
        {
            string url = _apiEndpoint;

            string query = "";
            if (_method == "GET")
            {
                foreach (var prop in options.GetType().GetProperties())
                {
                    query += prop.Name + "=" + prop.GetValue(options, null) + "&";
                }

                if (query.Length > 0 && query[query.Length - 1] == '&')
                {
                    query = query.Remove(query.Length - 1);
                }

                if (!string.IsNullOrEmpty(query))
                {
                    url += "?" + query;
                }
            }

            return url;
        }
    }    
}
