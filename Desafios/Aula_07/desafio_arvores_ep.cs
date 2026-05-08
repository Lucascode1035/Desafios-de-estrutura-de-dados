using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class Node
{
    public int Key;
    public Node Left;
    public Node Right;
    public int Height;

    public Node(int key)
    {
        Key = key;
        Height = 1;
    }
}

public class BST
{
    public Node Root;

    public void Insert(int key)
    {
        Root = InsertRec(Root, key);
    }

    private Node InsertRec(Node node, int key)
    {
        if (node == null) return new Node(key);
        
        if (key < node.Key) node.Left = InsertRec(node.Left, key);
        else if (key > node.Key) node.Right = InsertRec(node.Right, key);
        
        return node;
    }

    public int GetHeight()
    {
        return GetHeightRec(Root);
    }

    private int GetHeightRec(Node node)
    {
        if (node == null) return 0;
        int leftH = GetHeightRec(node.Left);
        int rightH = GetHeightRec(node.Right);
        return Math.Max(leftH, rightH) + 1;
    }
}

public class AVL
{
    public Node Root;

    public void Insert(int key)
    {
        Root = InsertRec(Root, key);
    }

    private int GetHeight(Node node)
    {
        return node == null ? 0 : node.Height;
    }

    private int GetBalance(Node node)
    {
        return node == null ? 0 : GetHeight(node.Left) - GetHeight(node.Right);
    }

    private Node RightRotate(Node y)
    {
        Node x = y.Left;
        Node T2 = x.Right;

        x.Right = y;
        y.Left = T2;

        y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
        x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

        return x;
    }

    private Node LeftRotate(Node x)
    {
        Node y = x.Right;
        Node T2 = y.Left;

        y.Left = x;
        x.Right = T2;

        x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
        y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;

        return y;
    }

    private Node InsertRec(Node node, int key)
    {
        if (node == null) return new Node(key);

        if (key < node.Key)
            node.Left = InsertRec(node.Left, key);
        else if (key > node.Key)
            node.Right = InsertRec(node.Right, key);
        else
            return node;

        node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

        int balance = GetBalance(node);

        if (balance > 1 && key < node.Left.Key)
            return RightRotate(node);

        if (balance < -1 && key > node.Right.Key)
            return LeftRotate(node);

        if (balance > 1 && key > node.Left.Key)
        {
            node.Left = LeftRotate(node.Left);
            return RightRotate(node);
        }

        if (balance < -1 && key < node.Right.Key)
        {
            node.Right = RightRotate(node.Right);
            return LeftRotate(node);
        }

        return node;
    }

    public int GetHeightTotal()
    {
        return GetHeight(Root);
    }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("EP2 - Exemplo de interacao");
        Console.WriteLine("-------------------------------------------");
        
        while (true)
        {
            Console.Write("Menu: 1) nova simulacao ou 2) sair\n_");
            string opcao = Console.ReadLine();
            
            if (opcao == "2")
            {
                Console.WriteLine("Tchau!");
                break;
            }
            
            if (opcao == "1")
            {
                Console.Write("Digite a quantidade de amostras: ");
                int A = int.Parse(Console.ReadLine());
                
                Console.Write("Digite a quantidade de elementos para cada amostra: ");
                int N = int.Parse(Console.ReadLine());

                double somaAlturaBST = 0;
                double somaAlturaAVL = 0;
                double somaTempoBST = 0;
                double somaTempoAVL = 0;

                Random rnd = new Random();

                for (int i = 0; i < A; i++)
                {
                    HashSet<int> numerosDistintos = new HashSet<int>();
                    while (numerosDistintos.Count < N)
                    {
                        numerosDistintos.Add(rnd.Next(1, N * 10));
                    }
                    
                    List<int> valores = numerosDistintos.ToList();

                    BST bst = new BST();
                    Stopwatch swBST = Stopwatch.StartNew();
                    foreach (int v in valores) bst.Insert(v);
                    swBST.Stop();
                    somaTempoBST += swBST.Elapsed.TotalMilliseconds;
                    somaAlturaBST += bst.GetHeight();

                    AVL avl = new AVL();
                    Stopwatch swAVL = Stopwatch.StartNew();
                    foreach (int v in valores) avl.Insert(v);
                    swAVL.Stop();
                    somaTempoAVL += swAVL.Elapsed.TotalMilliseconds;
                    somaAlturaAVL += avl.GetHeightTotal();
                }

                double mediaAlturaBST = somaAlturaBST / A;
                double mediaAlturaAVL = somaAlturaAVL / A;
                double mediaAlturaGeral = (mediaAlturaBST + mediaAlturaAVL) / 2.0;

                double mediaTempoBST = somaTempoBST / A;
                double mediaTempoAVL = somaTempoAVL / A;
                double mediaTempoGeral = (mediaTempoBST + mediaTempoAVL) / 2.0;

                Console.WriteLine($"\nExperimento com A = {A} e N = {N}");
                Console.WriteLine("----------------------------------");
                Console.WriteLine($"Altura média geral:     {mediaAlturaGeral}");
                Console.WriteLine($"Tempo médio geral de construção: {mediaTempoGeral:F4} ms");
                Console.WriteLine("---");
                Console.WriteLine($"Altura média BST comum: {mediaAlturaBST}");
                Console.WriteLine($"Tempo médio de construção BST: {mediaTempoBST:F4} ms");
                Console.WriteLine("---");
                Console.WriteLine($"Altura média AVL:       {mediaAlturaAVL}");
                Console.WriteLine($"Tempo médio de construção AVL: {mediaTempoAVL:F4} ms");
                Console.WriteLine("----------------------------------\n");
            }
        }
    }
}

/*Para montar esse simulador de experimentos, reutilizei a lógica da árvore binária clássica (BST) e criei uma nova classe AVL. A magia da AVL está nas funções de rotação (LeftRotate e RightRotate): logo após a inserção de um nó, calculamos o fator de balanceamento dele (a diferença de altura entre o lado esquerdo e o direito). Se o desequilíbrio for maior que 1 ou menor que -1, os ponteiros são rearranjados para achatar a árvore e garantir que a busca nela seja sempre super rápida. Para testar isso na prática com os parâmetros do professor, utilizei a classe HashSet<int> para sortear os N números sem repetição. A medição de tempo da Parte 1.1 foi feita utilizando a classe Stopwatch do System.Diagnostics, que funciona como um cronômetro de altíssima precisão: eu o inicio logo antes de inserir os valores e o paro imediatamente depois, guardando o resultado da propriedade TotalMilliseconds para gerar as médias do relatório.*/