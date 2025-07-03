using System.Linq; // Necessário para o método .Where(char.IsDigit)

public static class CpfValidator
{
    public static bool IsCpfValid(string cpf)
    {
        // Remove caracteres não numéricos
        string cleanCpf = new string(cpf.Where(char.IsDigit).ToArray());

        if (cleanCpf.Length != 11)
            return false;

        // Verifica padrões de CPF inválidos conhecidos (todos os dígitos são iguais)
        if (new string(cleanCpf[0], 11) == cleanCpf)
            return false;

        int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf;
        string digit;
        int sum;
        int rest;

        tempCpf = cleanCpf.Substring(0, 9);
        sum = 0;

        for (int i = 0; i < 9; i++)
            sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];

        rest = sum % 11;
        if (rest < 2)
            rest = 0;
        else
            rest = 11 - rest;

        digit = rest.ToString();
        tempCpf = tempCpf + digit;
        sum = 0;
        for (int i = 0; i < 10; i++)
            sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];

        rest = sum % 11;
        if (rest < 2)
            rest = 0;
        else
            rest = 11 - rest;

        digit = digit + rest.ToString();

        return cleanCpf.EndsWith(digit);
    }
}