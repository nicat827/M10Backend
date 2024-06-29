using Amazon.Lambda.APIGatewayEvents;
using NetlifyFunctions;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace NetlifyFunctions.Functions
{
    public class Handler
    {
        public async Task<APIGatewayProxyResponse> Run(APIGatewayProxyRequest request)
        {
            var response = new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } },
                Body = "Hello from C# Netlify Function!",
            };
            return response;
        }
    }
}
