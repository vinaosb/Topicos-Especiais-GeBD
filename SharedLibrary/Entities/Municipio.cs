using System.Collections.Generic;
using System.Numerics;

namespace API.SQL.Models
{
	public partial class Municipio
	{
		public Municipio()
		{
			Endereco = new HashSet<Endereco>();
		}
        public Municipio(string cod_Municipio, string cod_Estado, string pK_COD_MUNICIPIO_OLD, string nome_Municipio)
        {
            CodMunicipio = System.Convert.ToInt64(cod_Municipio);
            CodEstado = System.Convert.ToInt64(cod_Estado);
            PkCodMunicipioOld = System.Convert.ToInt64(pK_COD_MUNICIPIO_OLD);
            NomeMunicipio = nome_Municipio;
        }

        public long CodMunicipio { get; set; }
		public long CodEstado { get; set; }
		public long PkCodMunicipioOld { get; set; }
		public string NomeMunicipio { get; set; }
		

		public virtual Estado CodEstadoNavigation { get; set; }
		public virtual ICollection<Endereco> Endereco { get; set; }
	}
}
