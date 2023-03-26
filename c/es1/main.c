#include <stdio.h>

void swap(int *a, int *b){

int temp = *b;
*b= *a;
*a=temp;

}

int main(int argc, char *argv[])
{
    int a = 5;
    int b = 4;

   swap(&a,&b);
    printf("%d , %d" ,a,b);
    

    return 0;
}


