using System;
using System.Collections.Generic;
using System.IO;

namespace BulkLoad
{
    class Program
    {
        static void Main(string[] args)
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
            List<Censo_Escola> censoEscolas = new List<Censo_Escola>();
            List<CorreioEletronico> emails = new List<CorreioEletronico>();
            List<Telefone> telefones = new List<Telefone>();
            //mongo
            List<MongoData> mongo = new List<MongoData>();
            String[] headers;

            var count_endereço = 0;
            //passar por cada csv
            var path = @"C:\Users\Eduardo\Documents\ufsc\bdOPT\Populate\Populate\CADASTRO_MATRICULAS_REGIAO_NORTE_2012.csv";
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
                        var Cod_Regiao = regioes.Find(c => (c.Nome_Regiao == values[8].ToLower())).Cod_Regiao;
                        var Nome_Estado = values[10];
                        //var UF = vem do outro csv
                        //Console.WriteLine(Nome_Estado);
                        var atual = new Estado(Cod_Estado, Cod_Regiao, Nome_Estado);
                        estados.Add(atual);
                    }

                    //municipio
                    var Cod_Municipio = values[12];
                    if (!municipio_cod.Contains(Cod_Municipio))
                    {
                        municipio_cod.Add(Cod_Municipio);
                        var PK_COD_MUNICIPIO_OLD = values[13];
                        var Nome_Municipio = values[11];
                        //var UF = vem do outro csv
                        //Console.WriteLine(Nome_Municipio);
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


                    var atual4 = new Censo_Escola(ano, Cod_Entidade, iD_DEPENDENCIA_ADM,
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
                        var atual6 = new Telefone(Cod_Entidade, ano, numero1, dDD, "n");
                        telefones.Add(atual6);
                    }
                    if (numero2.Length > 1)
                    {
                        var atual6 = new Telefone(Cod_Entidade, ano, numero1, dDD, "n");
                        telefones.Add(atual6);
                    }
                    if (numeroFax.Length > 1)
                    {
                        var atual6 = new Telefone(Cod_Entidade, ano, numero1, dDD, "s");
                        telefones.Add(atual6);
                    }

                    //MONGO

                    var atual7 = new MongoData(Cod_Entidade, ano);
                    atual7.MATERIAL_ESP_NAO_UTILIZA = values[124];
                    atual7.MATERIAL_ESP_QUILOMBOLA = values[125];
                    atual7.MATERIAL_ESP_INDIGENA = values[126];
                    atual7.NUM_CNPJ_UNIDADE_EXECUTORA = values[47];
                    atual7.NUM_CNPJ_ESCOLA_PRIVADA = values[46];
                    atual7.CATESCPRIVADA = values[6];

                    for (var i=38;i<46;i++) {
                        if (values[i].ToLower().Equals("sim")) {
                            atual7.Dependencias.Add(headers[i]);
                        }
                    }
                    for (var i = 56; i < 80; i++)
                    {
                        if (values[i].ToLower().Equals("sim"))
                        {
                            atual7.Dependencias.Add(headers[i]);
                        }
                    }
                    for (var i = 82; i <= 86; i++)
                    {
                        if (values[i].ToLower().Equals("sim"))
                        {
                            atual7.Dependencias.Add(headers[i]);
                        }
                    }

                    //servicos
                    for (var i = 87; i <109; i++)
                    {
                        if (values[i].ToLower().Equals("sim"))
                        {
                            atual7.Servicos.Add(headers[i]);
                        }
                    }

                    //equips
                    atual7.Equipamentos.Add(new Equipamento("Computadores",values[110]));
                    for (var i = 113; i < 124; i++)
                    {
                        if (values[i].ToLower().Equals("sim"))
                        {
                            atual7.Equipamentos.Add(new Equipamento(headers[i],"1"));
                        }
                    }

                    if (values[135].Equals("1")) {
                        var tipo = "Educação Básica";
                        atual7.Matriculas.Add(new Matricula(tipo,"total",values[136]));
                    }
                    if (values[137].Equals("1"))
                    {
                        var tipo = "Educação Infantil";
                        atual7.Matriculas.Add(new Matricula(tipo, "total", values[138]));
                    }
                    if (values[139].Equals("1"))
                    {
                        var tipo = "CRECHE";
                        atual7.Matriculas.Add(new Matricula(tipo, "total", values[140]));
                    }
                    if (values[141].Equals("1"))
                    {
                        var tipo = "PRÉ-ESCOLA";
                        atual7.Matriculas.Add(new Matricula(tipo, "total", values[142]));
                    }
                    if (values[143].Equals("1"))
                    {
                        var tipo = "Ensino Fundamental - Total";
                        atual7.Matriculas.Add(new Matricula(tipo, "total", values[144]));
                        atual7.Matriculas.Add(new Matricula(tipo, "Series Iniciais", values[145]));
                        atual7.Matriculas.Add(new Matricula(tipo, "1ª Série", values[146]));
                        atual7.Matriculas.Add(new Matricula(tipo, "2ª Série", values[147]));
                        atual7.Matriculas.Add(new Matricula(tipo, "3ª Série", values[148]));
                        atual7.Matriculas.Add(new Matricula(tipo, "4ª Série", values[149]));
                        atual7.Matriculas.Add(new Matricula(tipo, "5ª Série", values[150]));
                        atual7.Matriculas.Add(new Matricula(tipo, "6ª Série", values[151]));
                        atual7.Matriculas.Add(new Matricula(tipo, "7ª Série", values[152]));
                        atual7.Matriculas.Add(new Matricula(tipo, "8ª Série", values[153]));
                    }
                    if (values[154].Equals("1"))
                    {
                        var tipo = "Ensino Fundamental";
                        atual7.Matriculas.Add(new Matricula(tipo, "1ª a 4ª Série e Anos Iniciais", values[155]));
                    }
                    if (values[156].Equals("1"))
                    {
                        var tipo = "Ensino Fundamental";
                        atual7.Matriculas.Add(new Matricula(tipo, "5ª a 8ª Série e Anos Finais", values[157]));
                    }
                    if (values[158].Equals("1"))
                    {
                        var tipo = "Ensino Fundamental - com 8 anos";
                        atual7.Matriculas.Add(new Matricula(tipo, "total", values[159]));
                        atual7.Matriculas.Add(new Matricula(tipo, "1ª Série", values[160]));
                        atual7.Matriculas.Add(new Matricula(tipo, "2ª Série", values[161]));
                        atual7.Matriculas.Add(new Matricula(tipo, "3ª Série", values[162]));
                        atual7.Matriculas.Add(new Matricula(tipo, "4ª Série", values[163]));
                        atual7.Matriculas.Add(new Matricula(tipo, "5ª Série", values[164]));
                        atual7.Matriculas.Add(new Matricula(tipo, "6ª Série", values[165]));
                        atual7.Matriculas.Add(new Matricula(tipo, "7ª Série", values[166]));
                        atual7.Matriculas.Add(new Matricula(tipo, "8ª Série", values[167]));
                    }
                    if (values[168].Equals("1"))
                    {
                        var tipo = "Ensino Fundamental - com 9 anos";
                        atual7.Matriculas.Add(new Matricula(tipo, "total", values[169]));
                        atual7.Matriculas.Add(new Matricula(tipo, "1ª Ano", values[170]));
                        atual7.Matriculas.Add(new Matricula(tipo, "2ª Ano", values[171]));
                        atual7.Matriculas.Add(new Matricula(tipo, "3ª Ano", values[172]));
                        atual7.Matriculas.Add(new Matricula(tipo, "4ª Ano", values[173]));
                        atual7.Matriculas.Add(new Matricula(tipo, "5ª Ano", values[174]));
                        atual7.Matriculas.Add(new Matricula(tipo, "6ª Ano", values[175]));
                        atual7.Matriculas.Add(new Matricula(tipo, "7ª Ano", values[176]));
                        atual7.Matriculas.Add(new Matricula(tipo, "8ª Ano", values[177]));
                        atual7.Matriculas.Add(new Matricula(tipo, "9ª Ano", values[178]));
                    }
                    if (values[179].Equals("1"))
                    {
                        var tipo = "Ensino Médio - Total";
                        atual7.Matriculas.Add(new Matricula(tipo, "total", values[180]));
                        atual7.Matriculas.Add(new Matricula(tipo, "1ª Série", values[181]));
                        atual7.Matriculas.Add(new Matricula(tipo, "2ª Série", values[182]));
                        atual7.Matriculas.Add(new Matricula(tipo, "3ª Série", values[183]));
                        atual7.Matriculas.Add(new Matricula(tipo, "4ª Série", values[184]));
                        atual7.Matriculas.Add(new Matricula(tipo, "Não Seriado", values[185]));
                    }
                    if (values[186].Equals("1"))
                    {
                        var tipo = "Ensino Médio - Regular";
                        atual7.Matriculas.Add(new Matricula(tipo, "total", values[187]));
                        atual7.Matriculas.Add(new Matricula(tipo, "1ª Série", values[188]));
                        atual7.Matriculas.Add(new Matricula(tipo, "2ª Série", values[189]));
                        atual7.Matriculas.Add(new Matricula(tipo, "3ª Série", values[190]));
                        atual7.Matriculas.Add(new Matricula(tipo, "4ª Série", values[191]));
                        atual7.Matriculas.Add(new Matricula(tipo, "Não Seriado", values[192]));
                    }
                    if (values[193].Equals("1"))
                    {
                        var tipo = "Ensino Médio - Integrado";
                        atual7.Matriculas.Add(new Matricula(tipo, "total", values[194]));
                        atual7.Matriculas.Add(new Matricula(tipo, "1ª Série", values[195]));
                        atual7.Matriculas.Add(new Matricula(tipo, "2ª Série", values[196]));
                        atual7.Matriculas.Add(new Matricula(tipo, "3ª Série", values[197]));
                        atual7.Matriculas.Add(new Matricula(tipo, "4ª Série", values[198]));
                        atual7.Matriculas.Add(new Matricula(tipo, "Não Seriado", values[199]));
                    }
                    if (values[200].Equals("1"))
                    {
                        var tipo = "Ensino Médio - Normal / Magistério";
                        atual7.Matriculas.Add(new Matricula(tipo, "total", values[201]));
                        atual7.Matriculas.Add(new Matricula(tipo, "1ª Série", values[202]));
                        atual7.Matriculas.Add(new Matricula(tipo, "2ª Série", values[203]));
                        atual7.Matriculas.Add(new Matricula(tipo, "3ª Série", values[204]));
                        atual7.Matriculas.Add(new Matricula(tipo, "4ª Série", values[205]));
                    }
                    var tipo1 = "Educação Profissional";
                    if (values[206].Equals("1"))
                    {
                        atual7.Matriculas.Add(new Matricula(tipo1, "Total", values[207]));
                    }
                    if (values[208].Equals("1"))
                    {
                        atual7.Matriculas.Add(new Matricula(tipo1, "Concomitante", values[209]));
                    }
                    if (values[210].Equals("1"))
                    {
                        atual7.Matriculas.Add(new Matricula(tipo1, "Concomitante", values[211]));
                    }
                    if (values[212].Equals("1"))
                    {
                        var tipo = "EJA";
                        atual7.Matriculas.Add(new Matricula(tipo, "Total", values[213]));
                    }
                    if (values[214].Equals("1"))
                    {
                        var tipo = "EJA";
                        atual7.Matriculas.Add(new Matricula(tipo, "Total - Ensino Fundamental", values[215]));
                    }
                    if (values[216].Equals("1"))
                    {
                        var tipo = "EJA";
                        atual7.Matriculas.Add(new Matricula(tipo, "Total - Ensino Médio", values[217]));
                    }
                    if (values[218].Equals("1"))
                    {
                        var tipo = "EJA";
                        atual7.Matriculas.Add(new Matricula(tipo, "Presencial", values[219]));
                    }

                    mongo.Add(atual7);
                }
            }

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

        static List<Regiao> dependencias()
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
    class Regiao
    {
        public String Cod_Regiao { get; set; }
        public String Nome_Regiao { get; set; }

        public Regiao(string cod_Regiao, string nome_Regiao)
        {
            Cod_Regiao = cod_Regiao;
            Nome_Regiao = nome_Regiao;
        }
    }

    class Estado
    {
        public String Cod_Estado { get; set; }
        public String Cod_Regiao { get; set; }
        public String Nome_Estado { get; set; }
        public String UF { get; set; }

        public Estado(string cod_Estado, string cod_Regiao, string nome_Estado, string uF)
        {
            Cod_Estado = cod_Estado;
            Cod_Regiao = cod_Regiao;
            Nome_Estado = nome_Estado;
            UF = uF;
        }

        public Estado(string cod_Estado, string cod_Regiao, string nome_Estado)
        {
            Cod_Estado = cod_Estado;
            Cod_Regiao = cod_Regiao;
            Nome_Estado = nome_Estado;
        }
    }
    class Municipio
    {
        public String Cod_Municipio { get; set; }
        public String Cod_Estado { get; set; }
        public String PK_COD_MUNICIPIO_OLD { get; set; }
        public String Nome_Municipio { get; set; }

        public Municipio(string cod_Municipio, string cod_Estado, string pK_COD_MUNICIPIO_OLD, string nome_Municipio)
        {
            Cod_Municipio = cod_Municipio;
            Cod_Estado = cod_Estado;
            PK_COD_MUNICIPIO_OLD = pK_COD_MUNICIPIO_OLD;
            Nome_Municipio = nome_Municipio;
        }
    }
    class Endereco
    {
        public String Cod_Endereco { get; set; }
        public String Cod_Municipio { get; set; }
        public String CEP { get; set; }
        public String Nome_Distrito { get; set; }
        public String Endereco1 { get; set; }
        public String Numero { get; set; }
        public String Complemento { get; set; }
        public String Bairro { get; set; }

        public Endereco(string cod_Endereco, string cod_Municipio, string cEP, string nome_Distrito, string endereco1, string numero, string complemento, string bairro)
        {
            Cod_Endereco = cod_Endereco;
            Cod_Municipio = cod_Municipio;
            CEP = cEP;
            Nome_Distrito = nome_Distrito;
            Endereco1 = endereco1;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
        }
    }

    class MantenedoraDaEscola
    {
        public String Cod_Entidade { get; set; }
        public String Empresa { get; set; }
        public String Sindicato { get; set; }
        public String Sistema_S { get; set; }
        public String Senai { get; set; }
        public String Sesc { get; set; }

        public MantenedoraDaEscola(String cod_Entidade, String empresa, String sindicato, String sistema_S, String senai, String sesc)
        {
            this.Cod_Entidade = cod_Entidade;
            Empresa = empresa;
            Sindicato = sindicato;
            Sistema_S = sistema_S;
            Senai = senai;
            Sesc = sesc;
        }
    }

    class Escola
    {
        public String Cod_Entidade { get; set; }
        public String Cod_Endereco { get; set; }
        public String Localizacao { get; set; }
        public String Nome { get; set; }
        public String Categoria { get; set; }
        public String ID_LATITUDE { get; set; }
        public String ID_LONGITUDE { get; set; }
        public String Instituicao_Sem_Fim_Lucrativo { get; set; }

        public Escola(string cod_Entidade, string cod_Endereco, string localizacao, string nome, string categoria, string iD_LATITUDE, string iD_LONGITUDE, string instituicao_Sem_Fim_Lucrativo)
        {
            Cod_Entidade = cod_Entidade;
            Cod_Endereco = cod_Endereco;
            Localizacao = localizacao;
            Nome = nome;
            Categoria = categoria;
            ID_LATITUDE = iD_LATITUDE;
            ID_LONGITUDE = iD_LONGITUDE;
            Instituicao_Sem_Fim_Lucrativo = instituicao_Sem_Fim_Lucrativo;
        }
    }

    class Censo_Escola
    {
        public String Ano { get; set; }
        public String Cod_Entidade { get; set; }
        public String ID_DEPENDENCIA_ADM { get; set; }
        public String Dependencia_Administrativa { get; set; }
        public String Rede { get; set; }
        public String DataInicioAnoLetivo { get; set; }
        public String DataFimAnoLetivo { get; set; }
        public String Situacao_Funcionamento { get; set; }
        public String EF_Organizado_Em_Ciclos { get; set; }
        public String Atividade_Complementar { get; set; }
        public String DOCUMENTO_REGULAMENTACAO { get; set; }
        public String ACESSIBILIDADE { get; set; }
        public String DEPENDENCIAS_PNE { get; set; }
        public String SANITARIO_PNE { get; set; }
        public String AEE { get; set; }
        public String NUM_SALAS_EXISTENTES { get; set; }
        public String NUM_SALAS_UTILIZADAS { get; set; }
        public String NUM_SALA_LEITURA { get; set; }
        public String NUM_FUNCIONARIOS { get; set; }
        public String EDUCACAO_INDIGENA { get; set; }
        public String LINGUA_INDIGENA { get; set; }
        public String LINGUA_PORTUGUESA { get; set; }
        public String ESPACO_TURMA_PBA { get; set; }
        public String ABRE_FINAL_SEMANA { get; set; }
        public String MOD_ENS_REGULAR { get; set; }
        public String MOD_EDUC_ESPECIAL { get; set; }
        public String MOD_EJA { get; set; }

        public Censo_Escola(string ano, string cod_Entidade, string iD_DEPENDENCIA_ADM, string dependencia_Administrativa, string rede, string dataInicioAnoLetivo, string dataFimAnoLetivo, string situacao_Funcionamento, string eF_Organizado_Em_Ciclos, string atividade_Complementar, string dOCUMENTO_REGULAMENTACAO, string aCESSIBILIDADE, string dEPENDENCIAS_PNE, string sANITARIO_PNE, string aEE, string nUM_SALAS_EXISTENTES, string nUM_SALAS_UTILIZADAS, string nUM_SALA_LEITURA, string nUM_FUNCIONARIOS, string eDUCACAO_INDIGENA, string lINGUA_INDIGENA, string lINGUA_PORTUGUESA, string eSPACO_TURMA_PBA, string aBRE_FINAL_SEMANA, string mOD_ENS_REGULAR, string mOD_EDUC_ESPECIAL, string mOD_EJA)
        {
            Ano = ano;
            Cod_Entidade = cod_Entidade;
            ID_DEPENDENCIA_ADM = iD_DEPENDENCIA_ADM;
            Dependencia_Administrativa = dependencia_Administrativa;
            Rede = rede;
            DataInicioAnoLetivo = dataInicioAnoLetivo;
            DataFimAnoLetivo = dataFimAnoLetivo;
            Situacao_Funcionamento = situacao_Funcionamento;
            EF_Organizado_Em_Ciclos = eF_Organizado_Em_Ciclos;
            Atividade_Complementar = atividade_Complementar;
            DOCUMENTO_REGULAMENTACAO = dOCUMENTO_REGULAMENTACAO;
            ACESSIBILIDADE = aCESSIBILIDADE;
            DEPENDENCIAS_PNE = dEPENDENCIAS_PNE;
            SANITARIO_PNE = sANITARIO_PNE;
            AEE = aEE;
            NUM_SALAS_EXISTENTES = nUM_SALAS_EXISTENTES;
            NUM_SALAS_UTILIZADAS = nUM_SALAS_UTILIZADAS;
            NUM_SALA_LEITURA = nUM_SALA_LEITURA;
            NUM_FUNCIONARIOS = nUM_FUNCIONARIOS;
            EDUCACAO_INDIGENA = eDUCACAO_INDIGENA;
            LINGUA_INDIGENA = lINGUA_INDIGENA;
            LINGUA_PORTUGUESA = lINGUA_PORTUGUESA;
            ESPACO_TURMA_PBA = eSPACO_TURMA_PBA;
            ABRE_FINAL_SEMANA = aBRE_FINAL_SEMANA;
            MOD_ENS_REGULAR = mOD_ENS_REGULAR;
            MOD_EDUC_ESPECIAL = mOD_EDUC_ESPECIAL;
            MOD_EJA = mOD_EJA;
        }
    }
    class CorreioEletronico
    {
        public String Cod_Entidade { get; set; }
        public String Ano { get; set; }
        public String Email { get; set; }

        public CorreioEletronico(string cod_Entidade, string ano, string email)
        {
            Cod_Entidade = cod_Entidade;
            Ano = ano;
            Email = email;
        }
    }
    class Telefone
    {
        public String Cod_Entidade { get; set; }
        public String Ano { get; set; }
        public String Numero { get; set; }
        public String DDD { get; set; }
        public String FAX { get; set; }

        public Telefone(string cod_Entidade, string ano, string numero, string dDD, string fAX)
        {
            Cod_Entidade = cod_Entidade;
            Ano = ano;
            Numero = numero;
            DDD = dDD;
            FAX = fAX;
        }
    }

    class MongoData
    {
        public String Cod_Entidade { get; set; }
        public String ano { get; set; }
        public String MATERIAL_ESP_NAO_UTILIZA { get; set; }
        public String MATERIAL_ESP_QUILOMBOLA { get; set; }
        public String MATERIAL_ESP_INDIGENA { get; set; }
        public String NUM_CNPJ_UNIDADE_EXECUTORA { get; set; }
        public String CATESCPRIVADA { get; set; }
        public String NUM_CNPJ_ESCOLA_PRIVADA { get; set; }
        public List<String> Dependencias { get; set; }
        public List<String> Servicos { get; set; }
        public List<Equipamento> Equipamentos { get; set; }
        public List<Matricula> Matriculas { get; set; }

        public MongoData(string cod_Entidade, string ano)
        {
            Cod_Entidade = cod_Entidade;
            this.ano = ano;
        }
    }

    class Equipamento
    {
        public String Nome_Equipamento { get; set; }
        public String Numero_De_Equip { get; set; }

        public Equipamento(string nome_Equipamento, string numero_De_Equip)
        {
            Nome_Equipamento = nome_Equipamento;
            Numero_De_Equip = numero_De_Equip;
        }
    }

    class Matricula
    {
        public String Tipo_Educacional { get; set; }
        public String Especificacao { get; set; }
        public String Numero_De_Matriculas { get; set; }

        public Matricula(string tipo_Educacional, string especificacao, string numero_De_Matriculas)
        {
            Tipo_Educacional = tipo_Educacional;
            Especificacao = especificacao;
            Numero_De_Matriculas = numero_De_Matriculas;
        }
    }



    }

    
