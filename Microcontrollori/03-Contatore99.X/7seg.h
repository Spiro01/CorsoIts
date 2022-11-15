/* 
 * File:   7seg.h
 * Author: Spiro
 *
 * Created on 25 ottobre 2022, 15.31
 */

#define SEGA 1 << 2 
#define SEGB 1 << 3
#define SEGC 1 << 5
#define SEGD 1 << 6
#define SEGE 1 << 7
#define SEGF 1 << 1
#define SEGG 1 << 0


#define N0 SEGA | SEGB | SEGC | SEGD | SEGE | SEGF
#define N1 SEGB | SEGC
#define N2 SEGA | SEGB | SEGD | SEGE | SEGG
#define N3 SEGA | SEGB | SEGC | SEGD | SEGG
#define N4 SEGB | SEGC | SEGF | SEGG 
#define N5 SEGA | SEGC | SEGD | SEGF | SEGG
#define N6 SEGA | SEGC | SEGD | SEGE | SEGF | SEGG
#define N7 SEGA | SEGB | SEGC
#define N8 SEGA | SEGB | SEGC | SEGD | SEGE | SEGF | SEGG
#define N9 SEGA | SEGB | SEGC | SEGD | SEGF | SEGG




#define PORT PORTB
#define SELECTORBIT 4

void writeDisplay(char value,char display) {

    const char displayPattern[] = {N0, N1, N2, N3, N4, N5, N6, N7, N8, N9};


    PORT = displayPattern[value];
    PORT |= display << SELECTORBIT;

}

