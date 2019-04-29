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

        public enum Switc
        {
            Close, // 关
            Open // 开
        }

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

        /// <summary>
        /// 控制设备开关量
        /// </summary>
        /// <param name="deviceId">设备ID</param>
        /// <param name="serialNumber">传感器序号</param>
        /// <param name="switc">开关</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public async Task<JContainer> ControlSwitchQuantity(string deviceId, string serialNumber, Switc switc, string time = null)
        {
            var body = JsonConvert.SerializeObject(new
            {
                deviceId,
                serialNumber,
                switc = (int)switc
            });
            using (var client = new HttpClient(new HttpInterceptor(_ApiKey, _Secret, _PassPhrase, body, time)))
            {
                var url = $"{_BaseUrl}{_Prefix}/control";
                var res = await client.PostAsync(url, new StringContent(body, Encoding.UTF8, "application/json"));
                var contentStr = await res.Content.ReadAsStringAsync();
                if (contentStr[0] == '[')
                    return JArray.Parse(contentStr);
                return JObject.Parse(contentStr);
            }
        }
    }
}
