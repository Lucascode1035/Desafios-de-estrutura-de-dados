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

void llPrint(ListaLigada lista) {
    for (Celula aux = lista->inicio; aux != NULL; aux = aux->prox) {
        printf("%d", aux->info);
        if (aux->prox != NULL) printf(" -> ");
    }
    printf("\n");
}

void insereOrdenado(ListaLigada lista, int info) {
    if (lista == NULL) return;

    Celula nova = novaCelula(info);

    if (lista->inicio == NULL) {
        lista->inicio = nova;
        lista->fim = nova;
        return;
    }

    if (info < lista->inicio->info) {
        nova->prox = lista->inicio;
        lista->inicio = nova;
        return;
    }

    Celula atual = lista->inicio;
    
    while (atual->prox != NULL && atual->prox->info < info) {
        atual = atual->prox;
    }

    nova->prox = atual->prox;
    atual->prox = nova;

    if (nova->prox == NULL) {
        lista->fim = nova;
    }
}

int main() {
    ListaLigada lista = novaLista();
    
    printf("Inserindo elementos (10, 30, 20, 5, 40) ordenadamente:\n");
    
    insereOrdenado(lista, 10);
    insereOrdenado(lista, 30);
    insereOrdenado(lista, 20); 
    insereOrdenado(lista, 5);  
    insereOrdenado(lista, 40); 
    
    printf("Lista final: ");
    llPrint(lista);
    
    return 0;
}


/*"Para inserir um valor de forma ordenada, precisei cobrir três cenários. O primeiro é se a lista estiver vazia: defino o novo nó como inicio e fim. O segundo cenário é se o novo valor for menor que o primeiro nó da lista: faço a inserção no início atualizando o ponteiro lista->inicio. O terceiro cenário é a inserção no meio ou final. Para isso, uso um ponteiro atual e percorro a lista checando o valor do próximo nó (atual->prox->info < info). Quando o laço para, sei que o atual está exatamente antes da posição correta de inserção. Faço o encadeamento e, no final, coloco um if para verificar se o nova->prox é NULL; se for, significa que o nó entrou no final, então aproveito para atualizar o ponteiro lista->fim para manter a estrutura da lista consistente."*/