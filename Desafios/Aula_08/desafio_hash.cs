using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static List<HashSet<string>> textosAnteriores = new List<HashSet<string>>();
    static Dictionary<string, int> contagemAtual = new Dictionary<string, int>();

    static void Main()
    {
        bool rodando = true;
        while (rodando)
        {
            Console.WriteLine("\nMenu: 1) novo texto  2) buscar palavra  3) comparar textos  4) sair");
            Console.Write("_");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    ProcessarNovoTexto();
                    break;
                case "2":
                    BuscarPalavra();
                    break;
                case "3":
                    CompararTextos();
                    break;
                case "4":
                    Console.WriteLine("Tchau!");
                    rodando = false;
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    static void ProcessarNovoTexto()
    {
        Console.WriteLine("Digite o texto (linha vazia para encerrar):");
        string textoCompleto = "";
        
        while (true)
        {
            string linha = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(linha))
                break;
            textoCompleto += linha + " ";
        }

        if (contagemAtual.Count > 0)
        {
            textosAnteriores.Add(new HashSet<string>(contagemAtual.Keys));
        }

        contagemAtual.Clear();
        string textoNormalizado = Regex.Replace(textoCompleto.ToLower(), @"[^\w\s]", "");
        string[] palavras = textoNormalizado.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);

        int totalPalavras = palavras.Length;

        foreach (string palavra in palavras)
        {
            if (contagemAtual.ContainsKey(palavra))
                contagemAtual[palavra]++;
            else
                contagemAtual[palavra] = 1;
        }

        Console.WriteLine("\n=== Resultado ===");
        Console.WriteLine($"Total de palavras: {totalPalavras}");
        Console.WriteLine($"Palavras distintas: {contagemAtual.Count}\n");
        
        Console.WriteLine("Top 10 palavras mais frequentes:");
        var top10 = contagemAtual.OrderByDescending(x => x.Value).Take(10).ToList();
        
        for (int i = 0; i < top10.Count; i++)
        {
            string s = top10[i].Value == 1 ? "ocorrencia" : "ocorrencias";
            Console.WriteLine($"{i + 1,2}. \"{top10[i].Key}\" - {top10[i].Value} {s}");
        }
    }

    static void BuscarPalavra()
    {
        Console.Write("Qual palavra? ");
        string busca = Console.ReadLine().ToLower();
        
        if (contagemAtual.TryGetValue(busca, out int qtd))
        {
            string s = qtd == 1 ? "vez" : "vezes";
            Console.WriteLine($"\"{busca}\" aparece {qtd} {s}");
        }
        else
        {
            Console.WriteLine($"\"{busca}\" aparece 0 vezes");
        }
    }

    static void CompararTextos()
    {
        if (textosAnteriores.Count == 0 || contagemAtual.Count == 0)
        {
            Console.WriteLine("Não há textos suficientes para comparar.");
            return;
        }

        HashSet<string> intersecao = new HashSet<string>(contagemAtual.Keys);
        foreach (var setAnterior in textosAnteriores)
        {
            intersecao.IntersectWith(setAnterior);
        }

        Console.WriteLine($"Palavras em comum entre todos os textos ({intersecao.Count}):");
        foreach (string p in intersecao)
        {
            Console.WriteLine($"- {p}");
        }
    }
}