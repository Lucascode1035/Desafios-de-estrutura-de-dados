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

struct no * remove_last(struct no * lista) {
    if (lista == NULL) return NULL; 
    
    if (lista->prox == NULL) {      
        free(lista);
        return NULL;
    }
    
    struct no * curr = lista;
    while (curr->prox->prox != NULL) {
        curr = curr->prox;
    }
    
    free(curr->prox);
    curr->prox = NULL;
    
    return lista;
}

int main() {
    struct no * lista = NULL;
    lista = insert_last(lista, 10);
    lista = insert_last(lista, 20);
    lista = insert_last(lista, 30);
    
    printf("Antes: ");
    print_list(lista);
    
    lista = remove_last(lista);
    
    printf("Depois: ");
    print_list(lista);
    
    return 0;
}

/*Aqui eu busco um passo antes do final. Se eu for até o último nó, não consigo voltar para atualizar o ponteiro do penúltimo para NULL. Então, fiz um while que checa se o 'próximo do próximo' (curr->prox->prox) é diferente de NULL. Quando o laço para, eu sei que estou exatamente no penúltimo nó. Aí é só dar um free no último nó (curr->prox) e definir o curr->prox atual como NULL, cortando a ligação. Também adicionei verificações no início para proteger o código caso a lista já esteja vazia ou tenha só um elemento.*/