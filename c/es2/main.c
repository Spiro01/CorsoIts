
#include <stdio.h>
#include <stdlib.h>

char *inputString(FILE* fp, size_t size){

    char *str;
    int ch;
    size_t len = 0;
    str = realloc(NULL, sizeof(*str)*size);
    if(!str)return str;
    while(EOF!=(ch=fgetc(fp)) && ch != '\n'){
        str[len++]=ch;
        if(len==size){
            str = realloc(str, sizeof(*str)*(size+=16));
            if(!str)return str;
        }
    }
    str[len++]='\0';

    return realloc(str, sizeof(*str)*len);
}

int main(void){
    char *s1,*s2;
    printf("Inserisci la prima stringa: ");
    s1 = inputString(stdin, 1);
    printf("Inserisci la seconda stringa: ");
    s2 = inputString(stdin,1);
    printf("%s%s", s1, s2);
    free(s1);
    free(s2);
    return 0;
}

