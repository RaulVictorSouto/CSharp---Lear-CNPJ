using System.Text.Json.Serialization;

namespace AAA.Visao.Atividades;

internal class AAAVAtividades
{
    //Atidades primárias e secundárias
    [JsonPropertyName("code")]
    public string? lCode {get; set;}
    [JsonPropertyName("text")]
    public string? lText {get; set;}
}
