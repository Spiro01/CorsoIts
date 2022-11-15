/*
 * File:   main.c
 * Author: User
 *
 * Created on 11 novembre 2022, 16.17
 */

#pragma config FOSC = HS        // Oscillator Selection bits (HS oscillator: High-speed crystal/resonator on RA6/OSC2/CLKOUT and RA7/OSC1/CLKIN)
#pragma config WDTE = OFF       // Watchdog Timer Enable bit (WDT disabled)
#pragma config PWRTE = ON       // Power-up Timer Enable bit (PWRT enabled)
#pragma config MCLRE = ON       // RA5/MCLR/VPP Pin Function Select bit (RA5/MCLR/VPP pin function is MCLR)
#pragma config BOREN = ON       // Brown-out Detect Enable bit (BOD enabled)
#pragma config LVP = OFF        // Low-Voltage Programming Enable bit (RB4/PGM pin has digital I/O function, HV on MCLR must be used for programming)
#pragma config CPD = OFF        // Data EE Memory Code Protection bit (Data memory code protection off)
#pragma config CP = OFF         // Flash Program Memory Code Protection bit (Code protection off)


#include <xc.h>
#include "7seg.h"

#define _XTAL_FREQ 8000000

char conteggio = 99;
char display, statoConteggio = 0;

void main(void) {
    INTCON = 0xA0; // GIE = 1; T0IE = 1;
    OPTION_REG = 0x86; // PS2 = 1; PS1 = 1; PS0 = 0;
    TRISB = 0x00;
    TRISA = 0x0E;
    char button, old_button;

    while (1) {

        button = PORTA & 0x02;
        if (!button && old_button) {
            __delay_ms(20);
            button = PORTA & 0x02;
            if (!button && old_button) {
                statoConteggio ^= 1;
            }
        }

        old_button = button;
        
        if (!display) {
            writeDisplay(conteggio / 10, 0);

        } else {
            writeDisplay(conteggio % 10, 1);

        }
    }
}

void __interrupt() ISR(void) {
    if (INTCONbits.T0IF) {
        INTCONbits.T0IF = 0;
        TMR0 = 6;
        display ^= 1;
        static char InterruptCounter;

        InterruptCounter++;

        if (InterruptCounter >= 25) {

            InterruptCounter = 0;

            if (statoConteggio)conteggio--;
            if (conteggio < 0) conteggio = 99;

        }


    }
}