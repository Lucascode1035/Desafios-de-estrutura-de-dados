#include <stdio.h>
#include <stdlib.h>

struct no {
    int info;
    struct no * prox;
};

struct no * novoNo(int info) {
    struct no * novo = malloc(sizeof(struct no));
    novo->info = info;
    novo->prox = NULL;
    return novo;
}

struct no * insert_last(struct no * lista, int info) {
    struct no *novo = novoNo(info);
    if (lista == NULL) return novo;
    struct no *curr = lista;
    while (curr->prox != NULL) curr = curr->prox;
    curr->prox = novo;
    return lista;
}

void print_list(struct no * lista) {
    struct no * curr = lista;
    while (curr != NULL) {
        printf("%d -> ", curr->info);
        curr = curr->prox;
    }
    printf("NULL\n");
}

struct no * reverse_list(struct no * lista) {
    struct no * prev = NULL;
    struct no * curr = lista;
    struct no * next = NULL;

    while (curr != NULL) {
        next = curr->prox;  
        curr->prox = prev;  
        prev = curr;        
        curr = next;
    }
    return prev;
}

int main() {
    struct no * lista = NULL;
    lista = insert_last(lista, 1);
    lista = insert_last(lista, 2);
    lista = insert_last(lista, 3);
    lista = insert_last(lista, 4);
    
    printf("Lista original: ");
    print_list(lista);
    
    lista = reverse_list(lista);
    
    printf("Lista invertida: ");
    print_list(lista);
    
    return 0;
}

/*Eu precisei de três ponteiros auxiliares: um para o nó anterior (prev), um para o atual (curr) e um para salvar o próximo (next). Dentro do laço, a primeira coisa é salvar o next para não perder o resto da lista. Depois, eu pego o ponteiro do nó atual e inverto, fazendo ele apontar para trás (curr->prox = prev). Feito isso, avanço os ponteiros do 'anterior' e do 'atual' para a próxima iteração. No final do processo, o prev acaba se tornando a nova cabeça da lista invertida.*/