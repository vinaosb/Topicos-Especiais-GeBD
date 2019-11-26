using System.Numerics;

namespace API.SQL.Models
{
	public partial class CorreioEletronico
	{
		public long CodEntidade { get; set; }
		public short Ano { get; set; }
		public string Email { get; set; }

		public CorreioEletronico()
		{

		}

        public CorreioEletronico(string cod_Entidade, string ano, string email)
        {
            CodEntidade = System.Convert.ToInt64(cod_Entidade);
            Ano = System.Convert.ToInt16(ano);
            Email = email;
        }

        public virtual Escola CodEntidadeNavigation { get; set; }
	}
}
