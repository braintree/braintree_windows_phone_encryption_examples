using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BraintreeEncryptionExample
{
    public class BraintreeHttpClient
    {
        private readonly WebClient _webClient;
        private readonly Uri _uri;
        private StringBuilder _postParams;

        public BraintreeHttpClient(string merchantServerURL, UploadStringCompletedEventHandler handler)
        {
            _uri = new Uri(merchantServerURL, UriKind.Absolute);
            _webClient = new WebClient();
            _webClient.Headers["Content-Type"] = "application/x-www-form-urlencoded";
            _webClient.Encoding = Encoding.UTF8;
            _webClient.UploadStringCompleted += handler;
        }

        public void Post(IDictionary<string, object> parameters)
        {
            AddParams(parameters);
            _webClient.UploadStringAsync(_uri, _postParams.ToString());
        }

        private void AddParams(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            _postParams = new StringBuilder();
            foreach (var pair in parameters)
            {
                _postParams.Append(string.Format("{0}={1}&", pair.Key, Uri.EscapeDataString((string)pair.Value)));
            }
        }
    }
}
