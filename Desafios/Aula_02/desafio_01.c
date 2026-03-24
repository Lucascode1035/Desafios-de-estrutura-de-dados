#include <stdio.h>
#include <stdlib.h>

typedef struct no {
    int info;
    struct no * prox;
} No;
typedef No * Celula;

typedef struct lista {
    Celula inicio;
    Celula fim;
} Lista;
typedef Lista * ListaLigada;

ListaLigada novaLista() {
    ListaLigada l = malloc(sizeof(Lista));
    if (!l) return NULL;
    l->inicio = NULL;
    l->fim = NULL;
    return l;
}

Celula novaCelula(int info) {
    Celula celula = (Celula) malloc(sizeof(No));
    celula->info = info;
    celula->prox = NULL;
    return celula;
}

void llInsereNoFim(ListaLigada lista, int info) {
    Celula nova = novaCelula(info);        
    if (lista->inicio == NULL) {
        lista->inicio = nova;
        lista->fim = nova;
        return;
    }
    lista->fim->prox = nova;
    lista->fim = nova;
}

void llPrint(ListaLigada lista) {
    for (Celula aux = lista->inicio; aux != NULL; aux = aux->prox) {
        printf("%d", aux->info);
        if (aux->prox != NULL) printf(" -> ");
    }
    printf("\n");
}

int somaLista(ListaLigada lista) {
    int soma = 0;
    
    if (lista == NULL || lista->inicio == NULL) {
        return 0; 
    }

    for (Celula aux = lista->inicio; aux != NULL; aux = aux->prox) {
        soma += aux->info;
    }
    
    return soma;
}

int main() {
    ListaLigada lista = novaLista();
    
    llInsereNoFim(lista, 10);
    llInsereNoFim(lista, 20);
    llInsereNoFim(lista, 30);
    
    printf("Lista: ");
    llPrint(lista);
    
    int total = somaLista(lista);
    printf("Soma total: %d\n", total);
    
    return 0;
}

/*Para somar os elementos da lista, criei uma variável inteira soma iniciando em zero. Depois, usei um laço for de lista ligada: criei um ponteiro auxiliar aux que começa no lista->inicio e vai avançando para aux->prox até chegar em NULL. A cada iteração, eu acumulo o valor de aux->info na variável soma. Antes de tudo, coloquei um if para garantir que, se a lista estiver vazia, a função retorne zero logo de cara.*/