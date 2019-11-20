using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using API.SQL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Gerencia_de_Escolas_Website.Data
{
    public class EscolasService
	{
		public List<Escola> escolas { get; set; }
		public HttpClient client = new HttpClient();
		public string path = "https://localhost:44309/api/";

		public EscolasService()
		{
			client.BaseAddress = new Uri(path);
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public async Task<Escola[]> GetAsync()
		{
			var response = await client.GetAsync("Escolas");


			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadAsStringAsync();
				escolas = JsonConvert.DeserializeObject<List<Escola>>(result);

			}
			return escolas.ToArray();
		}
	}
}