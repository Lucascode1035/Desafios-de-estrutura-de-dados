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
    printf("Lista: ");
    while (curr != NULL) {
        printf("%d -> ", curr->info);
        curr = curr->prox;
    }
    printf("NULL\n");
}

int main() {
    struct no * lista = NULL;
    
    lista = insert_last(lista, 5);
    lista = insert_last(lista, 10);
    lista = insert_last(lista, 15);
    
    print_list(lista);
    
    return 0;
}

/*Para imprimir a lista, eu criei um ponteiro auxiliar chamado curr (de atual) que recebe o início da lista. A ideia é usar um laço while para percorrer nó por nó. Enquanto o curr não for NULL (ou seja, não cheguei ao fim), eu imprimo o valor dele (curr->info) e depois atualizo o curr para apontar para o próximo nó (curr->prox). Uso esse ponteiro auxiliar para garantir que eu passe por todos os elementos sem perder a referência da cabeça da lista original.*/