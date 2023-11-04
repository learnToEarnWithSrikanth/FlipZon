namespace FlipZon.Service
{
    public sealed class RestServiceHttpClient
    {
        public HttpClient httpClient = new HttpClient();
        private static readonly object lock1 = new object();
        private static RestServiceHttpClient instance = null;
        public static RestServiceHttpClient Instance
        {
            get
            {
                lock (lock1)
                {
                    if (instance == null)
                        instance = new RestServiceHttpClient();
                    return instance;
                }
            }
        }
    }
}

