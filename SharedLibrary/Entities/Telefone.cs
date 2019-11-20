using System.Numerics;

namespace API.SQL.Models
{
	public partial class Telefone
	{
		public long Numero { get; set; }
		public long CodEntidade { get; set; }
		public short Ano { get; set; }
		public short? Ddd { get; set; }
		public bool? Fax { get; set; }

        public Telefone(string cod_Entidade, string ano, string numero, string dDD, bool fAX)
        {
            CodEntidade = System.Convert.ToInt64(cod_Entidade);
            Ano = System.Convert.ToInt16(ano);
            Numero = System.Convert.ToInt64(numero);
            Ddd = System.Convert.ToInt16(dDD);
            Fax = fAX;
        }

        public virtual Escola CodEntidadeNavigation { get; set; }
	}
}
