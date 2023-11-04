namespace FlipZon.Utilities
{
    public static class JsonConversion
    {
        public static StringContent Serialize<T>(T t)
        {
            var requestBody = JsonConvert.SerializeObject(t);
            var strContent = new StringContent(requestBody, Encoding.UTF8, Constants.APPLICATION_JSON);
            return strContent;
        }

        public static async Task<T> Deserialize<T>(HttpResponseMessage httpResponseMessage)
        {
            var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(responseContent);
            return result;
        }
    }
}

