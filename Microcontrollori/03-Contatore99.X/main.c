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
char display, statoConteggio,conteggioFinito = 0;

void main(void) {
    INTCON = 0xA0; // GIE = 1; T0IE = 1;
    OPTION_REG = 0x84; // PS2 = 1; PS1 = 0; PS0 = 0;
    TRISB = 0x00;
    TRISA = 0x0E;
    char button, old_button;

    while (1) {

        if (!display) {
            writeDisplay(conteggio / 10, 0);

        } else {
            writeDisplay(conteggio % 10, 1);

        }
        if(conteggioFinito) continue; //Durante il ciclo di conteggio finito i pulsanti sono inibiti;

        button = PORTA & 0x02;
        if (!button) {
            __delay_ms(20);
            button = PORTA & 0x02;
            if (!button && old_button) {
                statoConteggio ^= 1;
            }
            old_button = 0;
            continue;
        }

        button = PORTA & 0x04;
        if (!button) {
            __delay_ms(20);
            button = PORTA & 0x04;
            if (!button && old_button && !statoConteggio) {
                if (conteggio >= 99) conteggio = 99;
                conteggio++;
            }

            old_button = 0;
            __debug_break();
            continue;
        }

        button = PORTA & 0x08;
        if (!button) {
            __delay_ms(20);
            button = PORTA & 0x08;
            if (!button && old_button && !statoConteggio) {
                conteggio--;
                if (conteggio <= 1) conteggio = 1;
            }
            old_button = 0;
            continue;
        }

        old_button = 1;



    }
}

void __interrupt() ISR(void) {
    if (INTCONbits.T0IF) {
        INTCONbits.T0IF = 0;
        TMR0 = 6;
        display ^= 1;
        static unsigned char InterruptCounter;
        static unsigned int InterruptCounter2;
        InterruptCounter++;

        if (InterruptCounter >= 50) { //Impostare a 250 se si vuole che il timer decrementi una volta al secondo

            InterruptCounter = 0;

            if (statoConteggio)conteggio--;
            if (conteggio <= 0) {
                statoConteggio = 0;
                conteggioFinito = 1;
            }
        }

        if (conteggioFinito) {
            InterruptCounter2++;
            PORTAbits.RA0 = 1;
            if (InterruptCounter2 >= 1250) {
                InterruptCounter2 = 0;
                conteggioFinito = 0;
                PORTAbits.RA0 = 0;
                conteggio = 99;
            }
        }
    }
}