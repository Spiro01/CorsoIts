namespace WebViewer.Data
{
    public class KepServerAPIService
    {
        private HttpClient _httpClient;

        public KepServerAPIService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<KepServerList> GetNodes()
        {
            var result = await _httpClient.GetFromJsonAsync<KepServerList>("http://127.0.0.1:8000/iotgateway/browse");
            return result;
        }
        public async Task<KepServerResults> GetValues(string[] NodestoRead)
        {
            var req = await _httpClient.PostAsJsonAsync("http://127.0.0.1:8000/iotgateway/read", NodestoRead);
            req.EnsureSuccessStatusCode();
            var result = await req.Content.ReadFromJsonAsync<KepServerResults>();
            return result;
        }
        public async Task<KepServerResults> GetValues()
        {
            var nodes = await GetNodes();
            var result = await GetValues(nodes.browseResults.Select(x=>x.id).ToArray());
            return result;
        }
    }
}
