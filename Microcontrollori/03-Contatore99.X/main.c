/*
 * File:   main.c
 * Author: User
 *
 * Created on 11 novembre 2022, 16.17
 */


#include <xc.h>
#include "7seg.h"

#define _XTAL_FREQ 8000000

int conteggio =0;
char display;

char counter;

void main(void) {
    INTCON = 0xA0; // GIE = 1; T0IE = 1;
    OPTION_REG = 0x85; // PS2 = 1; PS1 = 1; PS0 = 0;
    TRISB = 0x00;
    TRISA = 0x00;
    while (1) {
        if (!display)
            PORTB = sevenSegment(conteggio / 10, display);
        else
            PORTB = sevenSegment(conteggio % 10, display);
    }


}

void __interrupt() ISR(void) {
    if (INTCONbits.T0IF) {
        counter++;
        display = !display;
        conteggio = 87;
       

        INTCONbits.T0IF = 0;
    }
}