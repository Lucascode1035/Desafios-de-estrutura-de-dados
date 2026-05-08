using System;
using System.Collections.Generic;

public class Pokemon
{
    public string Nome { get; set; }
    public string Tipo { get; set; }
    public int Vida { get; set; }
    public int Ataque { get; set; }
    public int Defesa { get; set; }

    public Pokemon(string nome, string tipo, int vida, int ataque, int defesa)
    {
        Nome = nome;
        Tipo = tipo;
        Vida = vida;
        Ataque = ataque;
        Defesa = defesa;
    }

    public virtual void Atacar(Pokemon alvo)
    {
        int dano = CalcularDanoBase(alvo);
        AplicarDano(alvo, dano);
    }

    protected int CalcularDanoBase(Pokemon alvo)
    {
        int dano = Ataque - alvo.Defesa;
        if (dano < 1) dano = 1; 
        return dano;
    }

    protected void AplicarDano(Pokemon alvo, int dano)
    {
        alvo.Vida -= dano;
        if (alvo.Vida < 0) alvo.Vida = 0; 

        Console.WriteLine($"{Nome} atacou {alvo.Nome} e causou {dano} de dano.");
        Console.WriteLine($"{alvo.Nome} agora está com {alvo.Vida} de vida.\n");
    }

    public void ExibirStatus()
    {
        Console.WriteLine($"{Nome} ({Tipo}) - HP: {Vida} | ATK: {Ataque} | DEF: {Defesa}");
    }
}

public class PokemonFogo : Pokemon
{
    public PokemonFogo(string nome, int vida, int ataque, int defesa) 
        : base(nome, "Fogo", vida, ataque, defesa) { }

    public override void Atacar(Pokemon alvo)
    {
        int dano = CalcularDanoBase(alvo) + 2; 
        
        if (alvo.Tipo == "Grama") {
            Console.WriteLine("É super efetivo!");
            dano *= 2; 
        }
        
        AplicarDano(alvo, dano);
    }
}

public class PokemonAgua : Pokemon
{
    public PokemonAgua(string nome, int vida, int ataque, int defesa) 
        : base(nome, "Água", vida, ataque, defesa) { }

    public override void Atacar(Pokemon alvo)
    {
        int dano = CalcularDanoBase(alvo);
        
        if (alvo.Tipo == "Fogo") {
            Console.WriteLine("É super efetivo!");
            dano *= 2;
        }

        AplicarDano(alvo, dano);
        
        Vida += 2;
        Console.WriteLine($"[Passiva] {Nome} recuperou 2 de vida! Vida atual: {Vida}\n");
    }
}

public class PokemonGrama : Pokemon
{
    private static Random rand = new Random();

    public PokemonGrama(string nome, int vida, int ataque, int defesa) 
        : base(nome, "Grama", vida, ataque, defesa) { }

    public override void Atacar(Pokemon alvo)
    {
        int dano = CalcularDanoBase(alvo);

        if (alvo.Tipo == "Água") {
            Console.WriteLine("É super efetivo!");
            dano *= 2;
        }

        if (rand.Next(1, 101) <= 20) {
            Console.WriteLine("Um ataque CRÍTICO!");
            dano *= 2;
        }

        AplicarDano(alvo, dano);
    }
}

public class Treinador
{
    public string Nome { get; set; }
    public LinkedList<Pokemon> Pokemons { get; set; }

    public Treinador(string nome)
    {
        Nome = nome;
        Pokemons = new LinkedList<Pokemon>();
    }

    public void AdicionarPokemon(Pokemon p)
    {
        Pokemons.AddLast(p); 
    }

    public void ListarPokemons()
    {
        Console.WriteLine($"Pokémons de {Nome}:");
        int indice = 0;
        foreach (Pokemon p in Pokemons)
        {
            Console.WriteLine($"[{indice}] {p.Nome} ({p.Tipo}) - HP: {p.Vida}");
            indice++;
        }
        Console.WriteLine();
    }

    public Pokemon EscolherPokemon(int indice)
    {
        if (indice < 0 || indice >= Pokemons.Count) return null;
        
        LinkedListNode<Pokemon> atual = Pokemons.First;
        for (int i = 0; i < indice; i++)
        {
            atual = atual.Next;
        }
        return atual.Value;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Treinador ash = new Treinador("Ash");
        ash.AdicionarPokemon(new PokemonFogo("Charmander", 45, 12, 10));
        ash.AdicionarPokemon(new PokemonGrama("Bulbasaur", 50, 10, 12));

        Treinador misty = new Treinador("Misty");
        misty.AdicionarPokemon(new PokemonAgua("Squirtle", 48, 11, 15));
        misty.AdicionarPokemon(new PokemonAgua("Starmie", 40, 14, 10));

        Console.WriteLine("=== BATALHA POOKÉMON ===\n");
        
        Pokemon p1 = ash.EscolherPokemon(0); 
        Pokemon p2 = misty.EscolherPokemon(0); 

        Console.WriteLine($"{ash.Nome} escolheu {p1.Nome}!");
        Console.WriteLine($"{misty.Nome} escolheu {p2.Nome}!\n");

        int turno = 1;
        
        while (p1.Vida > 0 && p2.Vida > 0)
        {
            Console.WriteLine($"--- Turno {turno} ---");
            
            p1.Atacar(p2);
            if (p2.Vida <= 0) break; 

            p2.Atacar(p1);
            
            turno++;
        }

        Console.WriteLine("=== FIM DA BATALHA ===");
        if (p1.Vida > 0)
            Console.WriteLine($"{p1.Nome} venceu a batalha! {ash.Nome} é o vencedor!");
        else
            Console.WriteLine($"{p2.Nome} venceu a batalha! {misty.Nome} é a vencedora!");
    }
}

/*A transição para C# facilitou bastante a organização do código. Criei uma classe base Pokemon que contém os atributos essenciais e a lógica principal no método Atacar. Para aplicar os efeitos especiais, usei Herança: criei subclasses como PokemonFogo e PokemonAgua, utilizando override para sobrescrever o método Atacar e injetar regras específicas (como recuperar vida ou aumentar dano), além de implementar vantagens de tipo. Na classe Treinador, resolvi o desafio extra utilizando a estrutura LinkedList<Pokemon> nativa do System.Collections.Generic. Em vez de gerenciar ponteiros manualmente com malloc e struct no*, o C# já oferece métodos prontos como AddLast para inserir os objetos na memória e First.Next para percorrer os nós da lista quando preciso escolher o Pokémon pela posição!*/