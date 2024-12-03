#region using
using System;
using System.Text;
using System.Collections.Generic;
using AAA.Atributos.Imagem;
using AAA.Atributos.GED;
using AAA.Controle.GED;
using AAA.Framework.Imagem;
using AAA.Framework.Util;
using AAA.Atributos.Banco;
using AAA.Framework.Banco;
using AAA.Framework.Aplicacao;
using AAA.Atributos.Exportacao;
using AAA.Atributos.GED.TipoDocumento;
using AAA.Framework.Log;
using System.Web.Script.Serialization;
using System.Net.Http;
using System.Threading.Tasks;
using AAA.Framework.Seguranca;
using System.Net;
#endregion


public class ClassePrincipal // 3A-Não alterar
{
    public string Main(AAAARegistro pRegistro, AAAATipoCampo pCampo) // 3A-Não alterar
    {
        string lRetorno = "";

        try
        {
            if (string.IsNullOrEmpty(pCampo.Valor))
            {
                return "Por gentileza, insira um CNPJ válido.";
            }
            
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string lUrl = "https://receitaws.com.br/v1/cnpj/" + pCampo.Valor;
            var lCnpjDic = GetCnpjData(lUrl);

            if (lCnpjDic == null)
            {
                return "Erro ao obter dados do CNPJ.";
            }

            PopulateRegistroCampos(pRegistro, lCnpjDic);
        }
        catch (Exception ex)
        {
            lRetorno = "Erro: " + ex.Message;
        }

        return lRetorno;
    }

    private Dictionary<string, object> GetCnpjData(string url)
    {
        using (WebClient client = new WebClient())
        {
            try
            {
                string json = client.DownloadString(url);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Deserialize<Dictionary<string, object>>(json);
            }
            catch (WebException webEx)
            {
                throw new Exception(String.Format("Erro na requisição: {0}", webEx.Message));
            }
        }
    }

    private void PopulateRegistroCampos(AAAARegistro pRegistro, Dictionary<string, object> lCnpjDic)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();

        // Serializar e desserializar listas aninhadas
        var qsaItems = serializer.Deserialize<List<QsaItem>>(serializer.Serialize(lCnpjDic["qsa"]));
        var atividadePrincipalItems = serializer.Deserialize<List<AtividadeItem>>(serializer.Serialize(lCnpjDic["atividade_principal"]));
        var atividadesSecundariasItems = serializer.Deserialize<List<AtividadeItem>>(serializer.Serialize(lCnpjDic["atividades_secundarias"]));

        // Constrói strings a partir das listas desserializadas
        StringBuilder textoQsa = new StringBuilder();
        foreach (var qsaItem in qsaItems)
        {
            textoQsa.AppendLine(string.Format("{0}: {1}", qsaItem.nome, qsaItem.qual));
        }

        StringBuilder textoAtividadePrincipal = new StringBuilder();
        foreach (var atividadeItem in atividadePrincipalItems)
        {
            textoAtividadePrincipal.AppendLine(string.Format("{0}: {1}", atividadeItem.code, atividadeItem.text));
        }

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