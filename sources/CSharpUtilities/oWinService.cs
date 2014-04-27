using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Testing;
using Owin;

namespace CSharpUtilities
{
    public class Startup
    {
        public int? StatusCode { get; set; }
        public string ReasonPhrase { get; set; }
        public string Body { get; set; }

        public Startup(int statusCode, string reasonPhrase, string body)
        {
            this.StatusCode = statusCode;
            this.ReasonPhrase = reasonPhrase;
            this.Body = body;
        }

        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.UseHandler((request, response) =>
            {
                if (StatusCode!=null)
                {
                    response.StatusCode = this.StatusCode.Value;
                }
                if (ReasonPhrase!=null)
                {
                    response.ReasonPhrase = this.ReasonPhrase;
                }
                if (Body!=null)
                {
                    response.Write(this.Body);   
                }
            });
        }
    }
    /*
    public class OWinService
    {
        public  HttpResponseMessage Execute(string url, int expectedStatusCode, string expectedReason)
        {
            var testServer = TestServer.Create(appBuilder => new Startup(expectedStatusCode, expectedReason).Configuration(appBuilder));
            return testServer.HttpClient.GetAsync(url).Result;
        }
    }
     */
}
    