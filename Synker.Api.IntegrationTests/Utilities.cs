using Newtonsoft.Json;
using Synker.Persistence;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Synker.Api.FunctionalTests
{
    public class Utilities
    {
        public static StringContent GetRequestJsonContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(stringResponse);
        }

        public static void InitializeDbForTests(SynkerDbContext context)
        {
            SynkerInitializer.Initialize(context);
        }
    }
}
