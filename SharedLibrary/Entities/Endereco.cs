using System.Collections.Generic;
using System.Numerics;

namespace API.SQL.Models
{
	public partial class Endereco
	{
		public Endereco()
		{
			Escola = new HashSet<Escola>();
		}

        public Endereco(string cod_Endereco, string cod_Municipio, string cEP, string nome_Distrito, string endereco1, string numero, string complemento, string bairro)
        {
            CodEndereco = System.Convert.ToInt64(cod_Endereco);
            CodMunicipio = System.Convert.ToInt64(cod_Municipio);
            Cep = cEP;
            NomeDestrito = nome_Distrito;
            Endereco1 = endereco1;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
        }

        public long CodEndereco { get; set; }
		public long CodMunicipio { get; set; }
		public string Cep { get; set; }
		public string NomeDestrito { get; set; }
		public string Endereco1 { get; set; }
		public string Numero { get; set; }
		public string Complemento { get; set; }
		public string Bairro { get; set; }
		

		public virtual Municipio CodMunicipioNavigation { get; set; }
		public virtual ICollection<Escola> Escola { get; set; }
	}
}
