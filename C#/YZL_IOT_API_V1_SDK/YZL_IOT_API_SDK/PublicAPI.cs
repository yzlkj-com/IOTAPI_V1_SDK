using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using YZL_IOT_API_SDK;

namespace YZL_IOT_API_V1_SDK
{
    public class PublicAPI : YZLAPI
    {
        private readonly string _Prefix = "api/v1";
        public PublicAPI(string apiKey, string secret, string passPhrase) : base(apiKey, secret, passPhrase)
        {
        }

        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <returns></returns>
        public async Task<JContainer> GetServerTime()
        {
            using (var client = new HttpClient(new HttpInterceptor(_ApiKey, _Secret, _PassPhrase, null)))
            {
                var url = $"{_BaseUrl}{_Prefix}/Time";
                var res = await client.GetAsync(url);
                var contentStr = await res.Content.ReadAsStringAsync();
                if (contentStr[0] == '[')
                    return JArray.Parse(contentStr);
                return JObject.Parse(contentStr);
            }
        }
    }
}
