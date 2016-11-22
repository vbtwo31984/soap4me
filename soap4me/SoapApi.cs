using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;

namespace soap4me
{
	class ModernHttpClientFactory : DefaultHttpClientFactory
	{
		public override HttpMessageHandler CreateMessageHandler()
		{
			HttpClientHandler handler = (HttpClientHandler)base.CreateMessageHandler();
			handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate;
			return handler;
		}
	}

	public class SoapApi
	{
		private static SoapApi instance;
		private SoapApi() {
			FlurlHttp.Configure(c =>
			{
				c.HttpClientFactory = new ModernHttpClientFactory();
			});
		}
		private Dictionary<String, String> headers = new Dictionary<string, string>()
		{
			{"User-Agent", SoapApi.USER_AGENT},
			{"Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8"},
			{"Accept-Encoding", "gzip,deflate,sdch"},
			{"Accept-Language", "ru-ru,ru;q=0.8,en-us;q=0.5,en;q=0.3"},
			{"x-api-token", ""}
		};
		public static SoapApi getInstance()
		{
			if (SoapApi.instance == null)
			{
				SoapApi.instance = new SoapApi();
			}
			return SoapApi.instance;
		}

		private const String BASE_URL = "https://soap4.me/";
		private const String API_URL = "https://soap4.me/api/";
		private const String LOGIN_URL = "http://soap4.me/login/";
		private const String USER_AGENT = "xbmc for soap";

		public async Task<LoginResult> login(String username, String pass)
		{
			var client = new FlurlClient(LOGIN_URL).WithHeaders(headers);
			LoginResultInternal result;
			try
			{
				result = await client.PostUrlEncodedAsync(new
				{
					login = username,
					password = pass
				}).ReceiveJson<LoginResultInternal>();
			}
			catch (Exception)
			{
				result = new LoginResultInternal();
			}
			return new LoginResult(result);
		}
	}
}
