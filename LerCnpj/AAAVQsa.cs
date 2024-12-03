using System.Text.Json.Serialization;

namespace AAA.Visao.Qsa;

internal class AAAVQsa
{
    //Dados dos sócios e suas qualificações
    [JsonPropertyName("nome")]
    public string? lQsaNome {get; set;}
    [JsonPropertyName("qual")]
    public string? lQsaQual {get; set;}
}