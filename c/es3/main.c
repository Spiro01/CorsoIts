#include <stdio.h>

int main(){

    int n[1];
    int* i = n;
    int counter;
    while(scanf("%d",i) != 0){
        //printf("%d", *i);
        i++;
        counter++;
    }
for(int j = 0 ; j< counter;j++){

    //printf("%d",n[j]);
}
    return 0;
}