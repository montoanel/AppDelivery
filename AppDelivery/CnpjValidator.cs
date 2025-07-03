using System.Linq; // Necessário para o método .Where(char.IsDigit)

public static class CnpjValidator
{
    public static bool IsCnpjValid(string cnpj)
    {
        // Remove caracteres não numéricos
        string cleanCnpj = new string(cnpj.Where(char.IsDigit).ToArray());

        if (cleanCnpj.Length != 14)
            return false;

        // Verifica padrões de CNPJ inválidos conhecidos (todos os dígitos são iguais)
        if (new string(cleanCnpj[0], 14) == cleanCnpj)
            return false;

        int[] multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplier2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCnpj;
        string digit;
        int sum;
        int rest;

        tempCnpj = cleanCnpj.Substring(0, 12);
        sum = 0;
        for (int i = 0; i < 12; i++)
            sum += int.Parse(tempCnpj[i].ToString()) * multiplier1[i];

        rest = (sum % 11);
        if (rest < 2)
            rest = 0;
        else
            rest = 11 - rest;

        digit = rest.ToString();
        tempCnpj = tempCnpj + digit;
        sum = 0;
        for (int i = 0; i < 13; i++)
            sum += int.Parse(tempCnpj[i].ToString()) * multiplier2[i];

        rest = (sum % 11);
        if (rest < 2)
            rest = 0;
        else
            rest = 11 - rest;

        digit = digit + rest.ToString();

        return cleanCnpj.EndsWith(digit);
    }
}