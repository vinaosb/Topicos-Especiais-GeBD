using System;
using System.Numerics;

namespace API.SQL.Models
{
	public partial class CensoEscola
	{
		public short? IdDependenciaAdm { get; set; }
		public string DependenciaAdministrativa { get; set; }
		public string Rede { get; set; }
		public DateTime? DataInicioAnoLetivo { get; set; }
		public DateTime? DataFimAnoLetivo { get; set; }
		public string SituacaoFuncionamento { get; set; }
		public bool? EfOrganizadoEmCiclos { get; set; }
		public string AtividadeComplementar { get; set; }
		public string DocumentoRegulamentacao { get; set; }
		public bool? Acessibilidade { get; set; }
		public string DependenciasPne { get; set; }
		public string SanitariosPne { get; set; }
		public string Aee { get; set; }
		public long? NumSalasExistentes { get; set; }
		public long? NumSalasUsadas { get; set; }
		public long? NumSalasLeitura { get; set; }
		public long? NumFuncionarios { get; set; }
		public bool? EducacaoIndigena { get; set; }
		public bool? LinguaIndigena { get; set; }
		public bool? LinguaPortuguesa { get; set; }
		public bool? EspacoTurmaPba { get; set; }
		public bool? AbreFinalSemana { get; set; }
		public bool? ModEnsRegular { get; set; }
		public bool? ModEducEspecial { get; set; }
		public bool? ModEja { get; set; }
		public short Ano { get; set; }
		public long CodEntidade { get; set; }

		public CensoEscola()
		{
		}

        public CensoEscola(string ano, string cod_Entidade, string iD_DEPENDENCIA_ADM, string dependencia_Administrativa, string rede, string dataInicioAnoLetivo, string dataFimAnoLetivo, string situacao_Funcionamento, string eF_Organizado_Em_Ciclos, string atividade_Complementar, string dOCUMENTO_REGULAMENTACAO, string aCESSIBILIDADE, string dEPENDENCIAS_PNE, string sANITARIO_PNE, string aEE, string nUM_SALAS_EXISTENTES, string nUM_SALAS_UTILIZADAS, string nUM_SALA_LEITURA, string nUM_FUNCIONARIOS, string eDUCACAO_INDIGENA, string lINGUA_INDIGENA, string lINGUA_PORTUGUESA, string eSPACO_TURMA_PBA, string aBRE_FINAL_SEMANA, string mOD_ENS_REGULAR, string mOD_EDUC_ESPECIAL, string mOD_EJA)
        {
            Ano = System.Convert.ToInt16(ano);
            CodEntidade = System.Convert.ToInt64(cod_Entidade);
            IdDependenciaAdm = System.Convert.ToInt16(iD_DEPENDENCIA_ADM);
            DependenciaAdministrativa = dependencia_Administrativa;
            Rede = rede;
            if (!dataInicioAnoLetivo.Equals(""))
            {
                DataInicioAnoLetivo = System.Convert.ToDateTime(dataInicioAnoLetivo);
            }
            if (!dataFimAnoLetivo.Equals(""))
            {
                DataFimAnoLetivo = System.Convert.ToDateTime(dataFimAnoLetivo);
            }

            SituacaoFuncionamento = situacao_Funcionamento;
            EfOrganizadoEmCiclos = eF_Organizado_Em_Ciclos.ToLower().Equals("sim") ? true : false;

            AtividadeComplementar = atividade_Complementar;
            DocumentoRegulamentacao = dOCUMENTO_REGULAMENTACAO;
            Acessibilidade = aCESSIBILIDADE.ToLower().Equals("sim") ? true : false;
            DependenciasPne = dEPENDENCIAS_PNE;
            SanitariosPne = sANITARIO_PNE;
            Aee = aEE;

			if (nUM_SALAS_EXISTENTES != "")
				NumSalasExistentes = System.Convert.ToInt64(nUM_SALAS_EXISTENTES);
			if(nUM_SALAS_UTILIZADAS != "")
				NumSalasUsadas = System.Convert.ToInt64(nUM_SALAS_UTILIZADAS);
            NumSalasLeitura = nUM_SALA_LEITURA.ToLower().Equals("sim") ? 1 : 0; ;
			if(nUM_FUNCIONARIOS != "")
				NumFuncionarios = System.Convert.ToInt64(nUM_FUNCIONARIOS);

            EducacaoIndigena = eDUCACAO_INDIGENA.ToLower().Equals("sim") ? true : false; 
            LinguaIndigena = lINGUA_INDIGENA.ToLower().Equals("sim") ? true : false;
            LinguaPortuguesa = lINGUA_PORTUGUESA.ToLower().Equals("sim") ? true : false;
            EspacoTurmaPba = eSPACO_TURMA_PBA.ToLower().Equals("sim") ? true : false;
            AbreFinalSemana = aBRE_FINAL_SEMANA.ToLower().Equals("sim") ? true : false;

            ModEnsRegular = mOD_ENS_REGULAR.ToLower().Equals("s") ? true : false;
            ModEducEspecial = mOD_EDUC_ESPECIAL.ToLower().Equals("s") ? true : false;
            ModEja = mOD_EJA.ToLower().Equals("s") ? true : false;
        }
        public virtual Escola CodEntidadeNavigation { get; set; }
	}
}
