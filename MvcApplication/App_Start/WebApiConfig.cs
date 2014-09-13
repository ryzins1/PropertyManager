using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MvcApplication
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
			// Web API configuration and services

			// make web requests from the browser return json
			config.Formatters.Add(new BrowserJsonFormatter());

			// make json return camelCase
			foreach (var jsonFormatter in config.Formatters.OfType<JsonMediaTypeFormatter>())
			{
				jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			}

			// Web API routes
			config.MapHttpAttributeRoutes();
        }
    }

	public class BrowserJsonFormatter : JsonMediaTypeFormatter
	{
		public BrowserJsonFormatter()
		{
			SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
			SerializerSettings.Formatting = Formatting.Indented;
		}

		public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
		{
			base.SetDefaultContentHeaders(type, headers, mediaType);
			headers.ContentType = new MediaTypeHeaderValue("application/json");
		}
	}
}
