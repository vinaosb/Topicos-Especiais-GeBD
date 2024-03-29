﻿using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Numerics;

namespace SharedLibrary.Entities.Custom
{
	public class ExtrasDaEscola
	{
		public class Tipo_Educacional
		{
			public int Cod_TE { get; set; }
			public string TipoEducacional { get; set; }

			public Tipo_Educacional()
			{

			}
		}
		public class Numero_Matriculas
		{
            public string TipoEducacional { get; set; }
            public string Especificacao { get; set; }
			public int Numero_De_Matriculas { get; set; }

			public Numero_Matriculas()
			{

			}

            public Numero_Matriculas(string tipo_Educacional, string especificacao, string numero_De_Matriculas)
            {
                TipoEducacional = tipo_Educacional;
                Especificacao = especificacao;
                Numero_De_Matriculas = System.Convert.ToInt32(numero_De_Matriculas);
            }
        }
		public class MediaPorAlunoPorEscola
		{
			public Tipo_Educacional Cod_TE { get; set; }
			public int Serie { get; set; }
			public double Media { get; set; }

			public MediaPorAlunoPorEscola()
			{

			}
		}
		public class EquipamentosDaEscola
		{
			public string Nome_Equip { get; set; }
			public short Numero_De_Equip { get; set; }
            public EquipamentosDaEscola(string nome_Equipamento, string numero_De_Equip)
            {
                Nome_Equip = nome_Equipamento;
                Numero_De_Equip = System.Convert.ToInt16(numero_De_Equip);
            }
			public EquipamentosDaEscola()
			{

			}
        }
		public class MateriaisDidaticosEspecificos
		{
			public bool MaterialEspecificoNaoUtiliza { get; set; }
			public bool MaterialEspecificoQuilombola { get; set; }
			public bool MaterialEspecificoIndigena { get; set; }
			public MateriaisDidaticosEspecificos()
			{

			}
		}
		public class EspecificacaoEscolaPrivada
		{
			public bool EscolaEFilantropica { get; set; }
			public long NumCNPJEscolaPrivada { get; set; }
			public EspecificacaoEscolaPrivada()
			{

			}
		}
		public class Indexer
		{
			public long Cod_Entidade { get; set; }
			public short Ano { get; set; }
			public Indexer()
			{

			}
		}

		[BsonId]
		public Indexer ID { get; set; }
		public List<Numero_Matriculas> Matriculas { get; set; }
		public List<MediaPorAlunoPorEscola> Medias { get; set; }
		public List<EquipamentosDaEscola> Equipamentos { get; set; }
		public MateriaisDidaticosEspecificos MateriaisEspecificos { get; set; }
		public List<string> DependenciasDaEscola { get; set; }
		public long NumCNPJUnidadeExecutora { get; set; }
		public List<string> ServicosDaEscola { get; set; }
		public EspecificacaoEscolaPrivada EscolaPrivada { get; set; }

        public ExtrasDaEscola()
        {
            Matriculas = new List<Numero_Matriculas>();
            Medias = new List<MediaPorAlunoPorEscola>();
            Equipamentos = new List<EquipamentosDaEscola>();
            DependenciasDaEscola = new List<string>();
            ServicosDaEscola = new List<string>();
			MateriaisEspecificos = new ExtrasDaEscola.MateriaisDidaticosEspecificos();
			ID = new ExtrasDaEscola.Indexer();
			EscolaPrivada = new EspecificacaoEscolaPrivada();
		}
    }
}
