#include <stdio.h>
#include <stdlib.h>
#include <string.h>


#define NALLIEVI 100


struct alunno{
char nome[50];
int voto;
};


int confronta (char a[], char b[]){
    
    int i = 0;
    for(int i=0;a[i]!=0 || b[i]!=0;i++){
        if(a[i]==b[i]) {continue;}
        else return 0;
    }
    return 1;
}

int main (){

   
    int scelta;
    int i = 0;
    while(1){
    printf("\n1)Inserisci un voto\n2)Visualizza tutti i voti\n3)Visualizza la media dell'allievo\n4)exit\n");
    scanf("%d",&scelta);
    
    FILE* stream;

    if(scelta==1){
        stream = fopen("voti.txt","a");
        char nome[50];
        int voto;
        printf("\nNome alunno:\n");
        scanf("%s",&nome);
        printf("Voto:\n");
        scanf("%d",&voto);

        fprintf(stream,"%s %d\n",nome,voto);
        
        fclose(stream);
    }

    else if (scelta==2){
        stream=fopen("voti.txt","r");
        int voto;
        char nome[50];
        printf("\n");
        while(fscanf(stream,"%s %d\n",&nome,&voto)!=EOF){
        printf("%s %d\n",nome,voto);
        }
        fclose(stream);
    }
    else if (scelta==3){
        stream=fopen("voti.txt","r");
        struct alunno alunni[NALLIEVI];


        printf("\n");
        while(fscanf(stream,"%s %d\n",alunni[i].nome,&alunni[i].voto)!=EOF){
            i++;
        }
        int rows = i;
        fclose(stream);
        printf("Inserisci il nome da cercare:\n");
        char nome [50];

        int somma = 0;
        int num=0;
        float result;
        scanf("%s",nome);
        
        for(i=0;i<rows;i++){
            if(confronta(nome,alunni[i].nome)){
                somma = somma + alunni[i].voto;
                num++;
            }
        }
        if(somma == 0){
            printf("Errore, questo alunno non ha voti\n");
        }else{
        
        float media;
        media = (float)somma/(float)num;
        printf("La media di %s e' %3.2f\n\n",nome,media);
        
        }
        free(stream);


    }
    else return 0;
    }
    return 0;
}



