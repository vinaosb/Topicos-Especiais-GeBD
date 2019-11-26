using System.Collections.Generic;
using System.Numerics;

namespace API.SQL.Models
{
	public partial class Regiao
	{
		public Regiao()
		{
			Estado = new HashSet<Estado>();
		}

        public Regiao(string cod_Regiao, string nome_Regiao)
        {
            CodRegiao = System.Convert.ToInt16(cod_Regiao);
            NomeRegiao = nome_Regiao;
        }

        public short CodRegiao { get; set; }
		public string NomeRegiao { get; set; }
		

		public virtual ICollection<Estado> Estado { get; set; }
	}
}
