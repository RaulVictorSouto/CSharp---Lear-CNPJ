namespace AAA.Visao.Cnpj;

internal class AAAVCnpj
{
    //Propriedade de CNPJ 
    public string lCnpj {get;}
    
    public AAAVCnpj(string lcnpj)
    {
        //Tratamento da string
        lCnpj = lcnpj.Trim().Replace(".", "").Replace("-", "").Replace("/","");
    }

}