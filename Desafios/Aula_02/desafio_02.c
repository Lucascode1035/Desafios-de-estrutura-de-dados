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

void bubbleSort(ListaLigada lista) {
    if (lista == NULL || lista->inicio == NULL) return;

    int trocou;
    Celula atual;
    Celula ultimo_verificado = NULL;

    do {
        trocou = 0;
        atual = lista->inicio;

        while (atual->prox != ultimo_verificado) {
            
            if (atual->info > atual->prox->info) {
                
                int temp = atual->info;
                atual->info = atual->prox->info;
                atual->prox->info = temp;
                
                trocou = 1;
            }
            atual = atual->prox;
        }
        
        ultimo_verificado = atual; 
        
    } while (trocou);
}

int main() {
    ListaLigada lista = novaLista();
    
    llInsereNoFim(lista, 42);
    llInsereNoFim(lista, 15);
    llInsereNoFim(lista, 8);
    llInsereNoFim(lista, 100);
    llInsereNoFim(lista, 4);
    
    printf("Lista desordenada: ");
    llPrint(lista);
    
    bubbleSort(lista);
    
    printf("Lista ordenada:    ");
    llPrint(lista);
    
    return 0;
}

/*Então, a minha ideia foi manter a estrutura da lista intacta e trocar os valores (info) dentro dos nós. Usei um laço do-while controlado por uma variável trocou. Dentro dele, o ponteiro atual percorre a lista comparando o seu valor com o do próximo nó (atual->prox). Se estiver fora de ordem, faço a troca dos valores inteiros usando uma variável temporária temp e marco trocou = 1. Ainda adicionei um ponteiro ultimo_verificado para otimizar o algoritmo, garantindo que o laço interno não reavalie os maiores números que já 'borbulharam' para o final.*/