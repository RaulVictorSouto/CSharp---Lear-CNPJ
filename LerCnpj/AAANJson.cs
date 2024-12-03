using System.Text.Json;
using AAA.Visao.DadosCNPJ;

namespace AAA.Negocio.Json;
internal class AAANJson
{
    //Captura o string de CNPJ
    public async Task<AAAVDadosCNPJ>GetCNPJ(string jsonNum)
    {
        string url = $"https://www.receitaws.com.br/v1/cnpj/{jsonNum}";
        try
        {
            string jsonString = await GetJsonStringFromUrl(url);
            if (string.IsNullOrEmpty(jsonString))
            {
                throw new Exception("O JSON retornado está vazio ou nulo.");
            }
            var cnpj = JsonSerializer.Deserialize<AAAVDadosCNPJ>(jsonString);
            if (cnpj == null)
            {
                throw new Exception("O CNPJ retornado é nulo.");
            }
            return cnpj;
        }
        catch (Exception ex)
        {
            throw new Exception($"Ocorreu um erro inesperado: {ex.Message}");
        }
    }

    //Realiza a conexão com o url e o json
    private async Task<string> GetJsonStringFromUrl(string url)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage resposta = await client.GetAsync(url);
            if (resposta.IsSuccessStatusCode)
            {
                return await resposta.Content.ReadAsStringAsync();
            }
            else
            {
                 throw new Exception($"Falha ao obter os dados da URL: {url}");
            }
        }
    }
}