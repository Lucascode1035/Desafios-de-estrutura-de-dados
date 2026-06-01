using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static Dictionary<string, List<string>> grafo = new Dictionary<string, List<string>>();

    static void Main()
    {
        InicializarGrafo();

        bool rodando = true;
        Console.WriteLine("=== Bora Viajar! ===");

        while (rodando)
        {
            Console.WriteLine("\nMenu: 1) listar  2) conexão direta  3) existe rota? (DFS)  4) menor rota (BFS)");
            Console.WriteLine("      5) adicionar conexão  6) grupos isolados  7) cidades próximas  8) sair");
            Console.Write("_");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    ListarCidades();
                    break;
                case "2":
                    VerificarConexao();
                    break;
                case "3":
                    ExisteRotaDFS();
                    break;
                case "4":
                    MenorRotaBFS();
                    break;
                case "5":
                    AdicionarConexao();
                    break;
                case "6":
                    GruposIsolados();
                    break;
                case "7":
                    CidadesProximas();
                    break;
                case "8":
                    Console.WriteLine("Boa viagem! 🧳");
                    rodando = false;
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    static void InicializarGrafo()
    {
        AdicionarArestaInicial("São Paulo", "Rio de Janeiro");
        AdicionarArestaInicial("São Paulo", "Curitiba");
        AdicionarArestaInicial("São Paulo", "Belo Horizonte");
        AdicionarArestaInicial("Rio de Janeiro", "Belo Horizonte");
        AdicionarArestaInicial("Rio de Janeiro", "Vitória");
        AdicionarArestaInicial("Belo Horizonte", "Brasília");
        AdicionarArestaInicial("Curitiba", "Florianópolis");
        AdicionarArestaInicial("Florianópolis", "Porto Alegre");
        AdicionarArestaInicial("Brasília", "Goiânia");
        AdicionarArestaInicial("Salvador", "Recife");
        AdicionarArestaInicial("Recife", "Fortaleza");
    }

    static void AdicionarArestaInicial(string origem, string destino)
    {
        if (!grafo.ContainsKey(origem)) grafo[origem] = new List<string>();
        if (!grafo.ContainsKey(destino)) grafo[destino] = new List<string>();

        if (!grafo[origem].Contains(destino)) grafo[origem].Add(destino);
        if (!grafo[destino].Contains(origem)) grafo[destino].Add(origem);
    }

    static void ListarCidades()
    {
        Console.WriteLine("\nCidades e conexões:");
        foreach (var cidade in grafo)
        {
            Console.WriteLine($"  {cidade.Key}: [{string.Join(", ", cidade.Value)}]");
        }
    }

    static void VerificarConexao()
    {
        Console.Write("Cidade 1: ");
        string c1 = Console.ReadLine();
        Console.Write("Cidade 2: ");
        string c2 = Console.ReadLine();

        if (grafo.ContainsKey(c1) && grafo[c1].Contains(c2))
        {
            Console.WriteLine($"{c1} e {c2} possuem conexão direta!");
        }
        else
        {
            Console.WriteLine($"{c1} e {c2} NÃO possuem conexão direta.");
        }
    }

    static void ExisteRotaDFS()
    {
        Console.Write("Origem: ");
        string origem = Console.ReadLine();
        Console.Write("Destino: ");
        string destino = Console.ReadLine();

        if (!grafo.ContainsKey(origem) || !grafo.ContainsKey(destino))
        {
            Console.WriteLine("Uma ou ambas as cidades não existem no mapa.");
            return;
        }

        HashSet<string> visitados = new HashSet<string>();
        List<string> ordemVisita = new List<string>();

        bool achou = BuscaEmProfundidade(origem, destino, visitados, ordemVisita);

        Console.WriteLine($"DFS visitando: {string.Join(" -> ", ordemVisita)}");

        if (achou)
        {
            Console.WriteLine($"Rota encontrada! É possível ir de {origem} até {destino}.");
        }
        else
        {
            Console.WriteLine($"Rota NÃO encontrada. Não é possível ir de {origem} até {destino}.");
        }
    }

    static bool BuscaEmProfundidade(string atual, string destino, HashSet<string> visitados, List<string> ordemVisita)
    {
        visitados.Add(atual);
        ordemVisita.Add(atual);

        if (atual == destino)
            return true;

        foreach (string vizinho in grafo[atual])
        {
            if (!visitados.Contains(vizinho))
            {
                if (BuscaEmProfundidade(vizinho, destino, visitados, ordemVisita))
                    return true;
            }
        }

        return false;
    }

    static void MenorRotaBFS()
    {
        Console.Write("Origem: ");
        string origem = Console.ReadLine();
        Console.Write("Destino: ");
        string destino = Console.ReadLine();

        if (!grafo.ContainsKey(origem) || !grafo.ContainsKey(destino))
        {
            Console.WriteLine("Uma ou ambas as cidades não existem no mapa.");
            return;
        }

        if (origem == destino)
        {
            Console.WriteLine($"Menor rota (BFS): {origem}");
            Console.WriteLine("Paradas: 0");
            return;
        }

        Queue<string> fila = new Queue<string>();
        HashSet<string> visitados = new HashSet<string>();
        Dictionary<string, string> pai = new Dictionary<string, string>();

        fila.Enqueue(origem);
        visitados.Add(origem);

        bool achou = false;

        while (fila.Count > 0)
        {
            string atual = fila.Dequeue();

            if (atual == destino)
            {
                achou = true;
                break;
            }

            foreach (string vizinho in grafo[atual])
            {
                if (!visitados.Contains(vizinho))
                {
                    visitados.Add(vizinho);
                    pai[vizinho] = atual;
                    fila.Enqueue(vizinho);
                }
            }
        }

        if (achou)
        {
            List<string> caminho = new List<string>();
            string passo = destino;

            while (passo != origem)
            {
                caminho.Add(passo);
                passo = pai[passo];
            }
            caminho.Add(origem);
            caminho.Reverse();

            Console.WriteLine($"Menor rota (BFS): {string.Join(" -> ", caminho)}");
            Console.WriteLine($"Paradas: {caminho.Count - 1}");
        }
        else
        {
            Console.WriteLine($"Não existe rota entre {origem} e {destino}.");
        }
    }

    static void AdicionarConexao()
    {
        Console.Write("Cidade 1: ");
        string c1 = Console.ReadLine();
        Console.Write("Cidade 2: ");
        string c2 = Console.ReadLine();

        AdicionarArestaInicial(c1, c2);
        Console.WriteLine($"Conexão adicionada: {c1} <-> {c2}");
    }

    static void GruposIsolados()
    {
        HashSet<string> todosVisitados = new HashSet<string>();
        List<List<string>> grupos = new List<List<string>>();

        foreach (string cidade in grafo.Keys)
        {
            if (!todosVisitados.Contains(cidade))
            {
                List<string> grupoAtual = new List<string>();
                PreencherComponente(cidade, todosVisitados, grupoAtual);
                grupos.Add(grupoAtual);
            }
        }

        Console.WriteLine("\nGrupos de cidades conectadas:");
        for (int i = 0; i < grupos.Count; i++)
        {
            Console.WriteLine($"  Grupo {i + 1}: [{string.Join(", ", grupos[i])}]");
        }

        if (grupos.Count == 1)
        {
            Console.WriteLine("Todas as cidades estão conectadas!");
        }
        else
        {
            Console.WriteLine($"Existem {grupos.Count} grupos isolados.");
        }
    }

    static void PreencherComponente(string inicio, HashSet<string> todosVisitados, List<string> grupoAtual)
    {
        Stack<string> pilha = new Stack<string>();
        pilha.Push(inicio);
        todosVisitados.Add(inicio);

        while (pilha.Count > 0)
        {
            string atual = pilha.Pop();
            grupoAtual.Add(atual);

            foreach (string vizinho in grafo[atual])
            {
                if (!todosVisitados.Contains(vizinho))
                {
                    todosVisitados.Add(vizinho);
                    pilha.Push(vizinho);
                }
            }
        }
    }

    static void CidadesProximas()
    {
        Console.Write("Cidade: ");
        string origem = Console.ReadLine();

        if (!grafo.ContainsKey(origem))
        {
            Console.WriteLine("Cidade não encontrada no mapa.");
            return;
        }

        Queue<Tuple<string, int>> fila = new Queue<Tuple<string, int>>();
        HashSet<string> visitados = new HashSet<string>();
        
        List<string> umaParada = new List<string>();
        List<string> duasParadas = new List<string>();

        fila.Enqueue(new Tuple<string, int>(origem, 0));
        visitados.Add(origem);

        while (fila.Count > 0)
        {
            var atual = fila.Dequeue();
            string nome = atual.Item1;
            int distancia = atual.Item2;

            if (distancia == 1) umaParada.Add(nome);
            if (distancia == 2) duasParadas.Add(nome);

            if (distancia < 2)
            {
                foreach (string vizinho in grafo[nome])
                {
                    if (!visitados.Contains(vizinho))
                    {
                        visitados.Add(vizinho);
                        fila.Enqueue(new Tuple<string, int>(vizinho, distancia + 1));
                    }
                }
            }
        }

        Console.WriteLine($"\nCidades alcançáveis com até 2 paradas a partir de {origem}:");
        Console.WriteLine($"  1 parada:  [{string.Join(", ", umaParada)}]");
        Console.WriteLine($"  2 paradas: [{string.Join(", ", duasParadas)}]");
    }
}