using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using YZL_IOT_API_SDK;

namespace YZL_IOT_API_V1_SDK
{
    class Program
    {
        static void Main(string[] args)
        {
            var apiKey = "";
            var secret = "";
            var passPhrase = "";

            DeviceAPI deviceAPI = new DeviceAPI(apiKey, secret, passPhrase);
            PublicAPI publicAPI = new PublicAPI(apiKey, secret, passPhrase);
            Console.WriteLine(deviceAPI.GetDeviceSome(new string[] { "HMTR-32" }).Result);
            Console.WriteLine(deviceAPI.GetDeviceList(1,5).Result);
            Console.ReadLine();
        }
    }

    class Result<T>
    {
        public int statusCode { get; set; }

        public string message { get; set; }

        public T data { get; set; }
    }

    class YTime
    {
        public int unixTimestamp { get; set; }

        public string iSO { get; set; }
    }

}
