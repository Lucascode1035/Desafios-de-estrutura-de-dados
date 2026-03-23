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

struct no * remove_value(struct no * lista, int value) {
    if (lista == NULL) return NULL;

    if (lista->info == value) {
        struct no * temp = lista->prox;
        free(lista);
        return temp;
    }

    struct no * curr = lista;
    while (curr->prox != NULL && curr->prox->info != value) {
        curr = curr->prox;
    }

    if (curr->prox != NULL) {
        struct no * temp = curr->prox;
        curr->prox = curr->prox->prox;
        free(temp);
    }

    return lista;
}

int main() {
    struct no * lista = NULL;
    lista = insert_last(lista, 100);
    lista = insert_last(lista, 200);
    lista = insert_last(lista, 300);
    
    printf("Antes: ");
    print_list(lista);
    
    lista = remove_value(lista, 200);
    
    printf("Removendo o 200: ");
    print_list(lista);
    
    return 0;
}

/*Nesse desafio, eu percorro a lista sempre olhando um passo à frente, verificando se o próximo nó tem o valor que eu quero remover (curr->prox->info == value). Se encontrar, eu crio um ponteiro temporário para segurar o nó que vai sair, atualizo o ponteiro do nó atual para 'pular' ele (curr->prox = curr->prox->prox) e, por fim, libero a memória do nó removido com free. Também fiz um if logo de cara para tratar a exceção de o valor estar logo na cabeça da lista.*/