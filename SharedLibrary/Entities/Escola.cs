using System.Collections.Generic;
using System.Numerics;

namespace API.SQL.Models
{
	public partial class Escola
	{
		public Escola()
		{
			CensoEscola = new HashSet<CensoEscola>();
			CorreioEletronico = new HashSet<CorreioEletronico>();
			Telefone = new HashSet<Telefone>();
		}

        public Escola(string cod_Entidade, string cod_Endereco, string localizacao, string nome, string categoria, string iD_LATITUDE, string iD_LONGITUDE, string instituicao_Sem_Fim_Lucrativo)
        {
            CodEntidade = System.Convert.ToInt64(cod_Entidade);
            CodEndereco = System.Convert.ToInt64(cod_Endereco);
            Localizacao = localizacao.ToLower().Equals("urbana") ? true : false; 
            Nome = nome;
            Categoria = categoria;
            IdLatitude = iD_LATITUDE;
            IdLongitude = iD_LONGITUDE;
            InstituicaoSemFimLucrativo = instituicao_Sem_Fim_Lucrativo;
        }

        public long CodEntidade { get; set; }
		public long CodEndereco { get; set; }
		public bool? Localizacao { get; set; }
		public string Nome { get; set; }
		public string Categoria { get; set; }
		public string IdLongitude { get; set; }
		public string IdLatitude { get; set; }
		public string InstituicaoSemFimLucrativo { get; set; }
		

		public virtual Endereco CodEnderecoNavigation { get; set; }
		public virtual MantenedoraDaEscola MantenedoraDaEscola { get; set; }
		public virtual ICollection<CensoEscola> CensoEscola { get; set; }
		public virtual ICollection<CorreioEletronico> CorreioEletronico { get; set; }
		public virtual ICollection<Telefone> Telefone { get; set; }
	}
}
