using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YZL_IOT_API_SDK
{
    class HttpInterceptor : DelegatingHandler
    {
        private readonly string _ApiKey;
        private readonly string _PassPhrase;
        private readonly string _Secret;
        private readonly string _BodyStr;
        private readonly string _Time;

        public HttpInterceptor(string apiKey, string secret, string passPhrase, string bodyStr, string time = null)
        {
            _ApiKey = apiKey;
            _PassPhrase = passPhrase;
            _Secret = secret;
            _BodyStr = bodyStr;
            _Time = time;
            InnerHandler = new HttpClientHandler();
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var timeStamp = _Time ?? TimeZoneInfo.ConvertTimeToUtc(DateTime.Now).ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
            string sign = 
                Encryptor.HmacSHA256($"{timeStamp}{request.Method.Method}{request.RequestUri.PathAndQuery}" +
                $"{(string.IsNullOrEmpty(_BodyStr) ? "" : _BodyStr)}", _Secret);
            request.Headers.Add("YZL-APIKEY", _ApiKey);
            request.Headers.Add("YZL-SIGN", sign);
            request.Headers.Add("YZL-TIME", timeStamp.ToString());
            request.Headers.Add("YZL-PASSPHRASE", _PassPhrase);
            return base.SendAsync(request, cancellationToken);
        }
    }

    public static class Encryptor
    {
        public static string HmacSHA256(string infoStr, string secret)
        {
            byte[] sha256Data = Encoding.UTF8.GetBytes(infoStr);
            byte[] secretData = Encoding.UTF8.GetBytes(secret);
            using (var hmacsha256 = new HMACSHA256(secretData))
            {
                byte[] buffer = hmacsha256.ComputeHash(sha256Data);

                return Convert.ToBase64String(buffer);
            }
        }
    }
}
