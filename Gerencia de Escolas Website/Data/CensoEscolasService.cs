using API.SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;


namespace Gerencia_de_Escolas_Website.Data
{
    public class CensoEscolasService
	{
		public List<CensoEscola> censosEscolas { get; set; }
		public HttpClient client = new HttpClient();
		public string path = "https://localhost:44309/api/";

		public CensoEscolasService()
		{
			client.BaseAddress = new Uri(path);
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public async Task<CensoEscola[]> GetAsync(string? text)
		{
			HttpResponseMessage response;
			if (string.IsNullOrEmpty(text))
				response = await client.GetAsync("CensoEscolas");
			else
				response = await client.GetAsync("CensoEscolas/" + text);

			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadAsStringAsync();
				censosEscolas = JsonConvert.DeserializeObject<List<CensoEscola>>(result);

			}
			return censosEscolas.ToArray();
		}

        public string Get(int id)
        {
            return "value";
        }

        public void Post(CensoEscola value)
        {
        }

        public void Put(int id, CensoEscola value)
        {
        }

        public void Delete(int id)
        {
        }
    }
}
