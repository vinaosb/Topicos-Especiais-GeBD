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
using SharedLibrary;

namespace Gerencia_de_Escolas_Website.Data
{
    public class EscolasService
	{
		public List<Escola> escolas { get; set; }
		public string path = "https://localhost:44309/";

		public EscolasService()
		{
		}

		public async Task<Escola[]> GetAsync(int index = 0)
		{
			List<Escola> t;
			using (Sender<Escola> s = new Sender<Escola>(path))
			{
				t = await s.Get("api/Escolas");
				t = t.GetRange(index * 5, 5);

				foreach (var e in t)
				{
					using (Sender<Endereco> ss = new Sender<Endereco>(path))
						e.CodEnderecoNavigation = await ss.Get("api/Enderecos", e.CodEndereco.ToString());
					using (Sender<Municipio> ss = new Sender<Municipio>(path))
						e.CodEnderecoNavigation.CodMunicipioNavigation = await ss.Get("api/Municipios", e.CodEnderecoNavigation.CodMunicipio.ToString());
					using (Sender<Estado> ss = new Sender<Estado>(path))
						e.CodEnderecoNavigation.CodMunicipioNavigation.CodEstadoNavigation = await ss.Get("api/Estados", e.CodEnderecoNavigation.CodMunicipioNavigation.CodEstado.ToString());
					using (Sender<Regiao> ss = new Sender<Regiao>(path))
						e.CodEnderecoNavigation.CodMunicipioNavigation.CodEstadoNavigation.CodRegiaoNavigation = await ss.Get("api/Regioes", e.CodEnderecoNavigation.CodMunicipioNavigation.CodEstadoNavigation.CodRegiao.ToString());
				}
			}
			return t.ToArray();
		}
	}
}