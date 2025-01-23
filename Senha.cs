using System;
using System.IO;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Bem-vindo ao gerador de senhas!");
        Console.Write("Digite o tamanho da senha desejada: ");
        int tamanho = int.Parse(Console.ReadLine());

        Console.Write("Deseja incluir letras? (s/n): ");
        bool incluirLetras = Console.ReadLine().ToLower() == "s";

        Console.Write("Deseja incluir caracteres especiais? (s/n): ");
        bool incluirEspeciais = Console.ReadLine().ToLower() == "s";

        string senha = GerarSenha(tamanho, incluirLetras, incluirEspeciais);
        Console.WriteLine($"Senha gerada: {senha}");

        SalvarSenhaEmArquivo(senha);
        Console.WriteLine("Senha salva em bkp.txt.");

        Console.WriteLine("Deseja recuperar as senhas salvas? (s/n): ");
        if (Console.ReadLine().ToLower() == "s")
        {
            RecuperarSenhas();
        }
    }

    static string GerarSenha(int tamanho, bool incluirLetras, bool incluirEspeciais)
    {
        const string numeros = "0123456789";
        const string letras = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string especiais = "@!#-";

        StringBuilder caracteres = new StringBuilder(numeros);

        if (incluirLetras)
            caracteres.Append(letras);
        if (incluirEspeciais)
            caracteres.Append(especiais);

        Random random = new Random();
        char[] senha = new char[tamanho];

        for (int i = 0; i < tamanho; i++)
        {
            senha[i] = caracteres[random.Next(caracteres.Length)];
        }

        return new string(senha);
    }

    static void SalvarSenhaEmArquivo(string senha)
    {
        using (StreamWriter sw = new StreamWriter("bkp.txt", true))
        {
            sw.WriteLine(senha);
        }
    }

    static void RecuperarSenhas()
    {
        if (File.Exists("bkp.txt"))
        {
            string[] senhas = File.ReadAllLines("bkp.txt");
            Console.WriteLine("Senhas salvas:");
            foreach (string s in senhas)
            {
                Console.WriteLine(s);
            }
        }
        else
        {
            Console.WriteLine("Nenhuma senha salva encontrada.");
        }
    }
}
