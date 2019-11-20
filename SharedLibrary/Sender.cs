using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SharedLibrary
{
	public class Sender<T>
	{
		public HttpClient client = new HttpClient();
		public Sender(string basePath)
		{
			client.BaseAddress = new Uri(basePath);
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public async Task<List<T>> Get(string path)
		{
			var response = await client.GetAsync("/" + path);
			var escolas = new List<T>();

			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadAsStringAsync();
				escolas = JsonConvert.DeserializeObject<List<T>>(result);

			}
			return escolas;
		}

		public async Task<T> Post(T obj,string path)
		{
			var cont = JsonConvert.SerializeObject(obj);
			HttpContent hcont = new StringContent(cont, Encoding.UTF8, "application/json");
			hcont.Headers.ContentType = new
				MediaTypeHeaderValue("application/json");
			var response = await client.PostAsync("/" + path, hcont);

			T ret = default;

			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadAsStringAsync();

				ret = JsonConvert.DeserializeObject<T>(result);
			}

			return ret;
		}
	}
}
