using System.Collections.Generic;
using System.Numerics;

namespace API.SQL.Models
{
	public partial class Estado
	{
		public Estado()
		{
			Municipio = new HashSet<Municipio>();
		}

        public Estado(string cod_Estado, long cod_Regiao, string nome_Estado)
        {
            CodEstado = System.Convert.ToInt64(cod_Estado);
            CodRegiao = cod_Regiao;
            NomeEstado = nome_Estado;
        }

        public long CodEstado { get; set; }
		public string Uf { get; set; }
		public string NomeEstado { get; set; }
		public long CodRegiao { get; set; }
		

		public virtual Regiao CodRegiaoNavigation { get; set; }
		public virtual ICollection<Municipio> Municipio { get; set; }
	}
}
