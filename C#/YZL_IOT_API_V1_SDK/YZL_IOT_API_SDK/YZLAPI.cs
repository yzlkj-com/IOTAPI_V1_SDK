using System;

namespace YZL_IOT_API_SDK
{
    public class YZLAPI
    {
        protected string _BaseUrl = "http://api.yzlkj.com/";

        protected readonly string _ApiKey;
        protected readonly string _Secret;
        protected readonly string _PassPhrase;

        public YZLAPI(string apiKey, string secret, string passPhrase)
        {
            _ApiKey = apiKey;
            _Secret = secret;
            _PassPhrase = passPhrase;
        }
    }
}
