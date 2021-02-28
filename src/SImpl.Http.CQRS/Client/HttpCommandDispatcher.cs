using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SImpl.CQRS.Commands;

namespace SImpl.Http.CQRS.Client
{
    public class HttpCommandDispatcher : ICommandDispatcher
    {
        private readonly HttpDispatchConfig _config;

        public HttpCommandDispatcher(HttpDispatchConfig config)
        {
            _config = config;
        }
        
        public async Task ExecuteAsync<T>(T command) 
            where T : class, ICommand
        {
            // TODO Add security
            var postRequest = new HttpRequestMessage(HttpMethod.Post, _config.Uri)
            {
                Content = JsonContent.Create(command) // TODO: use newton soft
            };

            using var client = new HttpClient();
            var postResponse = await client.SendAsync(postRequest);

            postResponse.EnsureSuccessStatusCode();
        }
    }
}