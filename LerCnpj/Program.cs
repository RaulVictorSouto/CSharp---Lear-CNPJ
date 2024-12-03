using AAA.Visao.Cnpj;
using AAA.Negocio.Json;
using AAA.Visao.DadosCNPJ;
using System.Text.RegularExpressions;
internal class Program
{
    public static async Task Main(string[] args)
    {
        //Insere numero de CNPJ
        Console.WriteLine("Por favor, insira o número de CNPJ: ");
        string lEntradaCnpj = Console.ReadLine()!;
        AAAVCnpj lCnpjVar = new AAAVCnpj(lEntradaCnpj);
        AAANJson jsonCnpj = new AAANJson();

        try
        {
            //Realiza a conexão com a API
           AAAVDadosCNPJ dadosCNPJ = await jsonCnpj.GetCNPJ(lCnpjVar.lCnpj);
           //Exibe os dados
           ExibirDadosCNPJ(dadosCNPJ);
        }
        catch (Exception ex)
        {

            Console.WriteLine($"Erro: {ex.Message}");
        }
    }

    static void ExibirDadosCNPJ(AAAVDadosCNPJ dadosCNPJ)
    {
        if (dadosCNPJ != null)
        {
        
            Console.WriteLine($"\n\nDados referentes à {dadosCNPJ.lNome} ({dadosCNPJ.lNumeroCnpj}):\n");

            Console.WriteLine($"Abertura: {dadosCNPJ.lAbertura}");
            Console.WriteLine($"Situação: {dadosCNPJ.LSituacao}");
            Console.WriteLine($"Tipo: {dadosCNPJ.lTipo}");
            Console.WriteLine($"Nome: {dadosCNPJ.lNome}");
            Console.WriteLine($"Porte: {dadosCNPJ.lPorte}");
            Console.WriteLine($"Natureza Jurídica: {dadosCNPJ.lNaturezaJuridica}");
            Console.WriteLine($"CNPJ: {dadosCNPJ.lNumeroCnpj}\n");

            Console.WriteLine("\nAtividades Principais:\n");
            #pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
            foreach(var atividade in dadosCNPJ.lListAtvPrincipais)
            {
                Console.WriteLine($"  - Código: {atividade.lCode}, Descrição: {atividade.lText}");
            }
            #pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.

            Console.WriteLine("\nAtividades Secundárias:\n");
            #pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
            foreach(var atividade in dadosCNPJ.lListAtvSecundarias)
            {
                Console.WriteLine($"  - Código: {atividade.lCode}, Descrição: {atividade.lText}");
            }
            #pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.

            Console.WriteLine("\nQuadro de Sócios e Administradores:\n");
            #pragma warning disable CS8602 // Desreferência de uma referência possivelmente nula.
            foreach (var qsa in dadosCNPJ.lListQsa)
            {
                
                Console.WriteLine($"  - Nome: {qsa.lQsaNome}, Qualificação: {Regex.Replace(qsa.lQsaQual.Replace("-", " "), @"\d", "")}");
            }
            #pragma warning restore CS8602 // Desreferência de uma referência possivelmente nula.

            Console.WriteLine("\nEndereço e demais dados:\n");
            Console.WriteLine($"Logradouro: {dadosCNPJ.lLogradouro}");
            Console.WriteLine($"Número: {dadosCNPJ.lNumero}");
            Console.WriteLine($"Complemento: {dadosCNPJ.lComplemento}");
            Console.WriteLine($"Município: {dadosCNPJ.lMunicipio}");
            Console.WriteLine($"Bairro: {dadosCNPJ.lBairro}");
            Console.WriteLine($"UF: {dadosCNPJ.lUf}");
            Console.WriteLine($"CEP: {dadosCNPJ.lCep}");
            Console.WriteLine($"E-mail: {dadosCNPJ.lEmail}");
            Console.WriteLine($"Telefone: {dadosCNPJ.lTelefone}");
            Console.WriteLine($"Data Situação: {dadosCNPJ.lDataSituacao}");
            Console.WriteLine($"Última Atualização: {dadosCNPJ.lUltimaAtualizacao}");
            Console.WriteLine($"Status: {dadosCNPJ.lStatus}");
            Console.WriteLine($"Fantasia: {dadosCNPJ.lFantasia}");
            Console.WriteLine($"EFR: {dadosCNPJ.lEfr}");
            Console.WriteLine($"Motivo Situação: {dadosCNPJ.lMotivoSituacao}");
            Console.WriteLine($"Situação Especial: {dadosCNPJ.lSituacaoEspecial}");
            Console.WriteLine($"Data Situação Especial: {dadosCNPJ.lDataSituacaoEspecial}");
            Console.WriteLine($"Capital Social: {dadosCNPJ.lCapitalSocial}\n");
        }
        else
        {
            Console.WriteLine("Os dados do CNPJ são nulos.");
        }
    }
}