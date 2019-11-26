using API.SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using SharedLibrary.Entities.Custom;
using SharedLibrary;

namespace Gerencia_de_Escolas_Website.Data
{
    public class CensoEscolasService
	{
		public List<CensoEscola> censosEscolas { get; set; }
		public string path = "https://localhost:44309/";
		public string pathMongo = "https://localhost:44390/";

		public CensoEscolasService()
		{
		}

		public async Task<CensoEscola[]> GetAsync(string? text, int index)
		{
			if (string.IsNullOrEmpty(text))
			{
				List<CensoEscola> ret;
				using (Sender<CensoEscola> s = new Sender<CensoEscola>(path))
				{
					ret = await s.Get("api/CensoEscolas");
				}
				ret = ret.GetRange(index*30, 30);
				using (Sender<Escola> ss = new Sender<Escola>(path))
					foreach (var ce in ret)
					{
						ce.CodEntidadeNavigation = await ss.Get("api/Escolas", ce.CodEntidade.ToString());
						using (Sender<List<Telefone>> s = new Sender<List<Telefone>>(path))
						{
							ce.CodEntidadeNavigation.Telefone = await s.Get("api/Telefones", ce.Ano.ToString() + "/" + ce.CodEntidade.ToString());
						}
						using (Sender<CorreioEletronico> s = new Sender<CorreioEletronico>(path))
						{
							ce.CodEntidadeNavigation.CorreioEletronico.Add(await s.Get("api/CorreioEletronico", ce.Ano.ToString() + "/" + ce.CodEntidade.ToString()));
						}
					}


				return ret.ToArray();
			}
			else
			{
				List<CensoEscola> rets;
				using (Sender<CensoEscola> s = new Sender<CensoEscola>(path))
					rets = await s.Get("api/CensoEscolas/" + text);
				foreach (var ret in rets)
				{
					using (Sender<Escola> ss = new Sender<Escola>(path))
						ret.CodEntidadeNavigation = await ss.Get("api/Escolas", ret.CodEntidade.ToString());
					using (Sender<ICollection<Telefone>> ss = new Sender<ICollection<Telefone>>(path))
					{
						ret.CodEntidadeNavigation.Telefone = await ss.Get("api/Telefones", ret.Ano.ToString() + "/" + ret.CodEntidade.ToString());
					}
					using (Sender<CorreioEletronico> ss = new Sender<CorreioEletronico>(path))
					{
						ret.CodEntidadeNavigation.CorreioEletronico.Add(await ss.Get("api/CorreioEletronico", ret.Ano.ToString() + "/" + ret.CodEntidade.ToString()));
					}
				}

				CensoEscola[] ce = rets.ToArray();
				return ce;
			}
		}

		public async Task<ExtrasDaEscola[]> GetExtras(string? text)
		{
			if (string.IsNullOrEmpty(text))
			{
				Sender<ExtrasDaEscola> s = new Sender<ExtrasDaEscola>(pathMongo);
				var ret = await s.Get("api/CensoEscolas");

				return ret.ToArray();
			}
			else
			{
				Sender<ExtrasDaEscola> s = new Sender<ExtrasDaEscola>(path);
				var ret = await s.Get("api/CensoEscolas", text);

				ExtrasDaEscola[] ce = new ExtrasDaEscola[] { ret };
				return ce;
			}
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
