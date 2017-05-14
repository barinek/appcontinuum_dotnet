using System;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace Timesheets
{
    public class ProjectClient : IProjectClient
    {
        private readonly string _endpoint;

        public ProjectClient()
        {
            _endpoint = Environment.GetEnvironmentVariable("REGISTRATION_SERVER_ENDPOINT");
        }

        public ProjectClient(string endpoint)
        {
            _endpoint = endpoint;
        }

        public async Task<ProjectInfo> Get(long projectId)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            var streamTask = client.GetStreamAsync($"{_endpoint}/project?projectId={projectId}");

            var serializer = new DataContractJsonSerializer(typeof(ProjectInfo));
            return serializer.ReadObject(await streamTask) as ProjectInfo;
        }
    }
}