using System;
using System.Collections.Generic;

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

    public Node MinimoIterativo() 
    {
        if (Root == null) return null;
        
        Node atual = Root;
        while (atual.Left != null) 
        {
            atual = atual.Left;
        }
        return atual;
    }

    public Node MinimoRecursivo() 
    {
        return MinimoRec(Root);
    }

    private Node MinimoRec(Node node) 
    {
        if (node == null || node.Left == null) 
        {
            return node;
        }
        return MinimoRec(node.Left);
    }

    public Node MaximoIterativo() 
    {
        if (Root == null) return null;
        
        Node atual = Root;
        while (atual.Right != null) 
        {
            atual = atual.Right;
        }
        return atual;
    }

    public Node MaximoRecursivo() 
    {
        return MaximoRec(Root);
    }

    private Node MaximoRec(Node node) 
    {
        if (node == null || node.Right == null) 
        {
            return node;
        }
        return MaximoRec(node.Right);
    }

    public void PrintInOrder() 
    {
        PrintInOrderRec(Root);
        Console.WriteLine();
    }

    private void PrintInOrderRec(Node node) 
    {
        if (node != null) 
        {
            PrintInOrderRec(node.Left);
            Console.Write(node.Key + " ");
            PrintInOrderRec(node.Right);
        }
    }

    public void PrintInOrderIterativo() 
    {
        Stack<Node> pilha = new Stack<Node>();
        Node atual = Root;

        while (atual != null || pilha.Count > 0) 
        {
            while (atual != null) 
            {
                pilha.Push(atual);
                atual = atual.Left;
            }

            atual = pilha.Pop();
            Console.Write(atual.Key + " ");
            
            atual = atual.Right;
        }
        Console.WriteLine();
    }

    public void CoolPrint() 
    {
        CoolPrintRec(Root, "");
    }

    private void CoolPrintRec(Node node, string indent) 
    {
        if (node != null) 
        {
            Console.WriteLine(indent + node.Key);
            CoolPrintRec(node.Left, indent + "    ");
            CoolPrintRec(node.Right, indent + "    ");
        }
    }
}

public class Program 
{
    public static void Main(string[] args) 
    {
        BST bst = new BST();
        
        bst.Insert(15);
        bst.Insert(10);
        bst.Insert(8);
        bst.Insert(12);
        bst.Insert(20);
        bst.Insert(21);
        
        Console.WriteLine("Minimo Iterativo: " + bst.MinimoIterativo()?.Key);
        Console.WriteLine("Maximo Recursivo: " + bst.MaximoRecursivo()?.Key);
        Console.WriteLine();

        Console.WriteLine("In-order traversal (Recursivo):");
        bst.PrintInOrder();

        Console.WriteLine("In-order traversal (Iterativo):");
        bst.PrintInOrderIterativo();
        Console.WriteLine();

        Console.WriteLine("Visualização mais legal:");
        bst.CoolPrint();
    }
}

/*A lógica para encontrar o mínimo e o máximo na BST aproveita a própria regra de ordenação da árvore: tudo que é menor vai para a esquerda, tudo que é maior vai para a direita. Portanto, para o Minimo, basta descer continuamente pelo ponteiro Left até encontrar um nó que não tenha filho à esquerda (seja usando um while na versão iterativa ou chamando a função dentro dela mesma na versão recursiva). O mesmo vale para o Maximo, mas descendo pelo ponteiro Right.

Para imprimir em ordem (PrintInOrder), a versão recursiva é a mais natural: chamo a função para a subárvore esquerda, imprimo o nó atual, e depois chamo para a direita. Fazer isso iterativamente é mais complexo, pois exige recriar o comportamento da 'memória' do computador. Usei a classe Stack<Node> (uma pilha) nativa do C# para ir guardando os nós por onde passo enquanto desço para a esquerda, e depois dou um Pop() para voltar, imprimir a chave e explorar a direita.

Por fim, o CoolPrint nada mais é do que um percurso pré-ordem (imprime o nó atual, depois explora esquerda e direita), passando uma string indent como parâmetro que ganha quatro espaços vazios a cada nova chamada recursiva, gerando o recuo correto no console para simular a hierarquia da árvore.*/