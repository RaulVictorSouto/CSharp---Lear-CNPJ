using System.Text.Json.Serialization;
using AAA.Visao.Atividades;
using AAA.Visao.Qsa;

namespace AAA.Visao.DadosCNPJ;

internal class AAAVDadosCNPJ
{
    //Principais caracterisitcas
    [JsonPropertyName("abertura")]
    public string? lAbertura {get;set; }
    [JsonPropertyName("situacao")]
    public string? LSituacao {get;set;}
    [JsonPropertyName("tipo")]
    public string? lTipo {get;set;}
    [JsonPropertyName("nome")]
    public string? lNome {get;set;}
    [JsonPropertyName("porte")]
    public string? lPorte {get;set;}
    [JsonPropertyName("natureza_juridica")]
    public string? lNaturezaJuridica {get;set;}
    [JsonPropertyName("cnpj")]
    public string? lNumeroCnpj { get; set; }

    //Atividades
    [JsonPropertyName("atividade_principal")]
    public List<AAAVAtividades>? lListAtvPrincipais {get;set;}
    [JsonPropertyName("atividades_secundarias")]
    public List<AAAVAtividades>? lListAtvSecundarias {get;set;}

    //Qsa
    [JsonPropertyName("qsa")]
    public List<AAAVQsa>? lListQsa {get;set;}


    //Endereço e demais dados
    [JsonPropertyName("logradouro")]
    public string? lLogradouro { get; set; }

    [JsonPropertyName("numero")]
    public string? lNumero { get; set; }

    [JsonPropertyName("complemento")]
    public string? lComplemento { get; set; }

    [JsonPropertyName("municipio")]
    public string? lMunicipio { get; set; }

    [JsonPropertyName("bairro")]
    public string? lBairro { get; set; }

    [JsonPropertyName("uf")]
    public string? lUf { get; set; }

    [JsonPropertyName("cep")]
    public string? lCep { get; set; }

    [JsonPropertyName("email")]
    public string? lEmail { get; set; }

    [JsonPropertyName("telefone")]
    public string? lTelefone { get; set; }

    [JsonPropertyName("data_situacao")]
    public string? lDataSituacao { get; set; }

    [JsonPropertyName("ultima_atualizacao")]
    public string? lUltimaAtualizacao { get; set; }

    [JsonPropertyName("status")]
    public string? lStatus { get; set; }

    [JsonPropertyName("fantasia")]
    public string? lFantasia { get; set; }

    [JsonPropertyName("efr")]
    public string? lEfr { get; set; }

    [JsonPropertyName("motivo_situacao")]
    public string? lMotivoSituacao { get; set; }

    [JsonPropertyName("situacao_especial")]
    public string? lSituacaoEspecial { get; set; }

    [JsonPropertyName("data_situacao_especial")]
    public string? lDataSituacaoEspecial { get; set; }

    [JsonPropertyName("capital_social")]
    public string? lCapitalSocial { get; set; }



}