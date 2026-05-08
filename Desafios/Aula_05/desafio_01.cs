using System;

public class Node 
{
    public int Key { get; set; }
    public Node Left { get; set; }
    public Node Right { get; set; }
    
    public Node(int key) 
    {
        this.Key = key;
    }
}

public class BST 
{
    public Node Root { get; private set; }

    public BST() 
    {
        Root = null;
    }

    public void Insert(int value) 
    {
        Root = InsertRec(Root, value);
    }

    private Node InsertRec(Node root, int value) 
    {
        if (root == null) 
        {
            root = new Node(value);
            return root;
        }

        if (value < root.Key) 
        {
            root.Left = InsertRec(root.Left, value);
        } 
        else if (value > root.Key) 
        {
            root.Right = InsertRec(root.Right, value);
        }

        return root;
    }

    public Node Search(int value) 
    {
        return SearchRec(Root, value);
    }

    private Node SearchRec(Node root, int value) 
    {
        if (root == null || root.Key == value) 
        {
            return root;
        }

        if (value < root.Key) 
        {
            return SearchRec(root.Left, value);
        }

        return SearchRec(root.Right, value);
    }
}

class Program 
{
    static void Main(string[] args) 
    {
        BST arvore = new BST();

        arvore.Insert(50);
        arvore.Insert(30);
        arvore.Insert(20);
        arvore.Insert(40);
        arvore.Insert(70);
        arvore.Insert(60);
        arvore.Insert(80);

        Console.WriteLine("=== TESTE DE BUSCA NA BST ===\n");

        Node encontrado = arvore.Search(60);
        if (encontrado != null) 
        {
            Console.WriteLine($"Valor {encontrado.Key} encontrado na árvore!");
        } 
        else 
        {
            Console.WriteLine("Valor não encontrado.");
        }

        Node naoEncontrado = arvore.Search(99);
        if (naoEncontrado != null) 
        {
            Console.WriteLine($"Valor {naoEncontrado.Key} encontrado na árvore!");
        } 
        else 
        {
            Console.WriteLine("Valor 99 não encontrado na árvore.");
        }
    }
}

/*A melhor estratégia para implementar operações em uma Árvore Binária de Busca é utilizar a recursão. Na classe BST, criei a propriedade Root para guardar o nó principal e expus os métodos públicos Insert e Search. O trabalho pesado acontece em métodos privados auxiliares (InsertRec e SearchRec). O atributo Key (que é simplesmente a nossa 'chave' de ordenação ou o valor do nó) dita o caminho lógico: a cada chamada recursiva, o método compara o valor buscado ou inserido com a Key do nó atual. Se for menor, a recursão desce para o nó da esquerda (Left); se for maior, desce para o da direita (Right). Isso acontece até encontrar uma posição vazia (null) para inserir o novo nó ou até a Key bater com o valor buscado.*/