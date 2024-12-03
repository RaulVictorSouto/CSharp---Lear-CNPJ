#region using
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using AAA.Atributos.Imagem;
using AAA.Atributos.GED;
using AAA.Framework.Imagem;
using AAA.Framework.Util;
using System.Web.Script.Serialization;
using AAA.Atributos.GED.TipoDocumento;
using AAA.Framework.Log;
using System.Net.Http;
#endregion

public class ClassePrincipal // 3A-Não alterar
{
    public string Main(AAAARegistro pRegistro, AAAATipoCampo pCampo) // 3A-Não alterar
    {
        string lRetorno = "";

        try
        {
            string lCnpj = pCampo.Valor;

            if (string.IsNullOrEmpty(lCnpj))
            {
                lRetorno = "Por gentileza, insira um CNPJ válido.";
                return lRetorno;
            }

            string lUrl = string.Format("https://www.receitaws.com.br/v1/cnpj/{0}", lCnpj);

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resposta = client.GetAsync(lUrl).Result;

                if (resposta.IsSuccessStatusCode)
                {
                    string json = resposta.Content.ReadAsStringAsync().Result;
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    Dictionary<string, object> lCnpjDic = serializer.Deserialize<Dictionary<string, object>>(json);

                    // Qsa
                    var qsaItems = serializer.Deserialize<List<QsaItem>>(serializer.Serialize(lCnpjDic["qsa"]));
                    StringBuilder textoQsa = new StringBuilder();
                    foreach (var qsaItem in qsaItems)
                    {
                        textoQsa.AppendLine(string.Format("{0}: {1}", qsaItem.nome, qsaItem.qual));
                    }

                    // Atividades principais
                    var atividadePrincipalItems = serializer.Deserialize<List<AtividadeItem>>(serializer.Serialize(lCnpjDic["atividade_principal"]));
                    StringBuilder textoAtividadePrincipal = new StringBuilder();
                    foreach (var atividadeItem in atividadePrincipalItems)
                    {
                        textoAtividadePrincipal.AppendLine(string.Format("{0}: {1}", atividadeItem.code, atividadeItem.text));
                    }

                    // Atividades secundárias
                    var atividadesSecundariasItems = serializer.Deserialize<List<AtividadeItem>>(serializer.Serialize(lCnpjDic["atividades_secundarias"]));
                    StringBuilder textoAtividadesSecundarias = new StringBuilder();
                    foreach (var atividadeItem in atividadesSecundariasItems)
                    {
                        textoAtividadesSecundarias.AppendLine(string.Format("{0}: {1}", atividadeItem.code, atividadeItem.text));
                    }

                    // Itera sobre os campos do registro e atribui valores
                    foreach (AAAATipoCampo lTipoCampo in pRegistro.TipoDocumento.TipoCampos)
                    {
                        switch (lTipoCampo.Titulo.Trim().Replace("\"", "").ToUpper())
                        {
                            case "ABERTURA":
                                lTipoCampo.Valor = lCnpjDic["abertura"].ToString();
                                break;
                            case "SITUACAO":
                                lTipoCampo.Valor = lCnpjDic["situacao"].ToString();
                                break;
                            case "TIPO":
                                lTipoCampo.Valor = lCnpjDic["tipo"].ToString();
                                break;
                            case "NOME":
                                lTipoCampo.Valor = lCnpjDic["nome"].ToString();
                                break;
                            case "PORTE":
                                lTipoCampo.Valor = lCnpjDic["porte"].ToString();
                                break;
                            case "NATUREZA_JURIDICA":
                                lTipoCampo.Valor = lCnpjDic["natureza_juridica"].ToString();
                                break;
                            case "LOGRADOURO":
                                lTipoCampo.Valor = lCnpjDic["logradouro"].ToString();
                                break;
                            case "NUMERO":
                                lTipoCampo.Valor = lCnpjDic["numero"].ToString();
                                break;
                            case "COMPLEMENTO":
                                lTipoCampo.Valor = lCnpjDic["complemento"].ToString();
                                break;
                            case "MUNICIPIO":
                                lTipoCampo.Valor = lCnpjDic["municipio"].ToString();
                                break;
                            case "BAIRRO":
                                lTipoCampo.Valor = lCnpjDic["bairro"].ToString();
                                break;
                            case "UF":
                                lTipoCampo.Valor = lCnpjDic["uf"].ToString();
                                break;
                            case "CEP":
                                lTipoCampo.Valor = lCnpjDic["cep"].ToString();
                                break;
                            case "EMAIL":
                                lTipoCampo.Valor = lCnpjDic["email"].ToString();
                                break;
                            case "TELEFONE":
                                lTipoCampo.Valor = lCnpjDic["telefone"].ToString();
                                break;
                            case "DATA_SITUACAO":
                                lTipoCampo.Valor = lCnpjDic["data_situacao"].ToString();
                                break;
                            case "ULTIMA_ATUALIZACAO":
                                lTipoCampo.Valor = lCnpjDic["ultima_atualizacao"].ToString();
                                break;
                            case "STATUS":
                                lTipoCampo.Valor = lCnpjDic["status"].ToString();
                                break;
                            case "FANTASIA":
                                lTipoCampo.Valor = lCnpjDic["fantasia"].ToString();
                                break;
                            case "EFR":
                                lTipoCampo.Valor = lCnpjDic["efr"].ToString();
                                break;
                            case "MOTIVO_SITUACAO":
                                lTipoCampo.Valor = lCnpjDic["motivo_situacao"].ToString();
                                break;
                            case "SITUACAO_ESPECIAL":
                                lTipoCampo.Valor = lCnpjDic["situacao_especial"].ToString();
                                break;
                            case "DATA_SITUACAO_ESPECIAL":
                                lTipoCampo.Valor = lCnpjDic["data_situacao_especial"].ToString();
                                break;
                            case "CAPITAL_SOCIAL":
                                lTipoCampo.Valor = lCnpjDic["capital_social"].ToString();
                                break;
                            case "QSA":
                                lTipoCampo.Valor = textoQsa.ToString();
                                break;
                            case "ATIVIDADE_PRINCIPAL":
                                lTipoCampo.Valor = textoAtividadePrincipal.ToString();
                                break;
                            case "ATIVIDADES_SECUNDARIAS":
                                lTipoCampo.Valor = textoAtividadesSecundarias.ToString();
                                break;
              
                            // Inclua outros casos conforme necessário
                        }
                    }
                }
                else
                {
                    throw new Exception("Falha ao obter os dados da URL: " + lUrl);
                }
            }
        }
        catch (Exception pErro)
        {
            lRetorno = pErro.Message;
        }

        return lRetorno;
    }

    public class QsaItem
    {
        public string nome { get; set; }
        public string qual { get; set; }
    }

    public class AtividadeItem
    {
        public string code { get; set; }
        public string text { get; set; }
    }
}



