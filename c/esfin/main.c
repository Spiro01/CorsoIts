#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>
const char *getfield(char *line, int num)
{
    const char *tok;
    for (tok = strtok(line, "-");
         tok && *tok;
         tok = strtok(NULL, "-\n"))
    {
        if (!--num)
            return tok;
    }
    return NULL;
}

int main()
{
    FILE *stream = fopen("log.txt", "r");
    bool flag = true;
    char line[1024];
    while (fgets(line, 1024, stream))
    {
        char *tmp = strdup(line);
        char *tmp2 = strdup(line);
        char *tmp3 = strdup(line);

        float gradi = atof(getfield(tmp, 3));

        if (gradi > 6.5)
        {
            if (flag)
            {
                printf(" %s - %s \n", getfield(tmp2, 2), getfield(tmp3, 1));
                flag = false;
            }
            else
            {
                flag = true;
            }
        }
        // NOTE strtok clobbers tmp
        free(tmp);
        free(tmp2);
        free(tmp3);
    }
    return 0;
}