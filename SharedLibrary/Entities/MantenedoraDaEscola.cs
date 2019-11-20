using System.Numerics;

namespace API.SQL.Models
{
	public partial class MantenedoraDaEscola
	{
		public long CodEntidade { get; set; }
		public bool Empresa { get; set; }
		public bool Sindicato { get; set; }
		public bool SistemsSSesi { get; set; }
		public bool Senai { get; set; }
		public bool Sesc { get; set; }

        public MantenedoraDaEscola(string cod_Entidade, string empresa, string sindicato, string sistema_S, string senai, string sesc)
        {
            CodEntidade = System.Convert.ToInt64(cod_Entidade);
            
            Sindicato = sindicato.ToLower().Equals("sim") ? true : false;
            SistemsSSesi = sistema_S.ToLower().Equals("sim") ? true : false;
            Senai = senai.ToLower().Equals("sim") ? true : false;
            Sesc = sesc.ToLower().Equals("sim") ? true : false;
            Empresa = empresa.ToLower().Equals("sim") ? true : false;
            System.Console.WriteLine(Sindicato);
        }

        public virtual Escola CodEntidadeNavigation { get; set; }
	}
}
