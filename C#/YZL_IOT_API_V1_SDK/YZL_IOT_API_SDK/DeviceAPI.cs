using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace YZL_IOT_API_SDK
{
    public class DeviceAPI : YZLAPI
    {
        private readonly string _Prefix = "api/v1/device";

        public DeviceAPI(string apiKey, string secret, string passPhrase) : base(apiKey, secret, passPhrase)
        {

        }

        /// <summary>
        /// 获取账户下所有设备信息
        /// </summary>
        /// <returns></returns>
        public async Task<JContainer> GetDeviceList(int page = 1, int pageSize = 50, string time = null)
        {
            using (var client = new HttpClient(new HttpInterceptor(_ApiKey, _Secret, _PassPhrase, null, time)))
            {
                var url = $"{_BaseUrl}{_Prefix}/list/?page={page}&pageSize={pageSize}";
                var res = await client.GetAsync(url);
                var contentStr = await res.Content.ReadAsStringAsync();
                if (contentStr[0] == '[')
                    return JArray.Parse(contentStr);
                return JObject.Parse(contentStr);
            }
        }

        /// <summary>
        /// 获取账户下指定设备信息
        /// </summary>
        /// <param name="deviceIds">设备ID</param>
        /// <returns></returns>
        public async Task<JContainer> GetDeviceSome(string[] deviceIds, string time = null)
        {
            string ids = string.Empty;
            foreach (var id in deviceIds)
            {
                ids += id + ",";
            }
            using (var client = new HttpClient(new HttpInterceptor(_ApiKey, _Secret, _PassPhrase, null, time)))
            {
                var url = $"{_BaseUrl}{_Prefix}/{ids}";
                var res = await client.GetAsync(url);
                var contentStr = await res.Content.ReadAsStringAsync();
                if (contentStr[0] == '[')
                    return JArray.Parse(contentStr);
                return JObject.Parse(contentStr);
            }
        }
    }
}
