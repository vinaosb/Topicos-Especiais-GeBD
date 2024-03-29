﻿using System;
using System.Collections.Generic;
using System.IO;
using API.SQL.Models;
using SharedLibrary.Entities.Custom;
using SharedLibrary;

namespace BulkLoad
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            // completamente otimizado
            List<Regiao> regioes = regiao();
            
            List<String> estados_cod = new List<String>();
            List<Estado> estados = new List<Estado>();
            List<String> municipio_cod = new List<String>();
            List<Municipio> municipios = new List<Municipio>();
            List<Endereco> enderecos = new List<Endereco>();
            List<MantenedoraDaEscola> mantenedoras = new List<MantenedoraDaEscola>();
            List<Escola> escolas = new List<Escola>();
            List<CensoEscola> censoEscolas = new List<CensoEscola>();
            List<CorreioEletronico> emails = new List<CorreioEletronico>();
            List<Telefone> telefones = new List<Telefone>();
            //mongo
            List<ExtrasDaEscola> mongo = new List<ExtrasDaEscola>();
            String[] headers;

            var count_endereço = 0;
            //passar por cada csv
            var path = @"C:\Users\Vinicius\Downloads\CADASTRO_MATRICULAS_REGIAO_NORTE_2012.csv";
            using (var reader = new StreamReader(path))
            {
                for (int o = 0; o < 11; o++)
                {
                    var line = reader.ReadLine();
                }
                var lin = reader.ReadLine();
                headers = lin.Split(';');
                
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    var ano = values[0];
                    if (ano.Equals(""))
                        break;
                    var Cod_Entidade = values[1];

                    //estado
                    var Cod_Estado = values[9];
                    if (!estados_cod.Contains(Cod_Estado))
                    {
                        estados_cod.Add(Cod_Estado);
                        //recomendo fazer consulta
                        var Cod_Regiao = regioes.Find(c => (c.NomeRegiao == values[8].ToLower())).CodRegiao;
                        var Nome_Estado = values[10];
                        var UF = "GG";
                        switch (Nome_Estado)
                        {
                            case "Rondonia":
                                UF = "RO";
                                break;
                            case "Acre":
                                UF = "AC";
                                break;
                            case "Amazonas":
                                UF = "AM";
                                break;
                            case "Roraima":
                                UF = "RR";
                                break;
                            case "Para":
                                UF = "PA";
                                break;
                            case "Amapa":
                                UF = "AP";
                                break;
                            case "Tocantins":
                                UF = "TO";
                                break;

                        }

                        var atual = new Estado(Cod_Estado, Cod_Regiao, Nome_Estado);
						atual.Uf = UF;
                        estados.Add(atual);
                    }

                    //municipio
                    var Cod_Municipio = values[12];
                    if (!municipio_cod.Contains(Cod_Municipio))
                    {
                        municipio_cod.Add(Cod_Municipio);
                        var PK_COD_MUNICIPIO_OLD = values[13];
                        var Nome_Municipio = values[11];
                        var atual = new Municipio(Cod_Municipio, Cod_Estado, PK_COD_MUNICIPIO_OLD, Nome_Municipio);
                        municipios.Add(atual);
                    }

                    //endereço
                    var Cod_Endereco = "" + count_endereço++;
                    var cEP = values[19];
                    var nome_Distrito = values[14];
                    var endereco1 = values[15];
                    var numero = values[16];
                    var complemento = values[17];
                    var bairro = values[18];
                    var atual1 = new Endereco(Cod_Endereco, Cod_Municipio, cEP, nome_Distrito, endereco1, numero, complemento, bairro);
                    enderecos.Add(atual1);

                    //mantenedoras
                    var empresa = values[31];
                    var sindicato = values[32];
                    var sistema_S = values[33];
                    var senai = values[34];
                    var sesc = values[35];

                    var atual2 = new MantenedoraDaEscola(Cod_Entidade, empresa, sindicato, sistema_S, senai, sesc);
                    mantenedoras.Add(atual2);

                    //escola
                    var localizacao = values[7];
                    var nome_escola = values[2];
                    var categoria = values[6];
                    var iD_LATITUDE = values[26];
                    var iD_LONGITUDE = values[27];
                    var instituicao_Sem_Fim_Lucrativo = values[30];
                    var atual3 = new Escola(Cod_Entidade, Cod_Endereco, localizacao, nome_escola, categoria, iD_LATITUDE, iD_LONGITUDE, instituicao_Sem_Fim_Lucrativo);
                    escolas.Add(atual3);

                    //censo escola
                    var iD_DEPENDENCIA_ADM = values[4];
                    var dependencia_Administrativa = values[5];
                    var rede = values[3];
                    var dataInicioAnoLetivo = values[28];
                    var dataFimAnoLetivo = values[29];
                    var situacao_Funcionamento = values[25];
                    var eF_Organizado_Em_Ciclos = values[37];
                    var atividade_Complementar = values[36];
                    var dOCUMENTO_REGULAMENTACAO = values[48];
                    var aCESSIBILIDADE = values[52];
                    var dEPENDENCIAS_PNE = values[53];
                    var sANITARIO_PNE = values[54];
                    var aEE = values[55];
                    var nUM_SALAS_EXISTENTES = values[80];
                    var nUM_SALAS_UTILIZADAS = values[81];
                    var nUM_SALA_LEITURA = values[82];// isso aqui nao é num é bool
                    var nUM_FUNCIONARIOS = values[86];
                    var eDUCACAO_INDIGENA = values[127];
                    var lINGUA_INDIGENA = values[128];
                    var lINGUA_PORTUGUESA = values[129];
                    var eSPACO_TURMA_PBA = values[130];
                    var aBRE_FINAL_SEMANA = values[131];
                    var mOD_ENS_REGULAR = values[132];
                    var mOD_EDUC_ESPECIAL = values[133];
                    var mOD_EJA = values[134];


                    var atual4 = new CensoEscola(ano, Cod_Entidade, iD_DEPENDENCIA_ADM,
                        dependencia_Administrativa, rede, dataInicioAnoLetivo, dataFimAnoLetivo,
                        situacao_Funcionamento, eF_Organizado_Em_Ciclos, atividade_Complementar,
                        dOCUMENTO_REGULAMENTACAO, aCESSIBILIDADE, dEPENDENCIAS_PNE, sANITARIO_PNE,
                        aEE, nUM_SALAS_EXISTENTES, nUM_SALAS_UTILIZADAS, nUM_SALA_LEITURA, nUM_FUNCIONARIOS,
                        eDUCACAO_INDIGENA, lINGUA_INDIGENA, lINGUA_PORTUGUESA, eSPACO_TURMA_PBA, aBRE_FINAL_SEMANA,
                        mOD_ENS_REGULAR, mOD_EDUC_ESPECIAL, mOD_EJA);
                    censoEscolas.Add(atual4);


                    //CorreioEletronico
                    var email = values[24];

                    var atual5 = new CorreioEletronico(Cod_Entidade, ano, email);
                    emails.Add(atual5);

                    // telefone
                    var dDD = values[20];
                    var numero1 = values[21];
                    var numero2 = values[22];
                    var numeroFax = values[23];

                    if (numero1.Length > 1)
                    {
                        var atual6 = new Telefone(Cod_Entidade, ano, numero1, dDD, false);
                        telefones.Add(atual6);
                    }
                    if (numero2.Length > 1)
                    {
                        var atual6 = new Telefone(Cod_Entidade, ano, numero1, dDD, false);
                        telefones.Add(atual6);
                    }
                    if (numeroFax.Length > 1)
                    {
                        var atual6 = new Telefone(Cod_Entidade, ano, numero1, dDD, true);
                        telefones.Add(atual6);
                    }

                    //MONGO

                    //var atual7 = new MongoData(Cod_Entidade, ano);
                    var atual7 = new ExtrasDaEscola();
                    atual7.ID.Ano = System.Convert.ToInt16(ano);
                    atual7.ID.Cod_Entidade = System.Convert.ToInt64(Cod_Entidade);

                    atual7.MateriaisEspecificos.MaterialEspecificoNaoUtiliza = values[124].ToLower().Equals("sim") ? true : false; 
                    atual7.MateriaisEspecificos.MaterialEspecificoQuilombola = values[125].ToLower().Equals("sim") ? true : false; 
                    atual7.MateriaisEspecificos.MaterialEspecificoIndigena = values[126].ToLower().Equals("sim") ? true : false;

					if(values[47] != "")
						atual7.NumCNPJUnidadeExecutora = System.Convert.ToInt64(values[47]);

					if (values[46] != "")
					{
						atual7.EscolaPrivada.NumCNPJEscolaPrivada = System.Convert.ToInt64(values[46]);
	                    atual7.EscolaPrivada.EscolaEFilantropica = values[6].ToLower().Equals("particular") ? true: false;
					}

                    for (var i=38;i<46;i++) {
                        if (values[i].ToLower().Equals("sim")) {
                            atual7.DependenciasDaEscola.Add(headers[i]);
                        }
                    }
                    for (var i = 56; i < 80; i++)
                    {
                        if (values[i].ToLower().Equals("sim"))
                        {
                            atual7.DependenciasDaEscola.Add(headers[i]);
                        }
                    }
                    for (var i = 82; i <= 86; i++)
                    {
                        if (values[i].ToLower().Equals("sim"))
                        {
                            atual7.DependenciasDaEscola.Add(headers[i]);
                        }
                    }

                    //servicos
                    for (var i = 87; i <109; i++)
                    {
                        if (values[i].ToLower().Equals("sim"))
                        {
                            atual7.ServicosDaEscola.Add(headers[i]);
                        }
                    }

                    //equips
                    atual7.Equipamentos.Add(new ExtrasDaEscola.EquipamentosDaEscola("Computadores",values[110]));
                    for (var i = 113; i < 124; i++)
                    {
                        if (values[i].ToLower().Equals("sim"))
                        {
                            atual7.Equipamentos.Add(new ExtrasDaEscola.EquipamentosDaEscola(headers[i],"1"));
                        }
                    }

                    if (values[135].Equals("1")) {
                        var tipo = "Educação Básica";
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo,"total",values[136]));
                    }
                    if (values[137].Equals("1"))
                    {
                        var tipo = "Educação Infantil";
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "total", values[138]));
                    }
                    if (values[139].Equals("1"))
                    {
                        var tipo = "CRECHE";
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "total", values[140]));
                    }
                    if (values[141].Equals("1"))
                    {
                        var tipo = "PRÉ-ESCOLA";
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "total", values[142]));
                    }
                    if (values[143].Equals("1"))
                    {
                        var tipo = "Ensino Fundamental - Total";
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "total", values[144]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "Series Iniciais", values[145]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "1ª Série", values[146]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "2ª Série", values[147]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "3ª Série", values[148]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "4ª Série", values[149]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "5ª Série", values[150]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "6ª Série", values[151]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "7ª Série", values[152]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "8ª Série", values[153]));
                    }
                    if (values[154].Equals("1"))
                    {
                        var tipo = "Ensino Fundamental";
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "1ª a 4ª Série e Anos Iniciais", values[155]));
                    }
                    if (values[156].Equals("1"))
                    {
                        var tipo = "Ensino Fundamental";
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "5ª a 8ª Série e Anos Finais", values[157]));
                    }
                    if (values[158].Equals("1"))
                    {
                        var tipo = "Ensino Fundamental - com 8 anos";
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "total", values[159]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "1ª Série", values[160]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "2ª Série", values[161]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "3ª Série", values[162]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "4ª Série", values[163]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "5ª Série", values[164]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "6ª Série", values[165]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "7ª Série", values[166]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "8ª Série", values[167]));
                    }
                    if (values[168].Equals("1"))
                    {
                        var tipo = "Ensino Fundamental - com 9 anos";
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "total", values[169]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "1ª Ano", values[170]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "2ª Ano", values[171]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "3ª Ano", values[172]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "4ª Ano", values[173]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "5ª Ano", values[174]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "6ª Ano", values[175]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "7ª Ano", values[176]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "8ª Ano", values[177]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "9ª Ano", values[178]));
                    }
                    if (values[179].Equals("1"))
                    {
                        var tipo = "Ensino Médio - Total";
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "total", values[180]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "1ª Série", values[181]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "2ª Série", values[182]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "3ª Série", values[183]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "4ª Série", values[184]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "Não Seriado", values[185]));
                    }
                    if (values[186].Equals("1"))
                    {
                        var tipo = "Ensino Médio - Regular";
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "total", values[187]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "1ª Série", values[188]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "2ª Série", values[189]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "3ª Série", values[190]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "4ª Série", values[191]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "Não Seriado", values[192]));
                    }
                    if (values[193].Equals("1"))
                    {
                        var tipo = "Ensino Médio - Integrado";
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "total", values[194]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "1ª Série", values[195]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "2ª Série", values[196]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "3ª Série", values[197]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "4ª Série", values[198]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "Não Seriado", values[199]));
                    }
                    if (values[200].Equals("1"))
                    {
                        var tipo = "Ensino Médio - Normal / Magistério";
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "total", values[201]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "1ª Série", values[202]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "2ª Série", values[203]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "3ª Série", values[204]));
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "4ª Série", values[205]));
                    }
                    var tipo1 = "Educação Profissional";
                    if (values[206].Equals("1"))
                    {
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo1, "Total", values[207]));
                    }
                    if (values[208].Equals("1"))
                    {
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo1, "Concomitante", values[209]));
                    }
                    if (values[210].Equals("1"))
                    {
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo1, "Concomitante", values[211]));
                    }
                    if (values[212].Equals("1"))
                    {
                        var tipo = "EJA";
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "Total", values[213]));
                    }
                    if (values[214].Equals("1"))
                    {
                        var tipo = "EJA";
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "Total - Ensino Fundamental", values[215]));
                    }
                    if (values[216].Equals("1"))
                    {
                        var tipo = "EJA";
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "Total - Ensino Médio", values[217]));
                    }
                    if (values[218].Equals("1"))
                    {
                        var tipo = "EJA";
                        atual7.Matriculas.Add(new ExtrasDaEscola.Numero_Matriculas(tipo, "Presencial", values[219]));
                    }

                    mongo.Add(atual7);
                }
            }

			Console.WriteLine("Sender");

			
			using (Sender<Regiao> sender = new Sender<Regiao>("https://localhost:44309/")){
                foreach(Regiao atual in regioes){
					_ = await sender.Post(atual,"api/Regioes");
                }
			}
			Console.WriteLine("Regiao");
			
			using (var sender = new Sender<Estado>("https://localhost:44309/")){
                foreach(Estado atual in estados){
					_ = await sender.Post(atual, "api/Estados");
                }
			}
			Console.WriteLine("Estados");
			
			using (var sender = new Sender<Municipio>("https://localhost:44309/")){
                foreach(Municipio atual in municipios){
					_ = await sender.Post(atual, "api/Municipios");
                }
			}
			Console.WriteLine("Muni");

			using (var sender = new Sender<List<Endereco>>("https://localhost:44309/")){

				_ = await sender.Post(enderecos, "api/Enderecos/bulk");
			}
			Console.WriteLine("Ende");
			
			using (var sender = new Sender<List<Escola>>("https://localhost:44309/"))
			{
				_ = await sender.Post(escolas, "api/Escolas/bulk");
			}
			Console.WriteLine("Escolas");

			using (var sender = new Sender<List<MantenedoraDaEscola>>("https://localhost:44309/"))
			{
				_ = await sender.Post(mantenedoras, "api/MantenedorasDasEscolas/bulk");
			}
			Console.WriteLine("Mant");

			using (var sender = new Sender<List<CorreioEletronico>>("https://localhost:44309/"))
			{
				_ = await sender.Post(emails, "api/CorreioEletronico/bulk");
			}
			Console.WriteLine("Email");
			
			using (var sender = new Sender<List<Telefone>>("https://localhost:44309/"))
			{
				_ = await sender.Post(telefones, "api/Telefones/bulk");
			}
			Console.WriteLine("Tel");
			
			using (var sender = new Sender<List<CensoEscola>>("https://localhost:44309/"))
			{
				_ = await sender.Post(censoEscolas, "api/CensoEscolas/bulk");
			}
			Console.WriteLine("Censo");
			
			using (var sender = new Sender<List<ExtrasDaEscola>>("https://localhost:44390/"))
			{
				int last = 0;
				for (int i = 0; i < mongo.Count/1000; i++)
				{
					_ = await sender.Post(mongo.GetRange(1000*i,1000), "api/Mongo/bulk");
					last = i*1000;
				}
				_ = await sender.Post(mongo.GetRange(last, mongo.Count-last), "api/Mongo/bulk");
			}
			Console.WriteLine("Mongo");
			

			path = @"C:\Users\Eduardo\Documents\ufsc\bdOPT\Populate\Populate\escolas_media_alunos_turma_2010.xls";
            //Application excel = new Application();
            //Workbook wb = excel.Workbooks.Open(path);
        }

        static List<Regiao> regiao()
        {
            List<Regiao> regioes = new List<Regiao>();
            regioes.Add(new Regiao("1", "norte"));
            regioes.Add(new Regiao("2", "nordeste"));
            regioes.Add(new Regiao("3", "sudeste"));
            regioes.Add(new Regiao("4", "sul"));
            regioes.Add(new Regiao("5", "centro-oeste"));
            return regioes;
        }

        



    }

    //SQL

    
    

    
    
    

   

    


    }

    
