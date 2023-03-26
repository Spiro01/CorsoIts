#include <stdio.h>
#include <stdlib.h>

#include <string.h>

#define CRYPT "file.txt"
#define LENGHT 10000

char modulosum (char a,int b){
    int i = 0;
    for(int i=0;i<b;i++){
        
        if(a<126){ a++;
        }
         
         else{ a=32;
        
         a++;
         }
        
    }
   return a;


}

char modulosub (char a,int b){
    int i;

    for(int i=0;i<b;i++){
        
        if(a>32){
            a--;
        }
        else{ a=126;
        a--;
        }
    }
return a;
}



int main()
{
    int scelta = 2;
    int i;

    printf("1) Per criptare \n2) Per decriptare\n");
    //scanf("%d", &scelta);

    if (scelta == 1)
    {
        FILE *stream = fopen(CRYPT, "r");
        char string[LENGHT] = "ciao a tutti";
        //fgets(string, LENGHT, stream);

        for (int i = 0; i < LENGHT; i++)
        {
            if (string[i] == '\0')
                break;

            
            if (string[i] % 2 == 0)
            {
                
                string[i] = modulosum(string[i], 41);
            }
            else
            {
               
                string[i] = modulosub(string[i],53);
            }

        
        }
        fclose(stream);
        fopen(CRYPT, "w+");

        fprintf(stream, "%s", string);
    }

    else if (scelta == 2)
    {
        FILE *stream = fopen(CRYPT, "r");
        char string[LENGHT];
        fgets(string, LENGHT, stream);

        for (i = 0; i < LENGHT; i++)
        {
            
            if (string[i] == '\0')
                break;
            if (string[i] % 2 == 0)
            {
                string[i] = modulosum(string[i],53);
            }
            else
            {
                string[i] = modulosub(string[i],41);
            }
            
            
            
        }
        fclose(stream);
        fopen(CRYPT, "w+");
        fprintf(stream, "%s", string);
        fclose(stream);
    }
}
